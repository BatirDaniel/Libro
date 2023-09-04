using Libro.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;

namespace Libro.Business.Libra.DTOs.IdentityDTOs
{
    public class UpdateUserDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string ConfirmPassword { get; set; }
        public string Password { get; set; }
        public string Telephone { get; set; }
        public bool IsArchieved { get; set; }
        public Role Role { get; set; }
    }
}
