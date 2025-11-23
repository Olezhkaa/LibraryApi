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
        public CollectionBookService(ICollectionBookRepository collectionBookRepository, IUserRepository userRepository, IBookRepository bookRepository, ICollectionRepository collectionRepository)
        {
            _collectionBookRepository = collectionBookRepository;
            _userRepository = userRepository;
            _bookRepository = bookRepository;
            _collectionRepository = collectionRepository;
        }


        public async Task<IEnumerable<CollectionBookDto>> GetAllAsync()
        {
            var collectionBooks = await _collectionBookRepository.GetAllWithDetailAsync();
            return collectionBooks.Select(MapToDto);
        }
        public async Task<CollectionBookDto?> GetByIdAsync(int id)
        {
            var collectionBook = await _collectionBookRepository.GetByIdWithDetailAsync(id);
            return collectionBook == null ? null : MapToDto(collectionBook);
        }
        public async Task<CollectionBookDto> CreateAsync(CreateCollectionBookDto createDto)
        {
            //Проверка пользователя
            if (!await _userRepository.ExistAsync(a => a.Id == createDto.UserId))
            {
                throw new ArgumentException($"Пользователь с ID {createDto.UserId} не найден");
            }
            //Проверка книги
            if (!await _bookRepository.ExistAsync(a => a.Id == createDto.BookId))
            {
                throw new ArgumentException($"Книга с ID {createDto.BookId} не найден");
            }
            //Проверка коллекции
            if (!await _collectionRepository.ExistAsync(a => a.Id == createDto.CollectionId))
            {
                throw new ArgumentException($"Коллекция с ID {createDto.CollectionId} не найден");
            }

            var collectionBook = MapToEntity(createDto);

            var created = await _collectionBookRepository.CreateAsync(collectionBook);
            var collectionBookResult = await _collectionBookRepository.GetByIdWithDetailAsync(created.Id);

            return MapToDto(collectionBookResult!);
        }
        public async Task<CollectionBookDto?> ReplaceBookCollectionDto(int id, CreateCollectionBookDto createDto)
        {
            var collectionBook = await _collectionBookRepository.GetByIdWithDetailAsync(id);
            if (collectionBook == null) return null;

            if (collectionBook.UserId != createDto.UserId)
            {
                throw new ArgumentException("Нельзя изменить пользователя у связи книга-коллекция");
            }

            await _collectionBookRepository.DeleteAsync(id);

            return await CreateAsync(createDto);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            return await _collectionBookRepository.DeleteAsync(id);
        }
        public async Task<IEnumerable<CollectionBookDto>> SearchAsync(string searchTerm, int collectionId, int userId)
        {
            var collectionBooks = await _collectionBookRepository.SearchBookInCollectionAsync(searchTerm, collectionId, userId);
            return collectionBooks.Select(MapToDto);
        }
        public async Task<IEnumerable<CollectionBookDto>> GetByUserIdAsync(int userId)
        {
            var collectionBooks = await _collectionBookRepository.GetByUserIdAsync(userId);
            return collectionBooks.Select(MapToDto);
        }
        public async Task<IEnumerable<CollectionBookDto>> GetByBookIdAsync(int bookId)
        {
            var collectionBooks = await _collectionBookRepository.GetByBookIdAsync(bookId);
            return collectionBooks.Select(MapToDto);        }

        public async Task<IEnumerable<CollectionBookDto>> GetByCollectionIdAsync(int collectionId)
        {
            var collectionBooks = await _collectionBookRepository.GetByCollectionIdAsync(collectionId);
            return collectionBooks.Select(MapToDto);
        }

        private CollectionBookDto MapToDto(CollectionBook collectionBook)
        {
            return new CollectionBookDto
            {
                Id = collectionBook.Id,
                UserFullName = collectionBook.User!.FullName,
                BookTitle = collectionBook.Book!.Title,
                CollectionTitle = collectionBook.Collection!.Title
            };
        }

        private CollectionBook MapToEntity(CreateCollectionBookDto createDto)
        {
            return new CollectionBook
            (
                userId: createDto.UserId,
                bookId: createDto.BookId,
                collectionId: createDto.CollectionId
            ) {};
        }





    }
}