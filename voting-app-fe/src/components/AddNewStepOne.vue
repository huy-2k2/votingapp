<template>
    <div class="px-5 py-4 border border-[#ccc] rounded-md">
        <v-form class="w-[500px] flex flex-col gap-y-4 max-h-[500px] overflow-y-auto" ref="formRef"
            v-model="isFormValid">
            <v-text-field :rules="requireStringRule" v-model="voteTitle" placeholder="Tiêu đề cuộc bình chọn"
                clearable></v-text-field>
            <v-text-field label="Thời gian bình chọn" v-model.number="voteTime" type="number"
                :rules="[...requireNumberRule, ...numberRules]"></v-text-field>
            <v-text-field v-if="!isSendQrEmail" label="Số lượng người tham gia" v-model.number="totalJoiner"
                type="number" :rules="numberRules"></v-text-field>
            <div class="flex items-center justify-start gap-x-5">
                <v-checkbox v-model="isSendQrEmail" label="Gửi QR qua Email"></v-checkbox>
                <v-checkbox v-show="isSendQrEmail == false" v-model="isRequireName"
                    label="Bắt buộc nhập Tên"></v-checkbox>
            </div>
            <v-autocomplete item-value="email" item-title="email" @input="handleChangeSearch" v-if="isSendQrEmail"
                v-model="emailList" :items="emails" label="Danh sách email" multiple :rules="selectRules"
                @blur="handleBlueSearch">
            </v-autocomplete>
            <div class="flex justify-end">
                <v-btn color="primary" @click="submitForm">Bước tiếp theo</v-btn>
            </div>
        </v-form>
    </div>
</template>

<script>
import { computed, ref } from 'vue';
import { useStore } from 'vuex';
import { requireNumberRule, numberRules, selectRules, requireStringRule } from '@/until/ruleValidate';
import { useEmailLoader } from '@/composition/emailComposition';
export default {
    name: 'AddNewStepOne',
    emits: ['submit'],
    components: {},
    props: {

    },
    setup(props, { emit }) {
        const store = useStore();  // Sử dụng Vuex store

        const formRef = ref(null);
        const isFormValid = ref(null);
        const { emailsByUser: emails } = useEmailLoader();

        const voteTitle = computed({
            get: () => store.getters['addnew/stepOne'].voteTitle,
            set: (value) => store.commit('addnew/setStepOne', { field: 'voteTitle', value }),
        });

        const voteTime = computed({
            get: () => store.getters['addnew/stepOne'].voteTime,
            set: (value) => store.commit('addnew/setStepOne', { field: 'voteTime', value }),
        });


        const isSendQrEmail = computed({
            get: () => store.getters['addnew/stepOne'].isSendQrEmail,
            set: (value) => {
                store.commit('addnew/setStepOne', { field: 'isSendQrEmail', value });
                if (value) {
                    isRequireName.value = false;
                }
            }

        });

        const isRequireName = computed({
            get: () => store.getters['addnew/stepOne'].isRequireName,
            set: (value) => store.commit('addnew/setStepOne', { field: 'isRequireName', value }),
        });

        const totalJoiner = computed({
            get: () => store.getters['addnew/stepOne'].totalJoiner,
            set: (value) => store.commit('addnew/setStepOne', { field: 'totalJoiner', value }),
        });


        const emailList = computed({
            get: () => store.getters['addnew/stepOne'].emails,
            set: (value) => store.commit('addnew/setStepOne', { field: 'emails', value }),
        });

        const submitForm = async () => {
            await formRef.value.validate();
            // nếu không có lỗi
            if (isFormValid.value) {
                emit('submit')
            }
        };

        function handleChangeSearch(event) {
            const value = event.target.value;

            // add thêm giá trị email đang search vào danh sách nếu chưa có
            if (!emails.value.some(e => e.email == value)) {

                // bỏ đi những thằng tự add trước đó nếu nó chưa được chọn
                emails.value = emails.value.filter(e => {

                    if (!e.isCustom) {
                        return true;
                    }

                    return emailList.value.some(eSelected => eSelected == e.email);
                })

                if (value) {
                    emails.value.push({
                        email: value,
                        isCustom: true,
                    });
                }
            }
        }

        function handleBlueSearch() {
            emails.value = emails.value.filter(e => {
                if (!e.isCustom) {
                    return true;
                }

                return emailList.value.some(eSelected => eSelected == e.email);
            })
        }
        return {
            voteTime,
            totalJoiner,
            isRequireName,
            isSendQrEmail,
            emailList,

            numberRules,
            requireNumberRule,
            submitForm,
            formRef,
            emails,
            selectRules,
            handleChangeSearch,
            handleBlueSearch,
            isFormValid,
            voteTitle,
            requireStringRule
        };
    },
};
</script>

<style scoped></style>