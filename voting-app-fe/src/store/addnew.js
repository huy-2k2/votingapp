const state = () => {
    return {
        stepOne: {
            voteTitle: '',
            voteTime: 0,
            isSendQrEmail: false,
            isRequireName: false,
            totalJoiner: null,
            emails: []
        },
        stepTwo: {
            currentQuestion: {
                questionTitle: null,
                isMultiAnswer: false,
                isAnswerAddable: false,
                answers: [],
                id: null,
            },
            questions: []
        }
    }
}

const mutations = {
    reset(state) {
        state.stepOne =
             {
                voteTitle: '',
                voteTime: 0,
                isSendQrEmail: false,
                isRequireName: false,
                totalJoiner: null,
                emails: []
            }

            state.stepTwo = {
                currentQuestion: {
                    questionTitle: null,
                    isMultiAnswer: false,
                    isAnswerAddable: false,
                    answers: [],
                    id: null,
                },
                questions: []
            }
    },
    setStepOne(state, {value, field}) {
        state.stepOne[field] = value;
    },
    setStepTwo(state, {value, field}) {
        state.stepTwo.currentQuestion[field] = value;
    },
    setQuestions(state, value) {
        state.stepTwo.questions = value;
    },
    resetCurrentQuestion(state) {
        state.stepTwo.currentQuestion = {
            questionTitle: null,
            isMultiAnswer: false,
            isAnswerAddable: false,
            answers: [],
            id: null,
        }
    }
}

const getters = {
    stepOne(state) {
        return state.stepOne;
    },
    stepTwo(state) {
        return state.stepTwo.currentQuestion;
    },
    questions(state) {
        return state.stepTwo.questions;
    },

    voteEntity(state) {
        const entitySubmit = {
            ...state.stepOne,
            emails: state.stepOne.emails.map(em => ({emailAddress: em})),
            questions: state.stepTwo.questions.map(q => {
                return {
                    ...q,
                    answers: q.answers.map(a => ({answerContent: a, resultDetails: []}))
                }
            }),
            results: []
        }

        return entitySubmit;
    }
}

export default {
    namespaced: true,
    state,
    mutations,
    getters,
}