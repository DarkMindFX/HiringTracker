
import React from "react";
import { Link, withRouter } from 'react-router-dom'
import { DataGrid } from '@material-ui/data-grid';
import Alert from '@material-ui/lab/Alert';
import { Button } from '@material-ui/core';
import constants from "../constants";
import CommentView from "../components/CommentView";
import { CandidateCommentDto, CommentDto } from "hrt.dto";

const PageHelper = require("../helpers/PageHelper");
const CandidateCommentsDal = require('../dal/CandidateCommentsDal');

const UsersDal = require('../dal/UsersDal');
const CommentsDal = require('../dal/CommentsDal');


class CandidateComments extends React.Component {

    _columns = null;
    _pageHelper = null;

    constructor(props) {
        super(props);

        this._pageHelper = new PageHelper(this.props);

        this.state = { 
            id: props.id, // candidate id
            canAddComment: props.canAddComment ? props.canAddComment : true,
            candidatecomments: [],
            commentText: "",
            showError: false,
            error: null
        };
        this._initColumns();
       
        this._getUsers = this._getUsers.bind(this);
        this._getCandidateComments = this._getCandidateComments.bind(this);
        this._redirectToLogin = this._redirectToLogin.bind(this);
        this._cleanCommentText = this._cleanCommentText.bind(this);

        this.onEditCommentClick = this.onEditCommentClick.bind(this);
        this.onDeleteCommentClick = this.onDeleteCommentClick.bind(this);
        this.onAddCommentClick = this.onAddCommentClick.bind(this);
        this.onCommentTextChanged = this.onCommentTextChanged.bind(this);
    }

    onCommentTextChanged(event) {
        let updatedState = this.state;

        updatedState.commentText = event.target.value;

        this.setState(updatedState);
    }

    onEditCommentClick(commentId, text) {
    }

    onDeleteCommentClick(commentId) {
        let dalComments = new CommentsDal();
        dalComments.deleteComment(commentId).then( (response) => {
            if(response.status == constants.HTTP_OK) {
                this._getCandidateComments().then( () => { } );
            }
        });        
    }

    onAddCommentClick() {   
        let dalComments = new CommentsDal();
        let comment = new CommentDto();
        comment.Text = this.state.commentText;
        let obj = this;
        dalComments.insertComment(comment).then( (respInsert) => {
            if(respInsert.status == constants.HTTP_Created) {

                console.log("New comment: ", respInsert.data.ID);

                let candidateComment = new CandidateCommentDto();
                candidateComment.CandidateID = this.props.id;
                candidateComment.CommentID = respInsert.data.ID;

                let dalCandidateComments = new CandidateCommentsDal();
                dalCandidateComments.insertCandidateComment( candidateComment ).then( (respAddCandComment) => {
                    if(respAddCandComment.status == constants.HTTP_Created) {
                        obj._getCandidateComments().then( () => { obj._cleanCommentText() } );
                    }
                })
            }   
        })          
    }

    componentDidMount() {
        const token = localStorage.getItem(constants.SESSION_TOKEN_KEY);
        console.log('Token: ', token);
        if(token != null) {
            let obj = this;
            obj._getUsers().then( () => {			
			    obj._getCandidateComments().then( () => {} );
			});
        }
        else {
            console.log('No token - need to login')
            this._redirectToLogin();           
        }
    }

    render() {
        let records = this._getRecords();

        const styleError = {
            display: this.state.showError ? "block" : "none"
        }

        const styleAddCommentBtn = {
            display: this.state.canAddComment ? "block" : "none"
        }

        var lstCommentViews = records.map( c => {

            return <CommentView 
                key={`CommentView-${c.id}`}
                id={c.id}
                ownerId={this.state.id}
                text={c.Text}
                createdDate={c.CreatedDate}
                createdBy={c.CreatedBy}
                modifiedDate={c.ModifiedDate}
                modifiedBy={c.ModifiedBy}
                canEdit={true}
                canDelete={true}
                onEdit={this.onEditCommentClick}
                onDelete={this.onDeleteCommentClick}
            />
        });

        return (
            <div>                
                {
                    lstCommentViews 
                }
                <div style={styleAddCommentBtn}>
                    <textarea style={{width: "100%"}} 
                                value={ this.state.commentText }
                                onChange={ this.onCommentTextChanged }>                        
                    </textarea>
                    <Button variant="contained" color="primary"                                        
                            onClick={ () => this.onAddCommentClick() }>+ Comment</Button>  
                </div>   
            </div>
        );
    }

    _initColumns() {
        this._columns = [
                { field: 'CandidateID', headerName: 'CandidateID', width: 250 },
                { field: 'CommentID', headerName: 'CommentID', width: 250 },
       
        ]        
    }

    _getRecords() {
        let records = [];

        let cs = Object.values(this.state.candidatecomments);

        for(let c in cs) {

            let r = {
                id: cs[c].ID,
                Text: cs[c].Text,
                CreatedBy: cs[c].CreatedByID ? this.state.users[ cs[c].CreatedByID ].FirstName + " " + this.state.users[ cs[c].CreatedByID ].LastName : "",
                CreatedDate: cs[c].CreatedDate,
                ModifiedBy: cs[c].ModifiedByID ? this.state.users[ cs[c].ModifiedByID ].FirstName + " " + this.state.users[ cs[c].ModifiedByID ].LastName : "",
                ModifiedDate: cs[c].ModifiedDate,
            };

            records.push(r);
        }

        return records;
    }

    async _getUsers() {
        let updatedState = this.state;
        updatedState.users = {};
        let dalUsers = new UsersDal();
        let response = await dalUsers.getUsers();

        if(response.status == constants.HTTP_OK)
        {
            for(let s in response.data)
            {
                updatedState.users[response.data[s].ID] = response.data[s];             
            }
        }
        else if(response.status == constants.HTTP_Unauthorized) {
            this._redirectToLogin();            
        }
        else {
            this._showError(updatedState, response);                        
        }

        this.setState(updatedState);
    }

    async _cleanCommentText() {
        let updatedState = this.state;
        updatedState.commentText = "";
        this.setState(updatedState);
    }
    

    async _getCandidateComments() {
        let updatedState = this.state;
        updatedState.candidatecomments = {};
        let dalCandidateComments = new CandidateCommentsDal();
        let dalComments = new CommentsDal();

        console.log(`Getting comments for candidate ${this.state.id}`);

        let response = await dalCandidateComments.getCandidateCommentsByCandidateID(this.state.id);

        if(response.status == constants.HTTP_OK)
        {
            for(let s in response.data)
            {
                var respComment = await dalComments.getComment(response.data[s].CommentID);
                if(respComment.status == constants.HTTP_OK )
                {
                    updatedState.candidatecomments[respComment.data.ID] = respComment.data;
                }             
            }

            console.log(updatedState.candidatecomments);
        }
        else if(response.status == constants.HTTP_Unauthorized) {
            this._redirectToLogin();            
        }
        else {
            this._showError(updatedState, response);                        
        }

        this.setState(updatedState);
    }

    _showError(updatedState, response) {
        var error = JSON.parse(response.data.response);
        updatedState.showError = true;
        updatedState.error = error.Message;
    }

    _redirectToLogin()
    {        
        this._pageHelper.redirectToLogin(`/candidatecomments`);
    }
}

export default withRouter(CandidateComments);