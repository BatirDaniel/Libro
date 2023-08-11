using Libro.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Libro.Business.Managers
{
    public class IssueManager : EntityManager
    {
        public IssueManager(
            UnitOfWork unitOfWork,
            ClaimsPrincipal user) : base(unitOfWork, user)
        {
        }
    }
}
