import React from 'react';
import { FormControl } from '@material-ui/core';
import { TextField } from '@material-ui/core';
import { Button } from '@material-ui/core';
import SkillsList from '../components/SkillsList';

const PositionsDal = require('../dal/PositionsDal');
const SkillsDal = require('../dal/SkillsDal');

const constants = require('../constants');
const { v4: uuidv4 } = require('uuid');

class PositionPage extends React.Component {

    constructor(props) {
        super(props);

        this.state = { 
            operation: this.props.match.params.operation,
            id: parseInt(this.props.match.params.id),
            canEdit: this.props.match.params.operation ? (this.props.match.params.operation.toLowerCase() == 'new' || 
                                                          this.props.match.params.operation.toLowerCase() == 'edit' ? true : false) : false,
            position: this._createEmptyPositionObj()
        };

        console.log(this.state);
    }

    onStatusChanged(event) {

    }

    componentDidMount() {
        if(this.state.id) {
            let dalPos = new PositionsDal();
            let obj = this;

        }
    }

    render() {
        return (
            <div>
                 <table>
                    <tbody>
                        <tr>
                            <td><Button variant="contained" color="primary">Save</Button>
                            </td>
                            <td><Button variant="contained" color="secondary">Delete</Button>
                            </td>
                            <td><Button variant="contained" >Cancel</Button>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <table>
                    <tbody>
                        <tr>                            
                            <td><TextField id="title" type="text" variant="filled" label="Position Title" value={this.state.position.title}/></td>
                        </tr>                    
                        <tr>
                            <td><TextField id="shortDesc" type="text" variant="filled" label="Short Description" value={this.state.position.shortDesc}/></td>
                        </tr>
                        <tr>
                            <td>
                                <TextField  id="status" 
                                                select 
                                                label="Status" 
                                                value="Draft"
                                                onChange={ (event) => this.onStatusChanged(event) }>
                                        {
                                            constants.POSITION_STATUSES.map( (status) => (
                                                <option key={status.statusID} value={status.name}>
                                                    {status.name}
                                                </option>
                                            ) )
                                        }
                                </TextField>
                            </td>
                        </tr>
                        <tr>
                            <td>Position Details</td>
                        </tr>
                        <tr>
                            <td>
                                <TextField  id="desc" 
                                            type="text" 
                                            variant="filled" 
                                            multiline 
                                            fullWidth
                                            value={this.state.position.desc}
                                            rows="10"/>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <SkillsList id="positionSkills"
                                    skills={ this._getPositionSkills()}
                                    canEdit={this.state.canEdit}
                                 />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>

        );
    }

    _createEmptyPositionObj() {
        let pos = {
            title: "",
            shortDesc: "",
            desc: "",
            statusId: constants.POSITION_STATUSES[0].statusID, // Draft
            skills: []
        }

        return pos;
    }

    _getPositionSkills() {
        let skills = {};

        let skill = {
            id: uuidv4(),
            SkillID: 1, ProficiencyID: 1, IsMandatory: true
        };
        skills[skill.id] = skill

        skill = {
            id: uuidv4(),
            SkillID: 3, ProficiencyID: 3, IsMandatory: true
        };
        skills[skill.id] = skill

        skill = {
            id: uuidv4(),
            SkillID: 4, ProficiencyID: 2, IsMandatory: false
        };
        skills[skill.id] = skill

        skill = {
            id: uuidv4(),
            SkillID: 8, ProficiencyID: 4, IsMandatory: false
        };
        skills[skill.id] = skill
        
        return Object.values(skills);
    }
}

export default PositionPage;