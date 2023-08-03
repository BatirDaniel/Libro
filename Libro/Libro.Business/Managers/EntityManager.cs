using Libro.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Libro.Business.Managers
{
    public class EntityManager
    {
        protected UnitOfWork _unitOfWork;
        protected ClaimsPrincipal _user;

        public EntityManager(UnitOfWork unitOfWork, ClaimsPrincipal user)
        {
            _unitOfWork = unitOfWork;
            _user = user;
        }
    }
}
