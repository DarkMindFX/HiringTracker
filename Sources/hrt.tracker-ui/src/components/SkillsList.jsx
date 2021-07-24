
import React from 'react';
import SkillItem from '../components/SkillItem'
import { Button } from '@material-ui/core';
const constants = require('../constants');
const { v4: uuidv4 } = require('uuid');


class SkillsList extends React.Component {


    constructor(props) {
        super(props);

        this.state = {
            canEdit: props.canEdit ? props.canEdit : false,
            showMustHave: props.showMustHave ? props.showMustHave : false,
            skills: props.skills, 
            dictSkills : props.dictSkills,
            dictProficiencies: props.dictProficiencies,
            onSkillChanged: props.onSkillChanged,
            onSkillDeleted: props.onSkillDeleted,
            onSkillAdded: props.onSkillAdded
        }

        this.onSkillChanged = this.onSkillChanged.bind(this);

    }

    static getDerivedStateFromProps(props, state) {
        if(props.canEdit !== state.canEdit ||
            props.skills !== state.skills ||
            props.dictSkills != state.dictSkills ||
            props.dictProficiencies != state.dictProficiencies ||
            props.onSkillChanged != state.onSkillChanged || 
            props.onSkillDeleted != state.onSkillDeleted ||
            props.onSkillAdded != state.onSkillAdded)
        {
            let updatedState = {
                canEdit: props.canEdit,
                skills: props.skills,
                dictSkills : props.dictSkills,
                dictProficiencies: props.dictProficiencies,
                onSkillChanged: props.onSkillChanged,
                onSkillDeleted: props.onSkillDeleted,
                onSkillAdded: props.onSkillAdded
            }

            return updatedState;
        }
        else 
        {
            return null;
        }

    }

   
    onAddSkillClicked() {
        let updatedState = this.state;

        let newSkill = {
            id: uuidv4(),
            SkillID: 1, // Object.values(updatedState.dictSkills)[0].ID,
            SkillProficiencyID: 1, //Object.values(updatedState.dictProficiencies)[0].ID,
            IsMandatory: false
        }

        this.state.onSkillAdded(newSkill);
    }

    onDeleteSkill(id) {
        
        let skillIdx = this.state.skills.findIndex( s => s.id == id );
        if(skillIdx >= 0) {
            this.state.onSkillDeleted(id);
        }
    }

    onSkillChanged(id, skillID, profID, isMandatory) {

        let updatedState = this.state;
        let updatedSkill = updatedState.skills.find( s => s.id == id );
        if(updatedSkill != null) {
            updatedSkill.SkillID = skillID;
            updatedSkill.SkillProficiencyID = profID;
            updatedSkill.IsMandatory = isMandatory;

            this.state.onSkillChanged(updatedSkill);
        }        
    }   

    render() 
    {
        return (
            <div>
                {
                    this.state.skills.map( (skill) => (
                        <table key={"tblSkillItem" + skill.id}>
                            <tbody>
                                <tr>
                                    <td>
                                        <SkillItem  key={ skill.id }
                                                id={ skill.id } 
                                                skillID={ skill.SkillID }
                                                proficiencyID={ skill.SkillProficiencyID }
                                                dictSkills = { this.state.dictSkills }
                                                dictProficiencies = { this.state.dictProficiencies }
                                                mustHave={ skill.IsMandatory }
                                                showMustHave={ this.state.showMustHave }
                                                canEdit={ this.state.canEdit }
                                                onSkillChanged={ this.onSkillChanged }
                                         />
                                    </td>
                                    <td>                                        
                                        <Button key={"btnDelSkill" + skill.id} variant="contained" size="small" onClick={ () => this.onDeleteSkill(skill.id) }>X</Button>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        
                    ))                    
                }
                <Button variant="contained" color="primary" size="small" onClick={ () => this.onAddSkillClicked() }>+ Skill</Button>
            </div>
        )
    }   
}

export default SkillsList;