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
        public DateTime FechaPedido { get; set; } = DateTime.Now;
        public EstadoPedido Estado { get; set; }
        public decimal Total { get; set; }
        public Usuario Usuario { get; set; }
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
