﻿using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

const string routingKey = "key-1";

var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(
    queue: routingKey,
    durable: false, 
    exclusive: false, 
    autoDelete: false, 
    arguments: null);
    
var consumer = new EventingBasicConsumer(channel);

consumer.Received += (model, ea) =>
{
    var message = Encoding.UTF8.GetString(ea.Body.ToArray());
    Console.WriteLine($"Consumer 1 received: {message}");
};
channel.BasicConsume(queue: routingKey,
    autoAck: true,
    consumer: consumer);
    
Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();