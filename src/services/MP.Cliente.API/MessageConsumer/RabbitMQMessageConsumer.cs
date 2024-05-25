
using MP.Cliente.API.Interfaces;
using MP.Cliente.API.Models;
using MP.Cliente.API.Models.Dtos;
using MP.Cliente.API.Repositories;
using MP.Core.Integration;
using MP.Core.ObjetosDeDominio;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace MP.Cliente.API.MessageConsumer
{
    public class RabbitMQMessageConsumer : BackgroundService
    {
        private readonly CriarClienteFromRabbitMQRepository _criarClienteRepository;
        private IConnection _connection;
        private IModel _chanel;

        public RabbitMQMessageConsumer(CriarClienteFromRabbitMQRepository criarClienteRepository)
        {
            _criarClienteRepository = criarClienteRepository;           
            
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest",
            };
            _connection = factory.CreateConnection();
            _chanel = _connection.CreateModel();
            _chanel.QueueDeclare(queue:"new-cliente-queue", false, false, false,arguments: null);

        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
           stoppingToken.ThrowIfCancellationRequested();
           var consummer = new EventingBasicConsumer(_chanel);
            consummer.Received += (chanel, evento) =>
            {               
                var content = Encoding.UTF8.GetString(evento.Body.ToArray());
                UsuarioDTO clienteVO = JsonSerializer.Deserialize<UsuarioDTO>(content);
                var novoCliente = new Usuario(clienteVO.Nome, clienteVO.Email,clienteVO.Cpf.Numero.ToString());
                CadastrarCliente(novoCliente).GetAwaiter().GetResult();
                _chanel.BasicAck(evento.DeliveryTag, false);
            };
            _chanel.BasicConsume("new-cliente-queue", false, consummer);
            return Task.CompletedTask;
        }
        private async Task CadastrarCliente(Usuario usuario)
        {
            var novoUsuario = new Usuario(usuario.Nome, usuario.Email, usuario.Cpf.ToString());
            _criarClienteRepository.Adicionar(novoUsuario);  
           
        }
    }
}
