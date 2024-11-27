using System;

namespace Eccomerce.Models.DTO
{
    public class InventarioDTO
    {
        public Guid? Id { get; set; } 
        public Guid ProductoId { get; set; }
        public int Cantidad { get; set; }
        public DateTime FechaEntrada { get; set; }
    }
}
