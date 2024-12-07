<template>
    <div class="bg-white min-w-[330px] rounded-md px-3 py-2">
        <h1 class="text-lg font-semibold">{{ questionEntity.questionTitle }}</h1>
        <v-form v-model="isFormValid" ref="formRef">
            <v-text-field class="mt-2" :rules="requireStringRule" ref="answerContentRef" v-model="answerContent"
                placeholder="Nội dung câu trả lời" clearable></v-text-field>
        </v-form>
        <div class="flex justify-end gap-x-3 mt-2">
            <v-btn @click="handleCancel" color="secondary normal-case">Hủy</v-btn>
            <v-btn @click="handleSubmit" color="primary normal-case">Thêm mới</v-btn>
        </div>
    </div>
</template>

<script>
import { ref } from 'vue';
import { requireStringRule } from '@/until/ruleValidate';


export default {
    name: 'AddNewAnswer',
    components: {},
    props: {
        popup: {
            type: Object,
            required: true,
        }
    },
    setup(props) {
        const questionEntity = ref();
        const answerContent = ref();
        const formRef = ref();
        const isFormValid = ref(false);
        questionEntity.value = props.popup.metaData.questionEntity;

        function handleCancel() {
            props.popup.visible = false;
            props.popup.resolve("");
        }

        async function handleSubmit() {
            await formRef.value.validate();
            // nếu không có lỗi
            if (isFormValid.value) {
                props.popup.visible = false;
                props.popup.resolve(answerContent.value);
            }
        }

        return {
            questionEntity,
            answerContent,
            requireStringRule,
            formRef,
            handleSubmit,
            handleCancel,
            isFormValid
        }
    },
};
</script>

<style scoped></style>