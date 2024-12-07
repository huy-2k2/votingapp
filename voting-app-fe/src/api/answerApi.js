import {init, initHandleAuth} from './baseApi'

const baseApi = init("answer")

const baseHandleAuthApi = initHandleAuth("answer")

export default {
    async createMultiAsync(answers) {
        const {data} = await baseHandleAuthApi.post(`/multi`, answers)
        return data
    },
}
