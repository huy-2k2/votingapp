<template>
    <v-dialog persistent v-for="(popup) in popups" :key="popup.id" v-model="popup.visible">
        <component :is="popup.component" :popup="popup" class="popup"></component>
    </v-dialog>
</template>

<script>
import { onMounted, ref } from 'vue';
import { uuid } from 'vue-uuid';
import commonFunction from '@/until/commonFunction'

export default {
    name: 'ListPopup',
    components: {},
    setup() {
        const popups = ref([]);

        onMounted(() => {
            commonFunction.initPopup(({ component, metaData }) => {
                return new Promise((resolve) => {
                    popups.value.push({
                        component,
                        metaData,
                        visible: true,
                        resolve,
                        id: uuid.v1()
                    });
                });
            })
        })

        return {
            popups,
        }
    },
};
</script>

<style scoped></style>