using System.Security.Cryptography;
using LibraryApi.Data;
using LibraryApi.Models;
using LibraryApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace LibraryApi.Repositories.Implementations
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context) { }

        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int Iterations = 10000;

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _dbSet.Where(u => u.IsActive && u.Email == email).FirstOrDefaultAsync();
        }

        public string HashPassword(string password)
        {
            // Генерируем соль
            byte[] salt = new byte[SaltSize];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Хешируем пароль с солью
            byte[] hash = PBKDF2(password, salt, Iterations, HashSize);

            // Объединяем соль и хеш
            byte[] hashBytes = new byte[SaltSize + HashSize];
            Array.Copy(salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

            return Convert.ToBase64String(hashBytes);
        }

        public bool VerifyPassword(string password, string hashPassword)
        {
            try
            {
                byte[] hashBytes = Convert.FromBase64String(hashPassword);

                // Извлекаем соль
                byte[] salt = new byte[SaltSize];
                Array.Copy(hashBytes, 0, salt, 0, SaltSize);

                // Хешируем введенный пароль с той же солью
                byte[] hash = PBKDF2(password, salt, Iterations, HashSize);

                // Сравниваем хеши
                for (int i = 0; i < HashSize; i++)
                {
                    if (hashBytes[i + SaltSize] != hash[i])
                        return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        private byte[] PBKDF2(string password, byte[] salt, int iterations, int outputBytes)
        {
            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
            return pbkdf2.GetBytes(outputBytes);
        }
    }
}