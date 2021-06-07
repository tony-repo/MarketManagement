<template>
  <el-container style="border: 1px,height:100%" direction="vertical">
    <el-header style="background-color: #219ed8">
      <el-container>
        <el-aside style="text-align: left; font-size: 20px">
          <span><b style="color: white">Market Management</b></span>
        </el-aside>
        <el-main>
          <el-dropdown style="float: right; margin-top: -10px">
            <span class="el-dropdown-link">
              <el-avatar :size="30" :src="imageSrc" style="margin-top: -5px" />
            </span>
            <el-dropdown-menu slot="dropdown">
              <el-dropdown-item>Profile</el-dropdown-item>
              <el-dropdown-item>Setting</el-dropdown-item>
              <el-dropdown-item>About</el-dropdown-item>
              <el-dropdown-item>Exit</el-dropdown-item>
            </el-dropdown-menu>
          </el-dropdown>
        </el-main>
      </el-container>
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
          <div v-for="item in menulist" :key="item.id" style="text-align: left">
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
</template>

<script>
import { siderRoutes } from "../../router";
export default {
  data() {
    return {
      isCollapse: false,
      menulist: siderRoutes[0].children,
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
</style>
