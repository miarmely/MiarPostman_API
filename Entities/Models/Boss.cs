using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Boss
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string LastName { get; set; }
        public List<Maid> Maid { get; set; }
    }
}
