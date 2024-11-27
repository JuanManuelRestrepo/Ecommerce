namespace ApiSampleFinal.Models.DTO
{
    public class UsuarioDTO
    {
        public Guid? id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        public long Telefono { get; set; }
        public string Contraseña { get; set; }
    }
}
