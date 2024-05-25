using MP.Cliente.API.Models;

namespace MP.Cliente.API.Interfaces
{
    public interface ICriarClienteFromRabbitMQRepository
    {
        void Adicionar(Usuario consulta);
    }
}
