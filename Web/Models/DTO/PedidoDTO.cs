using System;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class PedidoDTO
    {
        public Guid ?Id { get; set; }

        public Guid ? UsuarioId { get; set; }

        

        public string Estado { get; set; }

        public decimal Total { get; set; }


    }
}
