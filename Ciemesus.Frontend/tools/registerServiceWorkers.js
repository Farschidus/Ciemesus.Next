/* eslint-disable */
import sw from './sw';

export default function registerServiceWorkers() {
  if ('serviceWorker' in navigator) {
    navigator.serviceWorker.register(sw)
         .then((registration) => {
            console.log('Service Worker registration successful with scope: ', registration.scope);
         })
         .catch((err) => {
            console.log('Service Worker registration failed: ', err);
         });
   }
}
