import React, { Component, useState} from 'react';
import PropTypes from 'prop-types';
import { Input, Button, List, Container} from 'semantic-ui-react';
import config from '.././.config.secret';
import * as searchAction from '../actions/SearchAction';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

export class Search extends Component {
  
  constructor(props) {
    super(props);
    this.state = {
      searhValue: ''
    };
  }
  componentDidMount() {
    console.log(this.props);
    this.props.action.getSearchAction()
    .catch(error => {
        console.log(error);
        //toastr.error(error);
    });
}


  render() {
    const { results } = this.props;
    return (
      <div>
        <SearchArea history={this.props.history} location={this.props.location}/>
        <Container>
          <List>
            {results.map((thing) =>(<List.Item>{thing.firstName} {thing.lastName}</List.Item>))}
          </List>
        </Container>
      </div>
    );
  }

  

};

function SearchArea(props) {
  const [search, setSearch] = useState('');
  const handleSearch = (event) => {
    //do updating the url here and execute search api request
    //if search is the same do nothing
    let queryString = "?q="+search;
    console.log(props);
    if(props.location.search !== queryString){
      props.history.push("/search"+queryString);
    }
  };
  return (
    <div>
      <p>You searched for {search}</p>
      <Input placeholder='What are you looking for...' onInput={e => setSearch(e.target.value)}  />
      <Button primary onClick={handleSearch}>Search</Button>
    </div>
  );
}

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
