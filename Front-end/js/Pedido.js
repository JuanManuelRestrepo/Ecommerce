// Función para cargar los pedidos del usuario
function cargarPedidos() {
    const usuarioSesion = JSON.parse(sessionStorage.getItem('usuarioActual')); // Obtener datos del usuario desde sessionStorage
    const usuarioId = usuarioSesion ? usuarioSesion.id : null; // Obtener el usuarioId
    if (!usuarioId) {
        alert("Debes iniciar sesión para ver tus pedidos.");
        return;
    }
   
    // Llamada a la API para obtener los pedidos
    fetch(`https://localhost:57199/api/Pedido/GetPedidoUsuarioID?usuarioId=${usuarioId}`)
        .then(response => {
            if (!response.ok) {
                throw new Error(`Error en la respuesta de la API: ${response.statusText}`);
            }
            return response.json();
        })
        .then(data => {
            console.log("Pedidos recibidos:", data); // Verifica los datos recibidos en la consola

            const pedidosContainer = document.getElementById('pedidos-container');
            if (!pedidosContainer) return;  // Verificar si el contenedor existe
            pedidosContainer.innerHTML = ''; // Limpiar el contenedor antes de agregar los pedidos

            if (data.length === 0) {
                pedidosContainer.innerHTML = '<p>No tienes pedidos realizados.</p>';
                return;
            }

            // Crear tarjetas para cada pedido
            data.forEach(pedido => {
                const pedidoCard = document.createElement('div');
                pedidoCard.classList.add('pedido-item');
                pedidoCard.dataset.id = pedido.id; // Agregar el ID del pedido como un atributo

                pedidoCard.innerHTML = `

                    <div class="pedido-estado">Estado: ${pedido.estado}</div>
                    <div class="pedido-total">Total: $${pedido.total.toFixed(2)}</div>
                `;

                // Agregar el pedido al contenedor
                pedidosContainer.appendChild(pedidoCard);
            });
        })
        .catch(error => {
            console.error("Error al cargar los pedidos:", error);
            alert("Hubo un error al cargar los pedidos: " + error.message); // Mostrar mensaje de error más detallado
        });
}

// Llamadas iniciales cuando se carga el DOM
document.addEventListener('DOMContentLoaded', () => {
    cargarPedidos();  // Cargar los pedidos cuando se cargue la página
});
