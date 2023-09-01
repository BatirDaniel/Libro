using Libro.Business.Common.Helpers.OrderHelper;
using Libro.Business.Libra.DTOs.CityDTOs;
using Libro.Business.Libra.DTOs.ConnectionTypesDTOs;
using Libro.Business.Libra.DTOs.POSDTOs;
using Libro.Business.Libra.DTOs.TableParameters;
using Libro.DataAccess.Contracts;
using Libro.DataAccess.Data;
using Libro.DataAccess.Entities;
using Libro.DataAccess.Repository;
using Libro.Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using System.Security.Claims;

namespace Libro.Business.Managers
{
    public class PosManager : EntityManager
    {
        public Mapperly _mapperly;
        public IUnitOfWork _unitOfWork;
        public ApplicationDbContext _context;
        public ILogger<PosManager> _logger;

        public PosManager(
            IUnitOfWork unitOfWork,
            ClaimsPrincipal user,
            Mapperly mapperly,
            ApplicationDbContext context,
            ILogger<PosManager> logger) : base(unitOfWork, user)
        {
            _mapperly = mapperly;
            _unitOfWork = unitOfWork;
            _context = context;
            _logger = logger;
        }

        public async Task<string> Create(CreatePOSDTO? posDTO)
        {
            if (!await _unitOfWork.ConnectionTypes.isExists(x => x.Id == posDTO.IdConnectionType))
                return "Invalid connection type";

            var pos = _mapperly.Map(posDTO);

            pos.Id = Guid.NewGuid().ToString();
            pos.InserDate = DateTime.Now;

            await _unitOfWork.POSs.Create(pos);
            await _unitOfWork.Save();

            return null;
        }

        public async Task<string> Update(UpdatePOSDTO? posDTO)
        {
            if (!await _unitOfWork.ConnectionTypes.isExists(x => x.Id == posDTO.ConnectionType.Id))
                return "Invalid connection type";

            var pos = _mapperly.Map(posDTO);

            _unitOfWork.POSs.Update(pos);
            await _unitOfWork.Save();

            return null;
        }

        public async Task<string> Delete(string? id)
        {
            if (!await _unitOfWork.POSs.isExists(x => x.Id == id))
                return "Invalid POS provided";

            var pos = await GetFullPOS(id);

            _unitOfWork.POSs.Delete(pos);
            await _unitOfWork.Save();

            return null;
        }

        public async Task<Pos> GetFullPOS(string id)
        {
            var pos = (await _unitOfWork.POSs.Find<Pos>(
                where: x => x.Id == id,
                include: x => x.Include(i => i.Issues))).FirstOrDefault();

            return pos;
        }

        public async Task<UpdatePOSDTO> GetPOSById(string? id)
        {
            var pos = (await _unitOfWork.POSs.Find<UpdatePOSDTO>(
                where: x => x.Id == id,
                include: x => x.Include(i => i.Issues),
                select: x => _mapperly.Map(x))).FirstOrDefault();

            var origPos = (await _unitOfWork.POSs.Find<Pos>(
                where: x => x.Id == id)).FirstOrDefault();

            pos.City = new CityDTO();
            pos.City.Id = origPos.IdCity;

            pos.ConnectionType = new ConnectionTypeDTO();
            pos.ConnectionType.Id = origPos.IdConnectionType;

            return pos;
        }

        public async Task<List<PosDTO>> GetPOSs(DataTablesParameters param)
        {
            string searchValue = param.Search.Value?.ToLower() ?? "";
            Expression<Func<Pos, bool>> expression = q => true;

            if (!string.IsNullOrEmpty(searchValue))
            {
                expression = q => q.Name.ToLower().Contains(searchValue)
                                 || q.Telephone.ToLower().Contains(searchValue)
                                 || q.Address.ToLower().Contains(searchValue)
                                 || (_context.Issues.Count(x => x.IdPos == q.Id) > 0 ? 
                                 _context.Issues.Count(x => x.IdPos == q.Id).ToString() + "active issues" 
                                 : "No issues").Contains(searchValue);
            }

            var issues = await _context.POSs
               .Where(expression)
               .OrderByExtension(param.Columns[param.Order[0].Column].Name, param.Order[0].Dir)
               .Skip(param.Start)
               .Take(param.Length)
               .Select(x => new PosDTO
               {
                   Id = x.Id,
                   Name = x.Name,
                   Telephone = x.Telephone,
                   Address = x.Address,
                   Status = _context.Issues.Count(x => x.IdPos == x.Id),
               }).ToListAsync();

            return issues;
        }
    }
}
