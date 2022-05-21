<template>
  <v-dialog v-model="dialog" max-width="500px">
    <template v-slot:activator="{ on, attrs }">
      <v-btn color="blue darken-1" text  v-bind="attrs" v-on="on">
        Zapisz
      </v-btn>
    </template>
    <v-card>
      <v-card-title class="text-h5">Zapisać?</v-card-title>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn color="blue darken-1" text @click="closeConfirm">Anuluj</v-btn>
        <v-btn color="blue darken-1" text @click="save">Zapisz</v-btn>
        <v-spacer></v-spacer>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
export default {
  name: "ConfirmSave",
  data: () => ({
    dialog: false,
  }),
  methods:{
    closeConfirm() {
      this.dialog = false;
    },
    save(){
      this.$emit('save-confirmed');
      this.dialog = false;
    }

  }
};
</script>

<style scoped></style>
