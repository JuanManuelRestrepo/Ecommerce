const apiUrl = "https://localhost:57199/api/Usuario"; // Define la URL de la API

// Cargar todos los usuarios
function cargarUsuarios() {
    fetch(apiUrl)
        .then(response => response.json())
        .then(data => {
            console.log(data); // Verificar la respuesta de la API
            const listaUsuarios = document.getElementById("usuarios-lista");
            listaUsuarios.innerHTML = ""; // Limpiar la lista
            data.forEach(usuario => {
                const li = document.createElement("li");
                li.textContent = `${usuario.name} - ${usuario.email} - ${usuario.telefono} - ${usuario.direccion}`;
                
                // Crear un contenedor para los botones
                const botonesDiv = document.createElement("div");
                
                // Botón para editar el usuario
                const editarBtn = document.createElement("button");
                editarBtn.textContent = "Editar";
                editarBtn.onclick = () => editarUsuario(usuario);

                // Botón para eliminar el usuario
                const eliminarBtn = document.createElement("button");
                eliminarBtn.textContent = "Eliminar";
                eliminarBtn.onclick = () => eliminarUsuario(usuario.id);

                botonesDiv.appendChild(editarBtn);
                botonesDiv.appendChild(eliminarBtn);

                li.appendChild(botonesDiv); // Agregar el contenedor de botones al <li>
                listaUsuarios.appendChild(li);
            });
        })
        .catch(error => console.error('Error al cargar usuarios:', error));
}

const logoutBtn = document.getElementById('logout-btn');
if (logoutBtn) {
    logoutBtn.addEventListener('click', function() {
        sessionStorage.removeItem('usuarioActual');
        sessionStorage.removeItem('isLoggedIn');
        window.location.href = 'index.html';
    });
}


// Crear o actualizar un usuario
function manejarFormulario(event) {
    event.preventDefault();

    const id = document.getElementById("usuario-id").value;
    const name = document.getElementById("nombre").value;
    const email = document.getElementById("email").value;
    const telefono = document.getElementById("telefono").value;
    const direccion = document.getElementById("direccion").value;
    const contraseña = document.getElementById("contraseña").value;

    const usuario = { name, email, telefono, direccion, contraseña };

    if (id) {
        // Actualizar usuario
        fetch(`${apiUrl}/UpdateUsuario/${id}`, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(usuario)
        })
        .then(() => {
            alert("Usuario actualizado");
            cargarUsuarios();
            limpiarFormulario();
        })
        .catch(error => console.error("Error al actualizar usuario:", error));
    } else {
        // Crear nuevo usuario
        fetch(`${apiUrl}/CreateUsuario`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(usuario)
        })
        .then(() => {
            alert("Usuario creado");
            cargarUsuarios();
            limpiarFormulario();
        })
        .catch(error => console.error("Error al crear usuario:", error));
    }
}

// Cargar datos en el formulario para editar
function editarUsuario(usuario) {
    document.getElementById("usuario-id").value = usuario.id;
    document.getElementById("nombre").value = usuario.name;
    document.getElementById("email").value = usuario.email;
    document.getElementById("telefono").value = usuario.telefono;
    document.getElementById("direccion").value = usuario.direccion;
    document.getElementById("contraseña").value = ""; // Puedes dejarlo vacío si no se quiere predefinir
    document.getElementById("submit-btn").textContent = "Actualizar Usuario";
}

// Eliminar un usuario
function eliminarUsuario(id) {
    fetch(`${apiUrl}/${id}`, {
        method: "DELETE"
    })
    .then(() => {
        alert("Usuario eliminado");
        cargarUsuarios();
    })
    .catch(error => console.error("Error al eliminar usuario:", error));
}

// Limpiar formulario
function limpiarFormulario() {
    document.getElementById("usuario-id").value = "";
    document.getElementById("nombre").value = "";
    document.getElementById("email").value = "";
    document.getElementById("telefono").value = "";
    document.getElementById("direccion").value = "";
    document.getElementById("contraseña").value = "";
    document.getElementById("submit-btn").textContent = "Crear Usuario";
}

// Inicializar la página
document.getElementById("usuario-form").addEventListener("submit", manejarFormulario);
document.addEventListener("DOMContentLoaded", cargarUsuarios);
