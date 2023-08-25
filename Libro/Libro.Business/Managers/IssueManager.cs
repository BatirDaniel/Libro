using Libro.Business.Common.Helpers.OrderHelper;
using Libro.Business.Libra.DTOs.IdentityDTOs;
using Libro.Business.Libra.DTOs.IssueDTOs;
using Libro.DataAccess.Contracts;
using Libro.DataAccess.Data;
using Libro.DataAccess.Entities;
using Libro.DataAccess.Repository;
using Libro.Infrastructure.Helpers.ExpressionSuport;
using Libro.Infrastructure.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NHibernate.Criterion;
using NHibernate.Id;
using System.Linq.Expressions;
using System.Security.Claims;

namespace Libro.Business.Managers
{
    public class IssueManager : EntityManager
    {
        public Mapperly _mapperly; 
        public IUnitOfWork _unitOfWork;
        public ApplicationDbContext _context;
        public ILogger<IssueManager> _logger;
        public IssueManager(
            IUnitOfWork unitOfWork,
            ClaimsPrincipal user,
            Mapperly mapperly,
            ApplicationDbContext context,
            ILogger<IssueManager> logger) : base(unitOfWork, user)
        {
            _mapperly = mapperly;
            _unitOfWork = unitOfWork;
            _context = context;
            _logger = logger;
        }

        public async Task<string> Create(CreateIssueDTO model)
        {
            if (!await _unitOfWork.Issues.isExists(x => x.Id == model.IssueTypes.Id))
                return "The issue type does not exist";

            var user = (await _unitOfWork.Users.Find<User>(
                where: x => x.UserName == "admin@libra")).FirstOrDefault();

            if (!await _unitOfWork.Users.isExists(x => x.Id == model.User.Id 
                && model.User.Id == user.Id))
                return "Invalid user provided";

            if (!await _unitOfWork.Users.isExists(x => x.Id == model.UserAsigned.Id)
                && model.User.Id == user.Id)
                return "Invalid assigned user provided";

            model.Id = Guid.NewGuid().ToString();
            model.AssignedDate = DateTime.Now;
            model.CreationDate = DateTime.Now;

            await _unitOfWork.Issues.Create(_mapperly.Map(model));
            await _unitOfWork.Save();

            return null;
        }

        public async Task<string> Delete(string? id)
        {
            if (!await _unitOfWork.Issues.isExists(x => x.Id == id))
                return "Invalid issue provided";

            var issue = await GetFullIssue(id);

            _unitOfWork.Issues.Delete(issue);
            await _unitOfWork.Save();

            return null;
        }

        private async Task<Issue> GetFullIssue(string? id)
        {
            var issue = (await _unitOfWork.Issues.Find<Issue>(
                where: x => x.Id == id,
                include: x => x.Include(x => x.Logs))).FirstOrDefault();

            return issue;
        }

        public async Task<string> Update(UpdateIssueDTO? model)
        {
            if (!await _unitOfWork.Issues.isExists(x => x.Id == model.Id))
                return "Invalid issue provided";

            var user = (await _unitOfWork.Users.Find<User>(
                where: x => x.UserName == "admin@libra")).FirstOrDefault();

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

        public async Task<UpdateIssueDTO> GetIssueById(string? id)
        {
            var result = (await _unitOfWork.Issues.Find<UpdateIssueDTO>(
                where: x => x.Id == id,
                include: x => x.Include(i => i.Logs),
                select : x => _mapperly.Map(x))).FirstOrDefault();

            return result;
        }

        public async Task<List<IssueDTO>> GetIssues(Libra.DTOs.TableParameters.DataTablesParameters? param)
        {
            string searchValue = param.Search.Value ?? "";
            Expression<Func<Issue, bool>> expression = null;

            if (searchValue != "")
            {
                expression = q => q.Pos.Name.Contains(searchValue)
                || q.User.Id.Contains(searchValue)
                || q.CreationDate.ToString().Contains(searchValue)
                || q.IssueTypes.Name.Contains(searchValue)
                || q.Status.Status_Name.Contains(searchValue)
                || q.UserAsigned.Name.Contains(searchValue)
                || q.Priority.Contains(searchValue);
            }

            var issues = await _context.Issues
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
                   AssignedTo = x.UserAsigned.Name,
                   Memo = x.Memo,
                   Priority = x.Priority
               }).ToListAsync();

            return issues;
        }
    }
}
