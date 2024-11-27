// Función para obtener los detalles del usuario
async function obtenerUsuario(id) {
    try {
        // Realiza la solicitud GET a la API usando fetch
        const response = await fetch(`https://localhost:57199/api/Usuario/${id}`, {
            method: 'GET',
            headers: {
                'Accept': '*/*'
            }
        });

        if (!response.ok) {
            throw new Error("No se pudo obtener la información del usuario.");
        }

        // Parsear la respuesta en formato JSON
        const usuario = await response.json();

        // Verificar si los datos son válidos antes de actualizarlos
        if (usuario) {
            document.getElementById("user-name").textContent = usuario.name || "Nombre no disponible";
            document.getElementById("user-email").textContent = usuario.email || "Correo no disponible";
            document.getElementById("user-phone").textContent = usuario.telefono !== 0 ? usuario.telefono : "Teléfono no disponible";
            document.getElementById("user-direccion").textContent = usuario.direccion || "Dirección no disponible";
        }

    } catch (error) {
        console.error("Error al obtener los datos del usuario:", error);
        alert("Hubo un problema al cargar la información del usuario.");
    }
}

// Llamar a la función con el ID del usuario (por ejemplo, ID = 'AD20802D-09B9-46FF-5A70-08DD0E39EFBB')
obtenerUsuario("AD20802D-09B9-46FF-5A70-08DD0E39EFBB");
