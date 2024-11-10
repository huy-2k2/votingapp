<template>
    <div class="flex items-start justify-start">
        <div v-for="(step, index) in steps" :key="index">
            <div>
                <div class="text-lg font-medium px-8">
                    <div>
                        Bước {{ index + 1 }}: {{ step.title }}
                    </div>
                </div>
                <div class="step-foot relative" :class="[{ isActive: modelValue > index }]">
                    <div class="step-dot step-dot-first" :class="[{ isActive: modelValue >= index }]"></div>
                    <div v-show="index == steps.length - 1" class="step-dot step-dot-last"
                        :class="[{ isActive: modelValue == steps.length }]"></div>
                </div>
            </div>
        </div>
        <v-tooltip text="Quay lại">
            <template v-slot:activator="{ props }">
                <button @click="handleBackStep" :disabled="modelValue == 0 || isDone" class="icon-direct"
                    v-bind="props">
                    <i class="fas fa-arrow-left"></i>
                </button>
            </template>
        </v-tooltip>
    </div>
</template>

<script>

export default {
    name: 'StepByStep',
    emits: ["update:modelValue", "backStep"],
    components: {},
    props: {
        modelValue: {
            type: Number,
            required: true,
        },
        steps: {
            type: Array,
            required: true,
        },
        isDone: {
            type: Boolean,
            required: true,
            default: false
        }
    },
    setup(props, { emit }) {

        const updateValue = (stepId) => {
            emit('update:modelValue', stepId); // Emit giá trị mới
        };


        function handleBackStep() {
            const curentStep = props.modelValue
            updateValue(curentStep - 1);
            emit("backStep");
        }

        return {
            updateValue,
            handleBackStep
        }
    },
};
</script>

<style scoped>
.icon-direct {
    width: 30px;
    height: 30px;
    border: 1px solid #ccc;
    color: #ccc;
    display: flex;
    align-items: center;
    justify-content: center;
    border-radius: 100%;
}


.icon-direct:not(:disabled) {
    border-color: aquamarine;
    color: aquamarine;
}




.step-foot {
    width: 100%;
    height: 0;
    border-bottom: 8px dotted #ccc;
    margin-top: 16px;
}

.step-foot.isActive {
    border-color: aquamarine;
}

.step-dot {
    width: 20px;
    height: 20px;
    border-radius: 100%;
    background-color: #ccc;
    position: absolute;
    top: -6px;
}

.step-dot-first {
    left: -10px;
}

.step-dot-last {
    right: -10px;
}

.step-dot.isActive {
    background-color: aquamarine;
}
</style>