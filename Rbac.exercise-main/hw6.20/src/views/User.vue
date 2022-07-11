<template>
  <div>
    <div style="float: left">
      <el-button type="primary" @click="getrole">添加角色</el-button>
    </div>
    <el-table :data="tableData" style="width: 100%">
      <el-table-column
        prop="userId"
        label="用户ID"
        width="80px"
      ></el-table-column>
      <el-table-column prop="userName" label="用户名" width="80px">
        <template slot-scope="scope">
          <span style="margin-left: 10px">{{
            scope.row.userName | formname
          }}</span>
        </template>
      </el-table-column>
      <el-table-column prop="email" label="邮箱" width="130px">
      </el-table-column>
      <el-table-column label="创建时间">
        <template slot-scope="scope">
          <i class="el-icon-time"></i>
          <span style="margin-left: 10px">{{
            scope.row.createTime | formdate
          }}</span>
        </template>
      </el-table-column>
      <el-table-column label="最后一次登录时间">
        <template slot-scope="scope">
          <i class="el-icon-time"></i>
          <span style="margin-left: 10px">{{ scope.row.lastLoginDate }}</span>
        </template>
      </el-table-column>
      <el-table-column prop="lastLoginIP" label="最后一次登录的IP">
      </el-table-column>
      <el-table-column label="是否锁定" width="120px">
        <template slot-scope="scope">
          <span v-if="scope.row.isLock == 1">是</span>
          <span v-else>否</span>
        </template>
      </el-table-column>
      <el-table-column label="删除状态" width="120px">
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
          <el-button
            size="mini"
            type="danger"
            @click="handleDelete(scope.$index, scope.row)"
            >删除</el-button
          >
        </template>
      </el-table-column>
    </el-table>
    <el-pagination
      @size-change="handleSizeChange"
      @current-change="handleCurrentChange"
      :current-page="index"
      :page-sizes="[2, 4, 8]"
      :page-size="size"
      layout="total, sizes, prev, pager, next, jumper"
      :total="total"
    >
    </el-pagination>
    <el-dialog title="添加用户" :visible.sync="userroletable" width="40%">
      <Add :key="new Date().getTime()" ref="rolelist" />
    </el-dialog>
  </div>
</template>

<script>
import Add from "@/views/AddUserRole.vue";
export default {
  components: {
    Add,
  },
  data() {
    return {
      tableData: [],
      index: 1,
      size: 2,
      total: 0,
      userroletable: false,
    };
  },
  methods: {
    handleEdit(index, row) {
      console.log(index, row);
    },
    handleDelete(index, row) {
      console.log(index, row);
    },
    getUser() {
      this.$axios
        .get("User/UserPage", {
          params: { pageindex: this.index, pagesize: this.size },
        })
        .then((d) => {
          this.tableData = d.data.item1;
          this.total = d.data.item2;
        });
    },
    handleSizeChange(val) {
      this.index = 1;
      this.size = val;
      this.getUser();
    },
    handleCurrentChange(val) {
      this.index = val;
      this.getUser();
    },
    //分配角色
    getrole() {
      this.userroletable = true;
    },
  },
  created() {
    this.getUser();
  },
  filters: {
    formdate(val) {
      return val.substr(0, 10) + " " + val.substr(11, 8);
    },
    formname(val) {
      return val.toLocaleUpperCase();
    },
  },
};
</script>