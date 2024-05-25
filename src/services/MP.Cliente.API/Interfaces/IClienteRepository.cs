using MP.Cliente.API.Models;
using MP.Core.ObjetosDeDominio;

namespace MP.Cliente.API.Interfaces
{
    public interface IClienteRepository : IRepository<Usuario>
    {
            Task<IEnumerable<Usuario>> ObterTodos();
            Task<Usuario> ObterPorId(Guid id);
            void Adicionar(Usuario consulta);
            void Atualizar(Usuario consulta);
       
    }
}
