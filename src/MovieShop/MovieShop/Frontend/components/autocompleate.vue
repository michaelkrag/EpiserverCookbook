<template>
    <div class="autocomplete">
        <input type="text" placeholder="Search from vue" v-model="query" @keydown.enter='enter' @keydown.down='down' @keydown.up='up' @input='change' />
        <div class="autocomplete-items">
            <div class="item-text" v-for="(suggestion, index) in suggestions" v-bind:class="{'active': isActive(index)}" @click="suggestionClick(index)">
                <a href="#">{{ suggestion }}</a>
            </div>
        </div>
    </div>
</template>

<script>
    module.exports = {
        props: {
            stratQuery: {
                type: String,
                default: ''
            }
        },
        data: function () {
            return {
                suggestions: [],
                open: false,
                current: -1,
                query: this.stratQuery
            };
        },
        watch: {
            query: async function (newQuestion, oldQuestion) {
                console.log(newQuestion);
                const response = await fetch('/autocomplete?q=' + newQuestion);
                this.suggestions = await response.json();
            }
        },
        methods: {
            //When enter pressed on the input
            enter() {
                if (this.current === -1) {
                    window.location.href = 'http://localhost:62432/en/search?q=' + this.query;
                    return;
                }
                else {
                    this.query = this.suggestions[this.current];
                    this.open = false;
                    this.current = -1;
                }
            },

            //When up pressed while suggestions are open
            up() {
                if (this.current > -1)
                    this.current--;
            },

            //When up pressed while suggestions are open
            down() {
                if (this.current < this.suggestions.length - 1)
                    this.current++;
            },

            //For highlighting element
            isActive(index) {
                return index === this.current;
            },

            //When the user changes input
            change() {
                if (this.open == false) {
                    this.open = true;
                    this.current = -1;
                }
            },

            //When one of the suggestion is clicked
            suggestionClick(index) {
                this.query = this.suggestions[index];
                this.open = false;
            },
        }
        //node_modules\.bin\webpack
    };
</script>

<style scoped>
    .active {
        background-color: #e9e9e9;
    }

    .autocomplete {
        position: relative;
        display: inline-block;
    }

    .autocomplete-items {
        position: absolute;
        border: 1px solid #d4d4d4;
        border-bottom: none;
        border-top: none;
        z-index: 99;
        /*position the autocomplete items to be the same width as the container:*/
        top: 100%;
        left: 0;
        right: 0;
        background: whitesmoke;
    }

        .autocomplete-items div {
            padding: 10px;
            cursor: pointer;
            /*    background-color: #fff;*/
            border-bottom: 1px solid #d4d4d4;
        }

            /*when hovering an item:*/
            .autocomplete-items div:hover {
                background-color: #e9e9e9;
            }
</style>