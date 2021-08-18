
import React from "react";
import { Link, withRouter } from 'react-router-dom'
import { Button } from '@material-ui/core';

class CommentView extends React.Component {

    constructor(props) {
        super(props);

        this.state = {
            id: props.id,
            ownerId: props.ownerId,
            text: props.text,
            createdBy: props.createdBy,
            createdDate: props.createdDate,
            modifiedBy: props.modifiedBy,
            modifiedDate: props.modifiedBy,
            canEdit: props.canEdit,
            canDelete: props.canDelete,
            onEdit: props.onEdit,
            onDelete: props.onDelete
        }

        this.onEditClicked = this.onEditClicked.bind(this);
        this.onDeleteClicked = this.onDeleteClicked.bind(this);

        this._commentView = this._commentView.bind(this);
    }

    onEditClicked() {
        if(this.state.onEdit) {
            // TODO: call state.onEdit()
        }
    }

    onDeleteClicked() {
        if(this.state.onDelete) {
            // TODO: call state.onDelete()
        }
    }

    render() {
        return (
            <div>
                { this._commentView() }
            </div>

        )
    }

    _commentView() {

        const styleDeleteBtn = {
            display: this.state.canDelete ? "block" : "none"
        }

        const styleEditBtn = {
            display: this.state.canEdit ? "block" : "none"
        }

        console.log(this.state);

        return (
            <div>
                <table><tbody>
                    <tr>
                        <td>{this.state.createdBy}</td>
                        <td>{this.state.createdDate}</td>
                        <td>
                            <Button variant="contained" color="primary"
                                        style={styleEditBtn}
                                        onClick={ () => this.onEditClicked() }>Edit</Button>

                            <Button variant="contained" color="secondary"
                                        style={styleDeleteBtn}
                                        onClick={ () => this.onDeleteClicked() }>Delete</Button>
                        </td>
                    </tr>
                    <tr>
                        <td colSpan={3}>
                            <textarea readOnly style={{width: '100%'}}>
                                {this.state.text}
                            </textarea>
                        </td>
                    </tr>
                </tbody></table>
            </div>

        )
    }

}

export default CommentView;