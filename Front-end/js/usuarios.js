document.getElementById("form-producto").addEventListener("submit", function(event) {
    event.preventDefault();

    // Obtener los valores del formulario
    const nombre = document.getElementById("nombre").value;
    const descripcion = document.getElementById("descripcion").value;
    const precio = document.getElementById("precio").value;
    const imagen = document.getElementById("imagen").value;

    // Crear el nuevo producto como elemento HTML
    const productoDiv = document.createElement("div");
    productoDiv.classList.add("producto");
    productoDiv.innerHTML = `
        <img src="${imagen}" alt="${nombre}">
        <h4>${nombre}</h4>
        <p>${descripcion}</p>
        <p>Precio: $${precio}</p>
    `;

    // Añadir el nuevo producto a la lista de productos
    document.getElementById("lista-productos").appendChild(productoDiv);

    // Limpiar el formulario
    document.getElementById("form-producto").reset();

    // Cambiar a la vista de productos para ver el producto agregado
    mostrarSeccion("productos");
});

function mostrarSeccion(seccion) {
    document.getElementById("productos").classList.add("oculto");
    document.getElementById("agregar-producto").classList.add("oculto");

    document.getElementById(seccion).classList.remove("oculto");
}
