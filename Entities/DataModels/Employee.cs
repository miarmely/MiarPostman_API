using System.ComponentModel.DataAnnotations.Schema;


namespace Entities.DataModels
{
    public class Employee
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string LastName { get; set; }
        public string Job { get; set; }
        public decimal Salary { get; set; }
        public DateTime RegisterDate { get; set; }
    }
}
