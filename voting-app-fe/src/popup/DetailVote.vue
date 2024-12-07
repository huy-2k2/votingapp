<template>
    <div class="bg-white rounded-md px-5 py-2 w-full">
        <div class="detail-title">
            <h1 class="text-lg">{{ voteEntity?.voteTitle }}</h1>
            <div class="flex items-center justify-start gap-x-5">
                <div>Số lượng câu hỏi: {{ chartdata?.length }}</div>
                <div>Số người đã trả lời: {{ voteEntity?.results?.length || 0 }}</div>
                <div v-if="voteEntity?.totalJoiner">Số người tham gia: {{ voteEntity?.totalJoiner }}</div>
            </div>
            <div v-if="voteEntity?.voteStatus != 2 && lefttime != 0" class="flex items-center justify-start gap-x-2">
                <div>Thời gian còn lại: </div>
                <countdown-timer v-model="lefttime" :isStoped="false" v-if="voteEntity"
                    :startTime="voteEntity?.createdDate" :durationMinutes="voteEntity?.voteTime"></countdown-timer>
            </div>
            <div class="mt-3">
            </div>
        </div>
        <div class="w-full gap-x-10 flex items-start justify-start px-10 py-2">
            <div v-for="(chart, index) in chartdata" :key="index" class="w-[300px]">
                <Pie :data="chart" :options="getChartOption(chart.questionID)" />
                <div class="text-center mt-3">{{ chart.title }}</div>
            </div>
        </div>
        <div class="flex justify-end mt-5 gap-x-4">
            <v-btn v-if="!voteEntity?.isSendQrEmail" color="secondary" class="normal-case" @click="showQrFormCode">Màn
                QR
                Code</v-btn>
            <v-btn color="primary " class="normal-case" @click="handleClose">Đóng</v-btn>
        </div>
    </div>
</template>

<script>
import { computed, onBeforeMount, ref } from 'vue';
import voteApi from '@/api/voteApi';
import { Pie } from 'vue-chartjs'
import { useRouter } from 'vue-router';
import CountdownTimer from '@/components/CountdownTimer.vue';
import commonFunction from '@/until/commonFunction';
import QrCodePopup from '@/popup/QrCodePopup.vue'
export default {
    name: 'DetailVote',
    components: { Pie, CountdownTimer },
    props: {
        popup: {
            type: Object,
            required: true,
        }
    },
    setup(props) {
        const voteID = ref(props.popup.metaData.voteID);
        const voteEntity = ref(null);
        const router = useRouter();
        const lefttime = ref(null);
        const option = ref({
            responsive: true,
            plugins: {
                legend: {
                    onClick: null, // Vô hiệu hóa sự kiện click trên legend
                },
                tooltip: {
                    enabled: true, // Hiển thị tooltip
                },
            },
            onClick: (event, elements) => {
                if (elements.length > 0 || voteEntity.value?.isRequireName || voteEntity.value?.isSendQrEmail) {
                    const index = elements[0].index;
                    console.log(index, event, elements, chartdata.value)
                }

            },
        })


        const chartdata = computed(() => {
            const questions = voteEntity.value?.questions;

            if (!questions) {
                return []
            }
            return questions.map((q) => {
                const qTitle = q.questionTitle;

                const labels = q.answers.map(a => a.answerContent);
                const labelsIds = q.answers.map(a => a.answerID);

                const data = q.answers.map(a => a.resultDetails.length);
                return {
                    labels,
                    datasets: [{ data }],
                    title: qTitle,
                    questionID: q.questionID,
                    labelsIds
                }
            })
        })

        const tabs = computed(() => {
            return [
                { value: 1, text: 'Tổng quan' },
                { value: 2, text: 'Chi tiết' },
            ]
        })

        function handleClose() {
            router.push('/app/list');
            const resolveCallback = props.popup.resolve;
            props.popup.visible = false;
            resolveCallback("1")
        }


        function getChartOption(questionID) {
            return {
                responsive: true,
                plugins: {
                    legend: {
                        onClick: null, // Vô hiệu hóa sự kiện click trên legend
                    },
                    tooltip: {
                        enabled: true, // Hiển thị tooltip
                    },
                },
                onClick: (_, elements) => handleClickOnPie(elements, questionID),
            }
        }

        onBeforeMount(async () => {
            voteEntity.value = await voteApi.getEntireAsync(voteID.value);
        })


        async function handleClickOnPie(elements, questionID) {
            if (elements.length > 0 || voteEntity.value?.isRequireName || voteEntity.value?.isSendQrEmail) {
                const index = elements[0]?.index;
                console.log(index)
                if (index === null || index === undefined)
                    return;
                const chart = chartdata.value.filter(x => x.questionID == questionID)[0];

                const answerID = chart.labelsIds[index]

                const results = voteEntity.value?.results?.filter(r => r.resultDetails.some(rd => rd.answerID == answerID)) || [];

                let submitterName = [];

                if (voteEntity.value?.isSendQrEmail) {
                    submitterName = voteEntity.value?.emails.filter(e => results.some(r => r.emailID == e.emailID)).map(e => e.emailAddress);
                } else {
                    submitterName = results.map(r => r.submitterName)
                }

                await commonFunction.showDialog('Danh sách người đã chọn câu trả lời', submitterName.join(", "), [{ text: 'Đóng', value: 1, color: 'primary' }])

            }

        };

        async function showQrFormCode() {
            await commonFunction.showPopup({ component: QrCodePopup, metaData: { voteID: voteEntity.value.voteID } });
            voteEntity.value = await voteApi.getEntireAsync(voteID.value);
        }

        return {
            voteID,
            option,
            chartdata,
            handleClose,
            voteEntity,
            tabs,
            getChartOption,
            lefttime,
            showQrFormCode
        }
    },
};
</script>

<style scoped></style>