using RabbitMQ.Client;
using System;
using System.Linq;
using System.Text;

namespace RabbitMQ.Publisher
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory();
            factory.Uri = new Uri("amqps://kqzmktud:ESpgbAqKzq_uSrdLQp2tbc1O1oFe4AM3@toad.rmq.cloudamqp.com/kqzmktud");
            using var connection = factory.CreateConnection();

            var channel = connection.CreateModel();

            channel.QueueDeclare("hello-queue", true, false, false);

            Enumerable.Range(1, 50).ToList().ForEach(x =>
             {
                 string message = $"Message {x}";

                 //to byte
                 var messageBody = Encoding.UTF8.GetBytes(message);

                 channel.BasicPublish(string.Empty, "hello-queue", null, messageBody);

                 Console.WriteLine($"Mesaj gönderilmiştir : {message}");
             });
            Console.ReadLine();
        }
    }
}
