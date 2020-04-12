<template>
    <a href="">
        <div v-if="quantity>0">
            {{quantity}}
        </div>
        <!--    <div>
                <svg xmlns="http://www.w3.org/2000/svg" width="300" height="300" version="1.1">
                    <line x1="0" y1="10" x2="30" y2="20" style="stroke:rgb(192,192,192);stroke-width:10" />
                    <line x1="30" y1="20" x2="80" y2="100" style="stroke:rgb(192,192,192);stroke-width:10" />
                    <line x1="80" y1="100" x2="250" y2="100" style="stroke:rgb(192,192,192);stroke-width:10" />
                    <line x1="250" y1="100" x2="300" y2="20" style="stroke:rgb(192,192,192);stroke-width:10" />
                    <circle cx="110" cy="130" r="20" fill="rgb(192,192,192)" />
                    <circle cx="210" cy="130" r="20" fill="rgb(192,192,192)" />
                </svg>
            </div>-->
    </a>
</template>

<script>
    var cart = require('./cart.js');
    export default {
        data() {
            return {
                quantity: 0
            };
        },
        async created() {

        },
        async mounted() {
            const test = await cart.quantity();
            this.quantity = test;
        }
    };
</script>

<style scoped>
</style>