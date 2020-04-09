async function add(sku, quantity) {
    const url = window.appConfig.cartApiUrl + '?sku=' + sku + '&quantity=' + quantity;
    const response = await fetch(url, { method: 'POST' });

    alert('add ' + response);
}

function remove() {
    alert('delete');
}

export { add, remove }; // a list of exported variables