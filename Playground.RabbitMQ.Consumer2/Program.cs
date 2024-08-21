using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

const string routingKey = "key-2";

var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();
    
var consumer = new EventingBasicConsumer(channel);

consumer.Received += (model, ea) =>
{
    var message = Encoding.UTF8.GetString(ea.Body.ToArray());
    Console.WriteLine($"Consumer 2 received: {message}");
};
channel.BasicConsume(queue: routingKey,
    autoAck: true,
    consumer: consumer);
    
Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();