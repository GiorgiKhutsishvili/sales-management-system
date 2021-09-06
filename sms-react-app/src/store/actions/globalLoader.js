import { ACTION_TYPES } from "./actionsTypes";

export const useGlobalLoader = (loading) => dispatch => {
    dispatch({
        type: ACTION_TYPES.USE_GLOBAL_LOADER,
        payload: loading
    });
}