import {init, initHandleAuth} from './baseApi'

const baseApi = init("vote")

const baseHandleAuthApi = initHandleAuth("vote")

export default {
    
    async createAsync(voteEntity) {
        const {data} = await baseHandleAuthApi.post('/', voteEntity)
        return data
    },
    

    async getEntireAsync(id) {
        const {data} = await baseHandleAuthApi.get(`/entire?id=${id}`,)
        return data
    },

    async updateVoteStatusAsync(voteID, voteStatus) {
        await baseHandleAuthApi.put(`/voteStatus?voteID=${voteID}&status=${voteStatus}`,)
    },

    async getListByUserId() {
        const {data} = await baseHandleAuthApi.get('/list');
        return data;
    }
}
