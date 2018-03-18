using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using QuickGraph;
using QuickGraph.Graphviz;
using QuickGraph.Graphviz.Dot;

namespace WindowsFormsApp1
{
    //
    // Schema for Matkul (Used by BFS)
    //
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

    //
    // Schema for Courses class (Used by DFS)
    //
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

    //
    // Class that overwrite ordinary button
    //
    public class coolButton : Button
    {
        private static Font _normalFont = new Font("Arial", 10F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));

        private static Color _back = Color.Gray;
        private static Color _border = Color.Black;
        private static Color _activeBorder = Color.Red;
        private static Color _fore = Color.White;

        private static Padding _margin = new Padding(5, 0, 5, 0);
        private static Padding _padding = new Padding(3, 3, 3, 3);

        private static Size _minSize = new Size(100, 30);

        private bool _active;

        public coolButton() : base()
        {
            base.Font = _normalFont;
            base.BackColor = _border;
            base.ForeColor = _fore;
            FlatAppearance.BorderColor = _back;
            FlatStyle = FlatStyle.Flat;
            Margin = _margin;
            Padding = _padding;
            base.MinimumSize = _minSize;
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            UseVisualStyleBackColor = false;
        }

        protected override void OnMouseEnter(System.EventArgs e)
        {
            base.OnMouseEnter(e);
            if (!_active)
                base.FlatAppearance.BorderColor = _activeBorder;
        }

        protected override void OnMouseLeave(System.EventArgs e)
        {
            base.OnMouseLeave(e);
            if (!_active)
                base.FlatAppearance.BorderColor = _border;
        }

        public void SetStateActive()
        {
            _active = true;
            base.FlatAppearance.BorderColor = _activeBorder;
        }

        public void SetStateNormal()
        {
            _active = false;
            base.FlatAppearance.BorderColor = _border;
        }
    }

    //
    // The Main Classes
    //
    public partial class CourseScheduler : Form
    {
        //
        // All the atribute used for GUI
        //
        public Button showGraph;
        public Button browseFile;
        public TextBox nameBox;
        public Label selectLabel;
        public coolButton bfsButton;
        public coolButton dfsButton;
        public string fileToOpen = "";
        public int option = -1;
        public bool isButtonClicked = false;

        //
        // The default constructor
        //
        public CourseScheduler()
        {
            InitializeComponent();          
        }

        //
        // Function to load the form
        //
        private void Form1_Load(object sender, EventArgs e)
        {
            Text = "CourseHelpie App";
            Location = new Point(350, 150);
            BackColor = Color.CadetBlue;
            Size = new Size(650, 500);
            showGraph = new Button();
            browseFile = new Button();
            nameBox = new TextBox();
            selectLabel = new Label();
            coolButton bfsButton = new coolButton();
            coolButton dfsButton = new coolButton();

            //
            // Label for title
            //

            Label title = new Label
            {
                MaximumSize = new Size(600, 500),
                AutoSize = true,
                Location = new Point(133, 20),
                Text = "Welcome to CourseHelpie App",
                Font = new Font("Tahoma", 16, FontStyle.Bold)
            };

            // 
            // Button for bfs
            // 

            bfsButton.Cursor = Cursors.Hand;
            bfsButton.AccessibleName = "bfsButton";
            bfsButton.Location = new Point(150, 170);
            bfsButton.Size = new Size(100, 100);
            bfsButton.Text = "Do the bfs";
            bfsButton.Click += new EventHandler(DoTheBFS);
            
            // 
            // Button for dfs
            // 
            
            dfsButton.Cursor = Cursors.Hand;
            dfsButton.AccessibleName = "dfsButton";
            dfsButton.Location = new Point(370, 170);
            dfsButton.Size = new Size(100, 100);
            dfsButton.Text = "Do the dfs";
            dfsButton.Click += new EventHandler(DoTheDFS);

            // 
            // Show the result of sorted graph
            //

            showGraph.Cursor = Cursors.Hand;
            showGraph.Dock = DockStyle.Bottom;
            showGraph.Enabled = false;
            showGraph.FlatStyle = FlatStyle.System;
            showGraph.Font = new Font("Segoe UI", 10.5F);
            showGraph.Location = new Point(0, 104);
            showGraph.Margin = new Padding(4);
            showGraph.Name = "showGraph";
            showGraph.Size = new Size(594, 47);
            showGraph.TabIndex = 1;
            showGraph.Text = "Show The Result Here";
            showGraph.UseVisualStyleBackColor = true;
            //showGraph.Click += new System.EventHandler(showGraph_Click);

            // 
            // browseFile
            // 

            browseFile.Cursor = Cursors.Hand;
            browseFile.FlatStyle = FlatStyle.System;
            browseFile.Font = new Font("Segoe UI", 10F);
            browseFile.Location = new Point(546, 63);
            browseFile.Margin = new Padding(8, 8, 0, 0);
            browseFile.Name = "browseFile";
            browseFile.Size = new Size(28, 25);
            browseFile.TabIndex = 3;
            browseFile.Text = "...";
            browseFile.UseVisualStyleBackColor = true;
            browseFile.Click += new EventHandler(Browse_File);

            // 
            // nameBox
            // 

            nameBox.Location = new Point(203, 66);
            nameBox.Margin = new Padding(4, 8, 0, 0);
            nameBox.Name = "nameBox";
            nameBox.Size = new Size(325, 25);
            nameBox.TabIndex = 2;
            nameBox.KeyDown += new KeyEventHandler(Tb_KeyDown);

            // 
            // selectLabel
            // 

            selectLabel.AutoSize = true;
            selectLabel.FlatStyle = FlatStyle.System;
            selectLabel.Font = new Font("Segoe UI", 11F);
            selectLabel.Location = new Point(32, 64);
            selectLabel.Margin = new Padding(0, 8, 4, 0);
            selectLabel.Name = "selectLabel";
            selectLabel.Size = new Size(207, 20);
            selectLabel.TabIndex = 0;
            selectLabel.Text = "Browse Your File Here: ";
            selectLabel.TextAlign = ContentAlignment.MiddleCenter;

            //
            // Add all UI
            //

            Controls.Add(showGraph);
            Controls.Add(nameBox);
            Controls.Add(browseFile);
            Controls.Add(selectLabel);
            Controls.Add(title);
            Controls.Add(bfsButton);
            Controls.Add(dfsButton);
        }

        //
        // Event for path typed entered
        //
        private void Tb_KeyDown (object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                fileToOpen = nameBox.Text;                
            }
        }

        //
        // Event for browsing the file through windows explorer
        //
        private void Browse_File (object sender, EventArgs e)
        {
            var FD = new OpenFileDialog();
            if (FD.ShowDialog() == DialogResult.OK )
            {
                fileToOpen = FD.FileName;
                isButtonClicked = true;

                if (DialogResult.Yes == MessageBox.Show("You have chosen a file." + " Want to do the sorting? ", "Dialog", MessageBoxButtons.YesNo))
                {
                    DisplayGraph(fileToOpen);       
                }

                else
                {
                    Close();
                }
            }         
        }

        //
        // Event for doing the topological sort BFS
        //
        private void DoTheBFS(object sender, EventArgs e)
        {
            try
            {
                string fileKuliah = fileToOpen;
                List<string> kuliah = File.ReadAllLines(fileKuliah).ToList();
                List<Matkul> listMatkul = new List<Matkul>();
                List<Matkul> UrutanMatkul = new List<Matkul>();
                foreach (string linekuliah in kuliah)
                {
                    Matkul M = new Matkul();
                    List<string> matk = linekuliah.Split(',').ToList();
                    M.nama = matk[0];
                    matk.RemoveAt(0);
                    M.syaratMatkul = matk;
                    M.countSyarat = matk.Count;
                    M.matkulChecked = false;
                    M.semester = 0;
                    listMatkul.Add(M);
                }
                int semesterMatkul = 1;
                BFS(listMatkul, semesterMatkul);

                foreach (Matkul matkul in listMatkul)
                {
                    Console.WriteLine("Matkul " + matkul.nama + " diambil pada semester " + matkul.semester);
                }

                showGraph.Enabled = true;
            }

            catch (Exception ex)
            {
                MessageBox.Show("You have not selected any file yet\nOr the path you type is not right", "No file found", MessageBoxButtons.OK);
            }


            void BFS(List<Matkul> listMatkul, int semesterMatkul)
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

            bool notAllChecked(List<Matkul> listMatkul)
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

            bool checkSyarat(Matkul matkul1, Matkul matkul2)
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

            bool checkSyarat2(List<Matkul> listMatkul, Matkul matkul1, int semester)
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

        //
        // Event for doing the topological sort DFS
        //
        private void DoTheDFS(object sender, EventArgs e)
        {
            try
            {
                string fileOfCourses = fileToOpen;
                List<string> wholeCourses = File.ReadAllLines(fileOfCourses).ToList();
                List<Courses> listOfCourses = new List<Courses>();

                int semester = 0;

                // Split courses and set its attributes
                foreach (string course in wholeCourses)
                {
                    Courses thisCourse = new Courses();
                    List<string> prerequisiteName = course.Split(',').ToList();
                    thisCourse.nameOfCourses = prerequisiteName[0];
                    prerequisiteName.RemoveAt(0);
                    thisCourse.prerequisiteName = prerequisiteName;
                    thisCourse.semester = 0;
                    thisCourse.startTime = 0;
                    thisCourse.endTime = 0;
                    thisCourse.semester = 0;
                    thisCourse.courseChecked = false;
                    thisCourse.cOfAdj = prerequisiteName.Count;
                    listOfCourses.Add(thisCourse);
                }

                // Search the corresponding string course's name with its course's object
                foreach (Courses course in listOfCourses)
                {
                    List<Courses> listOfPrerequisite = new List<Courses>();
                    foreach (Courses anCourse in listOfCourses)
                    {
                        foreach (string name in course.prerequisiteName)
                        {
                            if (name == anCourse.nameOfCourses)
                            {
                                listOfPrerequisite.Add(anCourse);
                            }
                        }
                    }
                    course.prerequisite = listOfPrerequisite;
                }

                // Search for adjacent node
                foreach (Courses course in listOfCourses)
                {
                    foreach (Courses anCourse in listOfCourses)
                    {
                        if (anCourse != course)
                        {
                            foreach (string name in anCourse.prerequisiteName)
                            {
                                if (name == course.nameOfCourses)
                                {
                                    course.adjCourses.Add(anCourse);
                                }
                            }
                        }
                    }
                }

                List<Courses> solution = new List<Courses>();

                // Handle course with 0 prerequisite as a starting point
                foreach (Courses course in listOfCourses)
                {
                    if (course.prerequisiteName.Count == 0)
                    {
                        course.semester = 1;
                        sortDFS(course, solution);
                    }
                }

                int len = solution.Count - 1;
                for (int i = len; i >= 0; i--)
                {
                    //Console.WriteLine(solution[i].nameOfCourses);
                    foreach (Courses course in listOfCourses)
                    {
                        if (course == solution[i])
                        {
                            int parentSemester = getMaxSemester(course);
                            course.semester = parentSemester + 1;
                        }
                    }
                }

                foreach (Courses course in listOfCourses)
                {
                    Console.WriteLine("{0} is taken in semester {1}", course.nameOfCourses, course.semester);
                }

                showGraph.Enabled = true;
            }

            catch (Exception ex)
            {
                MessageBox.Show("You have not selected any file yet\nOr the path you type is not right", "No file found", MessageBoxButtons.OK);
            }


            int getMaxSemester(Courses course)
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

            void sortDFS(Courses course, List<Courses> solution)
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
        }

        //
        // Function to display graph that parsed from the file chosen
        //
        public void DisplayGraph(string fileCourse)
        {
            Form form = new Form();
            Microsoft.Msagl.GraphViewerGdi.GViewer viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
            Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");
            graph.AddEdge("A", "B");
            graph.AddEdge("B", "C");
            graph.AddEdge("A", "C").Attr.Color = Microsoft.Msagl.Drawing.Color.Green;
            graph.FindNode("A").Attr.FillColor = Microsoft.Msagl.Drawing.Color.Magenta;
            graph.FindNode("B").Attr.FillColor = Microsoft.Msagl.Drawing.Color.MistyRose;
            Microsoft.Msagl.Drawing.Node c = graph.FindNode("C");
            c.Attr.FillColor = Microsoft.Msagl.Drawing.Color.PaleGreen;
            c.Attr.Shape = Microsoft.Msagl.Drawing.Shape.Diamond;
            viewer.Graph = graph;
            form.SuspendLayout();
            viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            form.Controls.Add(viewer);
            form.ResumeLayout();
            form.ShowDialog();
        }     

    }
}
