const apiUrl = "https://localhost:57199/api/Producto"; // Ruta correcta de la API

// Crear producto
document.getElementById('crear-producto').addEventListener('click', function(event) {
    event.preventDefault();

    // Obtener los valores del formulario
    const producto = {
        nombre: document.getElementById('product-name').value,
        descripcion: document.getElementById('product-description').value,
        precio: parseFloat(document.getElementById('product-price').value),
        CantidadDisponible: (document.getElementById('product-quantity').value), // Cambié 'cantidadDisponible' a 'cantidad'
        idCategoria: document.getElementById('categoria').value, // Este es el id de la categoría
        proveedorId: document.getElementById('Proveedor').value // Este es el id del proveedor
    };


    console.log((document.getElementById('product-quantity').value))

    const apicreate = "https://localhost:57199/api/Producto/Create"; // Ruta correcta de la API para crear producto
    fetch(apicreate, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(producto) // Convierte el objeto en JSON
    })
    .then(response => {
        if (!response.ok) {
            return response.json().then(errorData => {
                throw new Error(errorData.error || 'Error al crear el producto');
            });
        }
        return response.json();
    })
    .then(data => {
        alert("Producto creado exitosamente");
        listarProductos(); // Actualizar la lista de productos
    })
    .catch(error => {
        console.error("Error al crear el producto:", error);
        alert("Hubo un error al crear el producto");
    });
});

// Listar productos
document.addEventListener("DOMContentLoaded", function() {
    listarProductos(); // Cargar productos cuando la página se haya cargado
});

// Función para listar productos
function listarProductos() {
    fetch(`https://localhost:57199/api/Producto`)
    .then(response => response.json())
    .then(data => {
        const productList = document.getElementById('product-list');
        productList.innerHTML = ''; // Limpiar la lista actual

        data.forEach(producto => {
            const li = document.createElement('li');
            li.innerHTML = `
                <span>${producto.nombre} - $${producto.precio} - ${producto.cantidadDisponible} disponible(s)</span>
                <div>
                    
                    <button class="delete-btn" onclick="eliminarProducto('${producto.id}')">Eliminar</button>
                </div>
            `;
            productList.appendChild(li);
        });
    })
    .catch(error => {
        console.error("Error al listar productos:", error);
        alert("Hubo un error al listar los productos");
    });
}

function mostrarFormularioActualizar(id) {
    fetch(`https://localhost:57199/api/Producto/${id}`)
        .then(response => response.json())
        .then(producto => {
            const nombre = producto.nombre || ""; // Asegura que no sea undefined
            const descripcion = producto.descripcion || "";
            const precio = producto.precio != null ? producto.precio : ""; // Acepta 0, pero no undefined
            const cantidadDisponible = producto.cantidadDisponible != null ? producto.cantidadDisponible : "";

            const formularioActualizar = `
                <h3>Actualizar Producto</h3>
                <form id="form-actualizar-producto">
                    <label for="update-product-name">Nombre:</label>
                    <input type="text" id="update-product-name" value="${nombre}" required>

                    <label for="update-product-description">Descripción:</label>
                    <textarea id="update-product-description" required>${descripcion}</textarea>

                    <label for="update-product-price">Precio:</label>
                    <input type="number" id="update-product-price" value="${precio}" min="0" required>

                    <label for="update-product-quantity">Cantidad disponible:</label>
                    <input type="number" id="update-product-quantity" value="${cantidadDisponible}" min="0" required>

                    <label for="update-categoria">Categoría:</label>
                    <select id="update-categoria"></select>

                    <label for="update-Proveedor">Proveedor:</label>
                    <select id="update-Proveedor"></select>

                    <button type="submit" id="actualizar-producto" onclick="actualizarProducto(${producto.id})">Actualizar Producto</button>
                </form>
            `;

            // Mostrar el formulario de actualización
            const productList = document.getElementById('product-list');
            productList.innerHTML = formularioActualizar;

            // Cargar categorías y proveedores para la actualización
            cargarCategoriasActualizar();
            cargarProveedoresActualizar();

            // Seleccionar la categoría y proveedor del producto actual
            if (producto.idCategoria) {
                document.getElementById('update-categoria').value = producto.idCategoria;
            }
            if (producto.proveedorId) {
                document.getElementById('update-Proveedor').value = producto.proveedorId;
            }
        })
        .catch(error => {
            console.error("Error al cargar el producto para actualizar:", error);
            alert("Hubo un error al cargar los datos del producto para actualizar.");
        });
}

function cargarCategoriasActualizar() {
    fetch("https://localhost:57199/api/Categoria/GetAllCategoria")
        .then(response => response.json())
        .then(data => {
            const categoriaSelect = document.getElementById('update-categoria');
            categoriaSelect.innerHTML = '';

            const defaultOption = document.createElement('option');
            defaultOption.value = '';
            defaultOption.textContent = 'Seleccione una categoría';
            categoriaSelect.appendChild(defaultOption);

            data.forEach(categoria => {
                const option = document.createElement('option');
                option.value = categoria.id;
                option.textContent = categoria.nombre;
                categoriaSelect.appendChild(option);
            });
        })
        .catch(error => {
            console.error("Error al cargar las categorías:", error);
            alert("Hubo un error al cargar las categorías");
        });
}
function cargarProveedoresActualizar() {
    fetch("https://localhost:57199/api/Proveedor")
        .then(response => response.json())
        .then(data => {
            const proveedorSelect = document.getElementById('update-Proveedor');
            proveedorSelect.innerHTML = '';

            const defaultOption = document.createElement('option');
            defaultOption.value = '';
            defaultOption.textContent = 'Seleccione un proveedor';
            proveedorSelect.appendChild(defaultOption);

            data.forEach(proveedor => {
                const option = document.createElement('option');
                option.value = proveedor.id;
                option.textContent = proveedor.nombre;
                proveedorSelect.appendChild(option);
            });
        })
        .catch(error => {
            console.error("Error al cargar los proveedores:", error);
            alert("Hubo un error al cargar los proveedores");
        });
}

function actualizarProducto(id) {
    // Crear el objeto con los datos actualizados del producto
    const updatedProduct = {
        nombre: document.getElementById('update-product-name').value.trim(),
        descripcion: document.getElementById('update-product-description').value.trim(),
        precio: parseFloat(document.getElementById('update-product-price').value),
        cantidadDisponible: parseInt(document.getElementById('update-product-quantity').value, 10),
        idCategoria: document.getElementById('update-categoria').value,
        proveedorId: document.getElementById('update-Proveedor').value
    };

    // Validaciones básicas
    if (!id || id.trim() === '') {
        alert("El ID del producto no es válido.");
        return;
    }
    if (isNaN(updatedProduct.precio) || updatedProduct.precio <= 0) {
        alert("El precio debe ser un número positivo.");
        return;
    }
    if (isNaN(updatedProduct.cantidadDisponible) || updatedProduct.cantidadDisponible < 0) {
        alert("La cantidad disponible no puede ser negativa.");
        return;
    }

    // Llamada a la API
    fetch(`https://localhost:57199/api/Producto/${id}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(updatedProduct) // El DTO en el cuerpo
    })
    .then(response => {
        if (response.ok) {
            alert("Producto actualizado exitosamente.");
            listarProductos(); // Recargar la lista de productos
        } else if (response.status === 400 || response.status === 404) {
            return response.text().then(text => {
                alert(`Error: ${text}`);
            });
        } else {
            alert("Ocurrió un error inesperado al actualizar el producto.");
        }
    })
    .catch(error => {
        console.error("Error al actualizar el producto:", error);
        alert("Hubo un error al realizar la solicitud.");
    });
}




// Cerrar el modal cuando se hace clic en el botón "Cerrar"
document.getElementById('close-modal-btn').onclick = function() {
    document.getElementById('update-product-modal').style.display = 'none';
};

// Eliminar producto
async function eliminarProducto(id) {
    if (!id) {
        alert("ID no válido");
        return;
    }
    console.log(id);

    try {
        const response = await fetch(`https://localhost:57199/api/Producto/${id}`, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json',
            },
        });

        if (response.ok) {
            alert('Producto eliminado exitosamente');
            listarProductos(); // Recargar la lista de productos
        } else {
            const errorData = await response.json();
            alert(`Error al eliminar el producto: ${errorData.message || "Error desconocido"}`);
        }
    } catch (error) {
        console.error('Error al realizar la eliminación:', error);
        alert('Ocurrió un error al intentar eliminar el producto.');
    }
}

// Cargar categorías en el formulario
function cargarCategorias() {
    fetch("https://localhost:57199/api/Categoria/GetAllCategoria")
        .then(response => response.json())
        .then(data => {
            const categoriaSelect = document.getElementById('categoria');
            categoriaSelect.innerHTML = '';

            const defaultOption = document.createElement('option');
            defaultOption.value = '';
            defaultOption.textContent = 'Seleccione una categoría';
            categoriaSelect.appendChild(defaultOption);

            data.forEach(categoria => {
                const option = document.createElement('option');
                option.value = categoria.id;
                option.textContent = categoria.nombre;
                categoriaSelect.appendChild(option);
            });
        })
        .catch(error => {
            console.error("Error al cargar las categorías:", error);
            alert("Hubo un error al cargar las categorías");
        });
}

// Cargar proveedores en el formulario
function cargarProveedores() {
    fetch("https://localhost:57199/api/Proveedor")
        .then(response => response.json())
        .then(data => {
            const proveedorSelect = document.getElementById('Proveedor');
            proveedorSelect.innerHTML = '';

            const defaultOption = document.createElement('option');
            defaultOption.value = '';
            defaultOption.textContent = 'Seleccione un proveedor';
            proveedorSelect.appendChild(defaultOption);

            data.forEach(proveedor => {
                const option = document.createElement('option');
                option.value = proveedor.id;
                option.textContent = proveedor.nombre;
                proveedorSelect.appendChild(option);
            });
        })
        .catch(error => {
            console.error("Error al cargar los proveedores:", error);
            alert("Hubo un error al cargar los proveedores");
        });
}

// Cargar categorías y proveedores cuando la página se cargue
document.addEventListener("DOMContentLoaded", function() {
    cargarCategorias(); // Cargar categorías al inicio
    cargarProveedores(); // Cargar proveedores al inicio
});
