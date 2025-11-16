using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi.DTOs.Authors
{
    public class AuthorDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? MiddleName { get; set; } = string.Empty;
        public DateOnly? DateOfBirh { get; set; }
        public DateOnly? DateOfDeath { get; set; }
        public string? Country { get; set; } = string.Empty;
    }
}