import React, { Component } from 'react';
import OneButtonTabBar from './OneButtonTopBar';
import styles from '../../styles/auth.module.css'

export default class Login extends Component {
  render() {
    return (
      <div className="center">
        <OneButtonTabBar to="/register" buttonText="Зарегистрировать" />
        <div>
          <form className="form-signin">
            <h1>TheBarbershop</h1>
            <h1 className="h4 mb-3 font-weight-normal">Панель управления </h1>
            <input type="email" id="inputEmail" className="form-control" placeholder="Логин" required="" autoFocus="" autoComplete="off" ></input>
            <input type="password" id="inputPassword" className="form-control" placeholder="Пароль" required="" autoComplete="off" />
            <button  className={`btn btn-lg btn-primary btn-block ${styles["btn"]}`} type="submit">Войти</button>
          </form>
        </div>
      </div>

    );
  }
}
