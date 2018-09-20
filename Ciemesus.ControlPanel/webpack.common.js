/* eslint-disable */
import path from 'path';
import webpack from 'webpack';
import HtmlWebpackPlugin from 'html-webpack-plugin';

const paths = {
   DIST: path.resolve(__dirname, 'dist'),
   SRC: path.resolve(__dirname, 'src')
};

module.exports = {
   target: 'web',
   entry: [
      'babel-polyfill',
      'whatwg-fetch',
      'webpack-hot-middleware/client?reload=true',
      path.join(paths.SRC, 'index.js')
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
      new webpack.NoEmitOnErrorsPlugin(),
      new HtmlWebpackPlugin({
        template: path.join(paths.SRC, 'index.ejs'),
        inject: true,
        minify: {
            removeComments: true,
            collapseWhitespace: false
        }
      }),
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
