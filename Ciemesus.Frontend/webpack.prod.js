/* eslint-disable */
import webpack from 'webpack';
import merge from 'webpack-merge';
import common from './webpack.common';
import WebpackMd5Hash from 'webpack-md5-hash';
import UglifyJsPlugin from 'uglifyjs-webpack-plugin';
import CleanWebpackPlugin from 'clean-webpack-plugin';

module.exports = merge(common, {
   watch: false,
   mode: 'production',
   plugins: [
      new CleanWebpackPlugin(['dist']),
      new WebpackMd5Hash(), // Hash the files using MD5 so that their names change when the content changes.
      // ensure that we get a production build of any dependencies
      // this is primarily for React, where this removes 179KB from the bundle
      new webpack.DefinePlugin({
         'process.env.NODE_ENV': JSON.stringify('production')
      })
   ]
});

// import UglifyJsPlugin from 'uglifyjs-webpack-plugin';
//   new UglifyJsPlugin({
//      test: /\.js($|\?)/i,
//      cache: true,
//      parallel: true,
//      sourceMap: true,
//      uglifyOptions: {
//         compress: false,
//         ecma: 6,
//         mangle: true,
//      },
//   }),


// optimization: {
//     minimizer: [
//        new UglifyJsPlugin({
//           test: /\.js($|\?)/i,
//           cache: true,
//           parallel: true,
//           uglifyOptions: {
//              compress: true,
//              ecma: 6,
//              mangle: true
//           },
//           sourceMap: false
//        })
//     ]
//  }
