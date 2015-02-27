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

namespace FileDateOrder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            richTextBox1.Text = "";                  
       }

        string path;

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                path = folderBrowserDialog1.SelectedPath;              
            }

            textBox1.Text = path;

            string[] files = Directory.GetFiles(path, "*.txt", SearchOption.AllDirectories);

            foreach (string file in files)
            {
                
                DateTime creationDate = File.GetLastWriteTime(file);
                int fileYear = creationDate.Year;
                int fileMonth = creationDate.Month;
                string newPath = path + "\\" + fileYear + "\\" + fileMonth;

                if (!Directory.Exists(newPath))
                
                {
                    Directory.CreateDirectory(newPath);
                    //MessageBox.Show("Folder " + newPath + " created!");
                }

                string fileName = Path.GetFileName(file);
                string newPathFull = Path.Combine(newPath,fileName);

                if (!File.Exists(newPathFull))
                {
                    File.Copy(file, newPathFull);
                    richTextBox1.Text += file + " Modification date: " + creationDate + " copied to: " + newPathFull + "\n";
                }                   
                
            }

            if (richTextBox1.Text == "")
            {
                MessageBox.Show("Everything is on its place!");
            }

        }
    }
}
