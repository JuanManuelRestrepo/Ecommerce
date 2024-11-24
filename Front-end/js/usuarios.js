document.addEventListener('DOMContentLoaded', function() {
    document.getElementById("add-product-form").addEventListener("submit", function(event) {
        event.preventDefault();

        // Obtener los valores del formulario
        const nombre = document.getElementById("product-name").value;
        const descripcion = document.getElementById("product-description").value;
        const precio = document.getElementById("product-price").value;
        const imagen = document.getElementById("product-image").value;
        const categoria = document.getElementById("categoria").value;
        const cantidad = document.getElementById("product-quantity").value;
        // Crear un objeto del producto
        const nuevoProducto = {
            nombre,
            descripcion,
            precio,
            imagen,
            categoria,
            cantidad
        };

        // Guardar en localStorage
        let productos = JSON.parse(localStorage.getItem("productos")) || [];
        productos.push(nuevoProducto);
        localStorage.setItem("productos", JSON.stringify(productos));

        //mostrar el producto en la lista
        mostrarProductosEnLista(nuevoProducto);

        //limpiar el formulario
        this.reset();

        alert("Producto agregado correctamente");

    });

    //funcion para mostrar el producto en la lista
    function mostrarProductosEnLista(producto) {
        const productoDiv = document.createElement("div");
        productoDiv.classList.add("producto");
        productoDiv.innerHTML = `
            <img src="${producto.imagen}" alt="${producto.nombre}">
            <h4>${producto.nombre}</h4>
            <p>${producto.descripcion}</p>
            <p>Precio: $${producto.precio}</p>
            <p>Categoria: ${producto.categoria}</p>
            <p>Cantidad: ${producto.cantidad}</p>
        `;
        document.getElementById("lista-productos").appendChild(productoDiv);
    }

    function mostrarSeccion(seccionId) {
        document.getElementById("agregar-producto").style.display = "none";
        document.getElementById("productos").style.display = "none";
        document.getElementById(seccionId).style.display = "block";
    }

    function cargarProductosPorCategoria(categoria) {
    const productos = JSON.parse(localStorage.getItem('productos')) || [];
    const productosFiltrados = productos.filter(producto => producto.categoria === categoria);

    const offersContainer = document.getElementById('offers-container');
    offersContainer.innerHTML = ''; // Limpiar las ofertas previas

    // Asignar eventos a los botones de eliminar
    document.querySelectorAll('.delete-btn').forEach(button => {
        button.addEventListener('click', function () {
            const index = this.getAttribute('data-index');
            const categoria = this.getAttribute('data-category');
            eliminarProducto(index, categoria);
        });
    });
}

    
});


