using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi.DTOs.Authors
{
    public class CreateAuthorDto
    {
        [Required(ErrorMessage = "Фамилия - обязательное поле")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Фамииля должна быть не менее 2 и не более 100 символов")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Имя - обязательное поле")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Имя должно быть не менее 2 и не более 100 символов")]
        public string LastName { get; set; } = string.Empty;

        [StringLength(100, ErrorMessage = "Отчество должно быть не более 100 символов")]
        public string? MiddleName { get; set; } = string.Empty;

        public DateOnly? DateOfBirh { get; set; }

        public DateOnly? DateOfDeath { get; set; }

        [StringLength(100, ErrorMessage = "Страна должна быть не более 100 символов")]
        public string? Country { get; set; } = string.Empty;
    }
}