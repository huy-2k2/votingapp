<template>
  <div class="max-h-[400px]">
    <v-data-table :search="textSearch" :headers="headers" @click:row="handleClick" :items="items"
      :height="`calc(100vh - 180px)`" class="table">
      <template v-slot:top>
        <v-toolbar flat>
          <v-toolbar-title>Dánh sách cuộc bình chọn</v-toolbar-title>
          <v-spacer></v-spacer>
          <v-text-field v-model="textSearch" append-icon="mdi-magnify" label="Tìm kiếm" single-line
            hide-details></v-text-field>
        </v-toolbar>
      </template>

      <template v-slot:[`item.isRequireName`]="{ item }">
        <div v-if="item.isRequireName" class="cell-check">
          <i class="fa-solid fa-check"></i>
        </div>
        <div v-else class="cell-uncheck">
          <i class="fa-solid fa-xmark"></i>
        </div>
      </template>
      <template v-slot:[`item.isSendQrEmail`]="{ item }">
        <div v-if="item.isSendQrEmail" class="cell-check">
          <i class="fa-solid fa-check"></i>
        </div>
        <div v-else class="cell-uncheck">
          <i class="fa-solid fa-xmark"></i>
        </div>
      </template>
    </v-data-table>
  </div>
</template>

<script>
import { computed, onMounted, ref, watch } from 'vue';
import commonFunction from '@/until/commonFunction';
import QrCodePopup from '@/popup/QrCodePopup'
import voteApi from '@/api/voteApi';
import { useRouter, useRoute } from 'vue-router';
import DetailVote from '@/popup/DetailVote.vue';
export default {
  name: 'VotingPage',
  components: {},
  setup() {
    const voteEntities = ref([])
    const textSearch = ref(null)
    const router = useRouter();
    const route = useRoute();

    function handleClick(e, { item }) {
      router.push(`/app/list?voteID=${item.voteID}`);
    }

    const headers = computed(() => {
      return [
        { title: 'Tiêu đề', key: 'voteTitle', align: 'start', },
        { title: 'Thời gian bắt đầu', key: 'createdDate', align: 'center', },
        { title: 'Thời gian bình chọn', key: 'voteTime', align: 'center', },
        { title: "Xác thực bằng email", key: 'isSendQrEmail', align: 'center', },
        { title: "Bắt buộc nhập tên", key: 'isRequireName', align: 'center', },
        { title: "Số người tham gia", key: "totalJoiner", align: 'center' },
      ]
    })

    const voteID = computed(() => {
      return route.query?.voteID
    })


    watch(() => voteID.value, () => {
      if (!voteID.value) return
      commonFunction.showPopup({ component: DetailVote, metaData: { voteID: voteID.value } })
    });

    onMounted(async () => {
      voteEntities.value = await voteApi.getListByUserId();
      if (voteID.value) {
        commonFunction.showPopup({ component: DetailVote, metaData: { voteID } })
      }


    })

    const items = computed(() => {
      return voteEntities.value.map(item => {
        return { ...item, createdDate: commonFunction.formatDateTimeIntl(item.createdDate) }
      })
    })



    return {
      headers,
      textSearch,
      items,
      handleClick
    }
  },
};
</script>

<style></style>
