<template>
  <div>
    <el-card class="box-card loginstyle">
      <div slot="header" class="clearfix">
        <span>卡片名称</span>
        <el-button style="float: right; padding: 3px 0" type="text"
          >操作按钮</el-button
        >
      </div>
      <el-form
        :model="ruleForm"
        :rules="rules"
        ref="ruleForm"
        label-width="100px"
        class="demo-ruleForm"
      >
        <el-form-item label="用户名" prop="username">
          <el-input v-model="ruleForm.username"></el-input>
        </el-form-item>
        <el-form-item label="密码" prop="password">
          <el-input v-model="ruleForm.password" type="password"></el-input>
        </el-form-item>
        <el-form-item label="验证码" prop="checkcode">
          <el-input
            v-model="ruleForm.checkcode"
            style="width: 140px"
          ></el-input>
          <img :src="imageUrl" @click="acquireVerification" />
        </el-form-item>
        <el-form-item>
          <el-button type="primary" @click="submitForm('ruleForm')"
            >登录</el-button
          >
          <el-button @click="resetForm('ruleForm')">重置</el-button>
        </el-form-item>
      </el-form>
    </el-card>
  </div>
</template>

<script>
export default {
  data() {
    return {
      imageUrl: "",
      ruleForm: {
        username: "",
        password: "",
        checkcode: "",
      },
      rules: {
        username: [
          { required: true, message: "请输入用户名", trigger: "blur" },
        ],
        password: [{ required: true, message: "请输入密码", trigger: "blur" }],
        checkcode: [
          { required: true, message: "请输入验证码", trigger: "blur" },
        ],
      },
    };
  },
  methods: {
    onSubmit() {
      this.$axios.post("User/Login", this.ruleForm).then((d) => {
        if (d.data.token != null) {
          this.$message.success("登录成功");
          sessionStorage.setItem("username", this.ruleForm.username);
          localStorage.setItem("token", d.data.token);
          this.$router.push("/home");
        }
        if (d.data.msg == "该用户名不存在") {
          this.$message.error("该用户名不存在");
          return;
        }
        if (d.data.msg == "密码输入有误") {
          this.$message.error("密码输入有误");
          return;
        }
        if (d.data.msg == "验证码错误") {
          this.$message.error("验证码错误");
          return;
        }
      });
    },
    submitForm(formName) {
      this.$refs[formName].validate((valid) => {
        if (valid) {
          this.onSubmit();
        } else {
          console.log("error submit!!");
          return false;
        }
      });
    },
    resetForm(formName) {
      this.$refs[formName].resetFields();
    },
    acquireVerification() {
      // {responseType: 'blob'} ，不加这个返回的就是乱码
      //直接获取
      this.$axios.get("Code", { responseType: "blob" }).then((response) => {
        console.log(response.data);
        this.imageUrl = window.URL.createObjectURL(response.data);
        console.log(this.imageUrl);
      });
    },
  },
  mounted() {
    this.acquireVerification();
  },
};
</script>

<style>
.text {
  font-size: 14px;
}

.item {
  margin-bottom: 18px;
}

.clearfix:before,
.clearfix:after {
  display: table;
  content: "";
}
.clearfix:after {
  clear: both;
}

.box-card {
  width: 480px;
}

.loginstyle {
  margin: 0 auto;
  margin-top: 10em;
}
</style>