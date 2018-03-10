using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Course_Scheduling
{
    class Program
    {
        static void Main(string[] args)
        {
            string content = File.ReadAllText(@"C:\Users\manasyebukit\Documents\GitHub\BFS-DFS-Courses-Scheduling\file.txt");
            var paragraphs = content.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);

            string[][] subjects = new string[paragraphs.Length][];

            for (int i = 0; i < subjects.Length; i++)
            {
                subjects[i] = paragraphs[i].Split(new char[] { ',' , ' ' , '\n' }, StringSplitOptions.RemoveEmptyEntries);
            }

            for (int i = 0; i < subjects.Length; i++)
            {   
                for (int j = 0; j < subjects[i].Length; j++)
                {
                    Console.WriteLine(subjects[i][j]);
                }
            }
            //var numOfLines = lines.Length;
            //Console.WriteLine(numOfLines);

            //for (int i = 0;i < numOfLines; i++)
            //{
            //    Console.WriteLine(lines[i]);
            //}

            //for (int i = 0; i < numOfLines; i++)
            //{
            //    //subject = lines[i].Split(new[] { ',', ' '}, StringSplitOptions.RemoveEmptyEntries);
            //    //for (int j = 0; j < subject.Length; j++)
            //    //{
            //    //    Console.WriteLine(subject[j][0]);
            //    //}
            //    subject = lines[i].Split(',').Select(
            //    x => new string[2] { x.Substring(0, 2), x.Substring(2, 2) }
            //    ).ToArray();
            //}

            //var subject = lines.Split(',')
            //    .Select(p => Regex.Split(p, "(?<=\\G.{2})"))
            //    .ToArray();

            //Console.WriteLine(subject[2][0]);


        }
    }
}
