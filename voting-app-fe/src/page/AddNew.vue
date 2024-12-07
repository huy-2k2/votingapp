<template>
    <div class="px-5 py-2 h-full flex flex-col">
        <step-by-step :isDone="isDone" :steps="steps" v-model="currentStepIndex"></step-by-step>
        <div class="step-item flex items-center justify-center" v-show="currentStepIndex == 0">
            <add-new-step-one @submit="currentStepIndex = currentStepIndex + 1"></add-new-step-one>
        </div>
        <div class="step-item" v-show="currentStepIndex > 0">
            <add-new-step-two @submit="handleSubmitStepTwo"></add-new-step-two>
        </div>
    </div>
</template>

<script>
import StepByStep from '@/components/StepByStep.vue';
import AddNewStepOne from '@/components/AddNewStepOne.vue';
import AddNewStepTwo from '@/components/AddNewStepTwo.vue';
import { onMounted, ref } from 'vue';
import commonFunction from '@/until/commonFunction';
import { useRouter } from 'vue-router';
import QrCodePopup from '@/popup/QrCodePopup.vue';
import { useStore } from 'vuex';

export default {
    name: 'AddNew',
    components: { StepByStep, AddNewStepOne, AddNewStepTwo },
    setup() {

        const router = useRouter();
        const steps = ref([
            {
                title: 'Thiết lập bình xét',
            },
            {
                title: 'Nhập nội dung câu hỏi',
            }
        ]);
        const store = useStore();
        const isDone = ref(false);

        const currentStepIndex = ref(0);

        async function handleSubmitStepTwo(voteEntity) {
            currentStepIndex.value = currentStepIndex.value + 1;
            isDone.value = true;

            // nếu gửi qr vào email thì chỉ show thông báo
            if (voteEntity.isSendQrEmail) {
                await commonFunction.showDialog('Thông báo', 'Đường dẫn đến cuộc bình chọn đã được gửi vào Email', [{ text: 'Đóng', value: 1, color: 'primary' }])
            }
            // nếu không thì phải show form QR
            else {
                const qrPopupResult = await commonFunction.showPopup({ component: QrCodePopup, metaData: { voteID: voteEntity.voteID } });
            }
            store.commit('addnew/reset'),
                router.push(`/app/list?voteID=${voteEntity.voteID}`);
        }

        onMounted(() => {
        })

        return {
            steps,
            currentStepIndex,
            isDone,
            handleSubmitStepTwo
        }
    },
};
</script>

<style scoped>
.step-item {
    flex-grow: 1;
    padding-top: 20px;
}
</style>