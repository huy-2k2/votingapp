const state = () => {
    return {
        isLoading: false,
    }
}

const mutations = {
    setIsLoading(state, isLoading) {
        state.isLoading = isLoading;
    }
}

const getters = {
    isLoading(state) {
        return state.isLoading;
    }
}

export default {
    namespaced: true,
    state,
    mutations,
    getters,
}