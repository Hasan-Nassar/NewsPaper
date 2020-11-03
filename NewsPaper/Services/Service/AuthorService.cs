using System;
using System.Threading.Tasks;
using Author.Core.Dto;
using Author.Persistence.Interfaces;
using Author.Services.Interface;
using AutoMapper;
using Author.Core.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Author.Services.Service
{
    public class AuthorService : IAuthorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
      

        public AuthorService(IUnitOfWork unitOfWork ,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            
        }
        public async Task<AuthorDto> AddAuthor([FromBody] AuthorDto authorDto)
        {
            var author = _mapper.Map<AuthorDto, Core.Entity.Author>(authorDto);
            _unitOfWork.AuthRepository.Add(author);
            await _unitOfWork.CompleteAsync();

            var result = _mapper.Map<Core.Entity.Author, AuthorDto>(author);
            result.AuthorName = author.AuthorName;
            return result;
        }
        
        
        public async Task<AuthorDto> GetById(int authorId)
        {
            var author = await _unitOfWork.AuthRepository.GetById(authorId);
            var p = _mapper.Map<Core.Entity.Author, AuthorDto>(author);
            p.AuthorName =author.AuthorName ;
            return p;
        }
        
        
       
        public async Task<AuthorDto> UpdateAuthor(int id, AuthorDto authorDto)
        {
            var author = await _unitOfWork.AuthRepository.GetById(id);
            if (author == null)
                throw new Exception("Not Found... ");
            var courses = _mapper.Map(authorDto, author);
            _unitOfWork.AuthRepository.Update(courses);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<Core.Entity.Author, AuthorDto>(courses);
        }

    }
}