import Vue from 'vue';
export default {
    props: {
        suggestions: {
            type: Array,
            required: true
        },
        selection: {
            type: String,
            required: true,
            twoWay: true
        }
    }
    /**
     * More to come here
     */
}