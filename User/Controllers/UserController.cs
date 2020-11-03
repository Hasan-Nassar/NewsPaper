using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using User.Core.Dto;
using User.Services.Interface;

namespace User.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class UserController: Controller
    {
        
        private readonly IUserService _userService;
        public UserController( IUserService userService )
        {
            _userService = userService;
        }
        
        [HttpPost("Add User")]
        public async Task<IActionResult> Add([FromBody] UserDto userDto ) => Ok(await _userService.AddUser(userDto));
        
        
        [HttpGet("GetById")]
        public async Task<IActionResult> Get(int userId) => Ok(await _userService.GetById(userId));
        
        
        [HttpPut("Update")]
        public async Task<IActionResult> Update(int id, [FromBody] UserDto userDto) => Ok(await _userService.UpdateUser(id, userDto));
        
        
    }
}