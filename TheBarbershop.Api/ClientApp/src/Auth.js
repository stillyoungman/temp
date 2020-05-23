import React, { Component } from 'react';
import Login from './pages/auth/Login'
import Register from './pages/auth/Register'

import { BrowserRouter, Route, Redirect, Switch } from 'react-router-dom';

export default class AuthPage extends Component {

    componentDidMount(){
        document.body.classList.add("auth-body")
        document.getElementById('html').classList.add("auth-body")
        // document.
    }

    componentWillUnmount(){
        document.body.classList.remove("auth-body")
        document.getElementById('html').classList.remove("auth-body")
    }

    render() {
        return (
            <BrowserRouter basename={this.props.basename}>
                <Switch>
                    <Route exact path='/' component={Login} />
                    <Route path="/register" component={Register} />
                    <Redirect path="/" to="/" />
                </Switch>
            </BrowserRouter>)
    }
}