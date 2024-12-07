<template>
    <div class="submit-wrapper w-full flex justify-center h-[100vh] px-4 py-3 flex-col items-center">
        <div v-if="isSubmitted">Bạn đã gửi bình chọn</div>
        <v-form v-else v-model="isFormValid" class="h-full" ref="formRef">
            <div ref="submitTitleRef" class="submit-title mb-3">
                <div class="text-lg font-semibold">Bình chọn: {{ voteEntity?.voteTitle }}</div>
                <div v-if="emailEntity">
                    Email: {{ emailEntity.emailAddress }}
                </div>
                <div v-else class="mt-2">
                    <v-text-field :rules="ruleValidateSubmitterName" v-model="submitModel.submitterName"
                        placeholder="Nhập họ và tên" clearable></v-text-field>
                </div>
            </div>
            <div ref="submitContentRef"
                class="submit-content px-3 py-2 rounded-md border border-[#ccc] overflow-auto flex flex-col gap-y-5">
                <div v-for="(questionEntity, index) in questionEntities" :key="questionEntity.questionID">
                    <div class="flex items-start justify-start gap-x-2">
                        <div>{{ index + 1 }}, </div>
                        <div>
                            {{ questionEntity.questionTitle }}
                        </div>
                    </div>
                    <div class="ml-2">
                        <div v-if="questionEntity.isMultiAnswer" :rules="[v => 'Chọn một đáp án']">
                            <div v-for="(answer, index) in questionEntity?.answers ?? []" :key="answer.answerID">
                                <div class="flex items-start justify-start submit-checkbox">
                                    <div class="flex items-center justify-between w-full">
                                        <v-checkbox :hide-details="index != questionEntity?.answers?.length - 1"
                                            :label="answer.answerContent" :value="answer.answerID"
                                            v-model="submitModel.resultDetails[questionEntity.questionID]"
                                            :rules="[v => !!v?.length || 'Chọn ít nhất một đáp án']">
                                        </v-checkbox>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <div v-else>
                            <v-radio-group :rules="[v => !!v || 'Chọn một đáp án']"
                                v-model="submitModel.resultDetails[questionEntity.questionID]">
                                <div v-for="answer in questionEntity?.answers ?? []" :key="answer.answerID">
                                    <div class="flex items-start justify-start">
                                        <v-radio :label="answer.answerContent" :value="answer.answerID">
                                        </v-radio>
                                    </div>
                                </div>
                            </v-radio-group>
                        </div>
                        <div v-if="questionEntity.isAnswerAddable">
                            <v-tooltip text="Thêm mới câu trả lời">
                                <template v-slot:activator="{ props }">
                                    <div @click="addNewAnswer(questionEntity)" v-bind="props"
                                        class="cursor-pointer w-8 h-8 flex items-center justify-center rounded-full border border-[#ccc] ml-2 mt-1">
                                        <i class="fa-solid fa-plus"></i>
                                    </div>
                                </template>
                            </v-tooltip>
                        </div>
                    </div>
                </div>
            </div>
            <div class="w-full flex gap-x-5 mt-3 justify-end  items-center">
                <countdown-timer :isStoped="false" v-model="leftTime" v-if="voteEntity && !isDisableSubmit"
                    :startTime="voteEntity?.createdDate" :durationMinutes="voteEntity?.voteTime"></countdown-timer>
                <v-btn :disabled="isDisableSubmit" color="primary normal-case" @click="submitForm">Hoàn thành</v-btn>
            </div>
        </v-form>
    </div>
</template>

<script>

import voteApi from '@/api/voteApi';
import { onMounted, ref, onBeforeMount, computed } from 'vue';
import { useRoute } from 'vue-router';
import { requireStringRule } from '@/until/ruleValidate';
import AddNewAnswer from '@/popup/AddNewAnswer.vue';
import commonFunction from '@/until/commonFunction';
import { uuid } from 'vue-uuid';
import answerApi from '@/api/answerApi';
import { object } from 'yup';
import { useStore } from 'vuex';
import resultApi from '@/api/resultApi';
import CountdownTimer from '@/components/CountdownTimer.vue';

export default {
    name: 'SubmitPage',
    components: { CountdownTimer },
    setup() {
        const store = useStore();
        const router = useRoute();
        const routeParam = ref({
            voteID: null,
            emailID: null
        });

        const formRef = ref(null);
        const isFormValid = ref(null);
        const voteEntity = ref(null);
        const emailEntity = ref(null);
        const questionEntities = ref(null);
        const submitTitleRef = ref(null)
        const submitContentRef = ref(null);
        const submitModel = ref({
            submitterName: null,
            resultDetails: {}
        });

        const submitedAnonymous = ref(false);

        const leftTime = ref(null);
        routeParam.value = {
            emailID: router.query.emailID,
            voteID: router.params.id,
        };

        function initSubmitModel() {
            const resultDetails = {};
            questionEntities.value.forEach(q => {
                resultDetails[q.questionID] = q.isMultiAnswer ? [] : null
            });


            submitModel.value = {
                submitterName: null,
                resultDetails: resultDetails,
            }
        }

        function initUI() {
            const titleHeight = submitTitleRef.value.clientHeight;
            submitContentRef.value.style.height = (window.innerHeight - titleHeight - 100) + 'px'
            // submitContentRef.value.style.height = '10px'

        }

        async function submitForm() {
            await formRef.value.validate();
            // nếu không có lỗi
            if (isFormValid.value) {
                // đoạn này để submit các câu trả lời tự thêm
                let newAnswers = []
                Object.entries(submitModel.value.resultDetails).forEach(([questionID, value]) => {
                    const questionEntity = questionEntities.value.filter(q => q.questionID == questionID)[0];
                    if (!questionEntity.isAnswerAddable) {
                        return;
                    }

                    let listAddNewAnswer = questionEntity.answers.filter(a => a.isAddNew);

                    listAddNewAnswer = listAddNewAnswer.filter(aNew => {
                        if (Array.isArray(value)) {
                            return value.includes(aNew.answerID);
                        }

                        return value == aNew.answerID;
                    });
                    newAnswers = [...newAnswers, ...listAddNewAnswer]
                });

                store.commit('common/setIsLoading', true);

                await answerApi.createMultiAsync(newAnswers);

                const resultDto = {
                    submitterName: submitModel.value.submitterName,
                    voteID: voteEntity.value.voteID,
                    emailID: emailEntity.value?.emailID,
                };

                let resultDetails = [];

                Object.values(submitModel.value.resultDetails).forEach(value => {
                    if (Array.isArray(value)) {
                        resultDetails = [...resultDetails, ...value.map(v => ({ answerID: v }))];
                    } else {
                        resultDetails.push({
                            answerID: value
                        });
                    }
                });

                resultDto.resultDetails = resultDetails;

                await resultApi.createAsync(resultDto);

                store.commit('common/setIsLoading', false);


                voteEntity.value = await voteApi.getEntireAsync(routeParam.value.voteID);

                submitedAnonymous.value = true;

            }
        }

        async function addNewAnswer(questionEntity) {
            const result = await commonFunction.showPopup({ component: AddNewAnswer, metaData: { questionEntity } })

            if (result) {
                questionEntity.answers.push({
                    isCustom: true,
                    isAddNew: true,
                    answerContent: result,
                    questionID: questionEntity.questionID,
                    resultDetails: [],
                    answerID: uuid.v1()
                });
            }
        }

        const isDisableSubmit = computed(() => {
            if (!voteEntity?.value) {
                return true;
            }

            if (leftTime.value == 0) {
                return true;
            }

            if (voteEntity.value?.voteStatus == 2) {
                return true;
            }


            if (voteEntity.value.totalJoiner <= voteEntity.value?.results?.length) {
                return true;
            }


            return false;

        });


        const isSubmitted = computed(() => {
            const emailID = routeParam.value?.emailID;
            if (!emailID) {
                return submitedAnonymous.value;
            }

            return voteEntity.value?.results?.find(r => r.emailID == emailID) != null;


        });

        onBeforeMount(async () => {
            voteEntity.value = await voteApi.getEntireAsync(routeParam.value.voteID);
            if (routeParam.value.emailID) {
                emailEntity.value = voteEntity.value.emails.filter(e => e.emailID == routeParam.value.emailID)[0];
            }
            questionEntities.value = voteEntity.value.questions;

            initSubmitModel();
        });


        onMounted(() => {
            initUI();
        })

        const ruleValidateSubmitterName = [
            (v) => !voteEntity.value?.isRequireName || (!!v?.trim()?.length) || "Không được để trống"
        ]

        return {
            routeParam,
            voteEntity,
            emailEntity,
            questionEntities,
            ruleValidateSubmitterName,
            submitModel,
            submitForm,
            formRef,
            isFormValid,
            addNewAnswer,
            requireStringRule,
            submitTitleRef,
            submitContentRef,
            leftTime,
            isDisableSubmit,
            isSubmitted
        }
    },
};
</script>

<style scoped>
.submit-content,
.submit-title {
    width: 600px;
}

.submit-checkbox ::v-deep(.v-messages__message) {
    margin-left: 16px;
}

@media screen and (max-width: 768px) {

    .submit-content,
    .submit-title {
        width: 350px;
    }

}
</style>
