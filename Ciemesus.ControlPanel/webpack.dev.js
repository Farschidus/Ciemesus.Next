/* eslint-disable */
import webpack from 'webpack';
import merge from 'webpack-merge';
import common from './webpack.common';

module.exports = merge(common, {
   watch: true,
   mode: 'development',
   devtool: 'eval-source-map',
   plugins: [
      new webpack.HotModuleReplacementPlugin(),
   ]
});
