using System.Linq.Expressions;
using LibraryApi.Models;

namespace LibraryApi.Repositories.Interfaces
{
    public interface IRepository<T> where T : BaseModel
    {
        //Основные операции
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<bool> DeleteAsync(int id);

        //Расширенные операции
        //Поиск
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        //Первый или по умолчанию
        Task<T?> FirstOrDefualtAsync(Expression<Func<T, bool>> predicate);
        //Проверить существование
        Task<bool> ExistAsync(Expression<Func<T, bool>> predicate);
        //Количество
        Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null);

        //Пагинация
        Task<IEnumerable<T>> GetPageAsync(int pageNumber, int pageSize);

    }
}