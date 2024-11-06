// register-script.js
document.getElementById('register-form').addEventListener('submit', function(event) {
    event.preventDefault();
    
    // Obtener valores del formulario
    const fullname = document.getElementById('fullname').value;
    const email = document.getElementById('email').value;
    const phone = document.getElementById('phone').value;
    const password = document.getElementById('password').value;
    const confirmPassword = document.getElementById('confirm-password').value;
    
    // Validación del formulario
    let isValid = true;
    
    // Validar nombre
    if (fullname.trim().length < 3) {
        showError('fullname', 'Name must be at least 3 characters long');
        isValid = false;
    }
    
    // Validar email
    if (!isValidEmail(email)) {
        showError('email', 'Please enter a valid email address');
        isValid = false;
    }
    
    // Validar teléfono
    if (!phone.match(/^\d{10}$/)) {
        showError('phone', 'Please enter a valid 10-digit phone number');
        isValid = false;
    }
    
    // Validar contraseña
    if (password.length < 8) {
        showError('password', 'Password must be at least 8 characters long');
        isValid = false;
    }
    
    // Validar confirmación de contraseña
    if (password !== confirmPassword) {
        showError('confirm-password', 'Passwords do not match');
        isValid = false;
    }
    
    if (isValid) {
        // Aquí iría la lógica para enviar los datos al servidor
        const userData = {
            fullname,
            email,
            phone,
            password
        };
        
        // Ejemplo de envío de datos al servidor (deberás implementar esto)
        registerUser(userData);
    }
});

// Función para mostrar errores
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

// Función para registrar usuario (deberás implementar la conexión con el servidor)
async function registerUser(userData) {
    try {
        // Aqui se hace el llamado del api de registro
        /*
        const response = await fetch('api/register.php', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(userData)
        });
        
        const data = await response.json();
        
        if (data.success) {
            window.location.href = 'login.html';
        } else {
            alert(data.message);
        }
        */
        
        // Por ahora, solo mostraremos un mensaje de éxito
        alert('Registration successful! You can now login.');
        window.location.href = 'login.html';
    } catch (error) {
        console.error('Error during registration:', error);
        alert('An error occurred during registration. Please try again.');
    }
}
// En login.js y register.js, después de un login/registro exitoso:
sessionStorage.setItem('isLoggedIn', 'true');
window.location.href = 'dashboard.html';

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