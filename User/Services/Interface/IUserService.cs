using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using User.Core.Dto;

namespace User.Services.Interface
{
    public interface IUserService
    {
        Task<UserDto> AddUser([FromBody] UserDto userDto);
        Task<UserDto> GetById(int libraryId);
        Task<UserDto> UpdateUser(int id,  UserDto userDto);
    }
}