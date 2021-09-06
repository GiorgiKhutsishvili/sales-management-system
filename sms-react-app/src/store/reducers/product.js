import { ACTION_TYPES } from "../actions/actionsTypes";
const initialState = {
    productsList: []
}

export const product = (state = initialState, action) => {
    switch (action.type) {
        case ACTION_TYPES.FETCH_ALL_PRODUCTS:
            return {
                ...state,
                productsList: [...action.payload]
            }
        case ACTION_TYPES.CREATE_PRODUCT:
            return {
                ...state,
                productsList: [...state.productsList, action.payload]
            }
        case ACTION_TYPES.UPDATE_PRODUCT:
            return {
                ...state,
                productsList: state.productsList.map(x => x.id === action.payload.id ? action.payload : x)
            }
        case ACTION_TYPES.DELETE_PRODUCT:
            return {
                ...state,
                productsList: state.productsList.filter(x => x.id !== action.payload)
            }
        default:
            return state;
    }
}