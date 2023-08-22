using MediatR;
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
        public string? Firstname
        {
            get { return Name?.Split(' ')[0]; }
            set
            {
                if (Name != null)
                {
                    var parts = Name.Split(' ');
                    if (parts.Length > 1)
                    {
                        parts[0] = value ?? "";
                        Name = string.Join(" ", parts);
                    }
                    else
                    {
                        Name = value ?? "";
                    }
                }
            }
        }

        public string? Lastname
        {
            get { return Name?.Split(' ')[1]; }
            set
            {
                if (Name != null)
                {
                    var parts = Name.Split(' ');
                    if (parts.Length > 1)
                    {
                        parts[1] = value ?? "";
                        Name = string.Join(" ", parts);
                    }
                    else
                    {
                        Name = value ?? "";
                    }
                }
            }
        }

        public string? Name
        {
            get { return string.Join(" ", Firstname, Lastname); }
            set
            {
                var parts = (value ?? "").Split(' ');
                Firstname = parts.Length > 0 ? parts[0] : null;
                Lastname = parts.Length > 1 ? parts[1] : null;
            }
        }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Telephone { get; set; }
        public string? IdUserType { get; set; }
    }
}
