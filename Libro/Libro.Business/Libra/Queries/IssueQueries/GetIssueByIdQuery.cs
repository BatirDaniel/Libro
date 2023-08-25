﻿using Libro.Business.Libra.DTOs.IssueDTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libro.Business.Libra.Queries.IssueQueries
{
    public class GetIssueByIdQuery : IRequest<UpdateIssueDTO>
    {
        public string? Id { get; set; }

        public GetIssueByIdQuery(string? issueId)
        {
            Id = issueId;
        }
    }
}
