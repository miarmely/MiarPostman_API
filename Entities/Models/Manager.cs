using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Manager
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string LastName { get; set; }
        public string Factory { get; set; }
    }
}
