import React from 'react';
import { Link } from 'react-router-dom';
import moment from 'moment';
class UsersListRow extends React.Component{

    formattedDate(month) {
        if (month >0) {
            return moment(month, 'MM').format('MMMM');
        } else {
            return '';
        }
    }
    rowData(user){
        return (
            <tr key={user.id}>
                <td>
                    <div className="member-entry">

                        <a href={"/users/"+user.id} className="member-img">
                            {user.pictureUrl ?
                                (
                                    <img src={"/public/uploads/"+user.pictureUrl} className="img-rounded"  alt="..."/>
                                ) :
                                (
                                    <img src="/public/uploads/yds.jpg" className="img-rounded"  alt="..."/>
                                )
                            }

                            <i className="entypo-forward"></i>
                        </a>

                        <div className="member-details">
                            <h4>
                                <Link to={"/users/"+user.id}>{user.firstName} {user.lastName}</Link>
                            </h4>

                            <div className="row info-list">

                                <div className="col-sm-4">
                                    <i className="entypo-briefcase"></i> Designation : {user.designation}
                                </div>

                                <div className="col-sm-4">
                                    <i className="entypo-mail"></i> Email : {user.email}
                                </div>

                                <div className="col-sm-4">
                                    <i className="entypo-phone"></i> Phone : {user.phoneNumber}
                                </div>

                                <div className="clear"></div>

                                <div className="col-sm-4">
                                    <i className="entypo-back-in-time"></i> Birth Date : {this.formattedDate(user.birthMonth)} {user.birthDay}
                                </div>

                                
                            </div>
                        </div>

                    </div>
                </td>
            </tr>
        )
    }


    render(){

        const {filteredRows} = this.props;
        let usersList = "";
        usersList = filteredRows.map(function (user) {
            return (
                this.rowData(user)
            )
        }.bind(this));

        return(
            <tbody>
                {usersList}
            </tbody>
        )
    }
}

export default UsersListRow;