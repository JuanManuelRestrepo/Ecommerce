using System.Data;

namespace Eccomerce.Models.DTO
{
    public class ProductoDTO
    {
        public Guid? Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int CantidadDisponible { get; set; }
        public Guid  idCategoria { get; set; }

        // Solo ID del proveedor
        public Guid ProveedorId { get; set; }
    }
}


