using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

Console.WriteLine("RabbitMQ.Consumer started");

const string queue = "queue-test-1";

var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(
    queue: queue,
    durable: false, 
    exclusive: false, 
    autoDelete: false, 
    arguments: null);
    
Console.WriteLine("Listening...");

var consumer = new EventingBasicConsumer(channel);

consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($"Received: {message}");
};
channel.BasicConsume(queue: queue,
    autoAck: true,
    consumer: consumer);
    
Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();