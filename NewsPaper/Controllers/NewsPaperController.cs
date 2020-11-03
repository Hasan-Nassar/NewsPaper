using System.Threading.Tasks;
using Author.Core.Dto;
using Author.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Author.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class NewsPaperController: Controller
    {
        
        private readonly IAuthorService _authorService;
        public NewsPaperController( IAuthorService authorService )
        {
            _authorService = authorService;
        }
        
        [HttpPost("Add Author")]
        public async Task<IActionResult> Add([FromBody] AuthorDto authorDto) => Ok(await _authorService.AddAuthor(authorDto));
        
        
        [HttpGet("GetById")]
        public async Task<IActionResult> Get(int libraryId) => Ok(await _authorService.GetById(libraryId));
        
        
        [HttpPut("Update")]
        public async Task<IActionResult> Update(int id, [FromBody] AuthorDto authorDto) => Ok(await _authorService.UpdateAuthor(id, authorDto));
        
    }
}