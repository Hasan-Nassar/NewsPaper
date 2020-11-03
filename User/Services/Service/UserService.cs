using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using User.Core.Dto;
using User.Persistence.Interfaces;
using User.Services.Interface;

namespace User.Services.Service
{
    public class UserService: IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
      

        public UserService(IUnitOfWork unitOfWork ,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            
        }
        public async Task<UserDto> AddUser([FromBody] UserDto userDto)
        {
            var user = _mapper.Map<UserDto, Core.Entity.User>(userDto);
            _unitOfWork.UserRepository.Add(user);
            await _unitOfWork.CompleteAsync();

            var result = _mapper.Map<Core.Entity.User, UserDto>(user);
            result.Name = user.Name;
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