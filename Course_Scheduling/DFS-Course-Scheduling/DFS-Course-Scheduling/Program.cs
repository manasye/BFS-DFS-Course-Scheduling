using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace DFS_Course_Scheduling
{
    class Courses
    {
        public Courses()
        {
            numOfCourses++;
            List<string> adjCourses = new List<string>();
        }
        public static int numOfCourses;
        public static int getNumCourses
        {
            get
            {
                return numOfCourses;
            }
        }
        public bool courseChecked { get; set; }
        public static int timeStamp = 0;
        public int semester { get; set; }
        public string nameOfCourses { get; set; }
        public int cOfAdj { get; set; }
        public List<Courses> adjCourses = new List<Courses>();
        public int startTime { get; set; }
        public int endTime { get; set; }
        public List<string> prerequisite = new List<string>();
    }

    class Program
    {
        static void Main(string[] args)
        {
            string fileOfCourses = @"C:\Users\manasyebukit\Documents\GitHub\BFS-DFS-Courses-Scheduling\file.txt";
            List<string> wholeCourses = File.ReadAllLines(fileOfCourses).ToList();
            List<Courses> listOfCourses = new List<Courses>();
            List<Courses> orderOfCourses = new List<Courses>();

            foreach (string course in wholeCourses)
            {
                Courses thisCourse = new Courses();
                List<string> prerequisite = course.Split(',').ToList();
                thisCourse.nameOfCourses = prerequisite[0];
                prerequisite.RemoveAt(0);
                thisCourse.prerequisite = prerequisite;
                thisCourse.semester = 0;
                thisCourse.startTime = 0;
                thisCourse.endTime = 0;
                thisCourse.courseChecked = false;
                thisCourse.cOfAdj = prerequisite.Count;
                listOfCourses.Add(thisCourse);
            }

            foreach (Courses course in listOfCourses)
            {
                foreach (Courses aCourse in listOfCourses)
                {
                    if(aCourse != course)
                    {
                        foreach(string name in aCourse.prerequisite)
                        {
                            if(name == course.nameOfCourses)
                            {
                                course.adjCourses.Add(aCourse);
                            }
                        }
                    }
                }
            }

            foreach (Courses course in listOfCourses)
            {
                if(course.prerequisite.Count == 0)
                {
                   
                    course.courseChecked = true;
                    Courses.timeStamp += 1;
                    course.startTime = Courses.timeStamp;
                    DFS(course.adjCourses, Courses.numOfCourses);
                    Courses.timeStamp += 1;
                    course.endTime = Courses.timeStamp;
                }
            }

            foreach (Courses course in listOfCourses)
            {
                Console.WriteLine(course.nameOfCourses);
                Console.WriteLine("{0}/{1}",course.startTime,course.endTime);
            }
                       
        }

        static int numOfChecked(List<Courses> listOfCourses)
        {
            int count = 0;

            foreach (Courses course in listOfCourses)
            {
                if (course.courseChecked)
                {
                    count++;
                }
            }

            return count;
        }

        static void DFS(List<Courses> listOfCourses, int numOfCourses)
        {
            if(listOfCourses.Count == 0)
            {
                return;
            }
             
            foreach(Courses course in listOfCourses)
            {
                if (course.courseChecked == false)
                {
                    course.courseChecked = true;
                    Courses.timeStamp += 1;

                    course.startTime = Courses.timeStamp;
                    DFS(course.adjCourses, numOfCourses);
                    Courses.timeStamp += 1;
                    course.endTime = Courses.timeStamp;
                }
            }
        }

    }
}
