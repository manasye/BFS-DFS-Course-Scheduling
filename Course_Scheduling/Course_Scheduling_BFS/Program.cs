using System;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;

namespace test
{
    class Matkul
    {
        public Matkul()
        {
            List<string> syaratMatkul = new List<string>();
            
        }
        public bool matkulChecked {get; set;}
        public int countSyarat {get; set;}
        public List<string> syaratMatkul {get; set;}
        public int semester {get; set;}
        public string nama {get; set;}
    }
    class Program
    {
        static void Main(string[] args)
        {
<<<<<<< HEAD
            string fileKuliah = @"E:\Users\juanf\Documents\GitHub\BFS-DFS-Course-Scheduling\Course_Scheduling\Course_Scheduling_BFS\kuliah.txt";
=======
            string fileKuliah = @"C:\Users\manasyebukit\Documents\GitHub\BFS-DFS-Courses-Scheduling\Course_Scheduling\Course_Scheduling_BFS\kuliah.txt";
>>>>>>> 0c80f6279fdb00c9544e29d4241c9521107f9c44
            List<string> kuliah = File.ReadAllLines(fileKuliah).ToList();
            List<Matkul> listMatkul = new List<Matkul>();
            List<Matkul> UrutanMatkul = new List<Matkul>();
            foreach (string linekuliah in kuliah)
            {   
                Matkul M = new Matkul();
                List<string> matk= linekuliah.Split(',').ToList();
                M.nama = matk[0];
                matk.RemoveAt(0);
                M.syaratMatkul = matk;
                M.countSyarat = matk.Count;
                M.matkulChecked = false;
                M.semester = 0;
                listMatkul.Add(M);
            }
            int semesterMatkul = 1;
            BFS(listMatkul,semesterMatkul);
            foreach (Matkul matkul in listMatkul)
            {
                Console.WriteLine("Matkul "+matkul.nama+" diambil pada semester "+matkul.semester);
            }
            
            
            
        }

        static bool checkSyarat(Matkul matkul1,Matkul matkul2)
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

        static bool checkSyarat2(List<Matkul> listMatkul, Matkul matkul1,int semester)
        {
            bool cek = false;
            foreach(Matkul matkul2 in listMatkul)
            {
                if ((checkSyarat(matkul1,matkul2)) && (semester == matkul2.semester))
                {
                    cek = true;
                    break;
                }
            }
            return cek;
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

        static void BFS(List<Matkul> listMatkul, int semesterMatkul)
        {
            if (notAllChecked(listMatkul))
            {
                foreach(Matkul matkul in listMatkul)
                {
                    if ((matkul.countSyarat == 0) && (!(checkSyarat2(listMatkul,matkul,semesterMatkul))))
                    {
                        matkul.semester = semesterMatkul;
                        foreach(Matkul matkul1 in listMatkul)
                        {
                            foreach(string syarat in matkul1.syaratMatkul)
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
                BFS(listMatkul,semesterMatkul);
            }
        }
    }
}
