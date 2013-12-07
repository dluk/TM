using System;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using System.IO;
using TM.LeksinisAnalizatorius;

namespace TransliavimoMetodai
{
    public partial class GUI : Form
    {
        public GUI()
        {
            InitializeComponent();
            openFileDialog1.Multiselect = false;
            string[] lines = File.ReadAllLines(Path.GetFullPath("../../")+"/test.txt", Encoding.UTF8);
            programosLaukas.Text = String.Join("\r\n", lines);
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
                programosLaukas.Text = String.Join("\r\n", lines);
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            programosLaukas.Text = String.Empty;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VarduLentelesLaukas.Text = "";
            var LA = new LeksinisAnalizatorius(programosLaukas.Text);
            if (LA.Analizuoti())
            {
                VarduLentelesLaukas.Text = LA.ToString();
            }
            
        }

        private void richTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.F5)
                toolsToolStripMenuItem_Click(this, new EventArgs());
        }
    }
}
