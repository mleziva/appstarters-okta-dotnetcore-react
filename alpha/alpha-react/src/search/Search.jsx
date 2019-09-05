import React, { Component} from 'react';
import PropTypes from 'prop-types';
import { Input} from 'semantic-ui-react';
import config from '.././.config.secret';
import * as searchAction from '../actions/SearchAction';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

export class Search extends Component {
  constructor(props) {
    super(props);
    this.state = { returnedValues: null, failed: null, resourceUrl: config.resourceServer.apiSampleUrl };

    
  }
  componentDidMount() {
    this.props.action.getSearchAction()
    .catch(error => {
        console.log(error);
        //toastr.error(error);
    });
}

  render() {
    const { results } = this.props;
    console.log("prior to logging results in jsx");
    console.log(results);
    return (
      <div>
        <Input placeholder='Search...' />
        <button text="Search" />
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



export default connect(mapStateToProps, mapDispatchToProps)(Search);
