import React, {PropTypes} from 'react';
import { viewUser,updateUser } from '../actions/'
import {connect} from 'react-redux';
import {bindActionCreators} from 'redux';
import Menu from '../components/menu';
import {ROOT_URL} from '../actions/index';

class UserDetails extends React.Component{ 

    static propTypes = {
        history: PropTypes.object.isRequired
    }

    componentDidMount() {
console.log("loaded");
        if(this.props.isAuthenticated){
            this.getUserDetails();
        }else{
            this.props.history.push('/login')
        }
    }

    getUserDetails(){
        const {user_id, family_id} = this.props.match.params;
        this.props.viewUser(user_id, (userData) => {
            console.log('userrs', userData)
            this.setState({ userData })
        });
    }

    setDefaultObject(){
        let defaultData = {
            pictureUrl : '',
            id : '',
            firstName : '',
            midName : '',
            lastName : '',
            email : '',
            phoneNumber : '',
            home_phone : '',
            birthMonth : '',
            birthDay : '',
            street : '',
            apt : '',
            city : '',
            state : '',
            zip : '',
            file: '',
            imagePreviewUrl: ''
        };
        this.state = {
            userData : defaultData
        }
    }

    _handleImageChange(e) {
        e.preventDefault();

        let reader = new FileReader();
        let file = e.target.files[0];

        reader.onloadend = () => {
            let currentState = this.state.userData;
            currentState.file = file;
            currentState.imagePreviewUrl = reader.result;
            this.setState({userData : currentState})
        }

        reader.readAsDataURL(file)
    }

    removeImage(e) {
        e.preventDefault();
        let currentState = this.state.userData;
        currentState.imagePreviewUrl = '';
        currentState.pictureUrl = '';
        this.setState({userData : currentState})

    }

    handleSubmit(evt) {
        const formData = this.state.userData;
        evt.preventDefault();
        // this.props.updateUser(formData, ()=>{
        //     this.setState({userData: this.props.userDetails})
        //     console.log('props', this.props.userDetails);
        // });
        console.log('formData.file',this.state, formData.file);
        this.updateUserData(formData.file).then(
            ()=>{
                this.getUserDetails();
            }
        )
    }

    updateUserData(imageFile) {
        return new Promise((resolve, reject) => {
            let imageFormData = new FormData();

            imageFormData.append('imageFile', imageFile);
            imageFormData.append('imageName', this.state.userData.pictureUrl);
            imageFormData.append('userData', JSON.stringify(this.state.userData));

            var xhr = new XMLHttpRequest();

            xhr.open("POST", ROOT_URL+'/users/'+this.state.userData.id, true); 
            xhr.onload = function () {
                if (this.status == 200) {
                    resolve(this.response);
                } else {
                    reject(this.statusText);
                }
            };

            console.log('imageFormData', imageFormData);
            xhr.send(imageFormData);

        });
    }

    renderImage(userDetails){
        // const { userDetails } = this.props;

        let {imagePreviewUrl} = this.state.userData;
        let {pictureUrl} = this.state.userData;

        let $imagePreview = null;
        if (imagePreviewUrl !== undefined && imagePreviewUrl !== '') {
            $imagePreview = (<img src={imagePreviewUrl} style={{height: 140}} alt="..."/>);
        }else if(pictureUrl){
            $imagePreview = (<img src={"/public/uploads/"+pictureUrl+"?"+new Date().getTime()} style={{height: 140}} alt="..."/>);
        }else{
            $imagePreview = (<img className="img-circle" src="http://placehold.it/200x150" alt="..."/>);
        }

        if(pictureUrl){
            return(
                <div className="fileinput fileinput-exists" data-provides="fileinput">
                    <input type="hidden" value="" name="" />
                    <div className="fileinput-preview fileinput-exists thumbnail" style={{width: 200, height: 150}}>
                        {$imagePreview}
                    </div>
                    <div>
											<span className="btn btn-white btn-file">
												<span className="fileinput-new">Select image</span>
												<span className="fileinput-exists">Change</span>
                                                <input type="file" onChange={this._handleImageChange.bind(this)} className="_user_profile" name="pictureUrl" accept="image/*" />
											</span>
                        <a href="#" className="btn btn-orange fileinput-exists" onClick={this.removeImage.bind(this)} data-dismiss="fileinput">Remove</a>
                    </div>
                </div>
            )
        }else{
            return (
                <div className="fileinput fileinput-new" data-provides="fileinput">
                    <div className="fileinput-new thumbnail" style={{width: 200, height: 150}} data-trigger="fileinput">
                        {$imagePreview}
                    </div>
                    <div>
											<span className="btn btn-white btn-file">
												<span className="fileinput-new">Select image</span>
												<span className="fileinput-exists">Change</span>
                                                <input type="file" onChange={this._handleImageChange.bind(this)} className="_user_profile" name="pictureUrl" accept="image/*" />
											</span>
                        <a href="#" className="btn btn-orange fileinput-exists" onClick={this.removeImage.bind(this)} data-dismiss="fileinput">Remove</a>
                    </div>
                </div>

            )
        }
    }

    constructor(props, context) {
        super(props, context);
        this.setDefaultObject();
    }

    handleChange(event) {
        let field_name = event.target.name;
        let userData = Object.assign({}, this.state.userData);    //creating copy of object
        userData[field_name] = event.target.value;                        //updating value
        this.setState({userData});
    }

    

    render(){
        let user = this.state.userData;
console.log('gg', this.state)
        return(
            <form className="form-horizontal" id="_user_profile_form" encType="multipart/form-data" action={"/users/"+user.id}  onSubmit={this.handleSubmit.bind(this)}>
                <div className="profile-env">
                    <section className="profile-info-tabs">
                        <div className="row">
                            <div className="col-md-4">
                                <div className="panel panel-default">
                                    <div className="panel-body text-center">
                                        <div className="pv-lg">
                                            {this.renderImage(user)}
                                        </div>
                                        <h3 className="m0 text-bold">{user.firstName} {user.lastName}</h3>
                                        <div className="mv-lg">
                                            <p>{user.description}</p>
                                        </div>

                                    </div>
                                </div>

                            </div>
                            <div className="col-md-8">
                                <div className="panel panel-default">
                                    <div className="panel-body">
                                        <div className="pull-right">
                                            <div className="btn-group">
                                                <button data-toggle="dropdown" className="btn btn-link">
                                                    <em className="fa fa-ellipsis-v fa-lg text-muted"></em>
                                                </button>
                                                <ul role="menu" className="dropdown-menu dropdown-menu-right animated fadeInLeft">
                                                    <li>
                                                        <a href="#">
                                                            <span>Send by message</span>
                                                        </a>
                                                    </li>
                                                    <li>
                                                        <a href="#">
                                                            <span>Share contact</span>
                                                        </a>
                                                    </li>
                                                    <li>
                                                        <a href="#">
                                                            <span>Block contact</span>
                                                        </a>
                                                    </li>
                                                    <li>
                                                        <a href="#">
                                                            <span className="text-warning">Delete contact</span>
                                                        </a>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                        <div className="h4 text-center">Contact Information</div>
                                        <div className="row pv-lg">
                                            <div className="col-lg-2"></div>
                                            <div className="col-lg-8">
                                                <div className="form-group">
                                                    <label htmlFor="firstName" className="col-sm-4 control-label">First Name</label>
                                                    <div className="col-sm-8">
                                                        <input id="firstName" name="firstName" type="text" placeholder="First Name" onChange={this.handleChange.bind(this)} value={user.firstName || ''} className="form-control"/>
                                                    </div>
                                                </div>
                                                <div className="form-group">
                                                    <label htmlFor="midName" className="col-sm-4 control-label">Middle Name</label>
                                                    <div className="col-sm-8">
                                                        <input id="midName" name="midName" type="text" placeholder="Middle Name" onChange={this.handleChange.bind(this)} value={user.midName || ''} className="form-control"/>
                                                    </div>
                                                </div>
                                                <div className="form-group">
                                                    <label htmlFor="lastName" className="col-sm-4 control-label">Last Name</label>
                                                    <div className="col-sm-8">
                                                        <input id="lastName" name="lastName" type="text" placeholder="Last Name" onChange={this.handleChange.bind(this)} value={user.lastName || ''} className="form-control"/>
                                                    </div>
                                                </div>
                                                <div className="form-group">
                                                    <label htmlFor="email" className="col-sm-4 control-label">Email</label>
                                                    <div className="col-sm-8">
                                                        <input id="email" name="email" type="email" placeholder="Email" onChange={this.handleChange.bind(this)} value={user.email || ''} className="form-control"/>
                                                    </div>
                                                </div>
                                                <div className="form-group">
                                                    <label htmlFor="phoneNumber" className="col-sm-4 control-label">Cell Phone</label>
                                                    <div className="col-sm-8">
                                                        <input id="phoneNumber" name="phoneNumber" placeholder="Cell Phone" type="text" onChange={this.handleChange.bind(this)} value={user.phoneNumber || ''} className="form-control"/>
                                                    </div>
                                                </div>
                                           
                                                <div className="form-group">
                                                    <label htmlFor="birthDay" className="col-sm-4 control-label">Birth Date (mm/dd)</label>
                                                    <div className="col-sm-8">
                                                        <input id="birthDay" name="birthDay" placeholder="mm/dd" type="text"  value={user.birth_month+"/"+user.birth_day || ''} onChange={this.handleChange.bind(this)} className="form-control"/>
                                                    </div>
                                                </div>

                                                <div className="form-group">
                                                    <label htmlFor="street" className="col-sm-4 control-label">Street</label>
                                                    <div className="col-sm-8">
                                                        <textarea id="street" name="street" className="form-control" onChange={this.handleChange.bind(this)}  value={user.street}></textarea>
                                                    </div>
                                                </div>
                                                <div className="form-group">
                                                    <label htmlFor="apt" className="col-sm-4 control-label">Apt #</label>
                                                    <div className="col-sm-8">
                                                        <input id="apt" name="apt" type="text" value={user.apt || ''} onChange={this.handleChange.bind(this)} className="form-control"/>
                                                    </div>
                                                </div>
                                                <div className="form-group">
                                                    <label htmlFor="city" className="col-sm-4 control-label">City</label>
                                                    <div className="col-sm-8">
                                                        <input id="city" name="city" type="text" value={user.city || ''} onChange={this.handleChange.bind(this)} className="form-control"/>

                                                    </div>
                                                </div>
                                                <div className="form-group">
                                                    <label htmlFor="state" className="col-sm-4 control-label">State</label>
                                                    <div className="col-sm-8">
                                                        <input id="state" name="state" type="text" value={user.state || ''} onChange={this.handleChange.bind(this)} className="form-control"/>
                                                    </div>
                                                </div>
                                                <div className="form-group">
                                                    <label htmlFor="zip" className="col-sm-4 control-label">Zip</label>
                                                    <div className="col-sm-8">
                                                        <input id="zip" name="zip" type="text" value={user.zip || ''} onChange={this.handleChange.bind(this)} className="form-control"/>
                                                    </div>
                                                </div>
                                                <div className="form-group">
                                                    <div className="col-sm-offset-2 col-sm-8">
                                                        <button type="submit" className="btn btn-info">Save</button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </section>
                </div>
            </form>

        )
    }
}


function mapStateToProps(state) {
    return{
        userDetails: state.data.userDetails,
        isAuthenticated : state.auth.authenticated
    }
}

function mapDispatchToProps(dispatch){
    return bindActionCreators({viewUser,updateUser},dispatch)
}

export default connect(mapStateToProps,mapDispatchToProps)(UserDetails);
