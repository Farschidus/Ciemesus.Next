/* eslint-disable */
/**
 * Welcome to your Workbox-powered service worker!
 *
 * You'll need to register this file in your web app and you should
 * disable HTTP caching for this file too.
 * See https://goo.gl/nhQhGp
 *
 * The rest of the code is auto-generated. Please don't update this file
 * directly; instead, make changes to your Workbox build configuration
 * and re-run your build process.
 * See https://goo.gl/2aRDsh
 */

importScripts('https://storage.googleapis.com/workbox-cdn/releases/3.0.1/workbox-sw.js');

/**
 * The workboxSW.precacheAndRoute() method efficiently caches and responds to
 * requests for URLs in the manifest.
 * See https://goo.gl/S9QRab
 */
self.__precacheManifest = [
   {
      url: 'assets/css/base.css',
      revision: '326d198e40600f5e7a5b44adcff74f73',
   },
   {
      url: 'assets/css/style.css',
      revision: 'bee33ace2e0e09d254eb99ed974397b8',
   },
   {
      url: 'assets/images/icons/icon.png',
      revision: '6dd81332b19f350f8376e8432bd8a44b',
   },
   {
      url: 'assets/images/icons/large-icon.png',
      revision: '71e8fbf5b831d5e22981e47fd26dfc66',
   },
   {
      url: 'components/App/index.js',
      revision: '60ba30dab8878a5553b28206b972c93a',
   },
   {
      url: 'index.ejs',
      revision: '06a9f940bee692ba0606a4ff0cb887a2',
   },
   {
      url: 'index.js',
      revision: '6dc6fd8527a0ff22775943ab5fe7ac84',
   },
].concat(self.__precacheManifest || []);
workbox.precaching.suppressWarnings();
workbox.precaching.precacheAndRoute(self.__precacheManifest, {});
