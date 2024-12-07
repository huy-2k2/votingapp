<template>
    <div class="tabselect flex items-center justify-start gap-x-3">
        <div v-for="(tab) in tabs" :key="tab.value">
            <v-btn class="normal-case" :color="modelValue == tab.value ? 'primary' : ''" @click="handleClick(tab)">{{
                tab.text
                }}</v-btn>
        </div>
    </div>
</template>
<script>

export default {
    name: 'TabSelect',
    emits: ['update:modelValue'],
    props: {
        tabs: {
            type: Array,
            default: []
        },
        modelValue: {
            type: Number,
            required: true,
        }
    },
    components: {},
    setup(props, { emit }) {

        function handleClick(tab) {
            emit("update:modelValue", tab.value)
        }

        return {
            handleClick
        }

    },
};
</script>

<style scoped></style>