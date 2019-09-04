import { withAuth } from '@okta/okta-react';
import React, { Component } from 'react';
import { Input} from 'semantic-ui-react';

import config from '.././.config.secret';

export default withAuth(class Search extends Component {
  constructor(props) {
    super(props);
    this.state = { returnedValues: null, failed: null, resourceUrl: config.resourceServer.apiSampleUrl };
  }

  render() {
    return (
      <div>
        <Input placeholder='Search...' />
      </div>
    );
  }
});
