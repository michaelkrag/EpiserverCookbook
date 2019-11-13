/*
 * export default {
    name: 'mycompont',
    props: {
    },
    data() {
        return {
        };
    },
    computed: {
    },
    mounted() {
    },
    methods: {
    }
};
*/
var app6 = new Vue({
    el: '#app-6',
    data: {
        query: 'Hello Vue!',
        citys: ['Bangalore', 'Chennai', 'Cochin', 'Delhi', 'Kolkata', 'Mumbai'],
        matches: []
    },
    watch: {
        // whenever question changes, this function will run
        query: function (newQuestion, oldQuestion) {
            this.matches = this.citys.filter(word => word.startsWith(newQuestion));
            console.log("n=" + newQuestion + "    old=" + oldQuestion);
        }
    }
})