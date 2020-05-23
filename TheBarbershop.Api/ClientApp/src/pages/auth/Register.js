import React, { Component } from 'react';
import OneButtonTabBar from './OneButtonTopBar'
import styles from '../../styles/auth.module.css'

export default class Register extends Component {
  render() {
    return (
      <div className="center">
        <OneButtonTabBar to="/" buttonText="Войти" />
        <div>
          <form className={styles["form"]}>
            <h4 className="h4 mb-3 font-weight-normal">Новый администратор</h4>
            <input type="email" id="inputEmail" className="form-control" placeholder="Логин" required="" autoFocus="" autoComplete="off" ></input>
            <input type="password" id="inputPassword" className="form-control" placeholder="Пароль" required="" autoComplete="off" />
            <input type="password" id="inputCode" className="form-control" placeholder="Код" required="" autoComplete="off" />
            <button className={`btn btn-lg btn-primary btn-block ${styles["btn"]}`} type="submit">Создать</button>
          </form>
        </div>
      </div>

    );
  }
}
