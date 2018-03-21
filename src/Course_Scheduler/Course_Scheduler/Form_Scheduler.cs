using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace Course_Scheduler
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
            List<string> adjCourses = new List<string>();
        }
        public bool courseChecked { get; set; }
        public static int timeStamp = 0;
        public int semester { get; set; }
        public string nameOfCourses { get; set; }
        public List<Courses> adjCourses = new List<Courses>();
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
        public Button showResult;
        public Button browseFile;
        public TextBox nameBox;
        public TextBox result;
        public TextBox process;
        public Label selectLabel;
        public coolButton bfsButton;
        public bool bfsClicked = false;
        public coolButton dfsButton;
        public bool dfsClicked = false;
        public string fileToOpen = "";
        public bool isButtonClicked = false;
        private List<Courses> listCourse = new List<Courses>();
        private List<Matkul> listMataKuliah = new List<Matkul>();

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
            BackColor = Color.LightGray;
            Size = new Size(650, 500);
            showResult = new Button();
            browseFile = new Button();
            nameBox = new TextBox();
            result = new TextBox();
            process = new TextBox();
            selectLabel = new Label();
            bfsButton = new coolButton();
            dfsButton = new coolButton();

            //
            // Label for title
            //

            Label title = new Label
            {
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
            bfsButton.Location = new Point(150, 110);
            bfsButton.Text = "Do the bfs";
            bfsButton.Click += new EventHandler(DoTheBFS);

            // 
            // Button for dfs
            // 

            dfsButton.Cursor = Cursors.Hand;
            dfsButton.AccessibleName = "dfsButton";
            dfsButton.Location = new Point(370, 110);
            dfsButton.Text = "Do the dfs";
            dfsButton.Click += new EventHandler(DoTheDFS);

            //
            // Label for process title
            //

            Label processLabel = new Label
            {
                AutoSize = true,
                Location = new Point(70, 180),
                Text = "The process is here",
                Font = new Font("Arial", 10, FontStyle.Bold)
            };

            //
            // Label for result title
            //

            Label resultLabel = new Label
            {
                AutoSize = true,
                Location = new Point(410, 180),
                Text = "The result is here",
                Font = new Font("Arial", 10, FontStyle.Bold)
            };


            // 
            // Process text box
            // 

            process.AutoSize = false;
            process.Font = new Font("Arial", 10, FontStyle.Regular);
            process.Multiline = true;
            process.Location = new Point(20, 200);
            process.Size = new Size(270, 200);
            process.ScrollBars = ScrollBars.Both;
            process.WordWrap = false;

            // 
            // Result text box
            // 

            result.AutoSize = false;
            result.Multiline = true;
            result.Font = new Font("Arial", 10, FontStyle.Regular);
            result.Location = new Point(350, 200);
            result.Size = new Size(260, 200);
            result.ScrollBars = ScrollBars.Both;
            result.WordWrap = false;

            // 
            // Show the result of sorted graph
            //

            showResult.Cursor = Cursors.Hand;
            showResult.Dock = DockStyle.Bottom;
            showResult.Enabled = false;
            showResult.FlatStyle = FlatStyle.System;
            showResult.Font = new Font("Segoe UI", 11);
            showResult.Location = new Point(0, 104);
            showResult.Margin = new Padding(4);
            showResult.Size = new Size(594, 47);
            showResult.TabIndex = 1;
            showResult.Text = "Show The Result";
            showResult.UseVisualStyleBackColor = true;
            showResult.Click += new EventHandler(Show_Result);

            // 
            // browseFile
            // 

            browseFile.Cursor = Cursors.Hand;
            browseFile.FlatStyle = FlatStyle.System;
            browseFile.Font = new Font("Segoe UI", 10F);
            browseFile.Location = new Point(541, 61);
            browseFile.Margin = new Padding(8, 8, 0, 0);
            browseFile.AutoSize = true;
            browseFile.Size = new Size(28, 25);
            browseFile.TabIndex = 3;
            browseFile.Text = "Browse";
            browseFile.UseVisualStyleBackColor = true;
            browseFile.Click += new EventHandler(Browse_File);

            // 
            // nameBox
            // 

            nameBox.Location = new Point(203, 66);
            nameBox.Margin = new Padding(4, 8, 0, 0);
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

            Controls.Add(showResult);
            Controls.Add(nameBox);
            Controls.Add(browseFile);
            Controls.Add(selectLabel);
            Controls.Add(title);
            Controls.Add(bfsButton);
            Controls.Add(dfsButton);
            Controls.Add(process);
            Controls.Add(result);
            Controls.Add(processLabel);
            Controls.Add(resultLabel);
        }

        //
        // Event for path typed entered
        //
        private void Tb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                fileToOpen = nameBox.Text;

                process.Text = "";
                result.Text = "";

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
        // Event for browsing the file through windows explorer
        //
        private void Browse_File(object sender, EventArgs e)
        {
            var FD = new OpenFileDialog();
            if (FD.ShowDialog() == DialogResult.OK)
            {
                fileToOpen = FD.FileName;
                isButtonClicked = true;

                process.Text = "";
                result.Text = "";

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
                process.Text = "";
                result.Text = "";
                bfsClicked = true;
                string fileKuliah = fileToOpen;
                List<string> kuliah = File.ReadAllLines(fileKuliah).ToList();
                List<Matkul> listMatkul = new List<Matkul>();
                listMataKuliah = listMatkul;
                List<Matkul> UrutanMatkul = new List<Matkul>();
                foreach (string linekuliah in kuliah)
                {
                    Matkul M = new Matkul();
                    List<string> matk = linekuliah.Split(new char[] { ',', ' ', '.' }, StringSplitOptions.RemoveEmptyEntries).ToList();
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

                showResult.Enabled = true;
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
                            string text = matkul.nama + " is visited" + "\n";
                            process.AppendText(text);
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
                            text = matkul.nama + " done visited" + "\n";
                            process.AppendText(text);
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
                process.Text = "";
                result.Text = "";
                dfsClicked = true;
                string fileOfCourses = fileToOpen;
                List<string> wholeCourses = File.ReadAllLines(fileOfCourses).ToList();
                List<Courses> listOfCourses = new List<Courses>();
                listCourse = listOfCourses;

                // Split courses and set its attributes
                foreach (string course in wholeCourses)
                {
                    Courses thisCourse = new Courses();
                    List<string> prerequisiteName = course.Split(new char[] { ',', ' ', '.' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    thisCourse.nameOfCourses = prerequisiteName[0];
                    prerequisiteName.RemoveAt(0);
                    thisCourse.prerequisiteName = prerequisiteName;
                    thisCourse.semester = 0;
                    thisCourse.startTime = 0;
                    thisCourse.endTime = 0;
                    thisCourse.semester = 0;
                    thisCourse.courseChecked = false;
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

                showResult.Enabled = true;
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
                string text = course.nameOfCourses + " is visited" + "\n";
                process.AppendText(text);

                foreach (Courses adj in course.adjCourses)
                {
                    if (!adj.courseChecked)
                    {
                        sortDFS(adj, solution);
                    }
                }

                Courses.timeStamp += 1;
                course.endTime = Courses.timeStamp;
                text = course.nameOfCourses + " done visited" + "\n";
                process.AppendText(text);
                solution.Add(course);
            }
        }

        //
        // Function to display graph that parsed from the file chosen
        //
        private void DisplayGraph(string fileCourse)
        {
            List<string> wholeCourses = File.ReadAllLines(fileCourse).ToList();
            List<Courses> listOfCourses = new List<Courses>();

            foreach (string course in wholeCourses)
            {
                Courses thisCourse = new Courses();
                List<string> prerequisiteName = course.Split(new char[] { ',', ' ', '.' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                thisCourse.nameOfCourses = prerequisiteName[0];
                prerequisiteName.RemoveAt(0);
                thisCourse.prerequisiteName = prerequisiteName;
                thisCourse.semester = 0;
                thisCourse.startTime = 0;
                thisCourse.endTime = 0;
                thisCourse.semester = 0;
                thisCourse.courseChecked = false;
                listOfCourses.Add(thisCourse);
            }
            Form form = new Form();
            Microsoft.Msagl.GraphViewerGdi.GViewer viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
            Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");

            Random r = new Random();
            foreach (Courses course in listOfCourses)
            {
                foreach (Courses anCourse in listOfCourses)
                {
                    if (course != anCourse)
                    {
                        foreach (string prereq in anCourse.prerequisiteName)
                        {
                            if (prereq == course.nameOfCourses)
                            {
                                graph.AddEdge(course.nameOfCourses, anCourse.nameOfCourses).Attr.Color =
                                    Microsoft.Msagl.Drawing.Color.BlueViolet;
                                break;
                            }
                        }
                    }
                }
            }

            foreach (Courses course in listOfCourses)
            {
                graph.FindNode(course.nameOfCourses).Attr.FillColor =
                    Microsoft.Msagl.Drawing.Color.Aqua;
                graph.FindNode(course.nameOfCourses).Attr.Shape = Microsoft.Msagl.Drawing.Shape.Ellipse;
            }

            viewer.Graph = graph;
            form.SuspendLayout();
            viewer.Dock = DockStyle.Fill;
            form.Controls.Add(viewer);
            form.ResumeLayout();
            form.ShowDialog();
        }

        //
        // Function to display the result
        //
        public void Show_Result(object sender, EventArgs e)
        {
            if (dfsClicked)
            {
                result.Text = "";
                foreach (Courses course in listCourse)
                {
                    String text = course.nameOfCourses + " is taken in semester " + course.semester + "\n";
                    result.AppendText(text);
                }
                dfsClicked = false;
            }

            else
            {
                result.Text = "";
                foreach (Matkul matkul in listMataKuliah)
                {
                    String text = matkul.nama + " is taken in semester " + matkul.semester + "\n";
                    result.AppendText(text);
                }
                bfsClicked = false;
            }
        }
    }
}
