using LibraryApi.DTOs.BookImages;
using LibraryApi.Models;
using LibraryApi.Repositories.Interfaces;
using LibraryApi.Services.Interfaces;

namespace LibraryApi.Services.Implementations
{
    public class BookImageService : IBookImageService
    {
        private readonly IBookImageRepository _bookImageRepository;
        private readonly IBookRepository _bookRepository;

        public BookImageService(
            IBookImageRepository bookImageRepository,
            IBookRepository bookRepository)
        {
            _bookImageRepository = bookImageRepository;
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<BookImageDto>> GetByBookIdAsync(int bookId)
        {
            var images = await _bookImageRepository.GetByBookIdAsync(bookId);
            return images.Select(MapToDto);
        }

        public async Task<BookImageDto?> GetByIdAsync(int id)
        {
            var image = await _bookImageRepository.GetByIdAsync(id);
            return image == null ? null : MapToDto(image);
        }

        public async Task<BookImageDto> CreateAsync(int bookId, UploadBookImageDto createDto)
        {
            // Проверяем существование автора
            if (!await _bookRepository.ExistAsync(a => a.Id == bookId))
                throw new ArgumentException($"Автор с ID {bookId} не найден");

            // Валидация файла
            ValidateImageFile(createDto.Image);

            // Читаем файл в массив байтов
            using var memoryStream = new MemoryStream();
            await createDto.Image.CopyToAsync(memoryStream);
            var fileData = memoryStream.ToArray();

            // Если это главное изображение, снимаем флаг с других
            if (createDto.IsMain)
            {
                await RemoveMainImageFlagAsync(bookId);
            }

            // Создаем сущность
            var image = new BookImage(
                bookId: bookId,
                fileName: Path.GetFileName(createDto.Image.FileName),
                contentType: createDto.Image.ContentType,
                fileData: fileData,
                isMain: createDto.IsMain
            )
            {
                FileSize = createDto.Image.Length
            };

            var created = await _bookImageRepository.CreateAsync(image);
            return MapToDto(created);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _bookImageRepository.DeleteAsync(id);
        }

        public async Task<bool> SetMainAsync(int bookId, int imageId)
        {
            return await _bookImageRepository.SetMainImageAsync(bookId, imageId);
        }

        public async Task<byte[]?> GetFileDataAsync(int imageId)
        {
            var image = await _bookImageRepository.GetByIdAsync(imageId);
            return image?.FileData;
        }

        public async Task<string?> GetContentTypeAsync(int imageId)
        {
            var image = await _bookImageRepository.GetByIdAsync(imageId);
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

        private async Task RemoveMainImageFlagAsync(int bookId)
        {
            var currentMainImages = await _bookImageRepository.FindAsync(img =>
                img.BookId == bookId && img.IsMain && img.IsActive);

            foreach (var img in currentMainImages)
            {
                img.IsMain = false;
                await _bookImageRepository.UpdateAsync(img);
            }
        }

        private BookImageDto MapToDto(BookImage image)
        {
            return new BookImageDto
            {
                Id = image.Id,
                BookId = image.BookId,
                FileName = image.FileName,
                ContentType = image.ContentType,
                FileSize = image.FileSize,
                Url = $"/api/books/{image.BookId}/images/{image.Id}",
                IsMain = image.IsMain,
            };
        }
    }
}