using System;
using System.Threading.Tasks;
using Author.Core.Dto;
using Author.Services.Interface;
using AutoMapper;
using Common.Commands;
using Common.Events;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;

namespace Library.Handler
{ 
    public class UserCreatedEventHandler : IEventHandler<UserCreatedEvent>
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public UserCreatedEventHandler( IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task HandleASync(UserCreatedEvent @event)
        {
            Console.WriteLine(@event.Name);

            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var _authorService = scope.ServiceProvider.GetService<IAuthorService>();
                    var _mapper = scope.ServiceProvider.GetService<IMapper>();
                    var dto = await _authorService.AddAuthor(_mapper.Map<UserCreatedEvent, AuthorDto>(@event));
                }
            }
            
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}