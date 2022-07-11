<template>
  <div>
    <el-form
      :model="ruleForm"
      :rules="rules"
      ref="ruleForm"
      label-width="100px"
      class="demo-ruleForm"
    >
      <el-form-item label="用户名" prop="userName">
        <el-input v-model="ruleForm.userName"></el-input>
      </el-form-item>
      <el-form-item label="密码" prop="password">
        <el-input type="password" v-model="ruleForm.password"></el-input>
      </el-form-item>
      <el-form-item label="邮箱" prop="email">
        <el-input v-model="ruleForm.email"></el-input>
      </el-form-item>
      <el-form-item label="角色" prop="parentId">
        <el-select
          v-model="ruleForm.roleId"
          multiple
          placeholder="请选择"
          :style="{ width: '360px' }"
        >
          <el-option
            v-for="item in options"
            :key="item.roleId"
            :label="item.roleName"
            :value="item.roleId"
          >
          </el-option>
        </el-select>
      </el-form-item>
      <el-form-item>
        <el-button type="primary" @click="submitForm('ruleForm')"
          >立即创建</el-button
        >
        <el-button @click="resetForm('ruleForm')">重置</el-button>
      </el-form-item>
    </el-form>
  </div>
</template>

<script>
export default {
  data() {
    return {
      options: [],
      value: [],
      ruleForm: {
        userName: "",
        password: "",
        email: "",
        roleId: [],
      },
      rules: {
        userName: [
          { required: true, message: "请输入用户名", trigger: "blur" },
        ],
        password: [{ required: true, message: "请输入密码", trigger: "blur" }],
        email: [
          { required: true, message: "请输入Email", trigger: "blur" },
          { type: "email", message: "请输入正确格式的邮箱", trigger: "blur" },
        ],
      },
    };
  },
  methods: {
    getrole() {
      this.$axios.get("Role/GetRoleAll").then((d) => {
        this.options = d.data;
      });
    },
    submitForm(formName) {
      this.$refs[formName].validate((valid) => {
        if (valid) {
          //console.log(this.value);
          //this.ruleForm.parentId = this.value.slice(-1)[0];
          this.$axios.post("User/AddUserInfo", this.ruleForm).then((d) => {
            if (d.data.msg == "该用户名已存在") {
              this.$message.error("该用户名已存在");
              return;
            }
            if (d.data.msg == "用户添加成功") {
              this.$message({
                showClose: true,
                message: "添加成功",
                type: "success",
                onClose: (d) => {
                  //手动resetFrom
                  this.$parent.$parent.userroletable = false;
                  this.$parent.$parent.getUser();
                },
              });
            }
          });
        } else {
          console.log("error submit!!");
          return false;
        }
      });
    },
    resetForm(formName) {
      this.$refs[formName].resetFields();
    },
  },
  created() {
    this.getrole();
  },
};
</script>