using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RabbitMQ.Publisher
{
    class Program
    {
        public enum LogNames
        {
            Critical = 1,
            Error = 2,
            Warning = 3,
            Info = 4
        }

        static void Main(string[] args)
        {
            var factory = new ConnectionFactory();
            factory.Uri = new Uri("amqps://kqzmktud:ESpgbAqKzq_uSrdLQp2tbc1O1oFe4AM3@toad.rmq.cloudamqp.com/kqzmktud");
            using var connection = factory.CreateConnection();

            var channel = connection.CreateModel();

            channel.ExchangeDeclare("header-exchange", durable: true, type: ExchangeType.Headers);

            Dictionary<string, object> headers = new Dictionary<string, object>();

            headers.Add("format", "pdf");
            headers.Add("shape", "a4");

            var properties = channel.CreateBasicProperties();
            properties.Headers = headers;
            properties.Persistent = true;

            channel.BasicPublish("header-exchange", string.Empty, properties, Encoding.UTF8.GetBytes("header mesajım"));

            Console.WriteLine("Mesaj Gönderilmiştir.");
            Console.ReadLine();
        }
    }
}
