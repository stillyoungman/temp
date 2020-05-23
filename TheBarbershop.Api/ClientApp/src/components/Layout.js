import React, { Component } from 'react';
import { Container } from 'reactstrap';
import { NavMenu } from './NavMenu';

export class Layout extends Component {
  static displayName = Layout.name;

  render () {
    return (
      <>
        <NavMenu {...this.props} />
        <Container style={{height: "100%"}}>
          {this.props.children}
        </Container>
      </>
    );
  }
}
