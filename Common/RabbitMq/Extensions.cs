using System.Reflection;
using System.Threading.Tasks;
using Common.Commands;
using Common.Events;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;
using RawRabbit.Instantiation;
using Microsoft.Extensions.Configuration;
using RawRabbit.Pipe.Middleware;


namespace Common.RabbitMq
{
    public static class Extensions
    {
        public static Task WithCommandHandlerAsync<TCommand>(this IBusClient bus,
            ICommandHandle<TCommand> handler) where TCommand : ICommand
            => bus.SubscribeAsync<TCommand>(
                msg => handler.HandleAsync(msg),
                ctx => ctx.UseSubscribeConfiguration(
                    cfg => cfg.FromDeclaredQueue(
                        q => q.WithName(GetQueueName<TCommand>())
                    )
                )
            );



        public static Task WithEventHandlerAsync<TEvent>(this IBusClient bus,
            IEventHandler<TEvent> handler) where TEvent : IEvent
            => bus.SubscribeAsync<TEvent>(
                msg => handler.HandleASync(msg),
                ctx => ctx.UseSubscribeConfiguration(
                    cfg => cfg.FromDeclaredQueue(
                        q => q.WithName(GetQueueName<TEvent>())
                    )
                )
            );



        private static string GetQueueName<T>()=> $"{Assembly.GetEntryAssembly().GetName()}/{typeof(T).Name}";
        

        public static void AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            var options = new Options();
            var section = configuration.GetSection("rabbitmq");
            section.Bind(options);

            var client = RawRabbitFactory.CreateSingleton(new RawRabbitOptions
            {
                ClientConfiguration = options
            });

            services.AddSingleton<IBusClient>(_ => client);
        }
    }
}