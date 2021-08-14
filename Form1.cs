using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace FolderCheck
{
    public delegate void InvokeDelegate();
    public partial class Form1 : Form
    {
        public bool threadstatuscheck;
        public List<string> listname = new List<string>();
        public List<string> listref = new List<string>();
        public List<int> listnum = new List<int>();
        public string textedit;
        public Form1()
        {
            InitializeComponent();
            string line;
            int counter = 0;
            System.IO.StreamReader file = new System.IO.StreamReader("list.txt");
            List<string> listnopars = new List<string>();
            while ((line = file.ReadLine()) != null)
            {
                listnopars.Add(line);
                counter++;
            }
            if (counter == 0)
            {
                label1.Text = "Нет папок";
                button1.Enabled = false;
                button2.Enabled = false;
            } else
            {
                foreach (var onenopars in listnopars)
                {
                    string[] subs = onenopars.Split('>');
                    listname.Add(subs[0]);
                    listref.Add(subs[1]);
                    listnum.Add(0);
                }
                counter = 0;
                Thread threadcheck = new Thread(new ThreadStart(Checker));
                threadcheck.Start();
                threadstatuscheck = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            threadstatuscheck = true;
            label1.Text = "Работает";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            threadstatuscheck = false;
            label1.Text = "Остановлено";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public void Checker()
        {
            int counter;
            string oneline;
            int listlen;
            while (true)
            {
                if (threadstatuscheck == true)
                {
                    counter = 0;
                    foreach (var oneref in listref)
                    {
                        string[] listfile = Directory.GetFiles(oneref.ToString());
                        listlen = listfile.Length;
                        if (listnum[counter]>=listlen)
                        {
                            listnum[counter] = listlen;
                        }
                        else
                        {
                            listnum[counter] = listlen;
                            oneline = listname[counter] + " - " + listlen.ToString() + " файлов" + "\n";
                            System.Windows.Forms.MessageBox.Show(oneline);
                        }
                        counter++;
                    }
                }
                else
                {
                    // NO
                }
                Thread.Sleep(30000);
            }
        }
    }
}
