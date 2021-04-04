
const constants = {
    HRT_API_HOST: process.env.HRT_API_HOST || "http://localhost:8082",
    HRT_API_VERSION: process.env.HRT_API_VERSION || "v1",

    POSITION_STATUSES: [
        { statusID: 1, name: "Draft" },
        { statusID: 2, name: "Open" },
        { statusID: 3, name: "On Hold" },
        { statusID: 4, name: "Closed" },
        { statusID: 5, name: "Cancelled" }
    ],
    
    HTTP_OK: 200,
    HTTP_NoContent: 204,
    HTTP_BadRequest: 400,
    HTTP_Unauthorized: 401,
    HTTP_Forbidden: 403,
    HTTP_NotFound: 404,
    HTTP_IntServerError: 500,
    HTTP_NotImplemented: 501
}

module.exports = constants;