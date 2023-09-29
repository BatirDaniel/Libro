using Libro.Business.Common.Helpers.OrderHelper;
using Libro.Business.Libra.DTOs.CityDTOs;
using Libro.Business.Libra.DTOs.ConnectionTypesDTOs;
using Libro.Business.Libra.DTOs.IssueDTOs;
using Libro.Business.Libra.DTOs.POSDTOs;
using Libro.Business.Libra.DTOs.TableParameters;
using Libro.DataAccess.Contracts;
using Libro.DataAccess.Data;
using Libro.DataAccess.Entities;
using Libro.Infrastructure.Helpers.ExpressionSuport;
using Libro.Infrastructure.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using System.Security.Claims;

namespace Libro.Business.Managers
{
    public class PosManager : EntityManager
    {
        private Mapperly _mapperly;
        private new IUnitOfWork _unitOfWork;
        private ApplicationDbContext _context;
        private ILogger<PosManager> _logger;
        private UserManager<User> _userManager;

        public PosManager(
            IUnitOfWork unitOfWork,
            ClaimsPrincipal user,
            Mapperly mapperly,
            ApplicationDbContext context,
            ILogger<PosManager> logger,
            UserManager<User> userManager = null) : base(unitOfWork, user)
        {
            _mapperly = mapperly;
            _unitOfWork = unitOfWork;
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<string> Create(CreatePOSDTO posDTO)
        {
            if (!await _unitOfWork.ConnectionTypes.isExists(x => x.Id == posDTO.IdConnectionType))
                return "Invalid connection type";

            posDTO.Id = Guid.NewGuid();
            posDTO.InserDate = DateTime.Now;

            var pos = _mapperly.Map(posDTO);

            await _unitOfWork.POSs.Create(pos);
            await _unitOfWork.Save();

            return null;
        }

        public async Task<string> Update(UpdatePOSDTO posDTO)
        {
            if (!await _unitOfWork.ConnectionTypes.isExists(x => x.Id == posDTO.ConnectionType.Id))
                return "Invalid connection type";

            if (!await _unitOfWork.Cities.isExists(x => x.Id == posDTO.City.Id))
                return "Invalid connection type";

            var pos = new Pos
            {
                Id = posDTO.Id,
                Name = posDTO.Name,
                Telephone = posDTO.Telephone,
                Cellphone = posDTO.Cellphone,
                Address = posDTO.Address,
                Model = posDTO.Model,
                Brand = posDTO.Brand,
                MorningOpening = posDTO.MorningOpening,
                MorningClosing = posDTO.MorningClosing,
                AfternoonOpening = posDTO.AfternoonOpening,
                AfternoonClosing = posDTO.AfternoonClosing,
                DaysClosed = posDTO.DaysClosed
            };

            pos.IdCity = posDTO.City.Id;
            pos.IdConnectionType = posDTO.ConnectionType.Id;

            _unitOfWork.POSs.Update(pos);
            await _unitOfWork.Save();

            return null;
        }

        public async Task<string> Delete(Guid id)
        {
            if (!await _unitOfWork.POSs.isExists(x => x.Id == id))
                return "Invalid POS provided";

            var pos = await GetFullPOS(id);

            _unitOfWork.POSs.Delete(pos);
            await _unitOfWork.Save();

            return null;
        }

        public async Task<Pos> GetFullPOS(Guid id)
        {
            var pos = (await _unitOfWork.POSs.Find<Pos>(
                where: x => x.Id == id,
                include: x => x
                .Include(i => i.City)
                .Include(i => i.ConnectionType))).FirstOrDefault();

            return pos;
        }

        public async Task<UpdatePOSDTO> GetPOSById(Guid id)
        {
            var pos = (await _unitOfWork.POSs.Find<UpdatePOSDTO>(
                where: x => x.Id == id,
                include: x => x
                .Include(i => i.City)
                .Include(i => i.ConnectionType),
                select: x => _mapperly.Map(x))).FirstOrDefault();

            var origPos = (await _unitOfWork.POSs.Find<Pos>(
                where: x => x.Id == id)).FirstOrDefault();
            
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
                                 _context.Issues
                                    .Include(x => x.Status)
                                    .Count(x => x.IdPos == q.Id && x.Status.Status_Name != "Done")
                                    .ToString() + "active issues" 
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

        public async Task<DetailsPOSDTO> GetPOSDetails(Guid id)
        {
            var pos = (await _unitOfWork.POSs.Find<DetailsPOSDTO>(
                where: x => x.Id == id,
                include: x => x
                .Include(i => i.City)
                .Include(i => i.ConnectionType),
                select: x => _mapperly.MapPosToPosDetails(x))).FirstOrDefault();

            pos.City.Name = (await _unitOfWork.Cities.Find<City>(
                where: x => x.Id == pos.City.Id)).FirstOrDefault()?.CityName;

            pos.ConnectionType.Type = (await _unitOfWork.ConnectionTypes.Find<ConnectionTypes>(
                where: x => x.Id == pos.ConnectionType.Id)).FirstOrDefault()?.ConnectionType;

            return pos;
        }

        public async Task<List<DetailsIssuesOfPOSDTO>> GetIssuesOfPOS(DataTablesParameters param, string id)
        {
            string? searchTerm = param.Search.Value?.ToLower();
            IQueryable<Issue> issueQuery = _context.Issues
                .Where(x => x.IdPos.ToString() == id);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                issueQuery = issueQuery.Where(x => 
                    x.Pos.Name.ToLower().Contains(searchTerm) || 
                    x.User.Name.ToLower().Contains(searchTerm) ||
                    x.IssueTypes.Name.ToLower().Contains(searchTerm) ||
                    x.CreationDate.ToString("dd/mm/yyyy").Contains(searchTerm) ||
                    x.Status.Status_Name.ToLower().Contains(searchTerm) ||
                    x.UsersAssigned.Name.ToLower().Contains(searchTerm) ||
                    x.Memo.ToLower().Contains(searchTerm));
            }

            if(param.Order[0].Dir == "desc")
            {
                issueQuery = issueQuery.OrderByDescending(GetSortProperty(param));
            }
            else
            {
                issueQuery = issueQuery.OrderBy(GetSortProperty(param));
            }

            var result = await issueQuery.Select(x => new DetailsIssuesOfPOSDTO
            {
                Id = x.Id,
                POSName = x.Pos,
                CreatedBy = x.User,
                IssueType = x.IssueTypes,
                DateCreated = x.CreationDate.ToString("dd/mm/yyyy"),
                Status = x.Status,
                AssignedTo = x.UsersAssigned,
                Memo = x.Memo
            }).ToListAsync();

            return result;
        }

        private static Expression<Func<Issue, object>> GetSortProperty(DataTablesParameters request) =>
            request.Columns[request.Order[0].Column].Data.ToLower() switch
            {
                "posname.name" => issue => issue.Pos.Name,
                "createdby.name" => issue => issue.User.Name,
                "issuetype.name" => issue => issue.IssueTypes.Name,
                "datecreated" => issue => issue.CreationDate,
                "status.status_name" => issue => issue.Status.Status_Name,
                "assignedto.name" => issue => issue.UsersAssigned.Name,
                "memo" => issue => issue.Memo,
                _ => issue => issue.Id 
            };

        public async Task<DetailsPOSDTO> GetPOSByIssue(Guid id)
        {
            var issue = (await _unitOfWork.Issues.Find<Issue>(
                where: x => x.Id == id,
                include: x => x
                    .Include(i => i.Pos))).FirstOrDefault();

            var pos = (await _unitOfWork.POSs.Find<DetailsPOSDTO>(
                where: x => x.Id == issue.IdPos,
                include: x => x
                    .Include(i => i.City)
                    .Include(i => i.ConnectionType),
                select: x => _mapperly.MapPosToPosDetails(x)))
                .FirstOrDefault();

            pos.City.Name = (await _unitOfWork.Cities.Find<City>(
                 where: x => x.Id == pos.City.Id)).FirstOrDefault()?.CityName;

            pos.ConnectionType.Type = (await _unitOfWork.ConnectionTypes.Find<ConnectionTypes>(
                where: x => x.Id == pos.ConnectionType.Id)).FirstOrDefault()?.ConnectionType;

            return pos;
        }
    }
}
