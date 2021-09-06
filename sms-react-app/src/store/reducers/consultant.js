import { ACTION_TYPES } from "../actions/actionsTypes";
const initialState = {
    consultantsList: []
}

export const consultant = (state = initialState, action) => {
    switch (action.type) {
        case ACTION_TYPES.FETCH_ALL_CONSULTANTS:
            return {
                ...state,
                consultantsList: [...action.payload]
            }
        case ACTION_TYPES.CREATE_CONSULTANT:
            return {
                ...state,
                consultantsList: [...state.consultantsList, action.payload]
            }
        case ACTION_TYPES.UPDATE_CONSULTANT:
            return {
                ...state,
                consultantsList: state.consultantsList.map(x => x.id === action.payload.id ? action.payload : x)
            }
        case ACTION_TYPES.DELETE_CONSULTANT:
            return {
                ...state,
                consultantsList: state.consultantsList.filter(x => x.id !== action.payload)
            }
        default:
            return state;
    }
}