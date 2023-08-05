using Entities.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModels
{
    public class EmployeeView
    {
        public int? Id { get; set; }
        public string? FullName { get; set; }
        public string? LastName { get; set; }
        public string? Job { get; set; }
        public decimal? Salary { get; set; }
        public string? RegisterDate { get; set; }
        public List<string> Roles { get; set; }
    }
}
