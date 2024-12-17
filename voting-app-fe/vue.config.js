const { defineConfig } = require('@vue/cli-service');
module.exports = defineConfig({
  transpileDependencies: true,

  devServer: {
    port: 5500, // Đặt cổng bạn muốn sử dụng, ví dụ: 3000
  },

  //publicPath: process.env.NODE_ENV !== 'development' ? 'CONFIG_CDN_URL' : '/',
  publicPath: process.env.NODE_ENV !== 'development' ? 'CDN_URL_REPLACE' : '/',
  lintOnSave: false,
  productionSourceMap: false,

  pluginOptions: {
    vuetify: {
			// https://github.com/vuetifyjs/vuetify-loader/tree/next/packages/vuetify-loader
		}
  }
});
