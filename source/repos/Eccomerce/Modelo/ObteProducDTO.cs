namespace Eccomerce.Modelo
{
    public class ObteProducDTO
    {
        public int id {  get; set; }
        public string Nombre { get; set; } // Nombre del producto
        public decimal Precio { get; set; } // Cambiado a "Precio" para seguir la convención
        public int CantidadDisponible { get; set; } // Cambiado a "CantidadDisponible" y tipo a int
    }
}

