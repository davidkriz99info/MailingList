import React, { Component } from 'react';

export default class App extends Component {
    constructor(props) {
        super(props);

        this.state = {
            emailAddress: '',
            emailAddressError: '',
            firstName: '',
            firstNameError: '',
            lastName: '',
            lastNameError: '',
            isSuccess: false
        };

        this.handleEmailAddressChange = this.handleEmailAddressChange.bind(this);
        this.handleFirstNameChange = this.handleFirstNameChange.bind(this);
        this.handleLastNameChange = this.handleLastNameChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    handleSubmit(e) {
        e.preventDefault();

        const entry = {
            EmailAddress: this.state.emailAddress,
            FirstName: this.state.firstName,
            LastName: this.state.lastName
        };

        fetch('MailingList/AddEntry', {
            method: 'POST',
            body: JSON.stringify(entry),
            headers: {
                'Content-Type': 'application/json'
            }
        }).then(response => {
            if (response.status === 200) {
                this.setState({
                    emailAddress: '',
                    emailAddressError: '',
                    firstName: '',
                    firstNameError: '',
                    lastName: '',
                    lastNameError: '',
                    isSuccess: true
                });

                return;
            } else {
                return response.json();
            }
        }).then(errorsJson => {
            if (errorsJson && errorsJson.errors) {
                const errors = errorsJson.errors;

                this.setState({
                    emailAddressError: errors.emailAddress
                        ? errors.emailAddress[0]
                        : '',
                    firstNameError: errors.firstName
                        ? errors.firstName[0]
                        : '',
                    lastNameError: errors.lastName
                        ? errors.lastName[0]
                        : ''
                });
            }
        });
    }

    handleEmailAddressChange(e) {
        this.setState({
            emailAddress: e.target.value,
            emailAddressError: ''
        });
    }

    handleFirstNameChange(e) {
        this.setState({
            firstName: e.target.value,
            firstNameError: ''
        });
    }

    handleLastNameChange(e) {
        this.setState({
            lastName: e.target.value,
            lastNameError: ''
        });
    }

    render() {
        return (
            <div>
                <h1>Mailing List</h1>
                {
                    this.state.isSuccess
                        ? (
                            <div>
                                Your entry was successful.<br />
                            </div>
                        )
                        : (
                            <form onSubmit={this.handleSubmit}>
                                <h2>New entry</h2>
                                <div className="form-group">
                                    <label htmlFor="emailAddress">Email </label>
                                    <input type="email" className="form-control" onChange={this.handleEmailAddressChange} name="emailAddress" required />
                                    <span style={{ color: "red" }}>{this.state.emailAddressError}</span>
                                </div>
                                <div className="form-group">
                                    <label htmlFor="firstName">First name </label>
                                    <input type="text" className="form-control" onChange={this.handleFirstNameChange} name="firstName" required />
                                    <span style={{ color: "red" }}>{this.state.firstNameError}</span>
                                </div>
                                <div className="form-group">
                                    <label htmlFor="lastName">Last name </label>
                                    <input type="text" className="form-control" onChange={this.handleLastNameChange} name="lastName" required />
                                    <span style={{ color: "red" }}>{this.state.lastNameError}</span>
                                </div>
                                <button type="submit">Submit</button>
                            </form>
                        )
                }
            </div>
        );
    }
}
