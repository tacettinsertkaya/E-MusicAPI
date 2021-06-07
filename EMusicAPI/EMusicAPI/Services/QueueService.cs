using EMusicAPI.Hub;
using EMusicAPI.Models.Wrappers;
using EMusicAPI.Services.Abstraction;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMusicAPI.Services
{
    public class QueueService : IQueueService
    {
        private IConfiguration _configuration { get; }
        private readonly IHubContext<NotifHub> _hub;

        public QueueService(IConfiguration configuration,
                             IHubContext<NotifHub> hub
                            )
        {
            _configuration = configuration;
            _hub = hub;
        }
        public void SendNotifQueue(Notif notif)
        {
            var factory = new ConnectionFactory()
            {
                Uri = new Uri(_configuration["MessageQueue:RabbitMQ"])
            };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "NotifTest2",
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null
                                         );

                    string message = notif.Content;
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "NotifTest2",
                                         basicProperties: null,
                                          body: body
                                                    );

                }
            }
        }


        public void GetNotifQueue()
        {
            var factory = new ConnectionFactory()
            {
                Uri = new Uri(_configuration["MessageQueue:RabbitMQ"])
            };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {

                    channel.QueueDeclare(queue: "NotifTest2",
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += (model, mq) =>
                    {
                        var body = mq.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine($"Message: {message}");
                         _hub.Clients.All.SendAsync("notifReceiver", message);
                    };

                    channel.BasicConsume(queue: "NotifTest2",
                                         autoAck: true,
                                         consumer: consumer);

                }
            }
        }




    }
}
