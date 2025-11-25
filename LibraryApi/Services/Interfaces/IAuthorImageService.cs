using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryApi.DTOs.AuthorImages;

namespace LibraryApi.Services.Interfaces
{
    public interface IAuthorImageService
    {
        Task<IEnumerable<AuthorImageDto>> GetByAuthorIdAsync(int authorId);
        Task<AuthorImageDto?> GetByIdAsync(int id);
        Task<AuthorImageDto> CreateAsync(int authorId, UploadAuthorImageDto createDto);
        Task<bool> DeleteAsync(int id);
        Task<bool> SetMainAsync(int authorId, int imageId);
        Task<byte[]?> GetFileDataAsync(int imageId);
        Task<string?> GetContentTypeAsync(int imageId);
    }
}