
const constants = {
    HRT_API_HOST: process.env.HRT_API_HOST || "http://localhost:8082",
    HRT_API_VERSION: process.env.HRT_API_VERSION || "v1",

    POSITION_STATUSES: [
        { statusID: 1, name: "Draft" },
        { statusID: 2, name: "Open" },
        { statusID: 3, name: "On Hold" },
        { statusID: 4, name: "Closed" },
        { statusID: 5, name: "Cancelled" }
    ]    
}

module.exports = constants;