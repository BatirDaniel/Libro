﻿namespace Libro.DataAccess.Entities
{
    public class Status
    {
        public string? Id { get; set; }
        public string? Status_Name { get; set; }

        public virtual ICollection<Issue> Issues { get; set; } = new List<Issue>();
    }
}
