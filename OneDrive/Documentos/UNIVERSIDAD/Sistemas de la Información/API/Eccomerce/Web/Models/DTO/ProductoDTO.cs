using System.Data;

namespace Eccomerce.Models.DTO
{
    public class ProductoDTO
    {
        public Guid ?id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int CantidadDisponible { get; set; }

        public DateTime fechacreacion { get; set; } = DateTime.Now;


    }
}
