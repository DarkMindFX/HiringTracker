
import React from 'react';
import { TextField } from '@material-ui/core';
import FormControlLabel from '@material-ui/core/FormControlLabel';
import Checkbox from '@material-ui/core/Checkbox';
import MenuItem from '@material-ui/core/MenuItem';
import Select from '@material-ui/core/Select';

const SkillsDal = require('../dal/SkillsDal');

class SkillItem extends React.Component {

    constructor(props) {
        super(props); 

        let updatedState = {
            id: props.id,
            skillID: props.skillID,
            proficiencyID: props.proficiencyID,
            canEdit: props.canEdit ?  props.canEdit : false,
            mustHave: props.mustHave ? props.mustHave : false,
            skill: null,
            proficiency: null,
            
            skills: [],
            profs: [],

            onSkillChanged: props.onSkillChanged ? props.onSkillChanged : null
        }      
        

        this.state = updatedState;
    }

    componentDidMount() {

        let dal = new SkillsDal();
        let obj = this;

        dal.getSkills().then( (skills) => {

            obj._setSkills(skills);

            dal.getProficiencies().then( (profs) => {

                obj._setProficiences(profs);
                
                obj._update();
            } )
        });
    }

    static getDerivedStateFromProps(props, state) {
        if( props.canEdit !== state.canEdit ||
            props.skillID !== state.skillID ||
            props.proficiencyID !== state.proficiencyID ||
            props.mustHave !== state.mustHave )
        {
            let updatedState = {
                canEdit:  props.canEdit,
                skillID:  props.skillID,
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

        let dal = new SkillsDal();
        let obj = this;

        dal.getSkillByName(event.target.value).then( newSkill => {
            obj._setSkill(newSkill); 
            obj._notifySkillDataChanged(this.state.id, 
                                        newSkill.SkillID,
                                        this.state.proficiencyID,
                                        this.state.mustHave);           
        })
    }

    onProficiencyChanged(event) {

        let dal = new SkillsDal();
        let obj = this;

        dal.getProficiencyByName(event.target.value).then( newProf => {
            obj._setProficiency(newProf);
            obj._notifySkillDataChanged(this.state.id, 
                this.state.skillID,
                newProf.ProficiencyID,
                this.state.mustHave);
        })        
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
        
        let key = 1;

        let itemsSkills = this.state.skills.map( (skill) => (
            <MenuItem key={skill.SkillID} value={skill.Name}>{skill.Name}</MenuItem>
        ));

        let itemsProficiences = this.state.profs.map( (prof) => (
            <MenuItem key={prof.ProfID} value={prof.Name}>{prof.Name}</MenuItem>            
        ) )

        return (
            <div>
                <div key={"divEdit" + this.state.SkillID} style={styleEdit}>
                    <Select
                        key="cbSkill"                     
                        value={this.state.skill ? this.state.skill.Name : ""}
                        onChange={ (event) => this.onSkillChanged(event) }>
                        {
                            itemsSkills                            
                        }
                    </Select>

                    <Select 
                        key="cbProficiency"                       
                        value={this.state.proficiency ? this.state.proficiency.Name : ""}
                        onChange={ (event) => this.onProficiencyChanged(event) }
                        >
                        {
                            itemsProficiences                            
                        }
                    </Select>

                    <FormControlLabel
                        key="lblMustHave"
                        control={<Checkbox checked={this.state.mustHave} onChange={(event) => this.onMustHaveChanged(event)} name="mustHave" />}
                        label="Must-Have"
                    />
                </div>  
                 
                <div key={"divView" + this.state.SkillID} style={styleView}>
                    <table>
                        <tbody>
                            <tr>
                                <td>
                                    { this.state.skill ? this.state.skill.Name : "" } - { this.state.proficiency ? this.state.proficiency.Name : "" }
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

        dal.getSkill(this.state.skillID).then( (skill) => {

            if(!skill) return;
            
            obj._setSkill(skill);

            dal.getProficiency(this.state.proficiencyID).then( (prof) => {

                if(!prof) return;

                obj._setProficiency(prof);
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
    

    _setSkills(skills) {
        let updatedState = this.state;
            
        updatedState.skills = skills          
        
        this.setState(updatedState);
    }

    _setSkill(skill) {
        let updatedState = this.state;

        updatedState.skillID = skill.SkillID;
        updatedState.skill = skill;
        
        this.setState(updatedState);
    }

    _setProficiences(profs) {
        let updatedState = this.state;

        updatedState.profs = profs;
        
        this.setState(updatedState);
    }

    _setProficiency(prof) {
        let updatedState = this.state;

        updatedState.proficiencyID = prof.ProficiencyID;
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