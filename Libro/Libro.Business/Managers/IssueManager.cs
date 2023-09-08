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

        public Task<List<IssueDTO>> GetIssues(DataTablesParameters? param)
        {
            string searchValue = param.Search.Value?.ToLower();
            Expression<Func<Issue, bool>> expression = null;

            if (!string.IsNullOrEmpty(searchValue))
            {
                expression = q => q.Pos.Name.Contains(searchValue)
                || q.User.Id.Contains(searchValue)
                || q.CreationDate.ToString().Contains(searchValue)
                || q.IssueTypes.Name.Contains(searchValue)
                || q.Status.Status_Name.Contains(searchValue)
                || q.Priority.Name.Contains(searchValue);
            }

            var issues = _context.Issues
               .Where(expression)
               .OrderByExtension(param.Columns[param.Order[0].Column].Name, param.Order[0].Dir)
               .Skip(param.Start)
               .Take(param.Length)
               .Select(x => new IssueDTO
               {
                   Id = x.Id,
                   POSName = x.Pos.Name,
                   CreatedBy = x.User.Name,
                   CreationDate = x.CreationDate,
                   IssueType = x.IssueTypes.Name,
                   Status = x.Status.Status_Name,
                   Memo = x.Memo,
                   Priority = x.Priority.Name
               }).ToList();

            return Task.FromResult(issues);
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
                posQuery = posQuery.OrderByDescending(GetSortProperty(param));
            }
            else
            {
                posQuery = posQuery.OrderBy(GetSortProperty(param));
            }

            var result = await posQuery.Select(x => new POSsForIssuesDTO (
                x.Id,
                x.Name,
                x.Telephone,
                x.Cellphone,
                x.Address)).ToListAsync();

            return result;
        }

        private static Expression<Func<Pos, object>> GetSortProperty(DataTablesParameters request) =>
            request.Columns[request.Order[0].Column].Data.ToLower() switch
            {
                "name" => pos => pos.Name,
                "telephone" => pos => pos.Telephone,
                "cellphone" => pos => pos.Cellphone,
                "address" => pos => pos.Address,
                _ => pos => pos.Id
            };

        public List<UsersForIssue> GetUsersForIssue()
        {
            var roles = _roleManager.Roles
                .Select(q => new UsersForIssue(
                    Guid.Parse(q.Id),
                    q.Name)).ToList();

            return roles;
        }
    }
}
