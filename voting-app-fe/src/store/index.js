// store/index.js
import { createStore } from 'vuex';
import common from './common';
import addnew from './addnew';
const store = createStore({
  modules: {
    common,
    addnew
  },
});

export default store;
