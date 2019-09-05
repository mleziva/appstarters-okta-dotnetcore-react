import * as ActionType from '../actions/ActionType';
//import initialState from './initialState';
import _ from 'lodash';



const initialState= {searchReducer: {
    results: []
    }
}
const searchReducer = (state = initialState.searchReducer, action) => {
    switch(action.type) {
        case ActionType.GET_SEARCH_RESPONSE: {
            // '...' spread operator clones the state
            // lodash Object assign simply clones action.courses into a new array.
            // The return object is a copy of state and overwrites the state.courses with a fresh clone of action.courses
            return {
                ...state, 
                results: _.assign(action.results)
            };
        }


        default: { return state; }
    }
};



export default searchReducer;