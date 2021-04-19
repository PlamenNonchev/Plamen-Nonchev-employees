using System;
using System.Collections.Generic;
using System.Text;

namespace BestColleagueDuo
{
    public class EmployeeDuo
    {
        public EmployeeDuo(int employee1Id, int employee2Id, int workedTogetherPeriod )
        {
            Employee1Id = employee1Id;
            Employee2Id = employee2Id;
            WorkedTogetherPeriod = workedTogetherPeriod;
        }
        public int Employee1Id { get; set; }

        public int Employee2Id { get; set; }

        public int WorkedTogetherPeriod { get; set; }

    }
}
