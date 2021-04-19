using System;
using System.Collections.Generic;
using System.Text;

namespace BestColleagueDuo
{
    public  class FileParser
    {
        public FileParser(string[] lines)
        {
            Lines = lines;
        }

        public string[]  Lines { get; set; }


        public ICollection<ProjectEmployee> GenerateProjectEmployees()
        {
            ICollection<ProjectEmployee> projEmp = new List<ProjectEmployee>();
            foreach (var line in Lines)
            {
                string[] splitLine = line.Split(", ");
                int employeeId = int.Parse(splitLine[0]);
                int projectId = int.Parse(splitLine[1]);
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
                projEmp.Add(pe);
            }

            return projEmp;
        }

        public ICollection<int> GetProjectIds()
        {
            ICollection<int> projectIds = new List<int>();
            foreach (var line in Lines)
            {
                string[] splitLine = line.Split(", ");
                projectIds.Add(int.Parse(splitLine[1]));
            }

            return projectIds; 
        }




    }
}
