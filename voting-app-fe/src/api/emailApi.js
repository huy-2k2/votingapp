import {init, initHandleAuth} from './baseApi'

const baseApi = init("email")

const baseHandleAuthApi = initHandleAuth("email")

export default {
    async getByUserAsync() {
        const {data} = await baseHandleAuthApi.get(`/byUser`,)
        return data
    },

    async getByIdAsync(emailID) {
        const {data} = await baseHandleAuthApi.get(`/?emailID=${emailID}`,)
        return data
    }
}
