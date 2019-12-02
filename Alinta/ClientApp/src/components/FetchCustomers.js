import React, { Component } from 'react';
import * as moment from 'moment';
import { Link } from 'react-router-dom';
import { handleErrors } from './Utils.js';
import { Search } from './Search.js';

export class FetchCustomers extends Component {
  static displayName = FetchCustomers.name;

  constructor (props) {
    super(props);
    this.state = { customers: [], loading: true, errors: [] };

      this.getCustomers();
      
      // This binding is necessary to make "this" work in the callback  
      this.handleDelete = this.handleDelete.bind(this);
      this.handleEdit = this.handleEdit.bind(this);
      this.handleErrors = handleErrors.bind(this);
      this.getCustomers = this.getCustomers.bind(this);
      this.getInfo = this.getInfo.bind(this);
    }

    getCustomers(searchString) {
        var updatedSearchString = (searchString) ? "searchString=" + searchString : "";
        fetch('api/Customer/Index?' + updatedSearchString)
            .then((response) => {
                if (response.ok) {
                    return response.json();
                }
            })
            .then(data => {
                this.setState({ customers: data, loading: false });
            })
            .catch(function (error) {
                this.setState({ errors: error });
            }.bind(this)); 
    }

    // Handle Delete request for an customer  
    handleDelete(id) {
        if (!window.confirm("Do you want to delete customer with Id: " + id))
            return;
        else {
            fetch('api/Customer/Delete/' + id, {
                method: 'delete'
            })
            .then((response) => {
                if (response.ok) {
                    this.setState(
                    {
                        customers: this.state.customers.filter((rec) => {
                            return (rec.id !== id);
                        })
                    });    
                }
                return response.json();
            })
            .then((responseJson) => {
                this.setState({ errors: this.handleErrors(responseJson) });
            })
            .catch(function (error) {
                this.setState({ errors: error });
            }.bind(this));                
        }
    }
    handleEdit(id) {
        this.props.history.push("/customer/edit/" + id);
    }  
    getInfo(searchString) {
        this.getCustomers(searchString);
    }
    renderCustomersTable (customers) {
        return (
            <div>                
                <Search func={this.getInfo} />  <br/>
                
                <table className='table table-striped'>
                    <thead>
                        <tr>
                        <th>First Name</th>
                        <th>Last Name</th>
                        <th>Date of birth</th>            
                        </tr>
                    </thead>
                    <tbody>
                        {customers.map(customer =>
                        <tr key={customer.id}>
                            <td>{customer.firstName}</td>
                            <td>{customer.lastName}</td>
                            <td>{moment(customer.dateOfBirth).format('LL')}</td>              
                            <td>
                                <a className="action" onClick={(id) => this.handleEdit(customer.id)}>Edit</a>  |
                                <a className="action" onClick={(id) => this.handleDelete(customer.id)}>Delete</a>
                            </td>  
                        </tr>
                        )}
                    </tbody>
                </table>
            </div>
    );
  }

  render () {
    let contents = this.state.loading
        ? <p><em>Loading...</em></p>        
        : (this.renderCustomersTable(this.state.customers));

    return (
      <div>
        <h1>Customers</h1>
        <p>Below is the list of available customers</p>
        <p>
            <Link to="/add-customer">Create New</Link>
        </p>  
        {contents}
      </div>
    );
  }
}

