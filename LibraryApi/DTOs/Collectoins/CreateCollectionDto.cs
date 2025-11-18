using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi.DTOs.Collectoins
{
    public class CreateCollectionDto
    {
        [Required(ErrorMessage = "Название - обязательное поле")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Название должно быть не менее 1 и не более 200 символов")]
        public string Title { get; set; } = string.Empty;
    }
}