using System.Threading.Tasks;
using Author.Core.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Author.Services.Interface
{
    public interface IAuthorService
    {
        Task<AuthorDto> AddAuthor([FromBody] AuthorDto authorDto);
        
        Task<AuthorDto> GetById(int authorDto);
        
        Task<AuthorDto> UpdateAuthor(int id,  AuthorDto authorDto);
    }
}