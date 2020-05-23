import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Users } from './components/Users';
import PostEditor from './components/PostEditor';
import Posts from './components/Posts';
import { usersPath, postsPath, servicesPath, postEditorPath } from './constants';
import { withRouter } from 'react-router';
import { Switch, Redirect } from "react-router-dom"

import './custom.css'


class App extends Component {
  static displayName = App.name;

  render() {
    return (
      <Layout {...this.props}>
        <Switch>
          <Route exact path={usersPath} component={Users} />
          <Route path={postsPath} component={Posts} />
          <Route path={servicesPath} component={() => <></>} />
          <Route path={postEditorPath} component={PostEditor} />
          <Redirect path="/" to={usersPath} />
        </Switch>
      </Layout>
    );
  }
}

export default withRouter(App);
