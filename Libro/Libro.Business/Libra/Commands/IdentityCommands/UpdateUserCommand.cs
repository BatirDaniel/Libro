using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Libro.Business.Commands.IdentityCommands
{
    public class UpdateUserCommand : IRequest<string>
    {
        public string Id { get; set; }
        public string? Name { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Telephone { get; set; }
        public IdentityRole Role { get; set; }
    }
}
