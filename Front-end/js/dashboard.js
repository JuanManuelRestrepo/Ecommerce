// Función para cargar las categorías como tarjetas
function cargarCategorias() {
    fetch("https://localhost:57199/api/Categoria/GetAllCategoria")
        .then(response => response.json())
        .then(data => {
            const categoriesContainer = document.getElementById('categories-container');
            categoriesContainer.innerHTML = ''; // Limpiar el contenedor antes de agregar las nuevas tarjetas

            data.forEach(categoria => {
                // Crear la tarjeta para cada categoría
                const categoryCard = document.createElement('div');
                categoryCard.classList.add('category-card');
                categoryCard.dataset.id = categoria.id; // Agregar el ID de la categoría como un atributo

                // Crear el contenido de la tarjeta
                categoryCard.innerHTML = `
                    <div class="category-title">${categoria.nombre}</div>
                `;

                // Agregar un evento al hacer clic en la tarjeta
                categoryCard.addEventListener('click', function() {
                    // Redirigir al usuario a la página de productos con el id de la categoría
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

// Función para cargar los productos en la sección de productos
function cargarProductos() {
    fetch("https://localhost:57199/api/Producto")
        .then(response => response.json())
        .then(data => {
            const productsContainer = document.getElementById('products-container');
            productsContainer.innerHTML = ''; // Limpiar el contenedor antes de agregar los nuevos productos

            data.forEach(producto => {
                // Crear la tarjeta de producto
                const productCard = document.createElement('div');
                productCard.classList.add('product-item');
                productCard.dataset.id = producto.id; // Agregar el ID del producto como un atributo

                // Crear el contenido de la tarjeta de producto
                productCard.innerHTML = `
                    <div class="product-title">${producto.nombre}</div>
                    <div class="product-description">${producto.descripcion}</div>
                    <div class="product-price">$${producto.precio}</div>
                    <button class="add-to-cart-btn">Agregar al carrito</button>
                `;

                // Agregar el evento para agregar el producto al carrito
                productCard.querySelector('.add-to-cart-btn').addEventListener('click', function() {
                    agregarAlCarrito(producto);
                });

                // Agregar la tarjeta de producto al contenedor
                productsContainer.appendChild(productCard);
            });
        })
        .catch(error => {
            console.error("Error al cargar los productos:", error);
        });
}

// Función para agregar un producto al carrito
function agregarAlCarrito(producto) {
    // Obtener el carrito del almacenamiento local o inicializar uno vacío si no existe
    let carrito = JSON.parse(localStorage.getItem('carrito')) || [];

    // Verificar si el producto ya está en el carrito
    const productoExistente = carrito.find(item => item.id === producto.id);
    if (productoExistente) {
        productoExistente.cantidad++;
    } else {
        carrito.push({ ...producto, cantidad: 1 });
    }

    // Guardar el carrito actualizado en el almacenamiento local
    localStorage.setItem('carrito', JSON.stringify(carrito));

    // Mostrar un mensaje de confirmación al usuario
    alert(`${producto.nombre} ha sido agregado al carrito.`);


}

const logoutBtn = document.getElementById('logout-btn');
if (logoutBtn) {
    logoutBtn.addEventListener('click', function() {
        sessionStorage.removeItem('usuarioActual');
        sessionStorage.removeItem('isLoggedIn');
        window.location.href = 'index.html';
    });
}

// Llamada inicial a las funciones para cargar categorías y productos
cargarCategorias();
cargarProductos();
