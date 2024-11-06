using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Inventario
    {
        public Guid  Id { get; set; }
        public Guid ProductoId { get; set; }
        public int Cantidad { get; set; }
        public DateTime FechaEntrada { get; set; }
        public Producto Producto { get; set; } // Relación con Producto


    }

}
