using LibraryApi.DTOs.CollectionBook;
using LibraryApi.Models;
using LibraryApi.Repositories.Interfaces;
using LibraryApi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace LibraryApi.Services.Implementations
{
    public class CollectionBookService : ICollectionBookService
    {
        private readonly ICollectionBookRepository _collectionBookRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBookRepository _bookRepository;
        private readonly ICollectionRepository _collectionRepository;

        public CollectionBookService(
            ICollectionBookRepository collectionBookRepository,
            IUserRepository userRepository,
            IBookRepository bookRepository,
            ICollectionRepository collectionRepository)
        {
            _collectionBookRepository = collectionBookRepository;
            _userRepository = userRepository;
            _bookRepository = bookRepository;
            _collectionRepository = collectionRepository;
        }

        public async Task<IEnumerable<CollectionBookDto>> GetAllAsync()
        {
            var collectionBooks = await _collectionBookRepository.GetAllWithDetailsAsync();
            return collectionBooks.Select(MapToDto);
        }

        public async Task<CollectionBookDto?> GetByIdAsync(int id)
        {
            var collectionBook = await _collectionBookRepository.GetByIdWithDetailsAsync(id);
            return collectionBook == null ? null : MapToDto(collectionBook);
        }

        public async Task<CollectionBookDto> AddBookToCollectionAsync(int userId, int collectionId, int bookId)
        {
            await ValidateRelationsExistAsync(userId, bookId, collectionId);

            // Проверяем, не существует ли уже такая связь
            if (await _collectionBookRepository.ExistsAsync(userId, bookId, collectionId))
            {
                throw new ArgumentException("Эта книга уже добавлена в данную коллекцию");
            }

            var collectionBook = new CollectionBook
            (
                userId: userId,
                bookId: bookId,
                collectionId: collectionId
            );

            var created = await _collectionBookRepository.CreateAsync(collectionBook);
            return MapToDto(created);
        }

        public async Task<CollectionBookDto?> MoveBookToCollectionAsync(int userId, int sourceCollectionId, int bookId, int targetCollectionId)
        {
            // Находим текущую связь
            var current = await _collectionBookRepository.GetByUserBookAndCollectionAsync(userId, bookId, sourceCollectionId);
            if (current == null)
                return null;

            await ValidateRelationsExistAsync(userId, bookId, targetCollectionId);

            // Проверяем, не существует ли уже такая связь в целевой коллекции
            if (await _collectionBookRepository.ExistsAsync(userId, bookId, targetCollectionId))
            {
                throw new ArgumentException("Эта книга уже находится в целевой коллекции");
            }

            // Обновляем коллекцию
            current.CollectionId = targetCollectionId;
            current.UpdatedAt = DateTime.UtcNow;

            var updated = await _collectionBookRepository.UpdateAsync(current);
            return MapToDto(updated);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _collectionBookRepository.DeleteAsync(id);
        }

        public async Task<bool> RemoveBookFromCollectionAsync(int userId, int bookId, int collectionId)
        {
            return await _collectionBookRepository.RemoveBookFromCollectionAsync(userId, bookId, collectionId);
        }

        public async Task<IEnumerable<CollectionBookDto>> SearchAsync(string searchTerm, int collectionId, int userId)
        {
            var results = await _collectionBookRepository.SearchBooksInCollectionAsync(searchTerm, collectionId, userId);
            return results.Select(MapToDto);
        }

        public async Task<IEnumerable<CollectionBookDto>> GetByUserIdAsync(int userId)
        {
            var collectionBooks = await _collectionBookRepository.GetByUserIdAsync(userId);
            return collectionBooks.Select(MapToDto);
        }

        public async Task<IEnumerable<CollectionBookDto>> GetByBookIdAsync(int bookId)
        {
            var collectionBooks = await _collectionBookRepository.GetByBookIdAsync(bookId);
            return collectionBooks.Select(MapToDto);
        }

        public async Task<IEnumerable<CollectionBookDto>> GetByCollectionIdAsync(int collectionId)
        {
            var collectionBooks = await _collectionBookRepository.GetWithDetailsByCollectionAsync(collectionId);
            return collectionBooks.Select(MapToDto);
        }

        public async Task<int> GetBooksCountInCollectionAsync(int collectionId)
        {
            return await _collectionBookRepository.GetBooksCountInCollectionAsync(collectionId);
        }

        // Private methods
        private async Task ValidateRelationsExistAsync(int userId, int bookId, int collectionId)
        {
            var userExists = await _userRepository.ExistAsync(u => u.Id == userId);
            if (!userExists)
                throw new ArgumentException($"Пользователь с ID {userId} не найден");

            var bookExists = await _bookRepository.ExistAsync(b => b.Id == bookId);
            if (!bookExists)
                throw new ArgumentException($"Книга с ID {bookId} не найдена");

            var collectionExists = await _collectionRepository.ExistAsync(c => c.Id == collectionId);
            if (!collectionExists)
                throw new ArgumentException($"Коллекция с ID {collectionId} не найдена");
        }

        private CollectionBookDto MapToDto(CollectionBook collectionBook)
        {
            return new CollectionBookDto
            {
                Id = collectionBook.Id,
                UserId = collectionBook.UserId,
                CollectionId = collectionBook.CollectionId,
                BookId = collectionBook.BookId,
                UserFullName = $"{collectionBook.User?.FirstName} {collectionBook.User?.LastName}",
                CollectionTitle = collectionBook.Collection?.Title ?? string.Empty,
                BookTitle = collectionBook.Book?.Title ?? string.Empty,
                BookAuthor = collectionBook.Book?.Author != null
                    ? $"{collectionBook.Book.Author.FirstName} {collectionBook.Book.Author.LastName}"
                    : string.Empty,
            };
        }





    }
}