using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms; 

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
        public string fileToOpen = "";
        public int option = -1;
        public bool isButtonClicked = false;

        public Form1()
        {
            //BackColor = Color.Green;
            InitializeComponent();
            //var timer = new Timer();
            //timer.Interval = 1000;
            //timer.Tick += new EventHandler(timer_tick);
            //timer.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BackColor = Color.CadetBlue;
            Text = "CourseHelpie";
            Size = new Size(800, 600);
            Location = new Point(50, 50);
            MaximizeBox = false;

            Label title = new Label
            {
                MaximumSize = new Size(600, 500),
                AutoSize = true,
                Location = new Point(205, 25),
                Text = "Welcome to CourseHelpie App",
                Font = new Font("Tahoma", 16, FontStyle.Bold)
            };

            Button button1 = new coolButton
            {
                Text = "Browse",
                MaximumSize = new Size(600, 500),
                Location = new Point(323, 80)
            };

            button1.Click += new EventHandler(Button_Click);               

            Label iomsg = new Label()
            {
                Text = "Locate your file here",
                MaximumSize = new Size(600, 500),
                AutoSize = true,
                Location = new Point(303, 120),
                Font = new Font("Arial", 10, FontStyle.Italic)
            };

            if (fileToOpen == "")
            {
                Controls.Add(iomsg);
            }

            Controls.Add(button1);
            Controls.Add(title);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            var FD = new OpenFileDialog();
            if (FD.ShowDialog() == DialogResult.OK )
            {
                fileToOpen = FD.FileName;
                isButtonClicked = true;
            }
            Close();
        }

        private void Timer_tick(object sender, EventArgs e)
        {
            var colors = new[] { Color.CornflowerBlue, Color.Green, Color.Aqua, Color.Azure, Color.CadetBlue };
            var index = DateTime.Now.Second % colors.Length;
            BackColor = colors[index];
        }   

    }
}
