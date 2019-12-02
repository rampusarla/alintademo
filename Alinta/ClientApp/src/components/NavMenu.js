import React, { Component } from 'react';
import { Container, Navbar, NavbarToggler } from 'reactstrap';
import './NavMenu.css';

export class NavMenu extends Component {
  static displayName = NavMenu.name;

  constructor (props) {
    super(props);

    this.toggleNavbar = this.toggleNavbar.bind(this);
    this.state = {
      collapsed: true
    };
  }

  toggleNavbar () {
    this.setState({
      collapsed: !this.state.collapsed
    });
  }

  render () {
    return (
      <header>
        <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" light>
          <Container>                    
            <a className="nav-link" href="/">Alinta Energy Demo</a>
            <NavbarToggler onClick={this.toggleNavbar} className="mr-2" />            
          </Container>
        </Navbar>
      </header>
    );
  }
}
