
import React from 'react';
import SkillItem from '../components/SkillItem'
import { Button } from '@material-ui/core';
import constants from '../constants';
const { v4: uuidv4 } = require('uuid');


class SkillsList extends React.Component {


    constructor(props) {
        super(props);

        this.state = {
            canEdit: props.canEdit ? props.canEdit : false,
            skills: props.skills, 
            onSkillChanged: props.onSkillChanged,
            onSkillDeleted: props.onSkillDeleted,
            onSkillAdded: props.onSkillAdded
        }

        this.onSkillChanged = this.onSkillChanged.bind(this);

    }

    static getDerivedStateFromProps(props, state) {
        if(props.canEdit !== state.canEdit ||
            props.skills !== state.skills ||
            props.onSkillChanged != state.onSkillChanged || 
            props.onSkillDeleted != state.onSkillDeleted ||
            props.onSkillAdded != state.onSkillAdded)
        {
            let updatedState = {
                canEdit: props.canEdit,
                skills: props.skills,
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
            SkillID: null,
            ProficiencyID: null,
            IsMandatory: false
        }

        this.state.onSkillAdded(newSkill);
    }

    onDeleteSkill(id) {
        console.log('Deleting: ' + id);
        
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
            updatedSkill.ProficiencyID = profID;
            updatedSkill.IsMandatory = isMandatory;

            this.state.onSkillChanged(updatedSkill);
        }        
    }   

    render() 
    {
        //console.log('Render: ', this.state.skills);
        return (
            <div>
                {
                    this.state.skills.map( (skill) => (
                        <table key={"tblSkillItem" + skill.id}>
                            <tbody>
                                <tr>
                                    <td>
                                        <SkillItem  key={skill.id}
                                                id={skill.id} 
                                                skillID={skill.SkillID}
                                                proficiencyID={skill.ProficiencyID}
                                                mustHave={skill.IsMandatory}
                                                canEdit={this.state.canEdit}
                                                onSkillChanged={this.onSkillChanged}
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
                <Button variant="contained" color="primary" size="small" onClick={ () => this.onAddSkillClicked() }>+ Add</Button>
            </div>
        )
    }   
}

export default SkillsList;