<template>
  <div class="sidebar pl-3 pr-2 py-5">
    <router-link :to="item.route"
      class="sidebar-item flex items-center justify-start gap-x-3 cursor-pointer px-2 py-2 rounded-md"
      active-class="isActive" v-for="item in items" :key="item.id">
      <div class="w-8">
        <img :src="item.iconSrc" alt="">
      </div>
      <div class="text-lg font-medium">
        {{ item.text }}
      </div>
    </router-link>
  </div>
</template>
<script>
import { ref } from 'vue';

export default {
  name: 'App',
  components: {},
  setup() {
    const items = ref([
      {
        text: 'Danh sách',
        iconSrc: '/icon/listVoted.svg',
        id: 'listVote',
        route: '/list',
        isActive: true,
      },
      {
        text: 'Thêm mới',
        iconSrc: '/icon/addVote.svg',
        route: '/new',
        id: 'addNew'
      }
    ]);

    return {
      items,
    }
  },
};
</script>

<style>
.sidebar {
  display: flex;
  flex-direction: column;
  row-gap: 16px;
  border-right: 1px solid #ccc;
  height: 100%;
  box-shadow: 1px 0 2px 0 #0000002b;
}

.sidebar-item:hover,
.sidebar-item.isActive {
  background-color: rgba(32, 231, 231, 0.312);
}
</style>
