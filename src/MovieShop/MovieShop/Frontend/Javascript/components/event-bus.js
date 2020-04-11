console.log('bus a');
//https://alligator.io/vuejs/global-event-bus/
//https://blog.logrocket.com/using-event-bus-in-vue-js-to-pass-data-between-components/
import Vue from 'vue';
const bus = new Vue();
export default bus;
console.log('bus b');