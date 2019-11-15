/*
Flow for loading data
componentDidMount calls action
Action calls API
API reponse triggers reducer
Page is updated
*/
import * as ActionType from './ActionType';
//import SearchApi from '../api/SearchApi';
import SearchApi from '../debug/SearchApi';
import { ApiCallBeginAction, ApiCallErrorAction } from './ApiAction.js';


//todo: determine where to store current page
export const getSearchResponse = results => ({
    type: ActionType.GET_SEARCH_RESPONSE,
    results
});



export function getSearchAction(query, top, skip) {
    return (dispatch) => {

        dispatch(ApiCallBeginAction());

        return SearchApi.getSearch(query, top, skip)
            .then(results => {
                dispatch(getSearchResponse(results));
            }).catch(error => {
                throw error;
            });
    };
}