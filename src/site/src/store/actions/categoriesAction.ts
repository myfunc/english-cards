export function fetchCategories () {
    return async (dispatch: any) => {
        console.log("test22");
        dispatch(fetchCategoriesStart());
        const options = {
            method: "POST"
        }
        try {
             
            let response = await fetch("http://localhost:9999/api/getGroups", {...options});
            const result = response.json();
            result.then(categories => {
                dispatch(fetchCategoriesSuccess(categories.groups));
            })
        } catch(error) {
            dispatch(fetchCategoriesError(error));
        }
    }
}

export function fetchCategoriesStart () {
    return {
        type: "FETCH_CATEGORIES_START"
    }
}

export function fetchCategoriesSuccess (categories: any) {
    return {
        type: "FETCH_CATEGORIES_SUCCESS",
        categories
    }
}

export function fetchCategoriesError (error: any) {
    return {
        type: "FETCH_CATEGORIES_ERROR",
        error
    }
}