using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace QueryExpressions
{
    internal static class Program
    {
        private static void Main()
        {
            var departments = new List<Department> {
                new Department {Name = "Engineering", Id = 1},
                new Department {Name = "Marketing", Id = 2}
            };

            var employees = new List<Employee> {
                new Employee {Name = "Michael", Address = "SW Liverpool Lane", State = "WA", Salary = 5675, DepId = 1},
                new Employee {Name = "Jennifer", Address = "1245 NW Baypony Dr", State = "OR", Salary = 6555, DepId = 1},
                new Employee {Name = "Sean", Address = "55217 SW Estate Dr", State = "WA", Salary = 8888, DepId = 1},
                new Employee {Name = "Peter", Address = "Ma Lane", State = "WA", Salary = 9999, DepId = 2},
                new Employee {Name = "Henry", Address = "Ma Dr", State = "OR", Salary = 3209, DepId = 2}
            };

            var projects = new List<Project> {
                new Project {Name = "Saturn", ProjectManager = employees[0]},
                new Project {Name = "Uranus", ProjectManager = employees[2]},
                new Project {Name = "Pluto"}

            };
            projects[0].AddEmployee(employees[0]);
            projects[0].AddEmployee(employees[2]);
            projects[0].AddEmployee(employees[3]);
            projects[1].AddEmployee(employees[1]);
            projects[1].AddEmployee(employees[2]);

            var queryWashington = employees.Where(e => e.State == "WA");
            // var queryWashington = from e in employees where e.State == "WA" select e;
            Console.WriteLine("---------- \r\nListe der Mitarbeiter die im State WA wohnen");
            queryWashington.ForEach(e => Console.WriteLine(e.Name));


            // var queryWashingtonSorted = from e in employees where e.State == "WA" orderby e.Name descending select new { e.Name, e.Address };
            var queryWashingtonSorted = employees
                .Where(e => e.State == "WA")
                .OrderByDescending(e => e.Name)
                .Select(e => new {e.Name, e.Address});
            Console.WriteLine("---------- \r\nListe der Namen und Adressen der Mitarbeiter im State WA");
            queryWashingtonSorted.ForEach(e => Console.WriteLine("Name: {0}, Phone: {1}", e.Name, e.Address));

            // Group Join LINQ
            // var queryDepartments = from d in departments from e in employees where d.Id == e.DepId group e by d.Name into g
            //                        select new { Department = g.Key, EmployeeCount = g.Count() };

            // Inner Join LINQ
            /*var queryDepartments = from d in departments
                join e in employees on d.Id equals e.DepId
                group d by d.Id
                into g
                select new {Department = g.Key, EmployeeCount = g.Count()};*/

            /*
             INNER JOIN 
             var queryDepartments = departments
                .Join(employees, d => d.Id, e => e.DepId, 
                    (d, e) => new
                    {
                        Department = d, 
                        Employee = e
                    })
                .GroupBy(x => x.Department.Id)
                .Select(x => new
                {
                    Department = x.Key, 
                    EmployeeCount = x.Count()
                });*/

            // Group Join
            var queryDepartments = departments
                .GroupJoin(employees, dKey => dKey.Id, eKey => eKey.DepId,
                    (d, e) => new
                    {
                        Department = d.Name, 
                        EmployeeCount = e.Count()
                    });
            Console.WriteLine("---------- \r\nListe der Department-Namen und der Anzahl Mitarbeiter der Departments");
            queryDepartments.ForEach(d => Console.WriteLine("Department: {0}, EmployeeCount: {1}", d.Department, d.EmployeeCount));

            //var empQuery =
            //        from e in employees
            //        from d in departments
            //        where e.DepId == d.Id
            //        orderby d.Name
            //        select new { EmployeeName = e.Name, DepartmentName = d.Name };
            var empQuery = departments
                .Join(employees, d => d.Id, e => e.DepId,
                    (d, e) => new
                    {
                        DepartmentName = d.Name, 
                        EmployeeName = e.Name
                    })
                .OrderBy(x => x.DepartmentName);
            Console.WriteLine("---------- \r\nListe der Departments mit ihren Mitarbeitern");
            empQuery.ForEach(c => Console.WriteLine("Name: {0}, Department: {1}", c.EmployeeName, c.DepartmentName));

            //var maxDeptSalary =
            //        from e in employees
            //        from d in departments
            //        where e.DepId == d.Id
            //        group e by d.Name into g
            //        let maxSalary = g.Max(eg => eg.Salary)
            //        orderby maxSalary descending
            //        select new { DepartmentName = g.Key, MaxSalary = maxSalary };
            var maxDeptSalary = departments
                .Join(employees, d => d.Id, e => e.DepId,
                    (d, e) => new {Department = d, Employee = e})
                .GroupBy(x => x.Department.Id)
                .Select(x => new
                {
                    DepartmentName = x.Key,
                    MaxSalary = x.Max(y => y.Employee.Salary)
                })
                .OrderByDescending(x => x.MaxSalary);
            Console.WriteLine("---------- \r\nListe der Departments mit dem Salär des bestverdienenden Mitarbeiters");
            maxDeptSalary.ForEach(c => Console.WriteLine("Department: {0}, Höchster Salär: {1}", c.DepartmentName, c.MaxSalary));

            /*
                var projList = from p in projects
                from e in p.Employees
                orderby p.Name, e.Name
                select new {Project = p.Name, Employee = e.Name};
             */
            var projList = projects
                .SelectMany(p => p.Employees
                    .Select(e => new {Project = p.Name, Employee = e.Name}))
                .OrderBy(p => p.Project)
                .ThenBy(p => p.Employee);
            Console.WriteLine("---------- \r\nListe der Projekte und der beteiligten Mitarbeiter");
            projList.ForEach(p => Console.WriteLine("Projekt: {0} Mitarbeiter: {1}", p.Project, p.Employee));

            /*
                var projStatistics = from p in projects
                    orderby p.Name, p.Employees.Count()
                    select new {
                    Project = p.Name,
                    Mgr = p.ProjectManager == null ? "tba" : p.ProjectManager.Name,
                    EmpCount = p.Employees.Count(),
                    AvgSalary = p.Employees.Any() ? p.Employees.Average(e => e.Salary) : 0
                };
             */
            var projStatistics = projects
                .Select(p => new
                {
                    Project = p.Name,
                    Mgr = p.ProjectManager == null ? "tba" : p.ProjectManager.Name,
                    EmpCount = p.Employees.Count(),
                    AvgSalary = p.Employees.Any() ? p.Employees.Average(e => e.Salary) : 0
                }).OrderBy(p => p.Project).ThenBy(p => p.EmpCount);
            Console.WriteLine("---------- \r\nListe der Projekte und ihrer Statistik");
            projStatistics.ForEach(p => Console.WriteLine("Project {0} Mgr {1} Number Employees {2} Average Salary {3}", p.Project, p.Mgr, p.EmpCount, p.AvgSalary));
            Console.ReadKey();
        }
        private static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (T item in source) action(item);
        }
    }
}
