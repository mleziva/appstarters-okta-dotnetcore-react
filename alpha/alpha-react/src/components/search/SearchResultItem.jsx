import React from 'react'
import PropTypes from 'prop-types'
import SearchResult from './SearchResult'
import { Segment} from 'semantic-ui-react';

const SearchResultItem = ({ searchResult, onActionToPerform }) => (
  <div style={{ marginBottom: 20 }}>
    <Segment>
    <SearchResult
      title={searchResult.title}
      price={searchResult.price}
      quantity={searchResult.inventory} />
    <button
      onClick={onActionToPerform}
      disabled={searchResult.inventory > 0 ? '' : 'disabled'}>
      {searchResult.inventory > 0 ? 'Add to cart' : 'Sold Out'}
    </button>
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