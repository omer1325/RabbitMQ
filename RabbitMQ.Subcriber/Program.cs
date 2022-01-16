using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.IO;
using System.Text;
using System.Threading;

namespace RabbitMQ.Subcriber
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory();
            factory.Uri = new Uri("amqps://kqzmktud:ESpgbAqKzq_uSrdLQp2tbc1O1oFe4AM3@toad.rmq.cloudamqp.com/kqzmktud");
            using var connection = factory.CreateConnection() ;

            var channel = connection.CreateModel();

            channel.BasicQos(0, 1, false);

            var queueName = "direct-queue-Critical";

            var consumer = new EventingBasicConsumer(channel);
            channel.BasicConsume(queueName, false, consumer);

            Console.WriteLine("Loglar dinliyor...");

            consumer.Received += (object sender, BasicDeliverEventArgs e) =>
            {
                var message = Encoding.UTF8.GetString(e.Body.ToArray());

                Thread.Sleep(1500);
                Console.WriteLine("Gelen Mesaj: " + message);

                File.AppendAllText("log-critical.txt", message + "\n");

                channel.BasicAck(e.DeliveryTag, false);
            };



            
            Console.ReadLine();
        }
    }
}
