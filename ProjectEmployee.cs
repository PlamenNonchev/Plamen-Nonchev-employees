using System;
using System.Collections.Generic;
using System.Text;

namespace BestColleagueDuo
{
    public class ProjectEmployee
    {
        public ProjectEmployee(int projectId, int employeeId, DateTime dateFrom, DateTime dateTo)
        {
            ProjectId = projectId;
            EmployeeId = employeeId;
            DateFrom = dateFrom;
            DateTo = dateTo;
        }

        public int ProjectId { get; set; }

        public int EmployeeId { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }
    }
}
