import React, { Component } from 'react'
import UserListItem from './UserListItem'

export default class Users extends Component {
  static displayName = "Пользователи";

  render() {
    return (
      <div>
        <table style={{ width: '100%' }}>
          <tbody>
            <UserListItem isBlocked={true} userType="master">Bob Lee Swagger</UserListItem>
            <UserListItem isBlocked={false} userType="client">Adam Smith</UserListItem>
          </tbody>

        </table >
      </div>
    );
  }
}
