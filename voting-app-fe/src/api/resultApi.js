import {init, initHandleAuth} from './baseApi'

const baseApi = init("result")

const baseHandleAuthApi = initHandleAuth("result")

export default {
    
    async createAsync(resultEntity) {
        const {data} = await baseHandleAuthApi.post('/', resultEntity)
        return data
    },
}
