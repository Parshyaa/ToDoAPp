import axios from 'axios'
export default {
    getUrlBase() {
        return "http://localhost:8082/api/";
    },
    getAllToDos() {
        var base = this.getUrlBase();
        return axios.get(base + 'todo/getall');
    },
    addTodo(todoVm) {
        const config = { headers: { 'Content-Type': 'application/json' } }
        var base = this.getUrlBase();
        return axios.post(base + 'todo/create', todoVm, config);
    },
    editTodo(todoVm) {
        const config = { headers: { 'Content-Type': 'application/json' } }
        var base = this.getUrlBase();
        return axios.put(base + 'todo/update', todoVm, config);
    },
    deleteTodo(todoId) {
        const config = { headers: { 'Content-Type': 'application/json' } }
        var base = this.getUrlBase();
        return axios.delete(base + 'todo/'+ todoId,  config);
    },
    markComplete(todoVm) {
        const config = { headers: { 'Content-Type': 'application/json' } }
        var base = this.getUrlBase();
        return axios.post(base + 'todo/complete', todoVm, config);
    },
}