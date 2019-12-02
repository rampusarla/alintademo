import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { FetchCustomers } from './components/FetchCustomers';
import { AddCustomer } from './components/AddCustomer';

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
        <Layout>
        <Route exact path='/' component={FetchCustomers} />                
        <Route path='/add-customer' component={AddCustomer} />    
        <Route path='/customer/edit/:id' component={AddCustomer} />  
      </Layout>
    );
  }
}
