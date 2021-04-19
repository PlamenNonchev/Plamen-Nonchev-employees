using System;
using System.Collections.Generic;
using System.Linq;

namespace BestColleagueDuo
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\Public\TestFolder\employeeProjects.txt");

            List<ProjectEmployee> projectEmployees = new List<ProjectEmployee>();

            foreach (var line in lines)
            {
                string[] splitLine = line.Split(",");
                int projectId = int.Parse(splitLine[0]);
                int employeeId = int.Parse(splitLine[1]);
                DateTime dateFrom = DateTime.Parse(splitLine[2]);
                DateTime dateTo;
                if (splitLine[3] == "NULL")
                {
                    dateTo = DateTime.Now.Date;
                }
                else
                {
                    dateTo = DateTime.Parse(splitLine[3]);
                }
                ProjectEmployee pe = new ProjectEmployee(projectId, employeeId, dateFrom, dateTo);
                projectEmployees.Add(pe);
            }

            List<EmployeeDuo> employeeDuos = new List<EmployeeDuo>();
            var projectIds = projectEmployees.Select(p => p.ProjectId).Distinct().ToList();

            foreach (var projectId in projectIds)
            {
                var employeeIds = projectEmployees.Where(p => p.ProjectId == projectId).Select(e => e.EmployeeId).OrderBy(x => x).ToArray();
                

                for (int i = 0; i < employeeIds.Length; i++)
                {
                    int emp1 = employeeIds[i];
                    var projEmp1 = projectEmployees.FirstOrDefault(pe => pe.ProjectId == projectId && pe.EmployeeId == emp1);
                    DateTime startDate1 = projEmp1.DateFrom;
                    DateTime endDate1 = projEmp1.DateTo;
                    for (int j = i + 1; j < employeeIds.Length; j++)
                    {
                        
                        int emp2 = employeeIds[j];
                        var projEmp2 = projectEmployees.FirstOrDefault(pe => pe.ProjectId == projectId && pe.EmployeeId == emp2);
                        DateTime startDate2 = projEmp2.DateFrom;
                        DateTime endDate2 = projEmp2.DateTo;

                        bool overlap = startDate1 < endDate2 && startDate2 < endDate1;
                        if (overlap)
                        {
                            DateTime overlapStart;
                            DateTime overlapEnd;
                            if (startDate1 < startDate2)
                            {
                                overlapStart = startDate2;
                            }
                            else
                            {
                                overlapStart = startDate1;
                            }

                            if (endDate1 < endDate2)
                            {
                                overlapEnd = endDate1;
                            }
                            else
                            {
                                overlapEnd = endDate2;
                            }

                            int overlapDays = (overlapEnd - overlapStart).Days;

                            var empDuo = employeeDuos.FirstOrDefault(ed => ed.Employee1Id == emp1 && ed.Employee2Id == emp2);
                            if(empDuo == null)
                            {
                                EmployeeDuo ed = new EmployeeDuo(emp1, emp2, overlapDays);
                                employeeDuos.Add(ed);
                            }
                            else
                            {
                                empDuo.WorkedTogetherPeriod += overlapDays;
                            }
                        }
                        


                    }
                }
            }

            var bestDuo = employeeDuos.OrderByDescending(ed => ed.WorkedTogetherPeriod).First();
            Console.WriteLine($"Employee {bestDuo.Employee1Id} and employee {bestDuo.Employee2Id} make the best duo!\nThey have worked together for a total of {bestDuo.WorkedTogetherPeriod} days");
        }
    }
}
