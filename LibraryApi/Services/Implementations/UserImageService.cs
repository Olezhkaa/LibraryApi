using LibraryApi.DTOs.UserImages;
using LibraryApi.Models;
using LibraryApi.Repositories.Interfaces;
using LibraryApi.Services.Interfaces;

namespace LibraryApi.Services.Implementations
{
    public class UserImageService : IUserImageService
    {
        private readonly IUserImageRepository _userImageRepository;
        private readonly IUserRepository _userRepository;

        public UserImageService(
            IUserImageRepository userImageRepository,
            IUserRepository userRepository)
        {
            _userImageRepository = userImageRepository;
            _userRepository = userRepository;
        }

        public async Task<UserImageDto?> GetByUserIdAsync(int userId)
        {
            var image = await _userImageRepository.GetByUserIdAsync(userId);
            return image == null ? null : MapToDto(image);
        }

        public async Task<UserImageDto?> GetByIdAsync(int id)
        {
            var image = await _userImageRepository.GetByIdAsync(id);
            return image == null ? null : MapToDto(image);
        }

        public async Task<UserImageDto> CreateAsync(int userId, UploadUserImageDto createDto)
        {
            // Проверяем существование автора
            if (!await _userRepository.ExistAsync(a => a.Id == userId))
                throw new ArgumentException($"Автор с ID {userId} не найден");

            // Валидация файла
            ValidateImageFile(createDto.Image);

            // Читаем файл в массив байтов
            using var memoryStream = new MemoryStream();
            await createDto.Image.CopyToAsync(memoryStream);
            var fileData = memoryStream.ToArray();

            //Удаляем прошлое фото
            var imageOld = await _userImageRepository.GetByUserIdAsync(userId);
            if (imageOld != null) await _userImageRepository.DeleteAsync(imageOld.Id);

            // Создаем сущность
            var image = new UserImage(
                userId: userId,
                fileName: Path.GetFileName(createDto.Image.FileName),
                contentType: createDto.Image.ContentType,
                fileData: fileData
            )
            {
                FileSize = createDto.Image.Length
            };

            var created = await _userImageRepository.CreateAsync(image);
            return MapToDto(created);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _userImageRepository.DeleteAsync(id);
        }

        public async Task<byte[]?> GetFileDataAsync(int imageId)
        {
            var image = await _userImageRepository.GetByIdAsync(imageId);
            return image?.FileData;
        }

        public async Task<string?> GetContentTypeAsync(int imageId)
        {
            var image = await _userImageRepository.GetByIdAsync(imageId);
            return image?.ContentType;
        }

        // Приватные методы
        private void ValidateImageFile(IFormFile file)
        {
            if (file.Length > 10 * 1024 * 1024)
                throw new ArgumentException("Размер файла не должен превышать 10MB");

            var allowedTypes = new[] { "image/jpeg", "image/png", "image/gif", "image/webp" };
            if (!allowedTypes.Contains(file.ContentType.ToLower()))
                throw new ArgumentException("Недопустимый формат изображения. Разрешены: JPEG, PNG, GIF, WebP");
        }

        private UserImageDto MapToDto(UserImage image)
        {
            return new UserImageDto
            {
                Id = image.Id,
                UserId = image.UserId,
                FileName = image.FileName,
                ContentType = image.ContentType,
                FileSize = image.FileSize,
                Url = $"/api/users/{image.UserId}/images/{image.Id}",
            };
        }
    }
}