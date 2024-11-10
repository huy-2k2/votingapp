const numberRules = [
    (v) => {
        return isNaN(parseInt(v)) || parseInt(v) > 0 || "Vui lòng nhập số lớn hơn không"
    }
];

const requireNumberRule = [
    (v) => ((v || v === 0) && v != null && v != undefined) || "Không được để trống"
]

const requireStringRule = [
    (v) => (!!v?.trim()?.length) || "Không được để trống"
]

const selectRules = [
    (v) => !!v?.length || "Chọn ít nhất một giá trị"
];


export {numberRules, requireNumberRule, selectRules, requireStringRule}