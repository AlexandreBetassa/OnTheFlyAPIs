using System.Text;
using System;
using RabbitMQ.Client;
using Models;

namespace SaleAPI.RabbitMQ
{
    public class SendRabbitMQ
    {
        public SendRabbitMQ() { }
        public void Send(Sale sale)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                Port = 8080,
                UserName = "guest",
                Password = "guest"

            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "SendSale",
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string messageSale = sale.ToString();
                var body = Encoding.UTF8.GetBytes(messageSale);

                channel.BasicPublish(exchange: "",
                                     routingKey: "SendSale",
                                     basicProperties: null,
                                     body: body);
            }
        }
    }
}
