document.addEventListener('DOMContentLoaded', function () {
    // Configuración de menús y barras laterales
    const menuToggle = document.querySelector('.menu-toggle');
    const sidebar = document.querySelector('.sidebar');
    const mainContent = document.querySelector('.main-content');

    // Toggle sidebar en móviles
    menuToggle.addEventListener('click', function () {
        sidebar.classList.toggle('active');
    });

    // Cerrar sidebar al hacer clic fuera de ella en móviles
    mainContent.addEventListener('click', function () {
        if (window.innerWidth <= 768 && sidebar.classList.contains('active')) {
            sidebar.classList.remove('active');
        }
    });

    // Manejar redimensionamiento de ventana
    window.addEventListener('resize', function () {
        if (window.innerWidth > 768) {
            sidebar.classList.remove('active');
        }
    });

    // Notificaciones
    const notificationBtn = document.querySelector('.notification-btn');
    notificationBtn.addEventListener('click', function () {
        alert('No tienes notificaciones nuevas');
    });

    // Configuración
    const settingsBtn = document.querySelector('.settings-btn');
    settingsBtn.addEventListener('click', function () {
        alert('Configuración');
    });

    // Verificar si el usuario está logueado
    checkLoginStatus();

    // Función para verificar el estado de inicio de sesión
    function checkLoginStatus() {
        const isLoggedIn = sessionStorage.getItem('isLoggedIn');
        if (!isLoggedIn) {
            window.location.href = 'index.html';
        }
    }

    // Función para cargar productos por categoría
    function cargarProductosPorCategoria(categoria) {
        const productos = JSON.parse(localStorage.getItem('productos')) || [];
        const productosFiltrados = productos.filter(producto => producto.categoria === categoria);

        const offersContainer = document.getElementById('offers-container');
        offersContainer.innerHTML = ''; // Limpiar las ofertas previas

        productosFiltrados.forEach((producto, index) => {
            const productoDiv = document.createElement("div");
            productoDiv.classList.add("offer-card");
            productoDiv.innerHTML = `
                <img src="${producto.imagen}" alt="${producto.nombre}">
                <div class="offer-details">
                    <h3>${producto.nombre}</h3>
                    <p>${producto.descripcion}</p>
                    <p class="price">$${producto.precio}</p>
                    <button class="delete-btn" data-index="${index}" data-category="${categoria}">Eliminar</button>
                </div>
            `;
            offersContainer.appendChild(productoDiv);
        });

        // Asignar eventos a los botones de eliminar
        document.querySelectorAll('.delete-btn').forEach(button => {
            button.addEventListener('click', function () {
                const index = this.getAttribute('data-index');
                const categoria = this.getAttribute('data-category');
                eliminarProducto(index, categoria);
            });
        });
    }

    // Función para eliminar productos
    function eliminarProducto(index, categoria) {
        let productos = JSON.parse(localStorage.getItem('productos')) || [];
        const productosFiltrados = productos.filter(producto => producto.categoria === categoria);

        // Remover el producto seleccionado
        productosFiltrados.splice(index, 1);

        // Actualizar `localStorage` con los productos restantes
        const productosActualizados = productos.filter(producto => producto.categoria !== categoria).concat(productosFiltrados);
        localStorage.setItem('productos', JSON.stringify(productosActualizados));

        // Recargar los productos en el contenedor
        cargarProductosPorCategoria(categoria);

        alert("El producto ha sido eliminado.");
    }

    // Asigna el evento de clic a las categorías en el dashboard
    document.querySelectorAll('.category-card').forEach(card => {
        card.addEventListener('click', function (event) {
            event.preventDefault(); // Evita redirección
            const categoriaSeleccionada = this.dataset.category; // Obtener la categoría seleccionada
            cargarProductosPorCategoria(categoriaSeleccionada);
        });
    });

});
