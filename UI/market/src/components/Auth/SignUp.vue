<template>
  <div>
    <div class="header">
      <img src="../../assets/logo.png" class="logo" alt="logo" />
    </div>
    <div class="signup">
      <el-card>
        <el-form
          class="signup-form"
          :model="model"
          :rules="rules"
          ref="form"
          @submit.native.prevent="signup"
        >
          <el-form-item prop="firstName">
            <el-input
              v-model="model.firstName"
              placeholder="First Name"
              prefix-icon="fas fa-lock"
            ></el-input>
          </el-form-item>
          <el-form-item prop="lastName">
            <el-input
              v-model="model.lastName"
              placeholder="Last Name"
              prefix-icon="fas fa-lock"
            ></el-input>
          </el-form-item>
          <el-form-item prop="organizationName">
            <el-input
              v-model="model.organizationName"
              placeholder="Organization Name"
              prefix-icon="fas fa-lock"
            ></el-input>
          </el-form-item>
          <el-form-item prop="username">
            <el-input
              v-model="model.username"
              placeholder="Email"
              prefix-icon="fas fa-user"
            ></el-input>
          </el-form-item>
          <el-form-item prop="phone">
            <el-input
              v-model="model.phone"
              placeholder="Phone"
              prefix-icon="fas fa-user"
            ></el-input>
          </el-form-item>
          <el-form-item prop="password">
            <el-input
              v-model="model.password"
              placeholder="Password"
              type="password"
              prefix-icon="fas fa-lock"
            ></el-input>
          </el-form-item>
          <el-form-item>
            <el-button
              :loading="loading"
              class="signup-button"
              type="primary"
              native-type="submit"
              block
              >Sign Up</el-button
            >
          </el-form-item>
          <el-form-item>
            <router-link :to="{ path: '/Login/Jwt Login' }">Login</router-link>
          </el-form-item>
        </el-form>
      </el-card>
    </div>
    <div class="footer">
      <div class="version">Version 1.0.1</div>
    </div>
  </div>
</template>

<script>
export default {
  name: "signUp",
  data() {
    return {
      validCredentials: {
        username: "lightscope",
        password: "lightscope",
      },
      model: {
        username: "",
        password: "",
        firstName: "",
        lastName: "",
        organizationName: "",
        phone: "",
      },
      loading: false,
      rules: {
        username: [
          {
            required: true,
            message: "Username is required",
            trigger: "blur",
          },
          {
            min: 4,
            message: "Username length should be at least 5 characters",
            trigger: "blur",
          },
        ],
        password: [
          { required: true, message: "Password is required", trigger: "blur" },
          {
            min: 5,
            message: "Password length should be at least 5 characters",
            trigger: "blur",
          },
        ],
      },
    };
  },
  methods: {
    async signup() {
      let valid = await this.$refs.form.validate();
      if (!valid) {
        return;
      }
      try {
        var response = await this.$axios.post("/Authentication/signUp", {
          username: this.model.username,
          password: this.model.password,
          firstName: this.model.firstName,
          lastName: this.model.lastName,
          organizationName: this.model.organizationName,
        });

        if (response.status == 200) {
          this.$router.push({
            path: "/Login/Jwt Login",
            data: {
              status: "success",
            },
          });
        } else {
          this.$message.error("Sign up failed");
        }
      } catch (error) {
        this.$message.error(error);
      }
    },
  },
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.signup {
  flex: 1;
  display: flex;
  justify-content: center;
  align-items: center;
}

.signup-button {
  width: 100%;
  margin-top: 40px;
}
.signup-form {
  width: 290px;
}
.forgot-password {
  margin-top: 10px;
}
</style>
<style lang="scss">
$teal: rgb(0, 124, 137);
.el-button--primary {
  background: $teal;
  border-color: $teal;

  &:hover,
  &.active,
  &:focus {
    background: lighten($teal, 7);
    border-color: lighten($teal, 7);
  }
}
.signup .el-input__inner:hover {
  border-color: $teal;
}
.signup .el-input__prefix {
  background: rgb(238, 237, 234);
  left: 0;
  height: calc(100% - 2px);
  left: 1px;
  top: 1px;
  border-radius: 3px;
  .el-input__icon {
    width: 30px;
  }
}
.signup .el-input input {
  padding-left: 35px;
}
.signup .el-card {
  padding-top: 0;
  padding-bottom: 30px;
}
h2 {
  font-family: "Open Sans";
  letter-spacing: 1px;
  font-family: Roboto, sans-serif;
  padding-bottom: 20px;
}
a {
  color: $teal;
  text-decoration: none;
  &:hover,
  &:active,
  &:focus {
    color: lighten($teal, 7);
  }
}
.signup .el-card {
  width: 340px;
  display: flex;
  justify-content: center;
}
</style>
