﻿using Libro.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libro.Business.Libra.DTOs.IssueDTOs
{
    public class UpdateIssueDTO
    {
        public string? Id { get; set; }
        public Pos? Pos { get; set; }
        public IssueTypes? IssueTypes { get; set; }
        public string? IdSubType { get; set; }
        public string? Priority { get; set; }
        public Status? Status { get; set; }
        public byte[]? Memo { get; set; }
        public User? User { get; set; }
        public User? UserAsigned { get; set; }
        public string? Description { get; set; }
        public DateTime? AssignedDate { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModifDate { get; set; }
        public string? Solution { get; set; }
    }
}
