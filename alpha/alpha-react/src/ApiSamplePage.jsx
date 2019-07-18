import { withAuth } from '@okta/okta-react';
import React, { Component } from 'react';
import { Header, Icon, Message, Table } from 'semantic-ui-react';

import config from './.config.secret';

export default withAuth(class Profile extends Component {
  constructor(props) {
    super(props);
    this.state = { returnedValues: null, failed: null, resourceUrl: config.resourceServer.apiSampleUrl };
  }

  componentDidMount() {
    this.getSampleApiCall();
  }

  async getSampleApiCall() {
    if (!this.state.returnedValues) {
      try {
        const accessToken = await this.props.auth.getAccessToken();
        /* global fetch */
        const response = await fetch(config.resourceServer.apiSampleUrl, {
          headers: {
            Authorization: `Bearer ${accessToken}`,
          },
        });

        if (response.status !== 200) {
          this.setState({ failed: true });
          return;
        }

        let index = 0;
        const returnedValues = await response.json();
        this.setState({ returnedValues, failed: false });
      } catch (err) {
        this.setState({ failed: true, error: err});
        /* eslint-disable no-console */
        console.error(err);
      }
    }
  }

  render() {
    return (
      <div>
        <Header as="h1"><Icon name="cogs" /> API Response</Header>
        {this.state.failed === true && <Message error header="Failed to fetch messages.  Error:" p={this.state.error} />}
        {this.state.failed === null && <p>Fetching Messages..</p>}
        {this.state.returnedValues &&
          <div>
            <p>This component makes a GET request to the resource server example, which must be running at <code>{this.state.resourceUrl}</code></p>
            <p>
              It attaches your current access token in the <code>Authorization</code> header on the request,
              and the resource server will attempt to authenticate this access token.
              If the token is valid the server will return a list of messages.  If the token is not valid
              or the resource server is incorrectly configured, you will see a 401 <code>Unauthorized response</code>.
            </p>
            <p>
              This route is protected with the <code>&lt;SecureRoute&gt;</code> component, which will
              ensure that this page cannot be accessed until you have authenticated and have an access token in local storage.
            </p>
            <p>API Response:</p>
            <p>{this.state.returnedValues}</p>
          </div>
        }
      </div>
    );
  }
});
