using System.Text;
using System.Text.Json;
using PlatformService.Dtos;
using RabbitMQ.Client;

namespace PlatformService.AsyncDataServices;

public class MessageBusClient : IMessageBusClient
{
    private readonly IConfiguration _configuration;
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public MessageBusClient(IConfiguration configuration)
    {
        _configuration = configuration;
        var factory = new ConnectionFactory()
        {
            HostName = _configuration["RabbitMQHost"],
            Port = int.Parse(_configuration["RabbitMQPort"])
        };
        try
        {
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);

            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Could not connect to message bus: {ex.Message}");
        }
    }
    public void PublishNewPlatform(PlatformPublishedDto platformPublished)
    {
        var message = JsonSerializer.Serialize(platformPublished);
        if (_connection.IsOpen)
        {
            Console.WriteLine("Connection is open, sending message...");
            SendMessage(message);
        }
        else
        {
            Console.WriteLine("Connection is closed, not sending!");
        }
    }

    public void Dispose()
    {
        Console.WriteLine("Message bus disposed");
        if (_channel.IsOpen)
        {
            _channel.Close();
            _connection.Close();
        }
    }

    private void SendMessage(string message)
    {
        var body = Encoding.UTF8.GetBytes(message);
        _channel.BasicPublish(exchange: "trigger", routingKey: "", body: body);
        Console.WriteLine($"Message: {message} has been sent");
    }

    private static void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
    {
        Console.WriteLine("Connection shutting down");
    }
}
