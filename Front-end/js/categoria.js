// Cargar las categorías desde la API
function cargarCategorias() {
    fetch("https://localhost:57199/api/Categoria/GetAllCategoria")
        .then(response => response.json())
        .then(data => {
            const categoriesUl = document.getElementById('categories-ul');
            categoriesUl.innerHTML = ''; // Limpiar la lista de categorías antes de agregar las nuevas

            data.forEach(categoria => {
                // Crear un nuevo elemento <li> para cada categoría
                const categoryLi = document.createElement('li');
                categoryLi.dataset.id = categoria.id; // Asignar el ID de la categoría al atributo 'data-id'

                categoryLi.innerHTML = `
                    <span id="category-name-${categoria.id}">${categoria.nombre}</span>
                    <button class="delete-btn" onclick="eliminarCategoria('${categoria.id}')">Eliminar</button>
                `;

                // Añadir el <li> al contenedor de categorías
                categoriesUl.appendChild(categoryLi);
            });
        })
        .catch(error => {
            console.error("Error al cargar las categorías:", error);
        });
}

const logoutBtn = document.getElementById('logout-btn');
if (logoutBtn) {
    logoutBtn.addEventListener('click', function() {
        sessionStorage.removeItem('usuarioActual');
        sessionStorage.removeItem('isLoggedIn');
        window.location.href = 'index.html';
    });
}

// Agregar una nueva categoría
document.getElementById('add-category-btn').addEventListener('click', () => {
    const categoryName = document.getElementById('category-name').value;
    
    if (categoryName) {
        const categoria = {
            nombre: categoryName
        };

        fetch("https://localhost:57199/api/Categoria/CreateCategoria", {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(categoria)
        })
        .then(response => response.json())
        .then(data => {
            // Limpiar el campo de entrada
            document.getElementById('category-name').value = '';
            // Recargar las categorías
            cargarCategorias();
        })
        .catch(error => {
            console.error("Error al agregar la categoría:", error);
        });
    } else {
        alert("Por favor ingresa un nombre para la categoría.");
    }
});

// Eliminar una categoría
function eliminarCategoria(id) {
    // Confirmación antes de eliminar
    const confirmDelete = confirm("¿Estás seguro de que deseas eliminar esta categoría?");
    
    if (confirmDelete) {
        fetch(`https://localhost:57199/api/Categoria/${id}`, {
            method: 'DELETE'
        })
        .then(() => {
            // Recargar las categorías después de eliminar una
            cargarCategorias();
        })
        .catch(error => {
            console.error("Error al eliminar la categoría:", error);
        });
    }
}

// Editar una categoría
function editarCategoria(id) {
    // Obtener el nombre de la categoría actual
    const categoryNameElement = document.getElementById(`category-name-${id}`);
    const currentName = categoryNameElement.textContent.trim();

    // Mostrar el formulario de edición para actualizar el nombre
    const newName = prompt("Ingrese el nuevo nombre de la categoría:", currentName);

    if (newName && newName !== currentName) {
        const categoria = {
            id: id,
            nombre: newName
        };

        fetch("https://localhost:57199/api/Categoria/UpdateCategoria", {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(categoria)
        })
        .then(response => response.json())
        .then(data => {
            // Recargar las categorías después de actualizar
            cargarCategorias();
        })
        .catch(error => {
            console.error("Error al actualizar la categoría:", error);
        });
    } else if (!newName) {
        alert("El nombre de la categoría no puede estar vacío.");
    }
}

// Cargar las categorías cuando se carga la página
window.onload = cargarCategorias;
