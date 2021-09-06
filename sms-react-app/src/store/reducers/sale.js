import { ACTION_TYPES } from "../actions/actionsTypes";
const initialState = {
    salesList: []
}

export const sale = (state = initialState, action) => {
    switch (action.type) {
        case ACTION_TYPES.FETCH_ALL_SALES:
            return {
                ...state,
                salesList: [...action.payload]
            }
        case ACTION_TYPES.CREATE_SALE:
            return {
                ...state,
                salesList: [...state.salesList, action.payload]
            }
        case ACTION_TYPES.UPDATE_SALE:
            return {
                ...state,
                salesList: state.salesList.map(x => x.id === action.payload.id ? action.payload : x)
            }
        case ACTION_TYPES.DELETE_SALE:
            return {
                ...state,
                salesList: state.salesList.filter(x => x.id !== action.payload)
            }
        default:
            return state;
    }
}