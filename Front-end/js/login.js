document.getElementById('login-form').addEventListener('submit', function(event) {
    event.preventDefault();

    const email = document.getElementById('email').value;
    const password = document.getElementById('password').value;

    // Agregar logs de depuración
    console.log('Intentando iniciar sesión con:', { email, password });

    // Datos para la API
    const data = {
        email: email,
        password: password
    };

    // Llamada a la API con fetch
    fetch('https://localhost:57199/api/Usuario/login', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    })
    .then(response => {
        if (response.ok) {
            return response.json();
        } else {
            return response.json().then(err => { 
                throw new Error(err.mensaje || 'Error en el login'); 
            });
        }
    })
    .then(result => {
        console.log('Respuesta completa del login:', result);
        
        // Guardar sesión del usuario
        const usuarioSesion = {
            id: result.usuarioid,
            email: result.nombre,
        };
        
        console.log('Datos a guardar en sessionStorage:', usuarioSesion);
        
        sessionStorage.setItem('usuarioActual', JSON.stringify(usuarioSesion));
        sessionStorage.setItem('isLoggedIn', 'true');
        
        // Mostrar mensaje y redirigir
        alert('Login exitoso: Bienvenido ' + result.nombre);
        window.location.href = '../Html/dashboard.html';
    })
    .catch(error => {
        console.error('Hubo un problema con la solicitud:', error);
        alert('Error en el login: ' + error.message);
    });
});

// Verificar si ya hay sesión iniciada
document.addEventListener('DOMContentLoaded', function() {
    if (sessionStorage.getItem('isLoggedIn')) {
        window.location.href = '../Html/dashboard.html';
    }
});