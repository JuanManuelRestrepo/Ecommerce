using System;

namespace Application.DTOs
{
    public class PagoDTO
    {
        public Guid Id { get; set; }
        public Guid PedidoId { get; set; }
        public string MetodoPago { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaPago { get; set; }
    }
}
