﻿namespace TransliavimoMetodai
{
    partial class GUI
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AnalyzeStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.VarduLentelesLaukas = new System.Windows.Forms.RichTextBox();
            this.programosLaukas = new System.Windows.Forms.RichTextBox();
            this.SinAnalText = new System.Windows.Forms.RichTextBox();
            this.analyzeSyntaxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.AnalyzeStripMenuItem,
            this.analyzeSyntaxToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1345, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.closeToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(120, 24);
            this.openToolStripMenuItem.Text = "Open..";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(120, 24);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(117, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(120, 24);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // AnalyzeStripMenuItem
            // 
            this.AnalyzeStripMenuItem.Name = "AnalyzeStripMenuItem";
            this.AnalyzeStripMenuItem.Size = new System.Drawing.Size(99, 24);
            this.AnalyzeStripMenuItem.Text = "Analyze Lex";
            this.AnalyzeStripMenuItem.Click += new System.EventHandler(this.toolsToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "TXT files|*.txt";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.SinAnalText);
            this.panel1.Controls.Add(this.VarduLentelesLaukas);
            this.panel1.Controls.Add(this.programosLaukas);
            this.panel1.Location = new System.Drawing.Point(0, 31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1347, 663);
            this.panel1.TabIndex = 1;
            // 
            // VarduLentelesLaukas
            // 
            this.VarduLentelesLaukas.Location = new System.Drawing.Point(472, 3);
            this.VarduLentelesLaukas.Name = "VarduLentelesLaukas";
            this.VarduLentelesLaukas.Size = new System.Drawing.Size(364, 657);
            this.VarduLentelesLaukas.TabIndex = 1;
            this.VarduLentelesLaukas.Text = "";
            this.VarduLentelesLaukas.ZoomFactor = 1.5F;
            // 
            // programosLaukas
            // 
            this.programosLaukas.CausesValidation = false;
            this.programosLaukas.DetectUrls = false;
            this.programosLaukas.Location = new System.Drawing.Point(0, 3);
            this.programosLaukas.Name = "programosLaukas";
            this.programosLaukas.Size = new System.Drawing.Size(466, 660);
            this.programosLaukas.TabIndex = 0;
            this.programosLaukas.Text = "";
            this.programosLaukas.ZoomFactor = 1.5F;
            this.programosLaukas.KeyUp += new System.Windows.Forms.KeyEventHandler(this.richTextBox1_KeyUp);
            // 
            // SinAnalText
            // 
            this.SinAnalText.Location = new System.Drawing.Point(842, 3);
            this.SinAnalText.Name = "SinAnalText";
            this.SinAnalText.Size = new System.Drawing.Size(493, 657);
            this.SinAnalText.TabIndex = 2;
            this.SinAnalText.Text = "";
            // 
            // analyzeSyntaxToolStripMenuItem
            // 
            this.analyzeSyntaxToolStripMenuItem.Name = "analyzeSyntaxToolStripMenuItem";
            this.analyzeSyntaxToolStripMenuItem.Size = new System.Drawing.Size(120, 24);
            this.analyzeSyntaxToolStripMenuItem.Text = "Analyze Syntax";
            this.analyzeSyntaxToolStripMenuItem.Click += new System.EventHandler(this.analyzeSyntaxToolStripMenuItem_Click);
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1345, 701);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "GUI";
            this.Text = "Leksinis analizatorius";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RichTextBox programosLaukas;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AnalyzeStripMenuItem;
        private System.Windows.Forms.RichTextBox VarduLentelesLaukas;
        private System.Windows.Forms.ToolStripMenuItem analyzeSyntaxToolStripMenuItem;
        private System.Windows.Forms.RichTextBox SinAnalText;


    }
}