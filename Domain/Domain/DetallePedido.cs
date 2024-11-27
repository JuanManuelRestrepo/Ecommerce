using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class DetallePedido
    {
        
            public Guid Id { get; set; }
            public Guid PedidoId { get; set; }
            public Pedido Pedido { get; set; }
            public Guid ProductoId { get; set; }
            public Producto Producto { get; set; }
            public int Cantidad { get; set; }
            public decimal Precio { get; set; }
        

    }


}

