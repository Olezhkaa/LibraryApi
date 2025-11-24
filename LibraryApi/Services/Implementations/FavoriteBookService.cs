
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
        public FavoriteBookService(IFavoriteBookRepository favoriteBookRepository, IUserRepository userRepository, IBookRepository bookRepository)
        {
            _favoriteBookRepository = favoriteBookRepository;
            _userRepository = userRepository;
            _bookRepository = bookRepository;
        }


        public async Task<IEnumerable<FavoriteBookDto>> GetAllAsync()
        {
            var favoriteBooks = await _favoriteBookRepository.GetAllWithDetailAsync();
            return favoriteBooks.Select(MapToDto);
        }
        public async Task<FavoriteBookDto?> GetByIdAsync(int id)
        {
            var favoriteBook = await _favoriteBookRepository.GetByIdWithDetailAsync(id);
            return favoriteBook == null ? null : MapToDto(favoriteBook);
        }
        public async Task<FavoriteBookDto> CreateAsync(CreateFavoriteBookDto createDto)
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

            var favoriteBook = MapToEntity(createDto);

            var created = await _favoriteBookRepository.CreateAsync(favoriteBook);
            var favoriteBookResult = await _favoriteBookRepository.GetByIdWithDetailAsync(created.Id);

            return MapToDto(favoriteBookResult!);
        }
        public async Task<FavoriteBookDto?> ReplacePriorityInList(int id, int newPriority)
        {
            var favoriteBook = await _favoriteBookRepository.GetByIdWithDetailAsync(id);
            if (favoriteBook == null) return null;

            favoriteBook.PriorityInList = newPriority;
            var update = await _favoriteBookRepository.UpdateAsync(favoriteBook);
            var favoriteBookResult = await _favoriteBookRepository.GetByIdWithDetailAsync(update.Id);

            return MapToDto(favoriteBookResult!);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            return await _favoriteBookRepository.DeleteAsync(id);
        }
        public async Task<IEnumerable<FavoriteBookDto>> SearchAsync(string searchTerm, int userId)
        {
            var favoriteBooks = await _favoriteBookRepository.SearchBookAsync(searchTerm, userId);
            return favoriteBooks.Select(MapToDto);
        }
        public async Task<IEnumerable<FavoriteBookDto>> GetByUserIdAsync(int userId)
        {
            var favoriteBooks = await _favoriteBookRepository.GetByUserIdAsync(userId);
            return favoriteBooks.Select(MapToDto);
        }
        public async Task<IEnumerable<FavoriteBookDto>> GetByBookIdAsync(int bookId)
        {
            var favoriteBooks = await _favoriteBookRepository.GetByBookIdAsync(bookId);
            return favoriteBooks.Select(MapToDto);
        }

        private FavoriteBookDto MapToDto(FavoriteBook favoriteBook)
        {
            return new FavoriteBookDto
            {
                Id = favoriteBook.Id,
                UserFullName = favoriteBook.User!.FullName,
                BookTitle = favoriteBook.Book!.Title,
                PriorityInList = favoriteBook!.PriorityInList,
            };
        }

        private FavoriteBook MapToEntity(CreateFavoriteBookDto createDto)
        {
            return new FavoriteBook
            (
                userId: createDto.UserId,
                bookId: createDto.BookId
            )
            { PriorityInList = createDto.PriorityInList };
        }
    }
}