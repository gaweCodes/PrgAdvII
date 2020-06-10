using System.Collections.Generic;

namespace QueryExpressions
{
    internal class Project
    {
        private readonly List<Employee> _employees = new List<Employee>();
        public string Name { get; set; }
        public IEnumerable<Employee> Employees => _employees;
        public void AddEmployee(Employee e)
        {
            if (!_employees.Contains(e))
                _employees.Add(e);

        }
        public Employee ProjectManager { get; set; }
    }
}
