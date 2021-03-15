<template>
  <v-card :color="currentColor" :class="{ 'lighten-3': item.isComplete }" dark hover>
    <v-card-title
      :class="{ 'text-decoration-line-through': item.isComplete }"
      class="headline"
    >
      {{ item.taskName }}
    </v-card-title>

    <v-card-actions class="justify-space-between">
      <v-btn text @click="markComplete">
        <span v-if="item.isComplete">Cancel done</span>
        <span v-else>I have done this</span>
      </v-btn>
      <div>
        <v-btn text class="btn-edit" @click="editTodo">
          <v-icon>mdi-pencil</v-icon>
        </v-btn>
        <v-btn text class="btn-delete" @click="deleteTodo">
          <v-icon>mdi-delete</v-icon>
        </v-btn>
      </div>
    </v-card-actions>
  </v-card>
</template>
<script>
export default {
  name: "ToDoItems",
  props: {
    item: Object
  },
  methods: {
    generator: function () {
      this.currentColor = "#" + ((Math.random() * 0xffffff) << 0).toString(16);
    },
    deleteTodo: function () {
      this.$emit("delete", this.item);
    },
    editTodo: function () {
      this.$emit("edit", this.item);
    },
    markComplete: function () {
      this.$emit("complete", this.item);
    },
  },
  data() {
    return {
      currentColor: "",
    };
  },
  mounted() {
    this.generator();
  },
};
</script>