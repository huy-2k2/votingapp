<template>
    <div class="h-full flex items-stretch justify-start gap-x-10 ">
        <v-form class="w-[600px] px-5 py-4 rounded-md border border-[#ccc] flex flex-col flex-shrink-0" ref="formRef"
            v-model="isFormAddQuestionValid">
            <div>
                <v-text-field :rules="requireStringRule" height="40px" v-model="questionTitle"
                    placeholder="Nội dung câu hỏi" clearable></v-text-field>
                <v-checkbox v-model="isMultiAnswer" label="Cho phép chọn nhiều câu trả lời"></v-checkbox>
                <v-checkbox v-model="isAnswerAddable" label="Cho phép tự thêm câu trả lời"></v-checkbox>
                <div class="flex items-stretch justify-between gap-x-2">
                    <v-text-field :rules="validateAnswerRule" ref="answerContentRef" v-model="answerContent"
                        placeholder="Nội dung câu trả lời" clearable></v-text-field>

                    <v-btn color="primary" class="normal-case" height="56px" :disabled="!answerContent?.trim()?.length"
                        @click="addAnswer">Thêm
                        mới</v-btn>
                </div>
                <div class="flex flex-col gap-y-3 answer-list overflow-y-auto">
                    <div class="answer-item border border-[#ccc] px-3 py-1 rounded-md flex items-center justify-between gap-x-2"
                        v-for="(answer, index) in listAnswer" :key="answer">
                        <div>{{ answer }}</div>
                        <v-tooltip text="Xóa câu trả lời">
                            <template v-slot:activator="{ props }">
                                <div v-bind="props" @click="handleRemoveAnswer(index)"
                                    class="answer-item-remove  w-6 flex-shrink-0 cursor-pointer">
                                    <img :src="require('@/assets/icon/close.svg')" alt="">
                                </div>
                            </template>
                        </v-tooltip>
                    </div>
                </div>
            </div>
            <div class="mt-2  flex-grow flex items-end justify-end gap-x-3">
                <v-btn v-show="questionId" color="secondary normal-case" @click="handleDiscardEdit">Hủy</v-btn>
                <v-btn @click="submitQuestion" color="primary" class="mt-auto normal-case">{{ questionId ? 'Sửa câu hỏi'
                    :
                    'Thêm mới câu hỏi'
                    }}</v-btn>
            </div>
        </v-form>
        <div class="flex flex-col flex-grow">
            <div class="flex flex-col question-list gap-y-4 items-stretch">
                <div class="px-2 py-1 border border-[#ccc] rounded-md question-item"
                    :class="[{ 'bg-red-100': questionId == question.id }]" v-for="(question) in listQuestion"
                    :key="question.questionTitle">
                    <v-tooltip :text="question.questionTitle">
                        <template v-slot:activator="{ props }">
                            <div v-bind="props" class="">
                                {{ question.questionTitle }}
                            </div>
                        </template>
                    </v-tooltip>
                    <div class="flex items-center justify-start gap-x-6 text-sm">
                        <div class="flex items-center justify-start gap-x-1">
                            <div>Chọn nhiều đáp án: </div>
                            <div :class="`${question.isMultiAnswer ? 'text-green-600' : 'text-red-600'} mb-[-1px]`">
                                <i v-if="question.isMultiAnswer" class="fa-solid fa-check"></i>
                                <i v-else class="fa-solid fa-xmark"></i>
                            </div>
                        </div>
                        <div class="flex items-center justify-start gap-x-1">
                            <div>Tự thêm đáp án: </div>
                            <div :class="`${question.isAnswerAddable ? 'text-green-600' : 'text-red-600'} mb-[-1px]`">
                                <i v-if="question.isAnswerAddable" class="fa-solid fa-check"></i>
                                <i v-else class="fa-solid fa-xmark"></i>
                            </div>
                        </div>
                        <div class="flex items-center justify-start gap-x-1">
                            <div>Số lượng đáp án: </div>
                            <div>{{ question.answers.length }}</div>
                        </div>
                        <div class="flex question-btn items-center justify-end gap-x-2 flex-grow opacity-0">
                            <v-btn color="secondary" @click="handleEditQuestion(question.id)"
                                class="normal-case custom-btn">Sửa</v-btn>
                            <v-btn color="red" @click="handleRemoveQuestion(question.id)"
                                class="normal-case custom-btn">Xóa</v-btn>
                        </div>
                    </div>
                </div>
            </div>
            <div class="flex-grow flex justify-end items-end">
                <v-btn :disabled="!listQuestion?.length" @click="handleSubmitVote"
                    color="primary normal-case mb-[12px]">Hoàn
                    thành</v-btn>
            </div>
        </div>
    </div>
</template>

<script>
import { computed, ref } from 'vue';
import { useStore } from 'vuex';
import { requireStringRule } from '@/until/ruleValidate'
import { uuid } from 'vue-uuid';
import voteApi from '@/api/voteApi';
export default {
    name: 'AddNewStepTwo',
    emits: ['submit'],
    components: {},
    props: {

    },
    setup(props, { emit }) {
        const store = useStore();
        const answerContent = ref("");
        const answerContentRef = ref(null);
        const isFormAddQuestionValid = ref(null);
        const formRef = ref(null);
        const questionTitle = computed({
            get: () => store.getters['addnew/stepTwo'].questionTitle,
            set: (value) => store.commit('addnew/setStepTwo', { field: 'questionTitle', value }),
        });

        const isMultiAnswer = computed({
            get: () => store.getters['addnew/stepTwo'].isMultiAnswer,
            set: (value) => store.commit('addnew/setStepTwo', { field: 'isMultiAnswer', value }),
        });

        const isAnswerAddable = computed({
            get: () => store.getters['addnew/stepTwo'].isAnswerAddable,
            set: (value) => store.commit('addnew/setStepTwo', { field: 'isAnswerAddable', value }),
        });

        const listAnswer = computed({
            get: () => store.getters['addnew/stepTwo'].answers,
            set: (value) => store.commit('addnew/setStepTwo', { field: 'answers', value }),
        });

        const listQuestion = computed({
            get: () => store.getters['addnew/questions'],
            set: (value) => store.commit('addnew/setQuestions', value),
        });

        const questionId = computed({
            get: () => store.getters['addnew/stepTwo'].id,
            set: (value) => store.commit('addnew/setStepTwo', { field: 'id', value }),
        });

        function addAnswer() {
            listAnswer.value.unshift(answerContent.value);
            answerContent.value = "";
            answerContentRef.value.focus();
        }

        function handleRemoveAnswer(index) {
            listAnswer.value.splice(index, 1);
        }

        async function submitQuestion() {
            await formRef.value.validate();
            // nếu không có lỗi
            if (isFormAddQuestionValid.value) {
                if (questionId.value) {
                    const question = listQuestion.value.find(q => q.id == questionId.value);
                    question.questionTitle = questionTitle.value;
                    question.isMultiAnswer = isMultiAnswer.value
                    question.isAnswerAddable = isAnswerAddable.value
                    question.answers = [...listAnswer.value]
                } else {
                    listQuestion.value.unshift({
                        questionTitle: questionTitle.value,
                        isMultiAnswer: isMultiAnswer.value,
                        isAnswerAddable: isAnswerAddable.value,
                        answers: [...listAnswer.value],
                        id: uuid.v1()
                    });
                }

                store.commit('addnew/resetCurrentQuestion')
            }
        }

        const validateAnswerRule = [
            (v) => {
                if (isAnswerAddable.value) {
                    return true;
                }
                return listAnswer.value?.length || "Cần nhập ít nhất 1 câu trả lời"
            }
        ]

        function handleRemoveQuestion(id) {
            listQuestion.value = listQuestion.value.filter(x => x.id != id);
        }

        function handleEditQuestion(id) {
            const question = listQuestion.value.find(x => x.id == id);
            questionTitle.value = question.questionTitle;
            isMultiAnswer.value = question.isMultiAnswer;
            listAnswer.value = [...question.answers];
            isAnswerAddable.value = question.isAnswerAddable;
            questionId.value = id;
        }

        function handleDiscardEdit() {
            store.commit('addnew/resetCurrentQuestion')
        }

        async function handleSubmitVote() {


            store.commit('common/setIsLoading', true);

            const submitEntity = store.getters['addnew/voteEntity']
            var voteResult = await voteApi.createAsync(submitEntity)
            store.commit('common/setIsLoading', false);

            emit("submit", voteResult);
        }

        return {
            questionTitle,
            isMultiAnswer,
            isAnswerAddable,
            listAnswer,
            answerContent,
            addAnswer,
            answerContentRef,
            handleRemoveAnswer,
            requireStringRule,
            isFormAddQuestionValid,
            submitQuestion,
            formRef,
            validateAnswerRule,
            listQuestion,
            handleRemoveQuestion,
            handleEditQuestion,
            questionId,
            handleDiscardEdit,
            handleSubmitVote
        };
    },
};
</script>

<style scoped>
.answer-item-remove {
    opacity: 0;
}

.answer-item:hover .answer-item-remove {
    opacity: 1;
}

.question-list {
    width: 100%;
    max-height: calc(100vh - 220px);
    overflow-y: auto;
}

.custom-btn {
    font-size: 12px;
    padding: 4px 4px !important;
    height: unset;
}

.question-item:hover .question-btn {
    opacity: 1 !important;
}

.answer-list {
    max-height: calc(100vh - 530px);
}
</style>