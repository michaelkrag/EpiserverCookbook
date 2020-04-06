<template>
    <div>
        <button class="btn" :title="hoverText" v-on:click="greet"><i class="fas fa-shopping-cart"></i> {{buttenLabel}}</button>
    </div>    
</template>

<script>
    module.exports = {
        props: {
            buttenLabel: {
                type: String,
                default: 'Add to cart'
            },
            hoverText: {
                type: String,
                default: 'Select <b>__dims__</b> from the left<br> to add to Shopping Cart'
            },
            sku:{
                type: String,

            }
        },
        methods: {
            greet: function (event) {
                alert('Hello ' + this.sku+ '!')
                if (event) {
                    alert(event.target.tagName)
                }

            }
        }
    };
</script>

<style scoped>    
    /* Style buttons */
    .btn {
      background-color: DodgerBlue; /* Blue background */
      border: none; /* Remove borders */
      color: white; /* White text */
      padding: 12px 16px; /* Some padding */
      font-size: 16px; /* Set a font size */
      cursor: pointer; /* Mouse pointer on hover */
    }

    /* Darker background on mouse-over */
    .btn:hover {
      background-color: RoyalBlue;
    }    
</style>