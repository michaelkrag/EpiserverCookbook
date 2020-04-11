import Vue from 'vue';
import autocompleate from './Javascript/components/autocompleate.vue';
import addToBasket from './Javascript/components/add-to-basket.vue';
import shoppingcartquantity from './Javascript/components/shoppingcartquantity.vue';

import './sass/styles.scss';

new Vue({
    el: "#app",
    components: { autocompleate, addToBasket, shoppingcartquantity }
});