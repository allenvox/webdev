using System.Collections.Generic;

namespace lab_3.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CustomerCompany { get; set; }
        public string ContractorCompany { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Priority { get; set; }

        // Инициализация коллекций в конструкторе
        public List<Task> Tasks { get; set; } = new List<Task>();
        public List<Employee> Employees { get; set; } = new List<Employee>();

        public Project()
        {
            // Инициализация коллекций для предотвращения ошибок
            Tasks = new List<Task>();
            Employees = new List<Employee>();
        }
    }
}
