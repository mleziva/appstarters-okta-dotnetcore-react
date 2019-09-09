import React from 'react'
import PropTypes from 'prop-types'
import SearchResult from './SearchResult'

const SearchResultItem = ({ searchResult, onActionToPerform }) => (
  <div style={{ marginBottom: 20 }}>
    <SearchResult
      title={searchResult.title}
      price={searchResult.price}
      quantity={searchResult.inventory} />
    <button
      onClick={onActionToPerform}
      disabled={searchResult.inventory > 0 ? '' : 'disabled'}>
      {searchResult.inventory > 0 ? 'Add to cart' : 'Sold Out'}
    </button>
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