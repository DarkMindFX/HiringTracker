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

            dalPos.getPosition(this.state.id).then( (pos) => {
                let updatedState = this.state;

                updatedState.position = pos;

                this.setState(updatedState); 

                dalPos.getPositionSkills(this.state.id).then( (skills) => {

                    let updatedState = this.state;
                    
                    updatedState.position._skills = skills.map(s => { s.id = uuidv4(); return s; });

                    this.setState(updatedState); 

                })
            })

        }
    }

    onSkillChanged(id, skillID, profID, isMandatory) {

        let updatedState = this.state;
        let skill = updatedState._skills.find( s => s._skill.id == id );
        if(skill != null) {
            skill.SkillID = skillID;
            skill.ProficiencyID = profID;
            skill.IsMandatory = isMandatory;

            console.log(skill);

            console.log('onSkillChanged',updatedState.skills);

            this.setState(updatedState);
        }        
    }   

    onSkillAdded(id, skillID, profID, isMandatory) {

    }

    onSkillRemoved(id) {

    }

    render() {

        let skills = this._getPositionSkills();

        console.log("Position.render: ", skills);

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
                            <td><TextField id="title" type="text" variant="filled" label="Position Title" value={this.state.position._title}/></td>
                        </tr>                    
                        <tr>
                            <td><TextField id="shortDesc" type="text" variant="filled" label="Short Description" value={this.state.position._shortDesc}/></td>
                        </tr>
                        <tr>
                            <td>
                                <TextField  id="status" 
                                                select 
                                                label="Status" 
                                                value={ this.state.position._status ? this.state.position._status._name : "Draft" }
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
                                            value={this.state.position._desc}
                                            rows="10"/>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <SkillsList id="positionSkills"
                                    skills={ skills }
                                    canEdit={ this.state.canEdit }
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
            _title: "",
            _shortDesc: "",
            _desc: "",
            _status: null,
            _skills: []
        }

        return pos;
    }

    _getPositionSkills() {
        let skills = [];

        if( this.state.position._skills ) {
            
            this.state.position._skills.forEach( s => {
                let skill = {
                    id: s.id,
                    SkillID: s._skill._skillId, 
                    ProficiencyID: s._proficiency._id, 
                    IsMandatory: s._isMandatory
                };
                skills.push(skill);
            })
        }  
      
        return skills;
    }
}

export default PositionPage;