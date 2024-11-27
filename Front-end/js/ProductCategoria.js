// Función para obtener el parámetro de la URL
function getUrlParameter(name) {
    const urlParams = new URLSearchParams(window.location.search);
    return urlParams.get(name);
}

// Función para cargar los productos de una categoría específica
function cargarProductosPorCategoria(categoriaId) {
    // Usamos GET y pasamos el categoriaId como parámetro en la URL
    fetch(`https://localhost:57199/api/Producto/GetproductoCategoria?categoriaId=${categoriaId}`, {
        method: "GET",  // Asegúrate de usar GET
        headers: {
            "Content-Type": "application/json"  // El encabezado Content-Type sigue siendo necesario, aunque no enviamos cuerpo.
        }
    })
    .then(response => response.json())  // Procesamos la respuesta como JSON
    .then(data => {
        const productsContainer = document.getElementById('products-container');
        productsContainer.innerHTML = '';  // Limpiar el contenedor antes de agregar los nuevos productos

        // Comprobar si la categoría tiene productos
        if (data.length > 0) {
            data.forEach(producto => {
                // Crear la tarjeta de producto
                const productCard = document.createElement('div');
                productCard.classList.add('product-item');
                productCard.dataset.id = producto.id;  // Agregar el ID del producto como un atributo

                // Crear el contenido de la tarjeta de producto
                productCard.innerHTML = `
                    <div class="product-title">${producto.nombre}</div>
                    <div class="product-description">${producto.descripcion}</div>
                    <div class="product-price">$${producto.precio}</div>
                    <button class="add-to-cart-btn">Agregar al carrito</button>
                `;
                productsContainer.appendChild(productCard);
            });
        } else {
            productsContainer.innerHTML = '<p>No hay productos disponibles en esta categoría.</p>';
        }
    })
    .catch(error => {
        console.error("Error al cargar los productos de la categoría:", error);
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

// Función para cargar el nombre de la categoría
function cargarCategoria(categoriaId) {
    fetch(`https://localhost:57199/api/Categoria/${categoriaId}`)
        .then(response => response.json())
        .then(categoria => {
            const categoryTitle = document.getElementById('category-title');
            categoryTitle.textContent = categoria.nombre; // Mostrar el nombre de la categoría
        })
        .catch(error => {
            console.error("Error al cargar la categoría:", error);
        });
}

// Obtener el id de la categoría desde la URL
const categoriaId = getUrlParameter('categoriaId');

// Cargar la categoría y los productos
if (categoriaId) {
    cargarCategoria(categoriaId);
    cargarProductosPorCategoria(categoriaId); // Llamar a la función para cargar los productos
} else {
    console.log("No se encontró el id de la categoría en la URL.");
}
