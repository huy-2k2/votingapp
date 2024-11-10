<template>
    <div>
        <p>{{ formattedTime }}</p>
    </div>
</template>

<script>
import { useCountDownTimer } from '@/composition/useCountDownTimer';
import { ref, onMounted, onUnmounted } from 'vue'

export default {
    props: {
        startTime: {
            type: String,
            required: true
        },
        durationMinutes: {
            type: Number,
            required: true
        }
    },
    setup(props) {
        const { formattedTime, lefttime} = useCountDownTimer(props.startTime, props.durationMinutes)

        return {
            formattedTime
        }
    }
}
</script>

<style scoped>
/* Styling tùy ý */
</style>