<template>
    <div class="bg-white rounded-md px-5 py-3 w-[600px]">
        <h1 class="text-lg">{{ voteEntity.voteTitle }}</h1>
        <div class="flex items-center justify-start gap-x-4">
            <div class="">Số lượng câu hỏi: {{ voteEntity.questions?.length }}</div>
            <div v-show="voteEntity?.totalJoiner" class="">Số người tham gia: {{ voteEntity?.totalJoiner }}</div>
            <div class="flex items-center justify-start gap-x-1">
                <div>Yêu cầu nhập tên: </div>
                <div :class="`${voteEntity.isRequireName ? 'text-green-600' : 'text-red-600'} mb-[-1px]`">
                    <i v-if="voteEntity.isRequireName" class="fa-solid fa-check"></i>
                    <i v-else class="fa-solid fa-xmark"></i>
                </div>
            </div>
        </div>
        <div>
            <div>Tổng thời gian bình chọn: {{ voteEntity?.voteTime }} phút</div>
        </div>
        <div class="mt-5">
            <div class="flex items-center justify-center text-base">
                <countdown-timer v-model="leftTime" v-if="voteEntity.createdDate" :startTime="voteEntity.createdDate"
                    :durationMinutes="voteEntity.voteTime" :isStoped="voteEntity.voteStatus == 2"></countdown-timer>
            </div>
            <div class="text-center">{{ url }}</div>
            <div class="flex items-center justify-center">
                <v-tooltip text="Tải xuống mã QR">
                    <template v-slot:activator="{ props }">
                        <div class="relative">
                            <div v-show="voteEntity.voteStatus == 2 || leftTime == 0"
                                class="flex items-center justify-center absolute inset-0 qr-blur">
                                <div class="font-bold text-lg">Hết hiệu lực</div>
                            </div>
                            <canvas @click="downloadQRCode" class="cursor-pointer" v-bind="props"
                                ref="qrcodeCanvasRef"></canvas>
                        </div>
                    </template>
                </v-tooltip>
            </div>
        </div>
        <div class="flex justify-end gap-x-3 mt-5">
            <v-btn :disabled="voteEntity.voteStatus == 2 || leftTime == 0" @click="handleStop" class="normal-case">
                Dừng bình chọn
            </v-btn>
            <!-- <v-btn color="secondary" class="normal-case">Tải mã QR</v-btn> -->
            <v-btn color="primary" @click="handleClose" class="normal-case">Đóng</v-btn>
        </div>
    </div>
</template>

<script>
import voteApi from '@/api/voteApi';
import { onMounted, ref, onBeforeMount } from 'vue';
import { uuid } from 'vue-uuid';
import QRCode from 'qrcode'
import CountdownTimer from '@/components/CountdownTimer.vue';

export default {
    name: 'QrCodePopup',
    components: { CountdownTimer },
    props: {
        popup: {
            type: Object,
            required: true,
        }
    },
    setup(props) {

        const voteEntity = ref({});
        const qrcodeCanvasRef = ref(null);
        const url = ref(null);
        const leftTime = ref(null);

        async function generateQRCode() {
            try {
                const canvas = qrcodeCanvasRef.value
                url.value = `${window.location.origin}/submit/${voteEntity.value.voteID}`
                await QRCode.toCanvas(canvas, url.value, { width: 250 })
            } catch (error) {
                console.error('Lỗi khi sinh mã QR:', error)
            }
        }

        function downloadQRCode() {
            const canvas = qrcodeCanvasRef.value
            const link = document.createElement('a')
            link.href = canvas.toDataURL('image/png')
            link.download = 'qrcode.png'
            link.click()
        }

        function handleClose() {
            const resolveCallback = props.popup.resolve;
            props.popup.visible = false;
            resolveCallback("1")
        }

        async function getVoteEntity() {
            voteEntity.value = await voteApi.getEntireAsync(props.popup?.metaData?.voteID);
        }

        async function handleStop() {
            await voteApi.updateVoteStatusAsync(voteEntity.value.voteID, 2)
            await getVoteEntity();
        }

        onBeforeMount(async () => {
            await getVoteEntity();

        })

        onMounted(() => {
            setTimeout(() => {
                generateQRCode();
            }, 1000);
        })

        return {
            voteEntity,
            qrcodeCanvasRef,
            url,
            downloadQRCode,
            handleClose,
            handleStop,
            leftTime
        }
    },
};
</script>

<style scoped>
.qr-blur {
    background-color: #ccccccea;
}
</style>