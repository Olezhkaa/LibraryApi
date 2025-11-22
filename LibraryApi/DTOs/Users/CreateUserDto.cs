using System.ComponentModel.DataAnnotations;


namespace LibraryApi.DTOs.Users
{
    public class CreateUserDto
    {
        [Required(ErrorMessage = "Почта - обязательное поле")]
        [EmailAddress(ErrorMessage = "Не верный формат почты")]
        [StringLength(100, ErrorMessage = "Почта должна быть не более 100 символов")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Пароль - обязательное поле")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Пароль должен быть не менее 6 и не более 50 символов")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Фамилия - обязательное поле")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Фамииля должна быть не менее 2 и не более 100 символов")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Имя - обязательное поле")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Имя должно быть не менее 2 и не более 100 символов")]
        public string LastName { get; set; } = string.Empty;

        [StringLength(100, ErrorMessage = "Отчество должно быть не более 100 символов")]
        public string? MiddleName { get; set; }
    }
}