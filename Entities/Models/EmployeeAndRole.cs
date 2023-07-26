using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class EmployeeAndRole
    {
        [Key]
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int RoleId { get; set; }
    }
}
