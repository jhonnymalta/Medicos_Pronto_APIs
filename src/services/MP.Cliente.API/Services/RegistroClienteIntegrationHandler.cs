using EasyNetQ;
using MP.Cliente.API.Interfaces;
using MP.Cliente.API.Models;
using MP.Core.Integration;
using System.ComponentModel.DataAnnotations;

namespace MP.Cliente.API.Services
{    
    public class RegistroClienteIntegrationHandler : BackgroundService
    {

        private IBus _bus;
        private readonly IServiceProvider _serviceProvider;
        private readonly IClienteRepository _clienteRepository;
        public RegistroClienteIntegrationHandler(IServiceProvider serviceProvider, IClienteRepository clienteRepository)
        {
            
            _clienteRepository = clienteRepository; 
            _serviceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _bus = RabbitHutch.CreateBus("host=localhost:5672");

            _bus.Rpc.RespondAsync<UsuarioRegistradoIntegrationEvent, ResponseMessage>(async request => new ResponseMessage(
                await RegistrarCliente(request)
                ));

            return Task.CompletedTask;
        }


        private async Task<ValidationResult> RegistrarCliente(UsuarioRegistradoIntegrationEvent message)
        {
            var novoUsuario = new Usuario(message.Nome,message.Email,message.Cpf);
            ValidationResult sucesso;
            using (var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IClienteRepository>();
                mediator.Adicionar(novoUsuario);
                sucesso = 
            }
            return sucesso;  
            
        }
    }
}
