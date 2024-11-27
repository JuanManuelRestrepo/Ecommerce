// Variables globales
const carrito = JSON.parse(localStorage.getItem('carrito')) || [];

// Función para cargar categorías como tarjetas
function cargarCategorias() {
    fetch("https://localhost:57199/api/Categoria/GetAllCategoria")
        .then(response => response.json())
        .then(data => {
            const categoriesContainer = document.getElementById('categories-container');
            if (!categoriesContainer) return;  // Verificar si el contenedor existe
            categoriesContainer.innerHTML = ''; // Limpiar el contenedor antes de agregar las nuevas tarjetas

            // Crear tarjetas para cada categoría
            data.forEach(categoria => {
                const categoryCard = document.createElement('div');
                categoryCard.classList.add('category-card');
                categoryCard.dataset.id = categoria.id; // Agregar el ID de la categoría como un atributo

                categoryCard.innerHTML = `
                    <div class="category-title">${categoria.nombre}</div>
                `;

                // Evento al hacer clic para redirigir a la página de productos con el id de la categoría
                categoryCard.addEventListener('click', function () {
                    window.location.href = `ProducCategoria.html?categoriaId=${categoria.id}`;
                });

                // Agregar la tarjeta al contenedor
                categoriesContainer.appendChild(categoryCard);
            });
        })
        .catch(error => {
            console.error("Error al cargar las categorías:", error);
        });
}

// Función para cargar los productos
function cargarProductos() {
    fetch("https://localhost:57199/api/Producto")
        .then(response => response.json())
        .then(data => {
            const productsContainer = document.getElementById('products-container');
            if (!productsContainer) return;  // Verificar si el contenedor existe
            productsContainer.innerHTML = ''; // Limpiar el contenedor antes de agregar los nuevos productos

            // Crear tarjetas para cada producto
            data.forEach(producto => {
                const productCard = document.createElement('div');
                productCard.classList.add('product-item');
                productCard.dataset.id = producto.id; // Agregar el ID del producto como un atributo

                productCard.innerHTML = `
                    <div class="product-title">${producto.nombre}</div>
                    <div class="product-description">${producto.descripcion}</div>
                    <div class="product-price">$${producto.precio.toFixed(2)}</div>
                    <button class="add-to-cart-btn">Agregar al carrito</button>
                `;

                // Evento para agregar al carrito
                productCard.querySelector('.add-to-cart-btn').addEventListener('click', function () {
                    agregarAlCarrito(producto);
                });

                // Agregar la tarjeta al contenedor
                productsContainer.appendChild(productCard);
            });
        })
        .catch(error => {
            console.error("Error al cargar los productos:", error);
        });
}

// Función para agregar un producto al carrito
function agregarAlCarrito(producto) {
    const productoExistente = carrito.find(item => item.id === producto.id);

    if (productoExistente) {
        // Si ya existe, incrementamos la cantidad
        productoExistente.cantidad++;
    } else {
        // Si no existe, lo agregamos al carrito
        carrito.push({ ...producto, cantidad: 1 });
    }

    // Guardar en localStorage
    localStorage.setItem('carrito', JSON.stringify(carrito));

    // Mostrar mensaje de éxito
    alert(`${producto.nombre} se agregó al carrito.`);
}

// Función para cargar el carrito en carrito.html
function cargarCarrito() {
    const cartItems = JSON.parse(localStorage.getItem('carrito')) || [];
    const cartContainer = document.getElementById('cart-container');
    const cartTotal = document.getElementById('cart-total');

    if (!cartContainer || !cartTotal) return;  // Verificar si los contenedores existen
    cartContainer.innerHTML = ''; // Limpiar contenido anterior
    let total = 0;

    // Mostrar los productos del carrito
    cartItems.forEach(item => {
        const cartItemDiv = document.createElement('div');
        cartItemDiv.classList.add('cart-item');

        cartItemDiv.innerHTML = `
            <span class="cart-item-title">${item.nombre}</span>
            <span class="cart-item-quantity">Cantidad: ${item.cantidad}</span>
            <span class="cart-item-price">$${(item.precio * item.cantidad).toFixed(2)}</span>
            <button class="remove-from-cart-btn">Eliminar</button>
        `;

        // Evento para eliminar el producto del carrito
        cartItemDiv.querySelector('.remove-from-cart-btn').addEventListener('click', () => {
            eliminarDelCarrito(item.id);
        });

        total += item.precio * item.cantidad; // Calcular el total
        cartContainer.appendChild(cartItemDiv);
    });

    // Mostrar total
    cartTotal.textContent = total.toFixed(2);
}

// Función para eliminar un producto del carrito
function eliminarDelCarrito(productId) {
    const indice = carrito.findIndex(item => item.id === productId);
    if (indice !== -1) {
        carrito.splice(indice, 1); // Eliminar el producto del carrito
        localStorage.setItem('carrito', JSON.stringify(carrito)); // Actualizar localStorage
        cargarCarrito(); // Recargar el carrito
    }
}

// Función para realizar el pedido
function realizarPedido() {
    const cartItems = JSON.parse(localStorage.getItem('carrito')) || [];
    const usuarioSesion = JSON.parse(sessionStorage.getItem('usuarioActual')); // Obtener datos del usuario desde sessionStorage
    const usuarioId = usuarioSesion ? usuarioSesion.id : null; // Obtener el usuarioId
    console.log(usuarioId)
    if (!usuarioId) {
        alert("Debes iniciar sesión para realizar un pedido.");
        return;
    }

    const total = cartItems.reduce((acc, item) => acc + item.precio * item.cantidad, 0);

    const pedidoDto = {
        usuarioId: usuarioId, // Usar el usuarioId desde sessionStorage
        estado: "Pendiente", // Estado inicial
        total: total
    };


    console.log(pedidoDto)

    fetch('https://localhost:57199/api/Pedido', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(pedidoDto)
    })
    .then(response => response.json())
    .then(data => {
        if (data.usuarioId) {
            alert("Pedido realizado con éxito");
            localStorage.removeItem('carrito'); // Limpiar el carrito después de realizar el pedido
            window.location.href = 'pedidos.html'; // Redirigir a pedidos.html
        } else {
            alert("Hubo un error al realizar el pedido. Intente nuevamente.");
        }
    })
    .catch(error => {
        console.error("Error al realizar el pedido:", error);
        alert("Hubo un error al procesar el pedido. Intente más tarde.");
    });
}



const logoutBtn = document.getElementById('logout-btn');
if (logoutBtn) {
    logoutBtn.addEventListener('click', function() {
        sessionStorage.removeItem('usuarioActual');
        sessionStorage.removeItem('isLoggedIn');
        window.location.href = 'index.html';
    });
}


// Asignar el evento al botón de realizar pedido
document.getElementById('place-order-btn').addEventListener('click', realizarPedido);

// Llamadas iniciales cuando se carga el DOM
document.addEventListener('DOMContentLoaded', () => {
    const currentPage = window.location.pathname.split('/').pop();
    if (currentPage === 'index.html') {
        cargarCategorias();
        cargarProductos();
    } else if (currentPage === 'carrito.html') {
        cargarCarrito();
    }
});
