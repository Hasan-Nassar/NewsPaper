using System;
using System.Threading.Tasks;
using AutoMapper;
using Common.Commands;
using Common.Events;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;
using User.Core.Dto;
using User.Persistence.Interfaces;
using User.Services.Interface;

namespace User.Services.Service
{
    public class UserService: IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IBusClient _busClient;


        public UserService(IUnitOfWork unitOfWork ,IMapper mapper,IBusClient busClient)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _busClient = busClient;
        }
        public async Task<UserDto> AddUser([FromBody] UserDto userDto)
        {
            var user = _mapper.Map<UserDto, Core.Entity.User>(userDto);
            _unitOfWork.UserRepository.Add(user);
            await _unitOfWork.CompleteAsync();
            
            await _busClient.PublishAsync(new UserCreatedEvent()
            {
                Name = userDto.Name + " " + userDto.Password 
            });

            var result = _mapper.Map<Core.Entity.User, UserDto>(user);
          

            return result;
        }
        
        
        public async Task<UserDto> GetById(int userId)
        {
            var user = await _unitOfWork.UserRepository.GetById(userId);
            var p = _mapper.Map<Core.Entity.User, UserDto>(user);
            p.Name =user.Name ;
            return p;
        }
        
       
        public async Task<UserDto> UpdateUser(int id, UserDto userDto)
        {
            var user = await _unitOfWork.UserRepository.GetById(id);
            if (user == null)
                throw new Exception("Not Found... ");
            var courses = _mapper.Map(userDto, user);
            _unitOfWork.UserRepository.Update(courses);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<Core.Entity.User, UserDto>(courses);
        }

    }
}