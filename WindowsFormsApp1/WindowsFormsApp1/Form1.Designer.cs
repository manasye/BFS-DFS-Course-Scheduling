namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tampilkanGraph = new System.Windows.Forms.Button();
            this.cariFile = new System.Windows.Forms.Button();
            this.namaFileTextBox = new System.Windows.Forms.TextBox();
            this.pilihFileLabel = new System.Windows.Forms.Label();
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tampilkanGraph
            // 
            this.tampilkanGraph.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tampilkanGraph.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tampilkanGraph.Enabled = false;
            this.tampilkanGraph.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.tampilkanGraph.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.tampilkanGraph.Location = new System.Drawing.Point(0, 84);
            this.tampilkanGraph.Margin = new System.Windows.Forms.Padding(4);
            this.tampilkanGraph.Name = "tampilkanGraph";
            this.tampilkanGraph.Size = new System.Drawing.Size(594, 47);
            this.tampilkanGraph.TabIndex = 1;
            this.tampilkanGraph.Text = "Tampilkan Graph Acyclic";
            this.tampilkanGraph.UseVisualStyleBackColor = true;
            this.tampilkanGraph.Click += new System.EventHandler(this.tampilkanGraph_Click);
            // 
            // cariFile
            // 
            this.cariFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cariFile.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cariFile.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cariFile.Location = new System.Drawing.Point(556, 16);
            this.cariFile.Margin = new System.Windows.Forms.Padding(8, 8, 0, 0);
            this.cariFile.Name = "cariFile";
            this.cariFile.Size = new System.Drawing.Size(28, 25);
            this.cariFile.TabIndex = 3;
            this.cariFile.Text = "...";
            this.cariFile.UseVisualStyleBackColor = true;
            this.cariFile.Click += new System.EventHandler(this.cariFile_Click);
            // 
            // namaFileTextBox
            // 
            this.namaFileTextBox.Location = new System.Drawing.Point(223, 16);
            this.namaFileTextBox.Margin = new System.Windows.Forms.Padding(4, 8, 0, 0);
            this.namaFileTextBox.Name = "namaFileTextBox";
            this.namaFileTextBox.Size = new System.Drawing.Size(325, 25);
            this.namaFileTextBox.TabIndex = 2;
            // 
            // pilihFileLabel
            // 
            this.pilihFileLabel.AutoSize = true;
            this.pilihFileLabel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.pilihFileLabel.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.pilihFileLabel.Location = new System.Drawing.Point(8, 16);
            this.pilihFileLabel.Margin = new System.Windows.Forms.Padding(0, 8, 4, 0);
            this.pilihFileLabel.Name = "pilihFileLabel";
            this.pilihFileLabel.Size = new System.Drawing.Size(207, 20);
            this.pilihFileLabel.TabIndex = 0;
            this.pilihFileLabel.Text = "Pilih text file yang diinginkan: ";
            this.pilihFileLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.Controls.Add(this.pilihFileLabel);
            this.flowLayoutPanel.Controls.Add(this.namaFileTextBox);
            this.flowLayoutPanel.Controls.Add(this.cariFile);
            this.flowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Padding = new System.Windows.Forms.Padding(8);
            this.flowLayoutPanel.Size = new System.Drawing.Size(594, 84);
            this.flowLayoutPanel.TabIndex = 4;
            this.flowLayoutPanel.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.flowLayoutPanel_ControlAdded);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(594, 131);
            this.Controls.Add(this.flowLayoutPanel);
            this.Controls.Add(this.tampilkanGraph);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(610, 170);
            this.MinimumSize = new System.Drawing.Size(610, 170);
            this.Name = "MainForm";
            this.Text = "Scheduler";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.flowLayoutPanel.ResumeLayout(false);
            this.flowLayoutPanel.PerformLayout();
            this.ResumeLayout(false);   

        }

        #endregion
        private System.Windows.Forms.Button tampilkanGraph;
        private System.Windows.Forms.Button cariFile;
        private System.Windows.Forms.TextBox namaFileTextBox;
        private System.Windows.Forms.Label pilihFileLabel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
    }
}

