using MP.Core.Integration;
using MP.Identidade.API.Models;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace MP.Identidade.API.RabbitMQSender
{
    public class RabbitMQMessageSender : IRabbitMQMessageSender
    {
        private readonly string _hostName;
        private readonly string _password;
        private readonly string _userName;
        private IConnection _connection;

        public RabbitMQMessageSender()
        {
            _hostName = "localhost";
            _password = "guest";
            _userName = "guest";
        }
        public void SendMessage(BaseMessage baseMessage, string queueName)
        {
            var factory = new ConnectionFactory
            {
                HostName = _hostName,
                UserName = _userName,
                Password = _password,
            }; 
            _connection = factory.CreateConnection();
            using var channel = _connection.CreateModel();
            channel.QueueDeclare(queueName, false, false, false, arguments: null);
            byte[] body = GetMessageAsByteArray(baseMessage);
            channel.BasicPublish("", queueName, null, body);

        }

        private byte[] GetMessageAsByteArray(BaseMessage baseMessage)
        {
            var option = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            var json = JsonSerializer.Serialize<ClienteVo>((ClienteVo)baseMessage,option);
            return Encoding.UTF8.GetBytes(json);
        }
    }
}
