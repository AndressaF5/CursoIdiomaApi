using System;

namespace CursoIdiomaApi.Model
{
    public class TokenUsuario
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
