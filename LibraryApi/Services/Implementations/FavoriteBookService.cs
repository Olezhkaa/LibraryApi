
using LibraryApi.DTOs.FavoriteBooks;
using LibraryApi.Models;
using LibraryApi.Repositories.Interfaces;
using LibraryApi.Services.Interfaces;

namespace LibraryApi.Services.Implementations
{
    public class FavoriteBookService : IFavoriteBookService
    {
        private readonly IFavoriteBookRepository _favoriteBookRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBookRepository _bookRepository;

        public FavoriteBookService(
            IFavoriteBookRepository favoriteBookRepository,
            IUserRepository userRepository,
            IBookRepository bookRepository)
        {
            _favoriteBookRepository = favoriteBookRepository;
            _userRepository = userRepository;
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<FavoriteBookDto>> GetAllAsync()
        {
            var favorites = await _favoriteBookRepository.GetAllWithDetailsAsync();
            return favorites.Select(MapToDto);
        }

        public async Task<FavoriteBookDto?> GetByIdAsync(int id)
        {
            var favoriteBook = await _favoriteBookRepository.GetByIdWithDetailsAsync(id);
            return favoriteBook == null ? null : MapToDto(favoriteBook);
        }

        public async Task<FavoriteBookDto> AddToFavoritesAsync(int userId, int bookId, int priority = 1)
        {
            await ValidateUserAndBookExistAsync(userId, bookId);

            // Проверяем, не добавлена ли уже книга в избранное
            if (await _favoriteBookRepository.ExistsAsync(userId, bookId))
            {
                throw new ArgumentException("Эта книга уже добавлена в избранное");
            }

            // Если приоритет не указан, ставим следующим после максимального
            if (priority <= 1)
            {
                var userFavoritesCount = await _favoriteBookRepository.GetUserFavoritesCountAsync(userId);
                priority = userFavoritesCount + 1;
            }

            var favoriteBook = new FavoriteBook(userId, bookId)
            {
                PriorityInList = priority,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var created = await _favoriteBookRepository.CreateAsync(favoriteBook);
            return MapToDto(created);
        }

        public async Task<bool> RemoveFromFavoritesAsync(int userId, int bookId)
        {
            return await _favoriteBookRepository.RemoveFromFavoritesAsync(userId, bookId);
        }

        public async Task<bool> UpdatePriorityAsync(int userId, int bookId, int priority)
        {
            if (priority <= 0)
                throw new ArgumentException("Приоритет должен быть положительным числом");

            return await _favoriteBookRepository.UpdatePriorityAsync(userId, bookId, priority);
        }

        public async Task<bool> IsBookInFavoritesAsync(int userId, int bookId)
        {
            return await _favoriteBookRepository.ExistsAsync(userId, bookId);
        }

        public async Task<IEnumerable<FavoriteBookDto>> GetUserFavoritesAsync(int userId)
        {
            var favorites = await _favoriteBookRepository.GetUserFavoritesWithDetailsAsync(userId);
            return favorites.Select(MapToDto);
        }

        public async Task<int> GetUserFavoritesCountAsync(int userId)
        {
            return await _favoriteBookRepository.GetUserFavoritesCountAsync(userId);
        }

        private async Task ValidateUserAndBookExistAsync(int userId, int bookId)
        {
            var userExists = await _userRepository.ExistAsync(u => u.Id == userId);
            if (!userExists)
                throw new ArgumentException($"Пользователь с ID {userId} не найден");

            var bookExists = await _bookRepository.ExistAsync(b => b.Id == bookId);
            if (!bookExists)
                throw new ArgumentException($"Книга с ID {bookId} не найдена");
        }

        private FavoriteBookDto MapToDto(FavoriteBook favoriteBook)
        {
            return new FavoriteBookDto
            {
                Id = favoriteBook.Id,
                UserId = favoriteBook.UserId,
                BookId = favoriteBook.BookId,
                PriorityInList = favoriteBook.PriorityInList,
                BookTitle = favoriteBook.Book?.Title ?? string.Empty,
                BookAuthor = favoriteBook.Book?.Author != null
                    ? $"{favoriteBook.Book.Author.FirstName} {favoriteBook.Book.Author.LastName}"
                    : string.Empty,
            };
        }
    }
}