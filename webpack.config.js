let path = require("path");
const VueLoaderPlugin = require('vue-loader/lib/plugin')

// mPerholtz webpack output will be a resolved path to 
// iis' wwwroot/dist folder, all code in a file called app.js
module.exports = {
  mode: 'development',
  entry: { "main": "./wwwroot/js/site.js" },
  output: {
    filename: "app.js",
    path: path.resolve("./wwwroot/dist"),
    publicPath: '/dist/'
  },

  // mPerholtz Browser Console warning:
  //  * [Vue warn]: You are using the runtime-only build of Vue where the template compiler is not available. 
  //  * Either pre-compile the templates into render functions, or use the compiler-included build.
  // So fix below is if using <template> in Vue:
  // https://vuejs.org/v2/guide/installation.html#Runtime-Compiler-vs-Runtime-only
  resolve: {
      alias: {
          'vue$': 'vue/dist/vue.esm.js'
      },
      extensions: ['*', '.js', '.vue', '.json']
  },
  // mPerholtz setup debugging in webpack with source map
  devtool: "source-map",
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