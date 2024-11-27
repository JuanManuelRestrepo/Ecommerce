// Cargar los proveedores desde la API
function cargarProveedores() {
    fetch("https://localhost:57199/api/Proveedor")
        .then(response => response.json())
        .then(data => {
            const providersUl = document.getElementById('providers-ul');
            providersUl.innerHTML = ''; // Limpiar la lista antes de agregar los nuevos

            data.forEach(proveedor => {
                // Crear un nuevo elemento <li> para cada proveedor
                const providerLi = document.createElement('li');
                providerLi.dataset.id = proveedor.id; // Asignar el ID al atributo 'data-id'

                providerLi.innerHTML = `
                    <span>${proveedor.nombre} - ${proveedor.telefono} - ${proveedor.direccion} - ${proveedor.correo}</span>
                    <button onclick="eliminarProveedor('${proveedor.id}')">Eliminar</button>
                `;

                // Añadir el <li> al contenedor de proveedores
                providersUl.appendChild(providerLi);
            });
        })
        .catch(error => {
            console.error("Error al cargar los proveedores:", error);
        });
}

// Agregar un nuevo proveedor
document.getElementById('add-provider-btn').addEventListener('click', () => {
    const nombre = document.getElementById('nombre').value;
    const telefono = document.getElementById('telefono').value;
    const direccion = document.getElementById('direccion').value;
    const correo = document.getElementById('correo').value;

    if (nombre && telefono && direccion && correo) {
        const proveedor = {
            nombre: nombre,
            telefono: telefono,
            direccion: direccion,
            correo: correo
        };

        fetch("https://localhost:57199/api/Proveedor", {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(proveedor)
        })
        .then(response => response.json())
        .then(data => {
            // Limpiar los campos del formulario
            document.getElementById('nombre').value = '';
            document.getElementById('telefono').value = '';
            document.getElementById('direccion').value = '';
            document.getElementById('correo').value = '';
            // Recargar los proveedores
            cargarProveedores();
        })
        .catch(error => {
            console.error("Error al agregar el proveedor:", error);
        });
    } else {
        alert("Por favor, completa todos los campos.");
    }
});

// Eliminar un proveedor
function eliminarProveedor(id) {
    const confirmDelete = confirm("¿Estás seguro de que deseas eliminar este proveedor?");
    
    if (confirmDelete) {
        fetch(`https://localhost:57199/api/Proveedor/${id}`, {
            method: 'DELETE'
        })
        .then(response => {
            if (response.ok) {
                // Recargar los proveedores después de eliminar uno
                cargarProveedores();
            } else {
                alert('No se pudo eliminar el proveedor.');
            }
        })
        .catch(error => {
            console.error("Error al eliminar el proveedor:", error);
        });
    }
}


// Cargar los proveedores cuando la página se carga
window.onload = cargarProveedores;
