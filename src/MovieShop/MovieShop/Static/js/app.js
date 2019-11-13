var app6 = new Vue({
    el: '#app-6',
    data: {
        query: 'Hello Vue!',
        matches: []
    },
    watch: {
        // whenever question changes, this function will run
        query: async function (newQuestion, oldQuestion) {
            const response = await fetch('/autocomplete?q=' + newQuestion);
            this.matches = await response.json();
            console.log(JSON.stringify(myJson));
        }
    }
})