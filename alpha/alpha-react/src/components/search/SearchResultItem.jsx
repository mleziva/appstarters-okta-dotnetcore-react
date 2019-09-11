import React from 'react'
import PropTypes from 'prop-types'
import SearchResult from './SearchResult'
import { Segment, Button, Grid} from 'semantic-ui-react';

const SearchResultItem = ({ searchResult, onActionToPerform }) => (
  <div style={{ marginBottom: 20 }}>
    <Segment>
      <Grid columns={2}>
          <Grid.Column width={12}>
          <SearchResult
      title={searchResult.title}
      price={searchResult.price}
      quantity={searchResult.inventory} />
          </Grid.Column>
          <Grid.Column width={4} className="search-result-item btn-grid">
          <Button 
      onClick={onActionToPerform}
      disabled={searchResult.inventory > 0 ? '' : 'disabled'}>
      {searchResult.inventory > 0 ? 'Add to cart' : 'Sold Out'}
    </Button>
            </Grid.Column>
      </Grid>
    </Segment>
  </div>
)

SearchResultItem.propTypes = {
  searchResult: PropTypes.shape({
    title: PropTypes.string.isRequired,
    price: PropTypes.number.isRequired,
    inventory: PropTypes.number.isRequired
  }).isRequired,
  actionToPerform: PropTypes.func.isRequired
}

export default SearchResultItem