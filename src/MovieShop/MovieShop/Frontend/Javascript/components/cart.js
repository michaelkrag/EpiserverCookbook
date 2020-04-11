console.log('cart.js a');
import bus from './event-bus.js';

export async function add(sku, quantity) {
    const url = window.appConfig.cartApiUrl + '?sku=' + sku + '&quantity=' + quantity;
    const response = await fetch(url, { method: 'POST' })
                                .then((response) => {
                                    return response.json();
                                })
                                .then((data) => {
                                    return data;
                                });

    bus.$emit('ProductAdded', response.QuantityAdded);
}

export async function quantity() {
    const url = window.appConfig.cartApiUrl + '/quantity';
    var result = await fetch(url)
        .then((response) => {
            return response.json();
        })
        .then((data) => {
            return data;
        });
    const rtn = result.quantity;
    return rtn;
}
export function remove() {
    alert('delete');
}
console.log('cart.js b');
//export { add, remove, quantity }; // a list of exported variables