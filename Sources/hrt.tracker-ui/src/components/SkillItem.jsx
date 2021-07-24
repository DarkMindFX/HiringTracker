
import React from 'react';
import { TextField } from '@material-ui/core';
import FormControlLabel from '@material-ui/core/FormControlLabel';
import FormControl from '@material-ui/core/FormControl';
import Checkbox from '@material-ui/core/Checkbox';
import MenuItem from '@material-ui/core/MenuItem';
import Select from '@material-ui/core/Select';

const constants = require('../constants');
const SkillsDal = require('../dal/SkillsDal');
const SkillProficienciesDal = require('../dal/SkillProficienciesDal');

class SkillItem extends React.Component {

    constructor(props) {
        super(props); 

        let updatedState = {
            id: props.id,
            skillID: props.skillID,
            proficiencyID: props.proficiencyID,
            dictSkills : props.dictSkills,
            dictProficiencies: props.dictProficiencies,
            canEdit: props.canEdit ?  props.canEdit : false,
            mustHave: props.mustHave ? props.mustHave : false,
            showMustHave: props.showMustHave ? props.showMustHave : false,
            skill: null,
            proficiency: null,
            
            skills: [],
            profs: [],

            onSkillChanged: props.onSkillChanged ? props.onSkillChanged : null
        }      
        

        this.state = updatedState;
    }

    componentDidMount() {

        this._update();
    }

    static getDerivedStateFromProps(props, state) {
        if( props.canEdit !== state.canEdit ||
            props.skillID !== state.skillID ||
            props.dictSkills != state.dictSkills ||
            props.dictProficiencies != state.dictProficiencies ||
            props.proficiencyID !== state.proficiencyID ||
            props.mustHave !== state.mustHave )
        {
            let updatedState = {
                canEdit:  props.canEdit,
                skillID:  props.skillID,
                dictSkills : props.dictSkills,
                dictProficiencies: props.dictProficiencies,
                proficiencyID:  props.proficiencyID,
                mustHave:  props.mustHave
            }    

            return updatedState;
        }
        else 
        {
            return null;
        }
    }

    onSkillChanged(event) {

        let newSkillID = parseInt(event.target.value);

        let newSkill = this.state.dictSkills[newSkillID];

        this._setSkill(newSkill); 
        this._notifySkillDataChanged(this.state.id, 
                                        newSkill.ID,
                                        this.state.proficiencyID,
                                        this.state.mustHave);
    }

    onProficiencyChanged(event) {

        let newProfID = parseInt(event.target.value);

        let newProf = this.state.dictProficiencies[newProfID];

        this._setProficiency(newProf);
        this._notifySkillDataChanged(this.state.id, 
                                        this.state.skillID,
                                        newProf.ProficiencyID,
                                        this.state.mustHave);
                
    }

    onMustHaveChanged(event) {
        this._setMustHave(event.target.checked);
        this._notifySkillDataChanged(this.state.id, 
            this.state.skillID,
            this.state.proficiencyID,
            event.target.checked);
    }
    

    render() {        

        let styleEdit = {
            display: this.state.canEdit ? "block": "none"
        };
        let styleView = {
            display: !this.state.canEdit ? "block": "none"
        };  
        let styleMustHave = {
            display: this.state.mustHave ? "block": "none"
        };  
        let styleShowMustHave = {
            display: this.state.showMustHave ? "block": "none"
        }; 
        let styleSkill = {
            width: "100%"
        }
         
        let itemsSkills = Object.values( this.state.dictSkills ).map( (skill) => (
            <MenuItem id={skill.id} key={skill.ID} value={skill.ID}>{skill.Name}</MenuItem>
        ));

        let itemsProficiences = Object.values( this.state.dictProficiencies ).map( (prof) => (
            <MenuItem id={prof.id} key={prof.ID} value={prof.ID}>{prof.Name}</MenuItem>            
        ) )


        return (
            <div>
                <div key={"divEdit" + this.state.skillID} style={styleEdit}>
                    <FormControl style={{minWidth: 250}}>
                    <Select
                        minWidth={300}                   
                        key="cbSkill"                     
                        value={this.state.skill ? this.state.skill.ID : ""}
                        onChange={ (event) => this.onSkillChanged(event) }>
                        {
                            itemsSkills                            
                        }
                    </Select>
                    </FormControl>
                    <FormControl style={{minWidth: 250}}>
                    <Select 
                        key="cbProficiency"                       
                        value={this.state.proficiency ? this.state.proficiency.ID : ""}
                        onChange={ (event) => this.onProficiencyChanged(event) }
                        >
                        {
                            itemsProficiences                            
                        }
                    </Select>
                    </FormControl>
                    <FormControlLabel
                        key="lblMustHave"                        
                        control={<Checkbox checked={this.state.mustHave} onChange={(event) => this.onMustHaveChanged(event)} name="mustHave" />}
                        label="Must-Have"
                        style={styleShowMustHave}
                    />

                </div>  
                 
                <div key={"divView" + this.state.skillID} style={styleView}>
                    <table>
                        <tbody>
                            <tr>
                                <td>
                                    { this.state.skill ? this.state.SkillName : "" } - { this.state.proficiency ? this.state.ProficiencyName : "" }
                                </td>
                                <td>
                                    <div style={styleMustHave}>
                                        [Must-Have]
                                    </div>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        )
    }

    _update() {

        let dal = new SkillsDal();
        let obj = this;

        dal.getSkill(this.state.skillID).then( (skillResp) => {

            if(skillResp.status != constants.HTTP_OK) return;
            
            obj._setSkill(skillResp.data);

            let dalSkillProficiences = new SkillProficienciesDal();

            dalSkillProficiences.getSkillProficiency(this.state.proficiencyID).then( (profResp) => {

                if(profResp.status != constants.HTTP_OK) return;

                obj._setProficiency(profResp.data);
            } )
        } )
    }

    _notifySkillDataChanged(id, skillID, proficiencyID, mustHave) {
        if(this.state.onSkillChanged) {
            this.props.onSkillChanged(
                id,
                skillID,
                proficiencyID,
                mustHave
            );
        }
    }

    _setSkill(skill) {
        let updatedState = this.state;

        updatedState.skillID = skill.ID;
        updatedState.skill = skill;
        
        this.setState(updatedState);
    }

    _setProficiency(prof) {
        let updatedState = this.state;

        updatedState.proficiencyID = prof.ID;
        updatedState.proficiency = prof;
        
        this.setState(updatedState);
    }

    _setMustHave(mustHave) {
        let updatedState = this.state;

        updatedState.mustHave = mustHave;
        
        this.setState(updatedState);
    }    
}

export default SkillItem;