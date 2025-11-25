using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi.DTOs.FavoriteBooks
{
    public class UpdateFavoritePriorityDto
    {
        [Required(ErrorMessage = "Приоритет обязателен")]
        [Range(1, int.MaxValue, ErrorMessage = "Приоритет должен быть положительным числом")]
        public int PriorityInList { get; set; }
    }
}