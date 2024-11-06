using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Categoria
    {

        [Key]
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public IList<Producto> Productos { get; set; } = new List<Producto>();
    }
}
