using System;
using System.Collections.Generic;

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


        }
    }
}
