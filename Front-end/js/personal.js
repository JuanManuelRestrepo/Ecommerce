const apiUrl = "https://localhost:57199/api/Usuario"; // Define la URL de la API

// Manejar la actualización de datos del formulario
document.getElementById("update-user-form").addEventListener("submit", function(event) {
    event.preventDefault();

    const id = document.getElementById("usuario-id").value;
    const nombre = document.getElementById("nombre").value;
    const email = document.getElementById("email").value;
    const telefono = document.getElementById("telefono").value;
    const direccion = document.getElementById("direccion").value;
    const password = document.getElementById("password").value;

    // Asegúrate de que los campos coincidan con el formato esperado en el backend
    const usuario = {
        name: nombre,          // El nombre se asigna a 'name'
        email: email,          // El email se asigna a 'email'
        direccion: direccion,  // La dirección se asigna a 'direccion'
        telefono: parseInt(telefono), // Convertir el teléfono a un número entero (por si es un string)
        contraseña: password   // La contraseña se asigna a 'contraseña'
    };

    if (id) {
        // Actualizar usuario
        fetch(`${apiUrl}/UpdateUsuario/${id}`, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(usuario)  // Enviar los datos correctamente formateados
        })
        .then(response => {
            // Verificar si la respuesta es exitosa (código de estado 2xx)
            if (!response.ok) {
                return response.text().then(errorText => {
                    throw new Error(errorText || "Error desconocido"); // Lanza un error con el mensaje recibido en texto
                });
            }

            // Verificar si la respuesta es JSON o texto
            const contentType = response.headers.get("content-type");
            if (contentType && contentType.includes("application/json")) {
                return response.json(); // Intentar parsear como JSON
            } else {
                return response.text(); // Si no es JSON, tratarlo como texto
            }
        })
        .then(data => {
            if (typeof data === "string") {
                // Si la respuesta es un texto (mensaje de éxito o error), mostrarlo
                alert(data);
            } else {
                // Si la respuesta es un JSON, manejarlo adecuadamente
                alert(data.mensaje || "Usuario actualizado exitosamente");  // Mostrar el mensaje del servidor
            }
            window.location.reload();  // Recargar la página después de actualizar
        })
        .catch(error => {
            console.error("Error al actualizar usuario:", error);
            alert("Error al actualizar usuario: " + error.message); // Mostrar el mensaje de error
        });
    } else {
        alert("No se pudo encontrar el ID del usuario.");
    }
});

// Cargar la información del usuario en el formulario
function cargarUsuario(id) {
    fetch(`${apiUrl}/${id}`)
        .then(response => response.json())
        .then(usuario => {
            document.getElementById("usuario-id").value = usuario.id;
            document.getElementById("nombre").value = usuario.name;  // Asegúrate de que coincida con el nombre
            document.getElementById("email").value = usuario.email;
            document.getElementById("telefono").value = usuario.telefono;
            document.getElementById("direccion").value = usuario.direccion;
            document.getElementById("password").value = ""; // Para no prellenar la contraseña
        })
        .catch(error => {
            console.error("Error al cargar los datos del usuario:", error);
        });
}

// Obtener el ID del usuario actual desde el almacenamiento o el backend y cargar los datos
const usuarioSesion = JSON.parse(sessionStorage.getItem('usuarioActual')); // Obtener datos del usuario desde sessionStorage
const usuarioId = usuarioSesion ? usuarioSesion.id : null; // Obtener el usuarioId
if (usuarioId) {
    cargarUsuario(usuarioId);
} else {
    alert("Debes iniciar sesión para acceder a esta página.");
}

// Logout
const logoutBtn = document.getElementById('logout-btn');
if (logoutBtn) {
    logoutBtn.addEventListener('click', function() {
        sessionStorage.removeItem('usuarioActual');
        sessionStorage.removeItem('isLoggedIn');
        window.location.href = 'index.html';
    });
}
