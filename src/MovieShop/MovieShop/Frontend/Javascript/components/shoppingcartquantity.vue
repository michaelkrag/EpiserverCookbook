<template>
    <div v-if="quantity>0">
        {{quantity}}
    </div>
</template>

<script>
    console.log('shoppingcartquantity a');
    var cart = require('./cart.js');
    import bus from './event-bus.js';
    export default {
        data () {
            return {
                quantity: 0
            };
        },
        created() {
            bus.$on('ProductAdded', (data) => {
                this.quantity += data;
            })
        },
        async mounted() {
            const test = await cart.quantity();
            this.quantity = test;
        }
    };
    console.log('shoppingcartquantity b');
</script>

<style scoped>
    
</style>