import React from 'react';

export class Errors extends React.Component {
    render() {
        const errors = this.props.errors || [];

        if (errors.length > 0) {
            return (
                <div className="alert alert-danger">
                    <ul>
                        {errors.map((e, i) => <li key={i}>{e}</li>)}
                    </ul>
                </div>
            );
        }     
        else return <div />
    }
}
