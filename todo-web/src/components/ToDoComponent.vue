<template>
  <div class="hello">
    <v-container max-width="60px">
      <v-text-field
        label="What needs to be done?(Press 'Enter' to add)"
        color="purple"
        outlined
        clearable
        v-model="newTodoText"
        :rules="[rules.required]"
        @keydown.enter="isEdit ? edit() : addTodo(newTodoText)"
        @click:clear="clearTodoText"
      />

      <h2 class="display-1 purple--text text--darken-2">
        To-Dos:&nbsp;
        <v-fade-transition leave-absolute>
          <span :key="`todos-${this.allCount}`">
            {{ this.allCount }}
          </span>
        </v-fade-transition>
      </h2>

      <v-row class="my-1" align="center">
        <strong class="mx-4 pink--text text--darken-2">
          Active: {{ this.activeCount }}
        </strong>

        <v-divider vertical></v-divider>

        <strong class="mx-4 orange--text text--darken-2">
          Completed: {{ this.completedCount }}
        </strong>

        <v-spacer></v-spacer>

        <v-progress-circular
          :value="progress"
          color="light-blue"
          class="mr-2"
        ></v-progress-circular>
      </v-row>

      <v-row dense>
        <v-col v-for="todo in currTodoList" :key="todo.id" cols="12">
          <todoItems
            :item="todo"
            @delete="deleteTodo"
            @edit="editTodo"
            @complete="markComplete"
          />
        </v-col>
      </v-row>
    </v-container>
  </div>
</template>

<script>
import todoItems from "@/components/ToDoItems.vue";
import todoService from "@/api/ToDoApi";

export default {
  name: "ToDo",
  components: {
    todoItems,
  },
  data() {
    return {
      isEdit: false,
      itemChanged: {},
      completedCount: 0,
      allCount: 0,
      progress: 0,
      activeCount: 0,
      currTodoList: [],
      todoList: [],
      newTodoText: "",
      defaultToDoItem: {
        ToDoId: 0,
        TaskName: "Default Task",
        IsComplete: false,
      },
      rules: {
        required: (value) => !!value || "Required.",
      },
    };
  },
  watch: {
    currTodoList: {
      handler: function () {
        let activeTodos = this.todoList.filter(
          (obj) => obj.isComplete !== true
        );
        let completedTodos = this.todoList.filter(
          (obj) => obj.isComplete === true
        );

        this.allCount = this.todoList.length;
        this.activeCount = activeTodos.length;
        this.completedCount = completedTodos.length;
        this.progress = (this.completedCount / this.allCount) * 100;
      },
      deep: true,
    },
  },
  methods: {
    markComplete(item) {
      if (item.isComplete === true) {
        item.isComplete = false;
      } else {
        item.isComplete = true;
      }
      todoService
        .markComplete(item)
        .then((resp) => {
          console.log(resp);
        })
        .finally(() => {
          this.newTodoText = "";

          this.load();
        });
    },
    clearTodoText() {
      this.newTodoText = "";
      this.isEdit = false;
    },
    editTodo(item) {
      this.itemChanged = item;
      this.newTodoText = item.taskName;
      this.isEdit = true;
    },
    edit() {
      this.itemChanged.TaskName = this.newTodoText;
      todoService
        .editTodo(this.itemChanged)
        .then((resp) => {
          console.log(resp);
        })
        .finally(() => {
          this.newTodoText = "";

          this.load();
        });
    },
    deleteTodo(item) {
      todoService
        .deleteTodo(item.toDoId)
        .then((resp) => {
          console.log(resp);
        })
        .finally(() => {
          this.load();
        });
    },
    addTodo(newString) {
      var newObj = Object.assign({}, this.defaultToDoItem);
      newObj.TaskName = newString;
      todoService
        .addTodo(newObj)
        .then((resp) => {
          console.log(resp);
        })
        .finally(() => {
          this.newTodoText = "";

          this.load();
        });
    },
    load() {
      todoService.getAllToDos().then((resp) => {
        this.todoList = resp.data;
        this.currTodoList = resp.data;
        console.log(this.todoList);
      });
    },
  },
  mounted() {
    this.load();
  },
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
