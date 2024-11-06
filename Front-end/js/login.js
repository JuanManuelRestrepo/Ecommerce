// script.js
document.getElementById('login-form').addEventListener('submit', function(event) {
    event.preventDefault(); // Evita que se recargue la página
  
    var email = document.getElementById('email').value;
    var password = document.getElementById('password').value;
  
    // Aquí deberías implementar la lógica de login utilizando PHP y JavaScript
    // Por ejemplo, puedes enviar una solicitud AJAX al servidor para verificar las credenciales
    // y luego redirigir al usuario a la página principal si el login es exitoso
  
    alert('Login successful!');

    // En login.js y register.js, después de un login/registro exitoso:
sessionStorage.setItem('isLoggedIn', 'true');
window.location.href = 'dashboard.html';
  });