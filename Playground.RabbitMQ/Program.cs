using System.Text;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// ===============================================================

// Setup
var factory = new ConnectionFactory { HostName = "localhost" };
var connection = factory.CreateConnection();

// ===============================================================
var app = builder.Build();
app.UseSwagger().UseSwaggerUI();
app.MapPost("/publish", (string routingKey, string message) =>
{
    var channel = connection.CreateModel();
    
    var properties = channel.CreateBasicProperties();
    properties.Persistent = false;
    
    channel.BasicPublish(
        exchange: string.Empty,
        routingKey: routingKey,
        basicProperties: properties,
        body: Encoding.UTF8.GetBytes(message));
});
app.Run();

// docker run -it --rm --name rabbitmq -d -p 5672:5672 -p 15672:15672 rabbitmq:3.13-management