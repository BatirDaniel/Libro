﻿using Libro.Business.Libra.DTOs.IdentityDTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libro.Business.Libra.Queries.IdentityQueries
{
    public class GetUserDetailsByIdQuery : IRequest<DetailsUserDTO>
    {
        public string Id { get; set; }

        public GetUserDetailsByIdQuery(string id)
        {
            Id = id;
        }
    }
}