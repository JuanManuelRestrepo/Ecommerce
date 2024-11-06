// dashboard.js
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
        if(window.innerWidth <= 768 && sidebar.classList.contains('active')) {
            sidebar.classList.remove('active');
        }
    });

    // Manejar redimensionamiento de ventana
    window.addEventListener('resize', function() {
        if(window.innerWidth > 768) {
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
        // Aquí puedes implementar la lógica para mostrar la configuración
        alert('Configuración');
    });

    // Verificar si el usuario está logueado
    checkLoginStatus();
});

function checkLoginStatus() {

    const isLoggedIn = sessionStorage.getItem('isLoggedIn');
    if (!isLoggedIn) {
        window.location.href = 'index.html';
    }
}