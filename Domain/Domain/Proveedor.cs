using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Proveedor
    {
        [Key]
        public Guid Id { get; set; }

        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Correo { get; set; }

        // Navegación: un proveedor tiene muchos productos
        public List<Producto> Productos { get; set; } = new List<Producto>();
    }
}
