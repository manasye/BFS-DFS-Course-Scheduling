using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApp1
{
    class Matkul
    {
        public Matkul()
        {
            List<string> syaratMatkul = new List<string>();

        }
        public bool matkulChecked { get; set; }
        public int countSyarat { get; set; }
        public List<string> syaratMatkul { get; set; }
        public int semester { get; set; }
        public string nama { get; set; }
    }

    // Schema for Courses class
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
        public float valOfTime { get; set; }
        public int startTime { get; set; }
        public int endTime { get; set; }
        public List<string> prerequisiteName = new List<string>();
        public List<Courses> prerequisite = new List<Courses>();
    }

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Form1 Form = new Form1();
            //Application.Run(Form);

            //Console.WriteLine("Pick one command");
            //Console.WriteLine("1. Sort by BFS");
            //Console.WriteLine("2. Sort by DFS");
            //Console.Write(">>> ");

            //int cmd = Int32.Parse(Console.ReadLine());
            //string file = @Form.fileToOpen;

            //if (cmd == 1)
            //{
            //    List<string> kuliah = File.ReadAllLines(file).ToList();
            //    List<Matkul> listMatkul = new List<Matkul>();
            //    List<Matkul> UrutanMatkul = new List<Matkul>();

            //    foreach (string linekuliah in kuliah)
            //    {
            //        Matkul M = new Matkul();
            //        List<string> matk = linekuliah.Split(',').ToList();
            //        M.nama = matk[0];
            //        matk.RemoveAt(0);
            //        M.syaratMatkul = matk;
            //        M.countSyarat = matk.Count;
            //        M.matkulChecked = false;
            //        M.semester = 0;
            //        listMatkul.Add(M);
            //    }

            //    int semesterMatkul = 1;
            //    BFS(listMatkul, semesterMatkul);
            //    foreach (Matkul matkul in listMatkul)
            //    {
            //        Console.WriteLine("Matkul " + matkul.nama + " diambil pada semester " + matkul.semester);
            //    }
            //}

            //else
            //{
            //    List<string> wholeCourses = File.ReadAllLines(file).ToList();
            //    List<Courses> listOfCourses = new List<Courses>();

            //    int semester = 0;

            //    // Split courses and set its attributes
            //    foreach (string course in wholeCourses)
            //    {
            //        Courses thisCourse = new Courses();
            //        List<string> prerequisiteName = course.Split(',').ToList();
            //        thisCourse.nameOfCourses = prerequisiteName[0];
            //        prerequisiteName.RemoveAt(0);
            //        thisCourse.prerequisiteName = prerequisiteName;
            //        thisCourse.semester = 0;
            //        thisCourse.startTime = 0;
            //        thisCourse.endTime = 0;
            //        thisCourse.semester = 0;
            //        thisCourse.courseChecked = false;
            //        thisCourse.cOfAdj = prerequisiteName.Count;
            //        listOfCourses.Add(thisCourse);
            //    }

            //    // Search the corresponding string course's name with its course's object
            //    foreach (Courses course in listOfCourses)
            //    {
            //        List<Courses> listOfPrerequisite = new List<Courses>();
            //        foreach (Courses anCourse in listOfCourses)
            //        {
            //            foreach (string name in course.prerequisiteName)
            //            {
            //                if (name == anCourse.nameOfCourses)
            //                {
            //                    listOfPrerequisite.Add(anCourse);
            //                }
            //            }
            //        }
            //        course.prerequisite = listOfPrerequisite;
            //    }

            //    // Search for adjacent node
            //    foreach (Courses course in listOfCourses)
            //    {
            //        foreach (Courses anCourse in listOfCourses)
            //        {
            //            if (anCourse != course)
            //            {
            //                foreach (string name in anCourse.prerequisiteName)
            //                {
            //                    if (name == course.nameOfCourses)
            //                    {
            //                        course.adjCourses.Add(anCourse);
            //                    }
            //                }
            //            }
            //        }
            //    }

            //    List<Courses> solution = new List<Courses>();

            //    // Handle course with 0 prerequisite as a starting point
            //    foreach (Courses course in listOfCourses)
            //    {
            //        if (course.prerequisiteName.Count == 0)
            //        {
            //            course.semester = 1;
            //            sortDFS(course, solution);
            //        }
            //    }

            //    int len = solution.Count - 1;
            //    for (int i = len; i >= 0; i--)
            //    {
            //        //Console.WriteLine(solution[i].nameOfCourses);
            //        foreach (Courses course in listOfCourses)
            //        {
            //            if (course == solution[i])
            //            {
            //                int parentSemester = getMaxSemester(course);
            //                course.semester = parentSemester + 1;
            //            }
            //        }
            //    }

            //    foreach (Courses course in listOfCourses)
            //    {
            //        Console.WriteLine("{0} is taken in semester {1}", course.nameOfCourses, course.semester);
            //    }
            //}

        }

        static int getMaxSemester(Courses course)
        {
            int max = 0;
            foreach (Courses anCourse in course.prerequisite)
            {
                if (anCourse.semester >= max)
                {
                    max = anCourse.semester;
                }
            }
            return max;
        }

        static void sortDFS(Courses course, List<Courses> solution)
        {
            Courses.timeStamp += 1;
            course.startTime = Courses.timeStamp;
            course.courseChecked = true;

            foreach (Courses adj in course.adjCourses)
            {
                if (!adj.courseChecked)
                {
                    sortDFS(adj, solution);
                }
            }

            Courses.timeStamp += 1;
            course.endTime = Courses.timeStamp;
            solution.Add(course);
        }

        static void BFS(List<Matkul> listMatkul, int semesterMatkul)
        {
            if (notAllChecked(listMatkul))
            {
                foreach (Matkul matkul in listMatkul)
                {
                    if ((matkul.countSyarat == 0) && (!(checkSyarat2(listMatkul, matkul, semesterMatkul))))
                    {
                        matkul.semester = semesterMatkul;
                        foreach (Matkul matkul1 in listMatkul)
                        {
                            foreach (string syarat in matkul1.syaratMatkul)
                            {
                                if (syarat == matkul.nama)
                                {
                                    matkul1.countSyarat--;
                                    break;
                                }
                            }
                        }
                        matkul.countSyarat = -999;
                        matkul.matkulChecked = true;
                    }
                }
                semesterMatkul++;
                BFS(listMatkul, semesterMatkul);
            }
        }

        static bool notAllChecked(List<Matkul> listMatkul)
        {
            bool check = false;
            foreach (Matkul matkul in listMatkul)
            {
                if (!(matkul.matkulChecked))
                {
                    check = true;
                    break;
                }
            }
            return check;
        }

        static bool checkSyarat(Matkul matkul1, Matkul matkul2)
        {
            bool cek = false;
            foreach (string syarat in matkul1.syaratMatkul)
            {
                if ((syarat == matkul2.nama) && matkul2.matkulChecked)
                {
                    cek = true;
                    break;
                }
            }
            return cek;
        }

        static bool checkSyarat2(List<Matkul> listMatkul, Matkul matkul1, int semester)
        {
            bool cek = false;
            foreach (Matkul matkul2 in listMatkul)
            {
                if ((checkSyarat(matkul1, matkul2)) && (semester == matkul2.semester))
                {
                    cek = true;
                    break;
                }
            }
            return cek;
        }
    }
}
