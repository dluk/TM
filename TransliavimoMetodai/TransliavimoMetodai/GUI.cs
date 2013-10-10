using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using LeksinisAnalizatorius;

namespace TransliavimoMetodai
{
    public partial class GUI : Form
    {
        public GUI()
        {
            InitializeComponent();
            openFileDialog1.Multiselect = false;
            string[] lines = File.ReadAllLines(Path.GetFullPath("../../")+"/test.txt", Encoding.UTF8);
            richTextBox1.Text = String.Join("\r\n", lines);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            if (Path.GetExtension(openFileDialog1.FileName) == ".txt")
            {
                string[] lines = File.ReadAllLines(openFileDialog1.FileName, Encoding.UTF8);
                richTextBox1.Text = String.Join("\r\n", lines);
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = String.Empty;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = "";
            var LA = new Analizatorius(richTextBox1.Text);
            richTextBox2.Text = LA.KitaLitera();
        }
    }
}
