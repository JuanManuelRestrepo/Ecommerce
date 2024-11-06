using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Rol
    {
        [Key]
        public Guid Id { get; set; }= Guid.NewGuid();

        public string RolName { get; set; }

        public IList<Usuario> Usuarios { get; set; }
    }
}
