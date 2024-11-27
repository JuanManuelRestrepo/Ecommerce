document.getElementById('register-form').addEventListener('submit', function(event) {
    event.preventDefault();
    
    // Obtener valores del formulario
    const fullname = document.getElementById('fullname').value;
    const email = document.getElementById('email').value;
    const Direccion = document.getElementById('address').value;  // La dirección sigue siendo string
    let phone = document.getElementById('phone').value;  // Este debe ser un número entero
    const password = document.getElementById('password').value;
    const confirmPassword = document.getElementById('confirm-password').value;

    // Validación del formulario
    let isValid = true;

    // Validar nombre
    if (fullname.trim().length < 3) {
        showError('fullname', 'El nombre debe tener al menos 3 caracteres');
        isValid = false;
    }

    // Validar email
    if (!isValidEmail(email)) {
        showError('email', 'Por favor, introduce una dirección de correo válida');
        isValid = false;
    }

    // Validar teléfono
    if (phone && !/^\d+$/.test(phone)) {
        showError('phone', 'Por favor, introduce un número de teléfono válido');
        isValid = false;
    } else {
        // Convertir teléfono a número entero (int)
        phone = phone ? parseInt(phone, 10) : null;  // Aseguramos que sea un entero
    }

    // Validar contraseña
    if (password.length < 8) {
        showError('password', 'La contraseña debe tener al menos 8 caracteres');
        isValid = false;
    }

    // Validar confirmación de contraseña
    if (password !== confirmPassword) {
        showError('confirm-password', 'Las contraseñas no coinciden');
        isValid = false;
    }

    if (isValid) {
        // Verificar los datos antes de enviarlos
        console.log("Datos antes de enviar:", { 
            Name: fullname, 
            Email: email, 
            Direccion: Direccion, 
            Telefono: phone, 
            Contraseña: password 
        });

        // Datos a enviar al servidor (envolver en usuarioDTO)
        const userData = {
            name: fullname,  // Usar el valor del formulario
            email: email,    // Usar el valor del formulario
            direccion: Direccion,  // La dirección sigue siendo string
            telefono: phone,  // El teléfono debe ser un número entero
            contraseña: password  // Usar el valor del formulario
        };

        console.log("Enviando datos:", userData);

        // Llamada a la API para registrar usuario
        registerUser(userData);
    }
});

// Función para mostrar errores en los campos del formulario
function showError(fieldId, message) {
    const field = document.getElementById(fieldId);
    field.classList.add('error');
    
    // Eliminar mensaje de error anterior si existe
    const existingError = field.parentElement.querySelector('.error-message');
    if (existingError) {
        existingError.remove();
    }
    
    // Agregar nuevo mensaje de error
    const errorDiv = document.createElement('div');
    errorDiv.className = 'error-message';
    errorDiv.textContent = message;
    field.parentElement.appendChild(errorDiv);
}

// Función para validar email
function isValidEmail(email) {
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return emailRegex.test(email);
}

// Función para registrar usuario llamando a la API
async function registerUser(userData) {
    try {
        console.log('Enviando datos de registro:', userData);

        const response = await fetch('https://localhost:57199/api/Usuario/CreateUsuario', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'accept': '*/*'
            },
            body: JSON.stringify(userData)
        });

        if (response.ok) {
            const message = await response.text();
            console.log('Registro exitoso:', message);
            alert('Registro exitoso! Ahora puedes iniciar sesión');
            window.location.href = '../Html/index.html';  // Redirigir a la página de inicio
        } else {
            const errorMessage = await response.text();
            console.error('Error en el registro:', errorMessage);
            alert(`Error en el registro: ${errorMessage}`);
        }
    } catch (error) {
        console.error('Error durante el registro:', error);
        alert('Ocurrió un error durante el registro. Por favor, inténtalo de nuevo.');
    }
}

// Limpiar errores cuando el usuario comienza a escribir
document.querySelectorAll('input').forEach(input => {
    input.addEventListener('input', function() {
        this.classList.remove('error');
        const errorMessage = this.parentElement.querySelector('.error-message');
        if (errorMessage) {
            errorMessage.remove();
        }
    });
});
