import { ACTION_TYPES } from "../actions/actionsTypes";
const initialState = {
    loading: false
}

export const globalLoader = (state = initialState, action) => {
    switch (action.type) {
        case ACTION_TYPES.USE_GLOBAL_LOADER:
            return {
                ...state,
                loading: action.payload
            }
        default:
            return state;
    }
}