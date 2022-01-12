using RabbitMQ.Client;

namespace EventBusRabbitMQ
{
    public interface IRabbitMQPersistentConnection
    {
        bool IsConnected { get; }
        bool TryConnect();
        IModel CreateModel();
        
    }
}