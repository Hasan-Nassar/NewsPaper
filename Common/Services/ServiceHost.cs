using System;
using Common.Commands;
using Common.Events;
using Common.RabbitMq;
using Microsoft.AspNetCore;
using RawRabbit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;


namespace Common.Service
{
    public class ServiceHost:IServiceHost
    {
        private readonly IWebHost _webHost;

        public ServiceHost(IWebHost webHost)
        {
            _webHost = webHost;
        }

        public void Run() => _webHost.Run();

        public static HostBuilder Create<TStartup>(string[] args) where TStartup : class
        {
            Console.Title = typeof(TStartup).Namespace;
            IConfigurationRoot config = null;
            config = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();

            var webHostBuilder = WebHost.CreateDefaultBuilder()

                .UseConfiguration(config)
                .UseStartup<TStartup>();

            return new HostBuilder(webHostBuilder.Build());
        }

        public abstract class BuilderBase
        {
            public abstract ServiceHost Build();
        }

        public class HostBuilder : BuilderBase
        {

            private readonly IWebHost _webHost;
            private IBusClient _bus;

            public HostBuilder(IWebHost webHost)
            {
                _webHost = webHost;
            }

            public Busbuilder UseRabbitMq()
            {
                _bus = (IBusClient) _webHost.Services.GetService(typeof(IBusClient));
                return new Busbuilder(_webHost, _bus);
            }


            public override ServiceHost Build()
            {
                return new ServiceHost(_webHost);
            }
        }

        public class Busbuilder : BuilderBase
        {
            private readonly IWebHost _webHost;
            private IBusClient _bus;

            public Busbuilder(IWebHost webHost, IBusClient bus)
            {
                _webHost = webHost;
                _bus = bus;
            }

            public Busbuilder SubscribeToCommand<TCommand>() where TCommand : ICommand
            {
                var handler = (ICommandHandle<TCommand>) _webHost.Services
                    .GetService(typeof(ICommandHandle<TCommand>));

                _bus.WithCommandHandlerAsync(handler);

                return this;

            }

            public Busbuilder SubscribeToEvent<TEvent>() where TEvent : IEvent
            {
                var handler = (IEventHandler<TEvent>) _webHost.Services
                    .GetService(typeof(IEventHandler<TEvent>));

                _bus.WithEventHandlerAsync(handler);

                return this;
            }

            public override ServiceHost Build()
            {
                return new ServiceHost(_webHost);
            }
        }
    }
}