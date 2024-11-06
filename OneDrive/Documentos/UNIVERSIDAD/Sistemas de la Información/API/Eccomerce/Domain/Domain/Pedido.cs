using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Pedido
    {

        public Guid ?Id { get; set; }
        public Guid UsuarioId { get; set; }
        public DateTime FechaPedido { get; set; }
        public EstadoPedido Estado { get; set; }
        public decimal Total { get; set; }
        public Usuario usuario { get; set; } // Relación con Usuario
        public List<DetallePedido> Detalles { get; set; } // Relación con DetallePedido
        public Pago Pago { get; set; }
    }


    public enum EstadoPedido
    {
        Pendiente,
        Procesado,
        Enviado,
        Entregado,
        Cancelado
    }

}
