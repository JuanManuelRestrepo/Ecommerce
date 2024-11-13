document.addEventListener('DOMContentLoaded', function() {
    const menuToggle = document.querySelector('.menu-toggle');
    const sidebar = document.querySelector('.sidebar');
    const mainContent = document.querySelector('.main-content');

    // Toggle sidebar en móviles
    menuToggle.addEventListener('click', function() {
        sidebar.classList.toggle('active');
    });

    // Cerrar sidebar al hacer clic fuera de ella en móviles
    mainContent.addEventListener('click', function() {
        if (window.innerWidth <= 768 && sidebar.classList.contains('active')) {
            sidebar.classList.remove('active');
        }
    });

    // Manejar redimensionamiento de ventana
    window.addEventListener('resize', function() {
        if (window.innerWidth > 768) {
            sidebar.classList.remove('active');
        }
    });

    // Notificaciones
    const notificationBtn = document.querySelector('.notification-btn');
    notificationBtn.addEventListener('click', function() {
        alert('No tienes notificaciones nuevas');
    });

    // Configuración
    const settingsBtn = document.querySelector('.settings-btn');
    settingsBtn.addEventListener('click', function() {
        alert('Configuración');
    });

    // Verificar si el usuario está logueado
    checkLoginStatus();

    // Almacena los datos de categorías
    const categorias = {
        vehiculos: {
            nombre: "Vehículos",
            ofertas: [
                {nombre: "Auto Deportivo", precio: "$200,000", imagen: "img/auto.jpg"},
                {nombre: "Moto", precio: "$15,000", imagen: "img/moto.jpg"}
            ]
        },
        ropa: {
            nombre: "Ropa",
            ofertas: [
                {nombre: "Conjunto Deportivo", precio: "$89.99", imagen: "img/oferta1.jpg"},
                {nombre: "Abrigo de Invierno", precio: "$150.00", imagen: "img/abrigo.jpg"}
            ]
        },
        hogar:{
            nombre: "Hogar",
            ofertas: [
                {nombre: "Mueble", precio: "$89.99", imagen: "img/mueble.jpg"},
                {nombre: "Mesa", precio: "$150.00", imagen: "img/mesa.jpg"}
            ]

        },
        accesorios:{
            nombre: "Accesorios",
            ofertas: [
                {nombre: "Cadena", precio: "$89.99", imagen: "img/cadena.jpg"},
                {nombre: "Reloj", precio: "$150.00", imagen: "img/reloj.jpg"}
            ]

        },

        deporte:{
            nombre: "Deporte",
            ofertas: [
                {nombre: "Balon de futbol", precio: "$89.99", imagen: "img/balon.jpg"},
                {nombre: "Raquetas de Tenis", precio: "$150.00", imagen: "img/raqueta.jpg"}
            ]

        },
            tecnologia:{
            nombre: "Tecnologia",
            ofertas: [
                {nombre: "Computador", precio: "$89.99", imagen: "img/cpu.jpg"},
                {nombre: "Audifonos", precio: "$150.00", imagen: "img/audifonos.jpg"}
            ]

        },

        belleza:{
            nombre: "Belleza",
            ofertas: [
                {nombre: "Labial", precio: "$89.99", imagen: "img/labial.jpg"},
                {nombre: "Pestanina", precio: "$150.00", imagen: "img/pestanina.jpg"}
            ]

        },
        
        

        // Agrega más categorías aquí
    };
    // Función para cargar ofertas en la página
    function cargarOfertas(categoria) {
        const categoriaData = categorias[categoria];
        
        if (categoriaData) {
            // Actualiza el título de la categoría
            document.getElementById('category-title').textContent = `Ofertas de ${categoriaData.nombre}`;
            
            // Contenedor donde se mostrarán las ofertas
            const offersContainer = document.getElementById('offers-container');
            offersContainer.innerHTML = ''; // Limpiar ofertas anteriores
            
            // Agregar cada oferta al contenedor
            categoriaData.ofertas.forEach(oferta => {
                const ofertaHTML = `
                    <div class="offer-card">
                        <img src="${oferta.imagen}" alt="${oferta.nombre}">
                        <div class="offer-details">
                            <h3>${oferta.nombre}</h3>
                            <p class="price">${oferta.precio}</p>
                        </div>
                    </div>
                `;
                offersContainer.innerHTML += ofertaHTML;
            });
        }
    }

    // Asigna el evento de clic para cada categoría
    document.querySelectorAll('.category-card').forEach(card => {
        card.addEventListener('click', function(event) {
            event.preventDefault(); // Evita redirección
            const categoriaId = this.getAttribute('data-category'); // Obtener el id de la categoría
            cargarOfertas(categoriaId);
        });
    });
});

// Función para verificar el estado de inicio de sesión
function checkLoginStatus() {
    const isLoggedIn = sessionStorage.getItem('isLoggedIn');
    if (!isLoggedIn) {
        window.location.href = 'index.html';
    }
}
