
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
            skills: props.skills
        }

        this.onSkillChanged = this.onSkillChanged.bind(this);

    }

    static getDerivedStateFromProps(props, state) {
        if(props.canEdit !== state.canEdit ||
            props.skills !== state.skills ||
            props)
        {
            let updatedState = {
                canEdit: props.canEdit,
                skills: props.skills            
            }

            console.log('After removal:', updatedState.skills);

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

        updatedState.skills.push(newSkill);

        this.setState(updatedState);
    }

    onDeleteSkill(id) {
        console.log('Deleting: ' + id);
        
        let skillIdx = this.state.skills.findIndex( s => s.id == id );
        if(skillIdx >= 0) {
            let updatedState = {}

            let newSkills = this.state.skills.map(s => s);
            newSkills.splice(skillIdx, 1);

            updatedState.skills = newSkills;
            updatedState.canEdit = this.state.canEdit;             
            
            this.setState(updatedState);
            this.forceUpdate();
        }
    }

    onSkillChanged(id, skillID, profID, isMandatory) {

        let updatedState = this.state;
        let skill = updatedState.skills.find( s => s.id == id );
        if(skill != null) {
            skill.SkillID = skillID;
            skill.ProficiencyID = profID;
            skill.IsMandatory = isMandatory;

            console.log(skill);

            console.log('onSkillChanged',updatedState.skills);

            this.setState(updatedState);
        }        
    }   

    render() 
    {
        console.log('Render: ', this.state.skills);
        return (
            <div>
                {
                    this.state.skills.map( (skill) => (
                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <SkillItem  id={skill.id} 
                                                skillID={skill.SkillID}
                                                proficiencyID={skill.ProficiencyID}
                                                mustHave={skill.IsMandatory}
                                                canEdit={this.state.canEdit}
                                                onSkillChanged={this.onSkillChanged}
                                         />
                                    </td>
                                    <td>                                        
                                        <Button id={"btnDelSkill" + skill.id} variant="contained" size="small" onClick={ () => this.onDeleteSkill(skill.id) }>X</Button>
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