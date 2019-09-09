import React, { Component, useState} from 'react';
import PropTypes from 'prop-types';
import { Input, Button, List, Container} from 'semantic-ui-react';
import config from '.././.config.secret';
import * as searchAction from '../actions/SearchAction';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

//if search button is clicked, action creates event which reducer handles
export class SearchResultsList extends Component {
  
  constructor(props) {
    super(props);
  }
  componentDidMount() {
    
  }

  render() {
    const { results } = this.props;
    return (
      <div>
        <Container>
          <List>
            {results.map((thing) =>(<List.Item>{thing.firstName} {thing.lastName}</List.Item>))}
          </List>
        </Container>
      </div>
    );
  }

};


const mapStateToProps = state => ({
    results: state.searchReducer.results
});

const mapDispatchToProps = dispatch => ({
    action: bindActionCreators(searchAction, dispatch)
});

Search.propTypes = {
    results: PropTypes.array,
    action: PropTypes.object.isRequired
};

export default connect(mapStateToProps, mapDispatchToProps)(SearchResultsContainer);
