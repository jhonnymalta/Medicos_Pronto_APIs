using MP.Core.ObjetosDeDominio;
namespace MP.Agenda.API.Models
{
    public class Consulta : Entity, IAggregateRoot
    {
        public DateTime Data { get; set; }
        public string Descricao { get; set; }
        public bool Confirmado { get; set; }
        public bool Finalizado { get; set; }
        public bool Cancelado { get; set; }
    }
}
