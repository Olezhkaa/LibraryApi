using LibraryApi.DTOs.UserImages;

namespace LibraryApi.Services.Interfaces
{
    public interface IUserImageService
    {
        Task<UserImageDto?> GetByUserIdAsync(int userId);
        Task<UserImageDto?> GetByIdAsync(int id);
        Task<UserImageDto> CreateAsync(int userId, UploadUserImageDto createDto);
        Task<bool> DeleteAsync(int id);
        Task<byte[]?> GetFileDataAsync(int imageId);
        Task<string?> GetContentTypeAsync(int imageId);
    }
}