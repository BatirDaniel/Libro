using Libro.Business.Managers;
using Libro.DataAccess.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libro.Business.Commands.IdentityCommands
{
    public class AddUserCommand : IRequest<string>
    {
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }

        public string? Name
        {
            get { return string.Concat(Firstname, " ", Lastname); }
            set { Firstname = value; Lastname = value; }
        }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

        public string? ConfirmPassword { get; set; }

        public string? Telephone { get; set; }
        public string? IdUserType { get; set; }
        public DateTime? DateRegistered { get; set; }
    }
}
