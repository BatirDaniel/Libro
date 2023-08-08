using Libro.DataAccess.Contracts;
using Libro.DataAccess.Repository;
using System.Security.Claims;

namespace Libro.Business.Managers
{
    public class EntityManager
    {
        protected IUnitOfWork _unitOfWork;
        protected ClaimsPrincipal _user;

        public EntityManager(UnitOfWork unitOfWork, ClaimsPrincipal user)
        {
            _unitOfWork = unitOfWork;
            _user = user;
        }
    }
}
