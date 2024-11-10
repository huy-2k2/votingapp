<template>
  <v-app>
    <screen-loading></screen-loading>
    <list-dialog></list-dialog>
    <list-popup></list-popup>
    <router-view></router-view>
  </v-app>

</template>

<script>
import ScreenLoading from '@/components/ScreenLoading.vue';
import ListDialog from '@/components/ListDialog.vue';
import ListPopup from './components/ListPopup.vue';
export default {
  name: 'App',

  components: {
    ScreenLoading,
    ListDialog,
    ListPopup
  },

  data: () => ({
    //
  }),
}
</script>
