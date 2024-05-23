using MP.Agenda.API.Models;
using MP.Core.ObjetosDeDominio;

namespace MP.Agenda.API.Interfaces
{
    public interface IConsultaRepository : IRepository<Consulta>
    {
        Task<IEnumerable<Consulta>> ObterTodas();
        Task<Consulta> ObterPorId(Guid id);

        void Adicionar(Consulta consulta);
        void Atualizar(Consulta consulta);
    }
}
