using MP.Core.ObjetosDeDominio;

namespace MP.Cliente.API.Models
{
    public class Usuario : Entity , IAggregateRoot
    {
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public Cpf Cpf { get; private set; }
        public bool Excluido { get; private set; }
        public Endereco Endereco { get; private set; }

        protected Usuario() { }

        public Usuario(string nome, string email, string cpf)
        {
            Nome = nome;
            Email = email;
            Cpf = new Cpf(cpf);
            Excluido = false;             
        } 
    }
  
}
