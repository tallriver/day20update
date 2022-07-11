<template>
  <div>
    <el-tree
      :data="data"
      show-checkbox
      node-key="value"
      :default-expanded-keys="[1, 2, 5]"
      :default-checked-keys="tableData"
      :props="defaultProps"
      ref="choosemenu"
    >
    </el-tree>
  </div>
</template>

<script>
export default {
  props: ["editid"],
  data() {
    return {
      data: [],
      defaultProps: {
        id: "value",
        children: "children",
        label: "label",
      },
      tableData: [],
    };
  },
  methods: {
    getlist() {
      this.$axios.get("Menu/GetList").then((d) => {
        var reg = new RegExp('\\,"children":\\[]', "g");
        this.data = JSON.parse(JSON.stringify(d.data).replace(reg, ""));
      });
    },
    getvalue() {
      var list = this.$refs.choosemenu
        .getCheckedNodes(true, true)
        .map((m) => m.value);
      console.log(list);
    },
    //反填权限
    getback() {
      this.$axios.get("Role/GetRoleMenuDto?roleid=" + this.editid).then((d) => {
        this.tableData = d.data.map((m) => m.menuId);
      });
    },
  },
  created() {
    this.getlist();
    this.getback();
  },
  watch: {
    editid(val) {
      this.getlist();
      this.getback();
    },
  },
};
</script>