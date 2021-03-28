
import React from 'react';
import { TextField } from '@material-ui/core';
import FormControlLabel from '@material-ui/core/FormControlLabel';
import Checkbox from '@material-ui/core/Checkbox';

const SkillsDal = require('../dal/SkillsDal');

class SkillItem extends React.Component {

    constructor(props) {
        super(props); 

        let updatedState = {
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

                dal.getSkill(this.state.skillID).then( (skill) => {

                    if(!skill) return;
                    
                    obj._setSkill(skill);

                    dal.getProficiency(this.state.proficiencyID).then( (prof) => {

                        if(!prof) return;

                        obj._setProficiency(prof);
                    } )
                } )
            } )
        });
    }

    onSkillChanged(event) {

        let dal = new SkillsDal();
        let obj = this;

        dal.getSkillByName(event.target.value).then( newSkill => {
            obj._setSkill(newSkill);
        })        
    }

    onProficiencyChanged(event) {

        let dal = new SkillsDal();
        let obj = this;

        dal.getProficiencyByName(event.target.value).then( newProf => {
            obj._setProficiency(newProf);
        })        
    }

    onMustHaveChanged(event) {
        this._setMustHave(event.target.checked);
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

        return (
            <div>
                <div style={styleEdit}>
                    <TextField 
                        id="skill" 
                        select                         
                        value={this.state.skill ? this.state.skill.Name : ""}
                        onChange={ (event) => this.onSkillChanged(event) }>
                        {
                            this.state.skills.map( (skill) => (
                                <option key={key++} value={skill.Name}>
                                    {skill.Name}
                                </option>
                            ) )
                        }
                    </TextField>

                    <TextField 
                        id="proficiency" 
                        select                          
                        value={this.state.proficiency ? this.state.proficiency.Name : ""}
                        onChange={ (event) => this.onProficiencyChanged(event) }
                        >
                        {
                            this.state.profs.map( (prof) => (
                                <option key={prof.ProficiencyID} value={prof.Name}>
                                    {prof.Name}
                                </option>
                            ) )
                        }
                    </TextField>

                    <FormControlLabel
                        control={<Checkbox checked={this.state.mustHave} onChange={(event) => this.onMustHaveChanged(event)} name="mustHave" />}
                        label="Must-Have"
                    />
                </div>  
                 
                <div style={styleView}>
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