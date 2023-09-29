using Libro.Business.Common.Helpers.OrderHelper;
using Libro.Business.Libra.DTOs.IssueDTOs;
using Libro.Business.Libra.DTOs.TableParameters;
using Libro.Business.Libra.Queries.IssueQueries;
using Libro.DataAccess.Contracts;
using Libro.DataAccess.Data;
using Libro.DataAccess.Entities;
using Libro.Infrastructure.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using System.Security.Claims;

namespace Libro.Business.Managers
{
    public class IssueManager : EntityManager
    {
        public Mapperly _mapperly; 
        public new IUnitOfWork _unitOfWork;
        public ApplicationDbContext _context;
        public ILogger<IssueManager> _logger;
        private RoleManager<IdentityRole> _roleManager;
        private readonly HttpContext _httpContext;
        public IssueManager(
            IUnitOfWork unitOfWork,
            ClaimsPrincipal user,
            Mapperly mapperly,
            ApplicationDbContext context,
            ILogger<IssueManager> logger,
            RoleManager<IdentityRole> roleManager,
            IHttpContextAccessor httpContext) : base(unitOfWork, user)
        {
            _mapperly = mapperly;
            _unitOfWork = unitOfWork;
            _context = context;
            _logger = logger;
            _roleManager = roleManager;
            _user = user;
            _httpContext = httpContext.HttpContext;
        }

        public async Task<string> Create(CreateIssueDTO model)
        {
            model.Id = Guid.NewGuid();
            model.AssignedDate = DateTime.Now;
            model.CreationDate = DateTime.Now;;

            var idClaim = _httpContext.User.FindFirst("ID");
            if (idClaim == null)
                return "Some went wrong";

            model.IdUserCreated = Guid.Parse(idClaim.Value);

            var issue = _mapperly.Map(model);
            issue.IdUsersAssigned = model.IdUsersAsigned.ToString();

            if (!await _unitOfWork.UserTypes.isExists(x => x.Id == issue.IdUsersAssigned))
                return "Invalid user assigned";

            await _unitOfWork.Issues.Create(issue);
            await _unitOfWork.Save();

            return null;
        }

        public async Task<string> Delete(Guid id)
        {
            if (!await _unitOfWork.Issues.isExists(x => x.Id == id))
                return "Invalid issue provided";

            var issue = await GetFullIssue(id);

            _unitOfWork.Issues.Delete(issue);
            await _unitOfWork.Save();

            return null;
        }

        private async Task<Issue> GetFullIssue(Guid id)
        {
            var issue = (await _unitOfWork.Issues.Find<Issue>(
                where: x => x.Id == id,
                include: x => x.Include(x => x.Logs))).FirstOrDefault();

            return issue;
        }

        public async Task<string> Update(UpdateIssueDTO model)
        {
            if (!await _unitOfWork.Issues.isExists(x => x.Id == model.Id))
                return "Invalid issue provided";

            var user = (await _unitOfWork.Users.Find<User>(
                where: x => x.UserName == "admin@libro")).FirstOrDefault();

            if (!await _unitOfWork.Users.isExists(x => x.Id == model.User.Id
                && model.User.Id == user.Id))
                return "Invalid user provided";

            if (!await _unitOfWork.Users.isExists(x => x.Id == model.UserAsigned.Id)
                && model.User.Id == user.Id)
                return "Invalid assigned user provided";

            model.ModifDate = DateTime.Now;

            _unitOfWork.Issues.Update(_mapperly.Map(model));
            await _unitOfWork.Save();

            return null;
        }

        public async Task<UpdateIssueDTO> GetIssueById(Guid id)
        {
            var result = (await _unitOfWork.Issues.Find<UpdateIssueDTO>(
                where: x => x.Id == id,
                include: x => x.Include(i => i.Logs),
                select : x => _mapperly.Map(x))).FirstOrDefault();

            return result;
        }

        public async Task<List<IssueR_DTO>> GetIssues(DataTablesParameters? param)
        {
            string? searchTerm = param.Search.Value?.ToLower();
            IQueryable<Issue> issueQuery = _context.Issues
                .Include(x => x.User)
                .Include(x => x.IssueTypes)
                .Include(x => x.Status)
                .Include(x => x.Priority)
                .Include(x => x.UsersAssigned);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                issueQuery = issueQuery.Where(x => x.Pos.Name.Contains(searchTerm)
                || x.User.UserName.Contains(searchTerm)
                || x.CreationDate.ToString("dd/mm/yyyy").Contains(searchTerm)
                || x.IssueTypes.Name.Contains(searchTerm)
                || x.Status.Status_Name.Contains(searchTerm)
                || x.UsersAssigned.Name.Contains(searchTerm)
                || x.Memo.Contains(searchTerm)
                || x.Priority.Name.Contains(searchTerm));
            }

            if (param.Order[0].Dir == "desc")
            {
                issueQuery = issueQuery.OrderByDescending(GetSortPropertyIssue(param));
            }
            else
            {
                issueQuery = issueQuery.OrderBy(GetSortPropertyIssue(param));
            }

            var result = await issueQuery.Select(x => new IssueR_DTO
               (
                   x.Id,
                   x.Pos.Name,
                   x.User.Name,
                   x.CreationDate,
                   x.IssueTypes.Name,
                   x.Status.Status_Name,
                   x.UsersAssigned.Name,
                   x.Memo,
                   x.Priority.Name
               )).ToListAsync();

            return result;
        }

        public async Task<List<POSsForIssuesDTO>> GetPOSsForIssues(DataTablesParameters? param)
        {
            string? searchTerm = param.Search.Value?.ToLower();
            IQueryable<Pos> posQuery = _context.POSs;

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                posQuery = posQuery.Where(x =>
                    x.Name.ToLower().Contains(searchTerm) ||
                    x.Telephone.ToLower().Contains(searchTerm) ||
                    x.Cellphone.ToLower().Contains(searchTerm) ||
                    x.Address.ToLower().Contains(searchTerm));
            }

            if (param.Order[0].Dir == "desc")
            {
                posQuery = posQuery.OrderByDescending(GetSortPropertyPOS(param));
            }
            else
            {
                posQuery = posQuery.OrderBy(GetSortPropertyPOS(param));
            }

            var result = await posQuery.Select(x => new POSsForIssuesDTO (
                x.Id,
                x.Name,
                x.Telephone,
                x.Cellphone,
                x.Address)).ToListAsync();

            return result;
        }
        public List<UsersForIssue> GetUsersForIssue()
        {
            var roles = _roleManager.Roles
                .Select(q => new UsersForIssue(
                    Guid.Parse(q.Id),
                    q.Name)).ToList();

            return roles;
        }

        private static Expression<Func<Pos, object>> GetSortPropertyPOS(DataTablesParameters request) =>
            request.Columns[request.Order[0].Column].Data.ToLower() switch
            {
                "name" => pos => pos.Name,
                "telephone" => pos => pos.Telephone,
                "cellphone" => pos => pos.Cellphone,
                "address" => pos => pos.Address,
                _ => pos => pos.Id
            };
        private static Expression<Func<Issue, object>> GetSortPropertyIssue(DataTablesParameters request) =>
            request.Columns[request.Order[0].Column].Data.ToLower() switch
            {
                "POSName" => issue => issue.Pos.Name,
                "CreatedBy" => issue => issue.User.UserName,
                "CreationDate" => issue => issue.CreationDate,
                "IssueType" => issue => issue.IssueTypes.Name,
                "Status" => issue => issue.Status.Status_Name,
                "" => issue => issue.UsersAssigned.Name,
                "Memo" => issue => issue.Memo,
                "Priority" => issue => issue.Priority.Name,
                _ => issue => issue.Id
            };
    }
}
