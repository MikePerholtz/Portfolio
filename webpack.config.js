let path = require("path");
const VueLoaderPlugin = require('vue-loader/lib/plugin')


module.exports = {
  mode: 'development',
  entry: "./wwwroot/js/site.js",
  output: {
    filename: "app.js",
    path: path.resolve("./wwwroot/dist")
  },
  // mPerholtz webpack output will be a resolved path to 
  // iis' wwwroot/dist folder, all code in a file called app.js
  devtool: "source-map",
  // mPerholtz setup debugging in webpack with source map
  module: {
    rules: [
      {
        test: /\.vue$/,
        exclude: "/node_modules/",
        loader: "vue-loader"
      },
      // this will apply to both plain `.js` files
      // AND `<script>` blocks in `.vue` files
      {
        test: /\.js$/,
        exclude: "/node_modules/",
        loader: "babel-loader",
        options: {
          presets: ["env"]
        }
      },
      // mPerholtz this will apply to both plain `.css` files
      // AND `<style>` blocks in `.vue` files
      {
        test: /\.css$/,
        exclude: "/node_modules/",
        use: [
          "vue-style-loader",
          "css-loader"
        ]
      }
    ]
  },
  plugins: [
    // mPerholtz make sure to include the plugin for the magic
    new VueLoaderPlugin()
  ]
}