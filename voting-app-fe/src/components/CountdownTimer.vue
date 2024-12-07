<template>
    <div>
        <p>{{ formattedTime }}</p>
    </div>
</template>

<script>
import { useCountDownTimer } from '@/composition/countDownTimerComposition';
import { ref, onMounted, onUnmounted, watch } from 'vue'
import { boolean } from 'yup';

export default {
    emits: ['update:model-value', 'update:modelValue'],
    props: {
        startTime: {
            type: String,
            required: true
        },
        durationMinutes: {
            type: Number,
            required: true
        },
        isStoped: {
            type: Boolean,
            required: true,
            default: false,
        },
        modelValue: {
            type: Number,
            required: true,
        }
    },
    setup(props, { emit }) {
        const isStoped = ref(false);

        const { formattedTime, lefttime } = useCountDownTimer(props.startTime, props.durationMinutes, isStoped)
        // Theo dõi sự thay đổi của prop 'message'
        watch(
            () => props.isStoped,
            (newvalue, oldValue) => {
                isStoped.value = newvalue;
            }
        )

        watch(() => lefttime.value,
            (newVal, oldVal) => {
                emit('update:modelValue', newVal);
            }
        )

        return {
            formattedTime,
            lefttime
        }
    }
}
</script>

<style scoped>
/* Styling tùy ý */
</style>
