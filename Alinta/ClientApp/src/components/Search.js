import React, { Component } from 'react'

export class Search extends Component {    
    state = {
        query: '',
    }

    handleClick = () => {
        this.setState({
            query: this.search.value
        }, () => {
            this.props.func(this.state.query);
        });
    }

    render() {
        return (
            <form>
                <input
                    placeholder="Search for..."
                    ref={input => this.search = input}                    
                />
                <input id="submit" type="button" onClick={this.handleClick} value="Submit"/>               
            </form>
        )
    }
}