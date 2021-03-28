
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
            skills: props.skills ? props.skills : []
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
    }

    render() 
    {
        console.log(this.state.skills);
        return (
            <div>
                {
                    this.state.skills.map( (skill) => (
                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <SkillItem  key={skill.id} 
                                                skillID={skill.SkillID}
                                                proficiencyID={skill.ProficiencyID}
                                                mustHave={skill.IsMandatory}
                                                canEdit={this.state.canEdit} />
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