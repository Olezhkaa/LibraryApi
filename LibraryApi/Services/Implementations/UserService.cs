using LibraryApi.DTOs.Users;
using LibraryApi.Models;
using LibraryApi.Repositories.Interfaces;
using LibraryApi.Services.Interfaces;

namespace LibraryApi.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(MapToDto);
        }

        public async Task<UserDto?> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return user == null ? null : MapToDto(user);
        }

        public async Task<UserDto> CreateAsync(CreateUserDto createDto)
        {
            //Проверка уникальности Email
            if (await _userRepository.ExistAsync(u => u.Email == createDto.Email))
            {
                throw new ArgumentException($"Пользователь с email {createDto.Email} уже существует");
            }

            var user = new User
            (
                email: createDto.Email,
                lastName: createDto.LastName,
                firstName: createDto.FirstName,
                passwordHash: _userRepository.HashPassword(createDto.Password)
            )
            {
                MiddleName = createDto.MiddleName,
            };

            var created = await _userRepository.CreateAsync(user);
            var userResult = await _userRepository.GetByIdAsync(created.Id);
            return MapToDto(userResult!);
        }

        public async Task<UserDto?> UpdateAsync(int id, UpdateUserDto updateDto)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return null;

            //Проверка Email
            if (!string.IsNullOrEmpty(updateDto.Email) && updateDto.Email != user.Email)
            {
                if (await _userRepository.ExistAsync(u => u.Email == updateDto.Email && u.Id != id))
                {
                    throw new ArgumentException($"Пользователь с email {updateDto.Email} уже существует");
                }
            }

            //Обновление полей
            if (!string.IsNullOrEmpty(updateDto.LastName) && updateDto.LastName != "string") user.LastName = updateDto.LastName;
            if (!string.IsNullOrEmpty(updateDto.FirstName) && updateDto.FirstName != "string") user.FirstName = updateDto.FirstName;
            if (!string.IsNullOrEmpty(updateDto.MiddleName) && updateDto.MiddleName != "string") user.MiddleName = updateDto.MiddleName;
            if (!string.IsNullOrEmpty(updateDto.Email) && updateDto.Email != "user@example.com") user.Email = updateDto.Email;

            var updated = await _userRepository.UpdateAsync(user);
            var userResult = await _userRepository.GetByIdAsync(updated.Id);

            return MapToDto(userResult!);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _userRepository.DeleteAsync(id);
        }


        public async Task<UserDto?> AuthAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null) return null;

            if (!_userRepository.VerifyPassword(password, user.PasswordHash)) return null;

            return MapToDto(user);
        }

        public async Task<bool> ChangePasswordAsync(int id, string currentPassword, string newPassword)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return false;

            if (!_userRepository.VerifyPassword(currentPassword, user.PasswordHash)) return false;

            user.PasswordHash = _userRepository.HashPassword(newPassword);
            await _userRepository.UpdateAsync(user);
            return true;
        }

        private UserDto MapToDto(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                MiddleName = user.MiddleName,
            };
        }



    }
}