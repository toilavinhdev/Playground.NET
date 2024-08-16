using System.Text;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Setup
var factory = new ConnectionFactory
{
    HostName = "localhost",
};
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

const string queue = "queue-test-1";

// Queues
channel.QueueDeclare(
    queue: queue,
    durable: false, 
    exclusive: false, 
    autoDelete: false, 
    arguments: null);

var app = builder.Build();
app.UseSwagger().UseSwaggerUI();
app.MapPost("/publish", (string message) =>
{
    channel.BasicPublish(
        exchange: string.Empty,
        routingKey: queue,
        basicProperties: null,
        body: Encoding.UTF8.GetBytes(message));
});
app.Run();