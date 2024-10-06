using System.Collections.Generic;

namespace lab_3.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }

        public List<Project> Projects { get; set; }
        public List<Task> Tasks { get; set; }
    }
}
