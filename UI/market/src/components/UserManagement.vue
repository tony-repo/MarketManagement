<template>
  <div v-cloak>
    <el-container style="height: 750px">
      <el-main>
        <el-table :data="tableData" v-cloak>
          <el-table-column prop="firstName" label="First Name" width="120">
          </el-table-column>
          <el-table-column prop="lastName" label="Last Name" width="120">
          </el-table-column>
          <el-table-column prop="userName" label="Email" width="120">
          </el-table-column>
          <el-table-column prop="phone" label="Phone"> </el-table-column>
          <el-table-column prop="organizationName" label="Organization">
          </el-table-column>
          <el-table-column fixed="right" label="Operations" width="100">
            <template slot-scope="scope">
              <el-button
                @click="handleClick(scope.row)"
                type="text"
                size="small"
                >Edit</el-button
              >
              <el-button type="text" size="small">Delete</el-button>
            </template>
          </el-table-column>
        </el-table>
      </el-main>
      <el-footer>
        <div class="block" style="float: right">
          <el-pagination
            @size-change="handleSizeChange"
            @current-change="handleCurrentChange"
            :current-page="currentPage"
            :page-sizes="[20, 50, 100]"
            :page-size="10"
            layout="total, sizes, prev, pager, next, jumper"
            :total="total()"
          >
          </el-pagination>
        </div>
      </el-footer>
    </el-container>
  </div>
</template>

<script>
export default {
  name: "UserManagement",
  data() {
    return {
      currentPage: 1,
      tableData: [],
    };
  },
  computed: {},
  created: function () {
    this.loadData();
  },
  methods: {
    loadData() {
      this.$axios.get("Users").then((response) => {
        this.tableData = response.data;
      });
    },
    total() {
      return this.tableData.length;
    },
    handleSizeChange(val) {
      console.log(`每页 ${val} 条`);
    },
    handleCurrentChange(val) {
      console.log(`当前页: ${val}`);
    },
  },
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
[v-cloak] {
  display: none;
}

h3 {
  margin: 40px 0 0;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>
