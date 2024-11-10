import {init, initHandleAuth} from './baseApi'

const baseApi = init("Auth")

const baseHandleAuthApi = initHandleAuth("Auth")

export default {
    async loginWithGoogle(googleToken) {
            const {data: token} = await baseApi.post('/login', JSON.stringify(googleToken))
            return token;
    },
    async checkSigninedHandleAuth() {
        const {data} = await baseHandleAuthApi.get('/')
        return data
    },
    async checkSignined() {
        const {data} = await baseApi.get('/')
        return data
    },
 
}
