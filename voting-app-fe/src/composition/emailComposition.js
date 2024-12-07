import emailApi from '@/api/emailApi';
import { email } from '@vee-validate/rules';
import { ref, onMounted, onUnmounted, onBeforeMount } from 'vue'

function useEmailLoader() {
    const emailsByUser = ref([]);


    onBeforeMount(async() => {
       const emails = await emailApi.getByUserAsync();

        emailsByUser.value = emails.map(x => {
            return {
                email: x,
                isCustom: false
            }
        });

    });

    return {
        emailsByUser,
    }

}

export {useEmailLoader};