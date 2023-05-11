// script.js

import {sleep} from "k6";
import {http} from "k6/http";

export const options = {
    thresholds: {
        http_req_duration: ['p(95)<200'],
    },
    vus: 10,
    duration: '1m',
};

const BASE_URL = __ENV.BASE_URL || 'http://localhost:8081';

export default function () {
    const res = http.get(`${BASE_URL}/WebAPI/Problem`);
    sleep(3);
}