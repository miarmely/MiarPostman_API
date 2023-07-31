using System.ComponentModel.DataAnnotations.Schema;


namespace Entities.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string LastName { get; set; }
        public string Job { get; set; }
        public decimal Salary { get; set; }
        public DateTime RegisterDate { get; set; }

        [NotMapped]  // for not create column on database. (i can exclude this property from database thanks to this key)
        public List<Role>? Roles { get; set; }
    }
}
