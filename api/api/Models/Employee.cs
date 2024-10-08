﻿using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class Employee
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;

        public int DepartmentId { get; set; }

        public virtual Department? Department { get; set; }
    }
}
