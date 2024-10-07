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

        // Инициализация коллекций в конструкторе
        public List<Task> Tasks { get; set; }
        public List<Project> Projects { get; set; }

        public Employee()
        {
            // Инициализируем коллекции для предотвращения ошибок валидации
            Tasks = new List<Task>();
            Projects = new List<Project>();
        }
    }
}
