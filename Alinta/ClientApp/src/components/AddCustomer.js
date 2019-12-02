import * as React from 'react';
import * as moment from 'moment';
import { Errors } from './Errors';
import { handleErrors } from './Utils.js';

export class AddCustomer extends React.Component {
    constructor(props) {
        super(props);
        this.state = { title: "", loading: true, customers: [], errors: [] };
        
        var id = this.props.match.params["id"];
        // This will set state for Edit customer  
        if (id > 0) {
            fetch('api/Customer/Details/' + id)
                .then(response => response.json())
                .then(data => {
                    data.dateOfBirth = moment(data.dateOfBirth).format('YYYY-MM-DD');                    
                    this.setState({ title: "Edit", loading: false, customers: data });
                });
        }
        // This will set state for Add Customer  
        else {
            this.state = { title: "Create", loading: false, customers: [] };
        }
        // This binding is necessary to make "this" work in the callback  
        this.handleSave = this.handleSave.bind(this);
        this.handleCancel = this.handleCancel.bind(this);
        this.handleErrors = handleErrors.bind(this);        
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderCreateForm();
        return <div>
            <h1>{this.state.title}</h1>
            <h3>Customer</h3>
            <hr />
            {contents}
        </div>;
    }
    // This will handle the submit form event.  
    handleSave(event) {
        event.preventDefault();        
        const data = new FormData(event.target);
        // PUT request for Edit customer.  
        if (this.state.customers.id) {
            var id = this.state.customers.id;
            fetch('api/Customer/Edit/'+id, {
                method: 'PUT',
                body: data,
            })
            .then((response) => {
                if (response.ok) {
                    this.props.history.push("/");
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
        // POST request for Add customer.  
        else {
            fetch('api/Customer/Create', {
                method: 'POST',
                body: data,
            })                        
            .then((response) => {
                if (response.ok) {
                    this.props.history.push("/");                    
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

    
    // This will handle Cancel button click event.  
    handleCancel(e) {
        e.preventDefault();
        this.props.history.push("/");
    }
    // Returns the HTML Form to the render() method.  
    renderCreateForm() {
        return (
            <form onSubmit={this.handleSave} >                    
                <Errors errors={this.state.errors} />
                
                <div className="form-group row" >
                    <input type="hidden" name="id" value={this.state.customers.id} />
                </div>
                < div className="form-group row" >
                    <label className=" control-label col-md-12" htmlFor="Name">First Name</label>
                    <div className="col-md-4">
                        <input className="form-control" type="text" name="firstName" defaultValue={this.state.customers.firstName} required onChange={(evt) => this.setState({ firstName: evt.target.value })} />
                    </div>                    
                </div >
                < div className="form-group row" >
                    <label className=" control-label col-md-12" htmlFor="Name">Last Name</label>
                    <div className="col-md-4">
                        <input className="form-control" type="text" name="lastName" defaultValue={this.state.customers.lastName} required onChange={(evt) => this.setState({ lastName: evt.target.value })}/>
                    </div>                    
                </div >
                < div className="form-group row" >
                    <label className=" control-label col-md-12" htmlFor="Name">Date of birth</label>
                    <div className="col-md-4">
                        <input className="form-control" type="date" name="dateOfBirth" defaultValue={this.state.customers.dateOfBirth} required onChange={(evt) => this.setState({ dateOfBirth: evt.target.value })} />
                    </div>                    
                </div >
                <div className="form-group">
                    <button type="submit" className="btn btn-primary">Save</button>
                    <button className="btn" onClick={this.handleCancel}>Cancel</button>
                </div >
            </form >
        )
    }
}