using MP.Core.Integration;
using MP.Core.ObjetosDeDominio;

namespace MP.Identidade.API.Models
{
    public class ClienteVo: BaseMessage
    {
        public string Nome { get;  set; }
        public string Email { get;  set; }
        public Cpf Cpf { get;  set; }       
       

    }
}
