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
            comboBox1.Text = "*.jpg";     
        }

        string path = "";
        string[] monthNames = {"empty", "01-Jan", "02-Feb", "03-March", "04-April", "05-May", "06-June", "07-July", "08-Aug", "09-Sep", "10-Oct", "11-Nov", "12-Dec"};

         private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

            textBox1.Text = "";
            richTextBox1.Text = "";            
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                path = folderBrowserDialog1.SelectedPath;
                textBox1.Text = path;               
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {

            richTextBox1.Text = "";
            if (path == "")
            {
                MessageBox.Show("Choose path first!");
            }
            else
            {
                string pattern = comboBox1.Text;
                string[] files = Directory.GetFiles(path, pattern, SearchOption.AllDirectories);

                //string numberOfFiles = files.Count().ToString();
                DialogResult messageBoxResult = MessageBox.Show("Are you sure?", "Delete confirmation", MessageBoxButtons.YesNo);

                if (messageBoxResult == DialogResult.Yes)
                {
                    foreach (string file in files)
                    {

                        DateTime creationDate = File.GetLastWriteTime(file);
                        int fileYear = creationDate.Year;
                        int fileMonth = creationDate.Month;
                        string newPath = path + "\\" + fileYear + "\\" + monthNames[fileMonth];

                        if (!Directory.Exists(newPath))
                        {
                            Directory.CreateDirectory(newPath);
                            //MessageBox.Show("Folder " + newPath + " created!");
                        }

                        string fileName = Path.GetFileName(file);
                        string newPathFull = Path.Combine(newPath, fileName);

                        if (!File.Exists(newPathFull))
                        {
                            File.Move(file, newPathFull);
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

    }
}
