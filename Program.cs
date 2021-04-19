using System;

namespace BestColleagueDuo
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\Public\TestFolder\employeeProjects.txt");

            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }
        }
    }
}
