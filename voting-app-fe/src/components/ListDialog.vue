<template>
    <v-dialog persistent v-for="(dialog) in dialogs" :key="dialog.id" v-model="dialog.visible" max-width="500">
        <v-card>
            <v-card-title>{{ dialog.title }}</v-card-title>
            <v-card-text>{{ dialog.content }}</v-card-text>
            <v-card-actions>
                <v-btn v-for="(button) in dialog.buttons" :color="button.color" :key="button.value"
                    @click="handleButtonClick(dialog, button)">
                    {{ button.text }}
                </v-btn>
            </v-card-actions>
        </v-card>
    </v-dialog>
</template>

<script>
import { onMounted, ref } from 'vue';
import { uuid } from 'vue-uuid';
import commonFunction from '@/until/commonFunction'
export default {
    name: 'ListDialog',
    components: {},
    setup() {

        const dialogs = ref([]);
        function handleButtonClick(dialog, button) {
            dialog.visible = false;
            dialog.resolve(button.value)
        }
        onMounted(() => {
            commonFunction.initDialog(({ title, content, buttons }) => {
                return new Promise((resolve) => {
                    dialogs.value.push({
                        title,
                        content,
                        buttons,
                        visible: true,
                        resolve,
                        id: uuid.v1()

                    });
                });
            })
        })
        return {
            dialogs,
            handleButtonClick,
        }
    },
};
</script>

<style scoped></style>