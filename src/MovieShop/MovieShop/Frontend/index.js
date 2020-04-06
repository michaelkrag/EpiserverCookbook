import Vue from 'vue';

import autocompleate from './components/autocompleate.vue';
import addToBasket from './components/add-to-basket.vue';

new Vue({
    el: "#app",
    components: { autocompleate, addToBasket }
});