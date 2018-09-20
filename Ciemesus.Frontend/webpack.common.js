/* eslint-disable */
import path from 'path';
import webpack from 'webpack';
import CopyWebpackPlugin from 'copy-webpack-plugin';
import HtmlWebpackPlugin from 'html-webpack-plugin';
import WebpackPwaManifest from 'webpack-pwa-manifest';
import swConfig from './workbox-config';

const paths = {
   DIST: path.resolve(__dirname, 'dist'),
   SRC: path.resolve(__dirname, 'src'),
   TOOL: path.resolve(__dirname, 'tools'),
};

module.exports = {
   target: 'web',
   entry: [
      'babel-polyfill',
      'whatwg-fetch',
      'webpack-hot-middleware/client?reload=true',
      path.join(paths.SRC, 'index.js'),
      path.join(paths.TOOL, 'webpack-public-path.js')
   ],
   output: {
      path: paths.DIST,
      publicPath: '/',
      filename: 'bundle.js'
   },
   resolve: {
      extensions: ['*', '.js', '.jsx', '.json'],
      modules: ['./node_modules']
   },
   plugins: [
      new CopyWebpackPlugin([
        {
          from: 'src/clientImages/',
          to: 'clientImages/',
          toType: 'dir'
        }
      ]),
      new webpack.NoEmitOnErrorsPlugin(),
      new HtmlWebpackPlugin({
        template: path.join(paths.SRC, 'index.ejs'),
        inject: true,
        minify: {
            removeComments: true,
            collapseWhitespace: false
        }
      }),
      new WebpackPwaManifest({
        filename: "manifest.json",
        name: 'Fika',
        short_name: 'Fika',
        description: 'Fika for Svenska!',
        theme_color: "#ffffff",
        background_color: "#000000",
        display: "standalone",
        start_url: "/",
        ios: true,
        splash_pages: null,
        icons: [
          {
            src: path.resolve('src/assets/images/icons/icon.png'),
            sizes: [72, 96, 128, 144, 152, 192, 256, 384, 512] // multiple sizes
          },
          {
            src: path.resolve('src/assets/images/icons/large-icon.png'),
            size: '1024x1024' // you can also use the specifications pattern
          }
        ]
      })
   ],
   module: {
      rules: [
        { test: /\.(js|jsx)$/, exclude: /node_modules/, use: ['babel-loader'] },
        { test: /\.css$/, use: ['style-loader', 'css-loader'] },
        { test: /\.(png|svg|jpg|gif)$/, use: [ {
          loader: 'file-loader',  
          options: {
            name: '[name].[ext]',
          }
        }] },
        { test: /\.(woff|woff2|eot|ttf|otf)$/, use: ['file-loader'] },
      ]
   }
};
