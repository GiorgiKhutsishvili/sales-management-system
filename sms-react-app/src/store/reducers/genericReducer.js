import { ACTION_TYPES } from "../actions/actionsTypes";
const initialState = {
    consultants: [],
    sales: [],
    products: []
}

export const genericReducer = (state = initialState, action) => {
    switch (action.type) {
        case ACTION_TYPES.FETCH_ALL_CONSULTANTS:
            return {
                ...state,
                consultants: [...action.payload]
            }
        case ACTION_TYPES.FETCH_ALL_SALES:
            return {
                ...state,
                sales: [...action.payload]
            }
        case ACTION_TYPES.FETCH_ALL_PRODUCTS:
            return {
                ...state,
                products: [...action.payload]
            }
        case ACTION_TYPES.CREATE:
            return {
                ...state,
                list: [...state.list, action.payload]
            }
        case ACTION_TYPES.UPDATE:
            return {
                ...state,
                list: state.list.map(x => x.id === action.payload.id ? action.payload : x)
            }
        case ACTION_TYPES.DELETE:
            return {
                ...state,
                list: state.list.filter(x => x.id !== action.payload)
            }
        default:
            return state;
    }
}