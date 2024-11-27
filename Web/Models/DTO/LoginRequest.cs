namespace ApiSampleFinal.Models.DTO
{

    public class LoginRequest
    {
        public Guid? id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
