using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_Scheduling
{
    class Program
    {
        static void Main(string[] args)
        {
            //string[] subject = "blabla";
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\manasyebukit\Documents\GitHub\BFS-DFS-Courses-Scheduling\file.txt");
            System.Console.WriteLine("Contents of file.txt = ");
            foreach (string line in lines)
            {
                //subject = line.Split('.');
                Console.WriteLine(line);
            }
            //Console.WriteLine(subject);
            //foreach (var x in subject)
            //{
            //    System.Console.WriteLine($"<{x}>");
            //}

        }   
    }
}
