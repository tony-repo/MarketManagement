<template>
  <div class="indexClass">
    <el-container style="border: 1px">
      <el-header style="background-color: #219ed8">
        <div style="margin-top: 15px">
          <div style="float: left">
            <span
              ><b style="color: white; font-size: 30px"
                >Market Management</b
              ></span
            >
          </div>
          <div style="float: right">
            <el-dropdown>
              <span class="el-dropdown-link">
                <el-avatar :size="35" :src="imageSrc" />
              </span>
              <el-dropdown-menu slot="dropdown">
                <el-dropdown-item>Profile</el-dropdown-item>
                <el-dropdown-item>Setting</el-dropdown-item>
                <el-dropdown-item>About</el-dropdown-item>
                <el-dropdown-item>Exit</el-dropdown-item>
              </el-dropdown-menu>
            </el-dropdown>
          </div>
        </div>
      </el-header>
      <el-container>
        <el-aside :width="isCollapse ? '64px' : '280px'">
          <div class="toggle-button" @click="toggleCollpase">|||</div>
          <el-menu
            :default-active="$route.path"
            @open="handleOpen"
            @close="handleClose"
            :collapse="isCollapse"
            router
            background-color="#444B65"
            text-color="#FFFFFF"
            style="height: 800px"
            mode="vertical"
            :collapse-transition="false"
          >
            <div
              v-for="item in menulist"
              :key="item.id"
              style="text-align: left"
            >
              <el-menu-item :index="item.path" v-if="item.children == null">
                <i class="el-icon-menu"></i>
                <span slot="title" class="sider-button-name">{{
                  item.name
                }}</span>
              </el-menu-item>

              <el-submenu :index="item.path" v-else-if="item.children != null">
                <template slot="title">
                  <i class="el-icon-user"></i>
                  <span class="sider-button-name">{{ item.name }}</span>
                </template>

                <el-menu-item
                  :index="subItem.path + ''"
                  v-for="subItem in item.children"
                  :key="subItem.id"
                >
                  <template slot="title">
                    <i class="el-icon-menu"></i>
                    <span class="sider-button-name">{{ subItem.name }}</span>
                  </template>
                </el-menu-item>
              </el-submenu>
            </div>
          </el-menu>
        </el-aside>
        <el-main>
          <router-view></router-view>
        </el-main>
      </el-container>
    </el-container>
  </div>
</template>

<script>
import { siderRoutes } from "../../router";
export default {
  data() {
    return {
      isCollapse: false,
      menulist: siderRoutes[0].children,
      imageSrc:
        "https://cube.elemecdn.com/3/7c/3ea6beec64369c2642b92c6726f1epng.png",
    };
  },

  methods: {
    toggleCollpase() {
      this.isCollapse = !this.isCollapse;
    },
    handleOpen(key, keyPath) {
      console.log(key, keyPath);
    },
    handleClose(key, keyPath) {
      console.log(key, keyPath);
    },
  },
};
</script>

<style>
.toggle-button {
  background-color: #4a5064;
  font-size: 10px;
  line-height: 24px;
  color: white;
  text-align: center;
  letter-spacing: 0.2em;
  cursor: pointer;
}

.sider-button-name {
  margin-left: 15px;
}

.indexClass {
  /*设置内部填充为0，几个布局元素之间没有间距*/
  padding: 0px;
  /*外部间距也是如此设置*/
  margin: 0px;
  /*统一设置高度为100%*/
  height: 100%;
}
</style>
