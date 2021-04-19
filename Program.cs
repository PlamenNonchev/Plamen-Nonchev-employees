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

            FileParser fp = new FileParser(lines);
            ICollection<ProjectEmployee> projectEmployees = fp.GenerateProjectEmployees();

            List<EmployeeDuo> employeeDuos = new List<EmployeeDuo>();
            var projectIds = fp.GetProjectIds().Distinct();

            foreach (var projectId in projectIds)
            {
                var employeeIds = projectEmployees.Where(p => p.ProjectId == projectId).Select(e => e.EmployeeId).OrderBy(x => x).ToArray();
                

                for (int i = 0; i < employeeIds.Length; i++)
                {
                    var projEmp1 = projectEmployees.FirstOrDefault(pe => pe.ProjectId == projectId && pe.EmployeeId == employeeIds[i]);
                    for (int j = i + 1; j < employeeIds.Length; j++)
                    {
                        var projEmp2 = projectEmployees.FirstOrDefault(pe => pe.ProjectId == projectId && pe.EmployeeId == employeeIds[j]);
                        bool overlap = projEmp1.DateFrom < projEmp2.DateTo && projEmp2.DateFrom < projEmp1.DateTo;
                        if (overlap)
                        {
                            int overlapDays = GetOverlapDays(projEmp1.DateFrom, projEmp2.DateFrom, projEmp1.DateTo, projEmp2.DateTo);
                            var empDuo = employeeDuos.FirstOrDefault(ed => ed.Employee1Id == projEmp1.EmployeeId && ed.Employee2Id == projEmp2.EmployeeId);
                            if (empDuo == null)
                            {
                                EmployeeDuo ed = new EmployeeDuo(projEmp1.EmployeeId, projEmp2.EmployeeId, overlapDays);
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


            var bestDuo = employeeDuos.OrderByDescending(ed => ed.WorkedTogetherPeriod).FirstOrDefault();
            if(bestDuo != null)
            Console.WriteLine($"Employee {bestDuo.Employee1Id} and employee {bestDuo.Employee2Id} make the best duo!\n" +
                $"They have worked together for a total of {bestDuo.WorkedTogetherPeriod} days");
        }
        static int GetOverlapDays(DateTime startDate1, DateTime startDate2, DateTime endDate1, DateTime endDate2)
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

            return overlapDays;
            
        }
    }
}
