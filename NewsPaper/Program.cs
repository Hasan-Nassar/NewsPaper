using Common.Commands;
using Common.Events;
using Common.Service;


namespace Author
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ServiceHost.Create<Startup>(args)
                .UseRabbitMq()
                .SubscribeToEvent<UserCreatedEvent>()
                .Build()
                .Run();
        
        }
    }
}