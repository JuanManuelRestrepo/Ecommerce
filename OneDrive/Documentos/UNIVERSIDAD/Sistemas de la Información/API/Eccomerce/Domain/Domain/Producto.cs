using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Producto
    {
        [Key]
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        // Especificando el tipo de columna en OnModelCreating
        public decimal Precio { get; set; }
        public int CantidadDisponible { get; set; }
        public DateTime FechaCreacion { get; set; }

        public Guid ProveedorId { get; set; }
        public Proveedor Proveedor { get; set; } // Relación con Proveedor

        public Guid CategoriaId { get; set; }
        // Navegación
        public List<DetallePedido> DetallePedidos { get; set; } = new List<DetallePedido>();
        public List<Inventario> Inventarios { get; set; } = new List<Inventario>();
    }
}

