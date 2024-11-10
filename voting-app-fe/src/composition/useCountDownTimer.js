import { ref, onMounted, onUnmounted } from 'vue'

export function useCountDownTimer(startTime, durationMinutes) {
    const formattedTime = ref('')
    const lefttime = ref(null);
    const callBackInterval = ref(null);

    function getSecondsBetweenDates(date1, date2) {
        const diffInMilliseconds = Math.abs(date1 - date2)
        return Math.floor(diffInMilliseconds / 1000)
    }

    function formatTime(seconds) {
        const hours = Math.floor(seconds / 3600); // Tính số giờ
        const minutes = Math.floor((seconds % 3600) / 60); // Tính số phút
        const remainingSeconds = seconds % 60; // Tính số giây còn lại

        // Đảm bảo định dạng 2 chữ số cho giờ, phút, giây
        const formattedHours = String(hours).padStart(2, '0');
        const formattedMinutes = String(minutes).padStart(2, '0');
        const formattedSeconds = String(remainingSeconds).padStart(2, '0');

        return `${formattedHours}:${formattedMinutes}:${formattedSeconds}`;
    }

    onMounted(() => {
       callBackInterval.value = setInterval(() => {
            lefttime.value = (60 * durationMinutes) - getSecondsBetweenDates(new Date(), new Date(startTime));
            formattedTime.value = formatTime(lefttime.value);
       }, 1000)
    })

    onUnmounted(() => {
        clearInterval(callBackInterval.value)
    })
    return {
        formattedTime,
        lefttime
    }
}