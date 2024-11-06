using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Pago
    {
        
            public Guid Id { get; set; }
            public Guid PedidoId { get; set; }
            public MetodoPago MetodoPago { get; set; }
            public decimal Monto { get; set; }
            public DateTime FechaPago { get; set; }
            public Pedido Pedido { get; set; } // Relación con Pedido
            public List<DetallePedido> DetallePedidos { get; set; } = new List<DetallePedido>();

            
        }

        public enum MetodoPago
        {
            Tarjeta,
            Efectivo,
            Transferencia
        }
    }

