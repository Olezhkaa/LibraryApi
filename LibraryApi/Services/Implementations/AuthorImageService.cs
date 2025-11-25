
using LibraryApi.DTOs.AuthorImages;
using LibraryApi.Models;
using LibraryApi.Repositories.Interfaces;
using LibraryApi.Services.Interfaces;
using SixLabors.ImageSharp;


namespace LibraryApi.Services.Implementations
{
    public class AuthorImageService : IAuthorImageService
    {
        private readonly IAuthorImageRepository _authorImageRepository;
        private readonly IAuthorRepository _authorRepository;

        public AuthorImageService(
            IAuthorImageRepository authorImageRepository,
            IAuthorRepository authorRepository)
        {
            _authorImageRepository = authorImageRepository;
            _authorRepository = authorRepository;
        }

        public async Task<IEnumerable<AuthorImageDto>> GetByAuthorIdAsync(int authorId)
        {
            var images = await _authorImageRepository.GetByAuthorIdAsync(authorId);
            return images.Select(MapToDto);
        }

        public async Task<AuthorImageDto?> GetByIdAsync(int id)
        {
            var image = await _authorImageRepository.GetByIdAsync(id);
            return image == null ? null : MapToDto(image);
        }

        public async Task<AuthorImageDto> CreateAsync(int authorId, UploadAuthorImageDto createDto)
        {
            // Проверяем существование автора
            if (!await _authorRepository.ExistAsync(a => a.Id == authorId))
                throw new ArgumentException($"Автор с ID {authorId} не найден");

            // Валидация файла
            ValidateImageFile(createDto.Image);

            // Читаем файл в массив байтов
            using var memoryStream = new MemoryStream();
            await createDto.Image.CopyToAsync(memoryStream);
            var fileData = memoryStream.ToArray();

            // Если это главное изображение, снимаем флаг с других
            if (createDto.IsMain)
            {
                await RemoveMainImageFlagAsync(authorId);
            }

            // Создаем сущность
            var image = new AuthorImage(
                authorId: authorId,
                fileName: Path.GetFileName(createDto.Image.FileName),
                contentType: createDto.Image.ContentType,
                fileData: fileData,
                isMain: createDto.IsMain
            )
            {
                FileSize = createDto.Image.Length
            };

            var created = await _authorImageRepository.CreateAsync(image);
            return MapToDto(created);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _authorImageRepository.DeleteAsync(id);
        }

        public async Task<bool> SetMainAsync(int authorId, int imageId)
        {
            return await _authorImageRepository.SetMainImageAsync(authorId, imageId);
        }

        public async Task<byte[]?> GetFileDataAsync(int imageId)
        {
            var image = await _authorImageRepository.GetByIdAsync(imageId);
            return image?.FileData;
        }

        public async Task<string?> GetContentTypeAsync(int imageId)
        {
            var image = await _authorImageRepository.GetByIdAsync(imageId);
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

        private async Task RemoveMainImageFlagAsync(int authorId)
        {
            var currentMainImages = await _authorImageRepository.FindAsync(img =>
                img.AuthorId == authorId && img.IsMain && img.IsActive);

            foreach (var img in currentMainImages)
            {
                img.IsMain = false;
                await _authorImageRepository.UpdateAsync(img);
            }
        }

        private AuthorImageDto MapToDto(AuthorImage image)
        {
            return new AuthorImageDto
            {
                Id = image.Id,
                AuthorId = image.AuthorId,
                FileName = image.FileName,
                ContentType = image.ContentType,
                FileSize = image.FileSize,
                Url = $"/api/authors/{image.AuthorId}/images/{image.Id}",
                IsMain = image.IsMain,
            };
        }
    }
}