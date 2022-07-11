<template>
  <div>
    <el-table :data="tableData" style="width: 100%">
      <el-table-column prop="roleId" label="管理员ID"></el-table-column>
      <el-table-column prop="roleName" label="管理员名称"> </el-table-column>
      <el-table-column label="创建时间">
        <template slot-scope="scope">
          <i class="el-icon-time"></i>
          <span style="margin-left: 10px">{{
            scope.row.createTime | formdate
          }}</span>
        </template>
      </el-table-column>
      <el-table-column label="删除状态">
        <template slot-scope="scope">
          <span v-if="scope.row.delState">已删除</span>
          <span v-else>未删除</span>
        </template>
      </el-table-column>
      <el-table-column label="操作">
        <template slot-scope="scope">
          <el-button size="mini" @click="handleEdit(scope.$index, scope.row)"
            >编辑</el-button
          >
          <el-button size="mini" @click="menurole(scope.$index, scope.row)"
            >分配权限</el-button
          >
          <el-button
            size="mini"
            type="danger"
            @click="handleDelete(scope.$index, scope.row)"
            >删除</el-button
          >
        </template>
      </el-table-column>
    </el-table>
    <el-dialog title="分配权限" :visible.sync="menuroletable" width="30%">
      <Add :key="new Date().getTime()" ref="menutree" :editid="roleId" />
      <span slot="footer" class="dialog-footer">
        <el-button type="primary" @click="createmenurole">确定</el-button>
        <el-button @click="menuroletable = false">取 消</el-button>
      </span>
    </el-dialog>
  </div>
</template>

<script>
import Add from "@/views/AddMenuRole.vue";
export default {
  components: {
    Add,
  },
  data() {
    return {
      tableData: [],
      menuroletable: false,
      roleId: 0,
    };
  },
  methods: {
    handleEdit(index, row) {
      console.log(index, row);
    },
    handleDelete(index, row) {
      console.log(index, row);
    },
    getRole() {
      this.$axios.get("Role/GetRoleAll").then((d) => {
        this.tableData = d.data;
      });
    },
    //分配权限
    menurole(index, row) {
      this.menuroletable = true;
      this.roleId = row.roleId;
    },
    createmenurole() {
      let menuId = this.$refs.menutree.$refs.choosemenu
        .getCheckedNodes(true, true)
        .map((m) => m.value);
      debugger;
      this.delmenurole();
      this.$axios
        .post("Menu/SaveMenuRole", { menuId: menuId, roleId: this.roleId })
        .then((d) => {
          if (d.data) {
            this.$message.success("权限分配成功");
            this.menuroletable = false;
          } else {
            this.$message.error("权限分配失败");
          }
        });
    },
    //删除权限
    delmenurole() {
      this.$axios
        .delete("Role/DelMenuRole?roleid=" + this.roleId)
        .then((d) => {});
    },
  },
  created() {
    this.getRole();
  },
  filters: {
    formdate(val) {
      return val.substr(0, 10) + " " + val.substr(11, 8);
    },
  },
};
</script>