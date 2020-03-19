var app6 = new Vue({
    el: '#app-6',
    data: {
        query: '',
        matches: []
    },
    watch: {
        query: async function (newQuestion, oldQuestion) {
            const response = await fetch('/autocomplete?q=' + newQuestion);
            this.matches = await response.json();
            console.log(JSON.stringify(myJson));
        }
    }
});