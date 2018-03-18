using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuickGraph;
using QuickGraph.Graphviz;
using QuickGraph.Graphviz.Dot;

namespace WindowsFormsApp1
{

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

    public partial class Form1 : Form
    {
        BidirectionalGraph<string, Edge<string>> graph = new BidirectionalGraph<string, Edge<string>>(true);
        TextBox isiFileTextBox = new TextBox();
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void cariFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Filter = "Text File | *txt";
            openFileDialog.Title = "Cari text file kuliah";

            if (openFileDialog.ShowDialog() == DialogResult.OK && openFileDialog.OpenFile() != null)
            {
                try
                {
                    this.namaFileTextBox.Text = openFileDialog.FileName;
                    Label isiFileLabel = new Label();

                    isiFileLabel.AutoSize = true;
                    isiFileLabel.Text = "Isi file dari " + openFileDialog.FileName.ToString() + " = ";
                    isiFileLabel.Font = (Font)pilihFileLabel.Font.Clone();
                    isiFileLabel.Margin = (Padding)pilihFileLabel.Margin;

                    isiFileTextBox.Text = File.ReadAllText(openFileDialog.FileName);
                    isiFileTextBox.Multiline = true;
                    isiFileTextBox.ScrollBars = ScrollBars.Vertical;
                    isiFileTextBox.ReadOnly = true;
                    isiFileTextBox.Height = 150;
                    isiFileTextBox.Margin = (Padding)pilihFileLabel.Margin;
                    isiFileTextBox.Width = (pilihFileLabel.Width + 16 + cariFile.Width + namaFileTextBox.Width) - pilihFileLabel.Location.X;


                    if (flowLayoutPanel.Controls.Count != 3)
                    {
                        flowLayoutPanel.Controls.RemoveAt(flowLayoutPanel.Controls.Count - 1);
                        flowLayoutPanel.Controls.RemoveAt(flowLayoutPanel.Controls.Count - 1);
                    }
                    flowLayoutPanel.Controls.Add(isiFileLabel);
                    flowLayoutPanel.Controls.Add(isiFileTextBox);
                    this.MaximumSize = new Size(610, 170 + isiFileLabel.Height + isiFileTextBox.Height);
                    this.MinimumSize = new Size(610, 170 + isiFileLabel.Height + isiFileTextBox.Height);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan!", "Error: " + ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void flowLayoutPanel_ControlAdded(object sender, ControlEventArgs e)
        {
            if (flowLayoutPanel.Controls.Count > 3)
            {
                tampilkanGraph.Enabled = true;
            }
        }

        private void tampilkanGraph_Click(object sender, EventArgs e)
        {
            graph.Clear();
            Console.Clear();
            if (Application.OpenForms.Count == 2)
            {
                Application.OpenForms[1].Dispose();
            }
            HashSet<string> hashSetMatKul = parseStringToSetOfIVertex(isiFileTextBox.Text.ToString());

            //create vertex (matkul)
            foreach (string matkul in hashSetMatKul)
            {
                graph.AddVertex(matkul);
            }

            addEdges();
            foreach (string w in graph.Vertices)
            {
                Console.WriteLine(w);
            }

            //TopologicalSortAlgorithm<string, Edge<string>> topo = new TopologicalSortAlgorithm<string, Edge<string>>(graph);
            //topo.Compute();
            //IList<string> sortVertices =  topo.SortedVertices;

            //Console.WriteLine();
            //foreach (string v in sortVertices) {
            //    Console.WriteLine(v);

            GraphvizAlgorithm<string, Edge<string>> graphvizAlgorithm = new GraphvizAlgorithm<string, Edge<string>>(graph);
            graphvizAlgorithm.ImageType = GraphvizImageType.Png;
            graphvizAlgorithm.FormatVertex += GraphvizAlgorithm_FormatVertex;
            //graphvizAlgorithm.Generate(new FileDotEngine(), Application.StartupPath + "\\image\\image");

            //show Graph!
            Form pictureForm = new Form();
            PictureBox pictureBox = new PictureBox();
            Button dfsButton = new Button();
            Button bfsButton = new Button();

            //Setting picturebox buat nampilin graph
            pictureBox.Image = Image.FromFile(Application.StartupPath + "\\image\\image.png");
            pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;

            //DFS Button 
            dfsButton.Text = "Terapkan DFS";
            dfsButton.Size = new Size(pictureBox.Width / 2, 50);
            dfsButton.FlatStyle = FlatStyle.System;
            dfsButton.Location = new Point(pictureBox.Location.X, pictureBox.Height + 2);
            dfsButton.Click += DfsButton_Click;

            //BFS Button
            bfsButton.Text = "Terapkan BFS";
            bfsButton.Size = new Size(pictureBox.Width / 2, 50);
            bfsButton.FlatStyle = FlatStyle.System;
            bfsButton.Location = new Point(dfsButton.Width, pictureBox.Height + 2);
            bfsButton.Click += BfsButton_Click;

            //Setting pictureForm buat nampilin window form
            pictureForm.Size = new Size(pictureBox.Width + 15, pictureBox.Height + 92);
            pictureForm.Show();
            pictureForm.MaximizeBox = false;
            pictureForm.FormBorderStyle = FormBorderStyle.FixedSingle;
            pictureForm.ShowIcon = false;
            pictureForm.Text = "Sketch Graph";
            pictureForm.Controls.Add(pictureBox);
            pictureForm.Controls.Add(dfsButton);
            pictureForm.Controls.Add(bfsButton);

        }

        private void BfsButton_Click(object sender, EventArgs e)
        {
            List<string> ListMatkulSorted = new List<string>();
            foreach (string v in graph.Vertices)
            {
                topologicalSortBFS(ListMatkulSorted, v);
            }

        }

        private void DfsButton_Click(object sender, EventArgs e)
        {
            List<string>[] listMatkulSorted = new List<string>[8];
            for (int i = 0; i < 8; i++)
            {
                listMatkulSorted[i] = new List<string>();
            }
            Stack<string> stack = new Stack<string>();
            Stack<string> temp = new Stack<string>();
            HashSet<string> visitedVertex = new HashSet<string>(); //buat ngecek vertex yang udah di-visit

            //nyari root
            foreach (string v in graph.Vertices)
            {
                if (graph.IsInEdgesEmpty(v))
                {
                    visitedVertex.Add(v);
                    listMatkulSorted[0].Add(v);
                    stack.Push(v);
                }
            }

            string current = listMatkulSorted[0].ElementAt<string>(0);
            while (visitedVertex.Count < graph.Vertices.Count())
            {
                IEnumerable<Edge<string>> edges;
                string tempVertex;
                if (graph.TryGetOutEdges(current, out edges) && graph.OutDegree(current) > 0)
                {
                    tempVertex = edges.ElementAt<Edge<string>>(0).Target;
                    int temp_idx = 0;
                    for (int i = 1; i < edges.Count<Edge<string>>(); i++)
                    {
                        if (!visitedVertex.Contains(edges.ElementAt<Edge<string>>(i).Target))
                        {
                            if (graph.InDegree(edges.ElementAt<Edge<string>>(i).Target) < graph.InDegree(tempVertex))
                            {
                                tempVertex = edges.ElementAt<Edge<string>>(i).Target;
                                temp_idx = i;
                            }
                            else if (graph.InDegree(edges.ElementAt<Edge<string>>(i).Target) == graph.InDegree(tempVertex))
                            {
                                if (graph.OutDegree(edges.ElementAt<Edge<string>>(i).Target) > graph.OutDegree(tempVertex))
                                {
                                    tempVertex = edges.ElementAt<Edge<string>>(i).Target;
                                    temp_idx = i;
                                }
                            }
                        }
                    }
                    graph.RemoveEdge(edges.ElementAt<Edge<string>>(temp_idx));
                    topoLogicalSortDFS(visitedVertex, stack, tempVertex);
                    current = tempVertex;
                }
                else if (graph.OutDegree(current) == 0)
                {
                    foreach (string v in graph.Vertices)
                    {
                        if (graph.OutDegree(v) > 0)
                        {
                            topoLogicalSortDFS(visitedVertex, stack, v);
                            current = v;
                        }
                    }
                }
            }

            while (stack.Count != 0)
            {
                temp.Push(stack.Pop());
            }

            while (temp.Count != 0)
            {
                Console.Write(temp.Pop() + " ");
            }
        }



        //Topological Sort BFS
        private void topologicalSortBFS(List<string> visitedVertex, string v)
        {
            if (graph.InDegree(v) == 0)
            {
                visitedVertex.Add(v);
            }
            graph.RemoveVertex(v);
        }

        private string getRoot()
        {
            string vertex = "";
            foreach (string v in graph.Vertices)
            {
                if (graph.InDegree(v) == 0)
                {
                    vertex = v;
                }
            }
            return vertex;
        }

        //topo search
        private void topoLogicalSortDFS(HashSet<string> visitedVertex, Stack<string> stack, string v)
        {
            //mark visited
            if (!visitedVertex.Contains(v))
            {
                stack.Push(v);
            }
            visitedVertex.Add(v);

            IEnumerable<Edge<string>> edges;
            string tempVertex;
            if (graph.TryGetOutEdges(v, out edges) && graph.OutDegree(v) > 0)
            {
                tempVertex = edges.ElementAt<Edge<string>>(0).Target;
                int temp = 0;
                for (int i = 1; i < edges.Count<Edge<string>>(); i++)
                {
                    if (!visitedVertex.Contains(edges.ElementAt<Edge<string>>(i).Target))
                    {
                        if (graph.InDegree(edges.ElementAt<Edge<string>>(i).Target) < graph.InDegree(tempVertex))
                        {
                            tempVertex = edges.ElementAt<Edge<string>>(i).Target;
                            temp = i;
                        }
                        else if (graph.InDegree(edges.ElementAt<Edge<string>>(i).Target) == graph.InDegree(tempVertex))
                        {
                            if (graph.OutDegree(edges.ElementAt<Edge<string>>(i).Target) > graph.OutDegree(tempVertex))
                            {
                                temp = i;
                                tempVertex = edges.ElementAt<Edge<string>>(i).Target;
                            }
                        }
                    }
                }
                graph.RemoveEdge(edges.ElementAt<Edge<string>>(temp));
                topoLogicalSortDFS(visitedVertex, stack, tempVertex);
            }
        }

        private void GraphvizAlgorithm_FormatVertex(object sender, FormatVertexEventArgs<string> e)
        {
            e.VertexFormatter.Label = e.Vertex;
        }

        private void addEdges()
        {
            char[] delimit1 = { '\n' };
            char[] delimit2 = { ',', ' ', '.' };

            string[] arraySplit1 = isiFileTextBox.Text.ToString().Split(delimit1);
            for (int i = 0; i < arraySplit1.Length; i++)
            {
                string[] arraySplit2 = arraySplit1[i].Split(delimit2);
                for (int j = 1; j < arraySplit2.Length; j++)
                {
                    if (!String.IsNullOrEmpty(arraySplit2[j]) && !String.IsNullOrWhiteSpace(arraySplit2[j]))
                    {
                        Edge<string> edge = new Edge<string>(arraySplit2[j], arraySplit2[0]);
                        graph.AddEdge(edge);
                    }
                }
            }
        }

        private HashSet<string> parseStringToSetOfIVertex(string text)
        {
            //Baca string di textbox
            char[] delimit = {
                ' ',
                '.',
                ',',
                '\n',
                '\0'
            };

            string[] arrayMatKul = text.Split(delimit);
            HashSet<string> hashSetMatKul = new HashSet<string>();
            foreach (string matkul in arrayMatKul)
            {
                if (!String.IsNullOrEmpty(matkul) && !String.IsNullOrWhiteSpace(matkul))
                {
                    hashSetMatKul.Add(matkul);
                }
            }
            return hashSetMatKul;
        }

    }
}
