/* eslint-disable */
import webpack from 'webpack';
import merge from 'webpack-merge';
import common from './webpack.common';
import workboxPlugin from 'workbox-webpack-plugin';

module.exports = merge(common, {
   watch: true,
   mode: 'development',
   devtool: 'eval-source-map',
   plugins: [
      new webpack.HotModuleReplacementPlugin(),
      new workboxPlugin.GenerateSW({
        swDest: 'sw.js',
        clientsClaim: true,
        skipWaiting: true
      })
   ]
});
