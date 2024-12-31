const state = {
    user: null,
    token: null,
  };
  
  const getters = {
    isAuthenticated: (state) => !!state.token,
    getUser: (state) => state.user,
  };
  
  const actions = {
    login({ commit }, { user, token }) {
      commit('SET_USER', user);
      commit('SET_TOKEN', token);
    },
    logout({ commit }) {
      commit('SET_USER', null);
      commit('SET_TOKEN', null);
    },
  };
  
  const mutations = {
    SET_USER(state, user) {
      state.user = user;
    },
    SET_TOKEN(state, token) {
      state.token = token;
    },
  };
  
  export default {
    state,
    getters,
    actions,
    mutations,
  };
  