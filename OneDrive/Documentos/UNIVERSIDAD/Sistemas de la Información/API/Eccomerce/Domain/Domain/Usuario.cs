using System;
using System.ComponentModel.DataAnnotations;


namespace Domain
{
    public class Usuario
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Contraseña { get; set; }

        public IList<Rol> Roles { get; set; } = new List<Rol>();
    }
}
