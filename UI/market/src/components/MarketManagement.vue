<template>
  <el-container style="border: 1px" direction="vertical">
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
    <el-container style="height: 800px; border: 1px solid #eee">
      <el-aside :class="isCollapsedClass">
        <el-menu
          :default-active="$route.path"
          class="el-menu-vertical-demo"
          @open="handleOpen"
          @close="handleClose"
          :collapse="isCollapse"
          router
          background-color="#444B65"
          text-color="#FFFFFF"
          style="height: 800px"
        >
          <el-menu-item index="/UserManagement">
            <i class="el-icon-user"></i>
            <span style="margin-left: 20px" slot="title">Users Management</span>
          </el-menu-item>
          <el-menu-item index="/OrderManagement">
            <i class="el-icon-menu"></i>
            <span style="margin-left: 20px" slot="title"
              >Orders Management</span
            >
          </el-menu-item>
          <el-menu-item index="/AccountManagement">
            <i class="el-icon-document"></i>
            <span style="margin-left: 20px" slot="title"
              >Account Management</span
            >
          </el-menu-item>
          <el-menu-item index="/Settings">
            <i class="el-icon-setting"></i>
            <span style="margin-left: 20px" slot="title">Settings</span>
          </el-menu-item>
        </el-menu>
      </el-aside>

      <el-container>
        <el-button @click="fold" style="width: 36px; height: 36px">
          <i class="el-icon-s-fold" style="margin-left: -8px"></i>
        </el-button>
        <el-main>
          <router-view></router-view>
        </el-main>
      </el-container>
    </el-container>
  </el-container>
</template>

<script>
export default {
  data() {
    return {
      userName: "Tony Williams",
      imageSrc:
        "https://fuss10.elemecdn.com/e/5d/4a731a90594a4af544c0c25941171jpeg.jpeg",
      isCollapse: true,
    };
  },
  methods: {
    handleOpen(key, keyPath) {
      console.log(key, keyPath);
    },
    handleClose(key, keyPath) {
      console.log(key, keyPath);
    },
    fold() {
      console.log(this.isCollapse);
      this.isCollapse = !this.isCollapse;
    },
  },
  computed: {
    isCollapsedClass() {
      if (this.isCollapse) {
        return "collapsed";
      } else {
        return "uncollapsed";
      }
    },
  },
};
</script>


<style>
.collapsed {
  width: 64px !important;
  min-height: 805px;
  display: inline-block;
  overflow: hidden;
}
.uncollapsed {
  width: 300px !important;
  min-height: 805px;
  display: inline-block;
  overflow: hidden;
}

.el-menu-vertical-demo:not(.el-menu--collapse) {
  width: 300px;
  min-height: 800px;
  text-align: left;
  display: inline-block;
  overflow: hidden;
}

.el-header {
  background-color: #b3c0d1;
  color: #333;
  line-height: 60px;
}

.el-aside {
  color: #333;
}

.el-dropdown-link {
  cursor: pointer;
  color: #409eff;
}

.el-icon-arrow-down {
  font-size: 12px;
}
</style>
