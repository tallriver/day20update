<template>
  <div>
    <el-container style="height: 100%; border: 1px solid #eee">
      <el-header>授权管理系统</el-header>
      <el-container
        ><el-aside width="200px">
          <el-menu
            style="height: 100%"
            default-active="/menu"
            class="el-menu-vertical-demo"
            background-color="#545c64"
            text-color="#fff"
            active-text-color="#ffd04b"
            :router="true"
            :unique-opened="true"
          >
            <el-submenu
              :index="String(item.menuId)"
              v-for="item in menuArr.filter((m) => m.parentId == 0)"
              :key="item.menuId"
            >
              <template slot="title">
                <i class="el-icon-location"></i>
                <span>{{ item.menuName }}</span>
              </template>
              <template
                v-for="nextitem in menuArr.filter(
                  (m) => m.parentId == item.menuId
                )"
              >
                <el-menu-item
                  :index="menuitem.linkUrl"
                  v-for="menuitem in menuArr.filter(
                    (m) => m.parentId == nextitem.menuId
                  )"
                  :key="menuitem.menuId"
                  >{{ menuitem.menuName }}</el-menu-item
                >
              </template>
            </el-submenu>
          </el-menu> </el-aside
        ><el-main> <router-view /> </el-main
      ></el-container>
    </el-container>
  </div>
</template>

<style>
.el-header {
  background-color: #b3c0d1;
  color: #333;
  line-height: 60px;
}

.el-aside {
  color: #333;
}
</style>

<script>
export default {
  data() {
    return {
      menuArr: [],
    };
  },
  mounted() {
    this.getmenu();
  },
  methods: {
    getmenu() {
      this.$axios.get("Menu/GetMenuShow").then((d) => {
        this.menuArr = d.data;
      });
    },
  },
};
</script>