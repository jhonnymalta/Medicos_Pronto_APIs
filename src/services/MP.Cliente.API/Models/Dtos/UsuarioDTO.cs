using MP.Core.ObjetosDeDominio;

namespace MP.Cliente.API.Models.Dtos
{
    public class UsuarioDTO
    {
        public string Nome { get;  set; }
        public string Email { get;  set; }
        public Cpf Cpf { get;  set; }
    }
}
