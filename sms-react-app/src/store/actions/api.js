import axios from "axios";

const baseUrl = "https://localhost:44352/api/";

export default {
    api(url = baseUrl) {
        return {
            fetchAll: (action) => axios.get(url + action),
            fetchById: id => axios.get(url + id),
            create: (action, newRecord) => axios.post(url + action, newRecord),
            update: (id, action, updateRecord) => axios.put(url + action + id, updateRecord),
            delete: (id, action) => axios.delete(url + action + id),
        }
    }
}