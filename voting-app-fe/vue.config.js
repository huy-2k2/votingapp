const { defineConfig } = require('@vue/cli-service');
module.exports = defineConfig({
  transpileDependencies: true,

  devServer: {
    port: 5500, // Đặt cổng bạn muốn sử dụng, ví dụ: 3000
  },

  //publicPath: process.env.NODE_ENV !== 'development' ? 'CONFIG_CDN_URL' : '/',
  publicPath: process.env.NODE_ENV !== 'development' ? 'http://votingappcdn.com:5678' : '/',
  lintOnSave: false,

  pluginOptions: {
    vuetify: {
			// https://github.com/vuetifyjs/vuetify-loader/tree/next/packages/vuetify-loader
		}
  }
});
