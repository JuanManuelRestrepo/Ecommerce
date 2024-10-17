let cart = [];

document.querySelectorAll('.add-to-cart').forEach(button => {
    button.addEventListener('click', () => {
        const productElement = button.parentElement;
        const productId = productElement.getAttribute('data-id');
        const productName = productElement.querySelector('h3').innerText;
        const productPrice = parseFloat(productElement.querySelector('.price').innerText.replace('$', ''));

        const product = { id: productId, name: productName, price: productPrice };
        cart.push(product);
        updateCartCount();
    });
});

function updateCartCount() {
    const cartButton = document.getElementById('cart-button');
    cartButton.innerText = `Carrito (${cart.length})`;
}
