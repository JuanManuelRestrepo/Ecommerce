using Eccomerce.ConexionBD;
using Eccomerce.Modelo;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.ObjectPool;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
namespace Eccomerce.ConexionBD
{
    public class ProductoRepository
    {
        ConexionBD conexionBD = new ConexionBD();
        public async Task<string> InsertarProducto(string Name, string Descripcion, int precio, int CantidadDisponible)
        {
            try
            {
                using (var sqlConnection = new System.Data.SqlClient.SqlConnection(conexionBD.CadenaSQL()))
                {

                    string insertQuery = @"INSERT INTO Productos (nombre, descripcion, precio, cantidad_disponible, fecha_creacion)
                                       VALUES (@nombre, @descripcion, @precio, @cantidad_disponible, @fecha_creacion )";

                    using (var comando = new System.Data.SqlClient.SqlCommand(insertQuery, sqlConnection))
                    {
                        comando.Parameters.AddWithValue("@nombre", Name);
                        comando.Parameters.AddWithValue("@descripcion", Descripcion);
                        comando.Parameters.AddWithValue("@precio", precio);
                        comando.Parameters.AddWithValue("@cantidad_disponible", CantidadDisponible);
                        comando.Parameters.AddWithValue("@fecha_creacion", DateTime.Now);

                        await sqlConnection.OpenAsync();
                        int filasAfectadas = await comando.ExecuteNonQueryAsync();

                        if (filasAfectadas > 0)
                        {
                            return "Producto insertado correctamente.";
                        }
                        else
                        {
                            return "No se pudo insertar el producto.";
                        }
                    }
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                return $"Error al insertar el producto (SQL): {ex.Message}";
            }
            catch (Exception ex)
            {
                return $"Error al insertar el producto (General): {ex.Message}";
            }
        }


        public async Task<List<ObteProducDTO>> ObtenerProductos()
        {
            var listaProductos = new List<ObteProducDTO>();

            using (var sqlConnection = new System.Data.SqlClient.SqlConnection(conexionBD.CadenaSQL()))
            {
                string selectQuery = @"SELECT id_producto, nombre, precio, cantidad_disponible FROM productos";

                using (var comando = new System.Data.SqlClient.SqlCommand(selectQuery, sqlConnection))
                {
                    // Abrimos la conexión de manera asíncrona
                    await sqlConnection.OpenAsync();
                    comando.CommandType = CommandType.Text;

                    using (var productos = await comando.ExecuteReaderAsync()) // Ejecutamos el query 
                    {
                        // Mientras todavía hayan datos en productos
                        while (productos.Read())
                        {
                            // Creamos un nuevo objeto del DTO de producto
                            var producto = new ObteProducDTO
                            {
                                id = productos.GetInt32(productos.GetOrdinal("id_producto")),
                                Nombre = productos.GetString(productos.GetOrdinal("nombre")),
                                Precio = productos.GetDecimal(productos.GetOrdinal("precio")),
                                CantidadDisponible = productos.GetInt32(productos.GetOrdinal("cantidad_disponible")) // Asegúrate que cantidad_disponible es int
                            };

                            // Agregamos el objeto a la lista de productos
                            listaProductos.Add(producto);
                        }
                    }
                }
            }

            return listaProductos; // Devuelve la lista de productos obtenida
        }

        //sera un metodo de tipo booleano y recibira un id y un DTO que definira la estruvrura de los datos que seran enviados
        public async Task<bool> ActualizarProducto(int id, ModificarProductoModel productoDTO)
        {
            using (var sqlConnection = new System.Data.SqlClient.SqlConnection(conexionBD.CadenaSQL()))
            {
                // Obtener valores actuales antes de la actualización
                string selectQuery = @"SELECT nombre, descripcion, precio, cantidad_disponible FROM productos WHERE id_producto = @Id";



                //lo que haremos es crear estas variables para almacenar la ifnromacion del query, en caso tal que los datos vemngan vacios lo que se hara es que estos datos se mantendran
                string nombreActual, descripcionActual;
                decimal precioActual = 0;
                int cantidadActual = 0;
                int categoriaActual = 0; // Para almacenar la categoría actual

                using (var selectCommand = new System.Data.SqlClient.SqlCommand(selectQuery, sqlConnection))
                {
                    selectCommand.Parameters.AddWithValue("@Id", id);
                    await sqlConnection.OpenAsync();
                    using (var reader = await selectCommand.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            nombreActual = reader.IsDBNull(0) ? null : reader.GetString(0);
                            descripcionActual = reader.IsDBNull(1) ? null : reader.GetString(1);
                            precioActual = reader.IsDBNull(2) ? 0 : reader.GetDecimal(2);
                            if (reader.IsDBNull(3)) // Columna 3 es cantidad_disponible
                            {
                                cantidadActual = 0; // O manejar como creas conveniente
                            }
                            else
                            {
                                cantidadActual = reader.GetInt32(3); // Leer como int
                            }

                        }
                        else
                        {
                            {
                                return false; // Producto no encontrado
                            }
                        }
                    }

                    // Preparar la consulta de actualización
                    string updateQuery = @"UPDATE productos SET 
                                        nombre = @Nombre,
                                        descripcion = @Descripcion,
                                        precio = @Precio,
                                        cantidad_disponible = @CantidadDisponible
                                        WHERE id_producto= @Id";

                    using (var updateCommand = new System.Data.SqlClient.SqlCommand(updateQuery, sqlConnection))
                    {
                        updateCommand.Parameters.AddWithValue("@Id", id);
                        updateCommand.Parameters.AddWithValue("@Nombre", string.IsNullOrEmpty(productoDTO.Nombre) ? nombreActual : productoDTO.Nombre); // verifica si el valor enviado es nulo mantien el valor actual sino lo actualiza
                        updateCommand.Parameters.AddWithValue("@Descripcion", string.IsNullOrEmpty(productoDTO.Descripcion) ? descripcionActual : productoDTO.Descripcion);
                        updateCommand.Parameters.AddWithValue("@Precio", productoDTO.Precio != 0 ? productoDTO.Precio : precioActual);
                        updateCommand.Parameters.AddWithValue("@CantidadDisponible", productoDTO.CantidadDisponible ?? cantidadActual);


                        // ejutamos el query y se lo almacenamos en una variable y asi retornaremos un booleano
                        int filasAfectadas = await updateCommand.ExecuteNonQueryAsync();
                        if (filasAfectadas > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }

                    }
                }
            }


        }

        public async Task<bool> BorrarProducto(int id)
        {
            using (var sqlConnection = new System.Data.SqlClient.SqlConnection(conexionBD.CadenaSQL()))
            {
                // Obtener valores actuales antes de la actualización
                string selectQuery = @"DELETE FROM productos WHERE id_producto = @Id";
                using (var selectCommand = new System.Data.SqlClient.SqlCommand(selectQuery, sqlConnection))
                {
                    selectCommand.Parameters.AddWithValue("@Id", id);


                    await sqlConnection.OpenAsync();
                    int rowsAffected = await selectCommand.ExecuteNonQueryAsync();

                    if (rowsAffected == 0)
                    {
                        return (false);
                    }
                    else
                    {
                        return (true);
                    }
                }


            }
        }
    }
}


