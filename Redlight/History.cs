using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Redlight
{
    public partial class History : Form
    {
        public History()
        {
        
            InitializeComponent();
            this.dataGridView.RowHeadersVisible = false;

        }

        private string seldate;
        private string seltime;

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView.Rows.Clear();
            seldate = dtTime.Text.Replace("-", "");
            seltime = hour_box.Text;
            string path = @Application.StartupPath + "\\redlight\\" + seldate;
            //  DirectoryInfo folder = new DirectoryInfo(path);
            string hour1 = seltime;
            if (!Directory.Exists(path))
            {
                return;
            }
            if (hour1 != "(全天)")
            {
                path  += "\\" + hour1;
                if (Directory.Exists(path))
                {
                    string[] num = Directory.GetDirectories(path);
                    Array.Sort(num, new CustomComparer());
                    Array.Reverse(num);
                    foreach (string son in num)
                    {
                        string[] picpath = Directory.GetFiles(son, "*");
                        Dgvadd(picpath);
                        //foreach (string pic in picpath)
                        //{
                        //    MessageBox.Show(pic.Replace(son, ""));
                        //}
                    }
                }
                else
                {
                    MessageBox.Show("此时段无违章记录");
                }
               
            }
            else
            {
                string[] files = Directory.GetDirectories(path);
                Array.Sort(files, new CustomComparer());
                Array.Reverse(files);
                foreach (string file in files)
                {
                    hour1 = file.Replace(path, "");
                    //DirectoryInfo sonfolder = new DirectoryInfo(file);
                    string[] num = Directory.GetDirectories(file);
                    Array.Sort(num, new CustomComparer());
                    Array.Reverse(num);
                    foreach (string son in num)
                    {
                        string[] picpath = Directory.GetFiles(son, "*");
                        Dgvadd(picpath);
                    }
                }
            }
        }
        private void Dgvadd(string [] path)
        {
            string pic = "";
            string pan1 = "";
            string pan2 = "";
            string pan3 = "";
            Readtxt rdt = new Readtxt();
            foreach (string p in path)
            {
                if (p.Contains(".jpg"))
                {
                    if (p.Contains("特写"))
                    {
                        pic = p;
                    }
                    else if (p.Contains("闯入1"))
                    {
                        pan1 = p;

                    }else if (p.Contains("闯入2"))
                    {
                        pan2 = p;
                    }
                    else if (p.Contains("闯入3"))
                    {
                        pan3 = p;
                    }

                }else if (p.Contains("result.txt"))
                {
                    
                    Readtxt(p,ref rdt);
                }
            }
            DataGridViewRow dr = new DataGridViewRow();
            dr.CreateCells(dataGridView);
            dr.Cells[0].Value = Image.FromFile(pic); 
            dr.Cells[1].Value = Image.FromFile(pan1);
            dr.Cells[2].Value = Image.FromFile(pan2);
            dr.Cells[3].Value = Image.FromFile(pan3);
            dr.Cells[4].Value = rdt.gender;
            dr.Cells[5].Value = rdt.age;
            dr.Cells[6].Value = rdt.time;
            dr.Height = 56;
            dataGridView.Rows.Insert(0, dr);
        }
        private void Readtxt(string path,ref Readtxt rt)//
        {
            StreamReader sr = new StreamReader(path);
            string line;
            string txt="";
            while ((line = sr.ReadLine()) != null)
            {
                txt += line;
            }
            int i = txt.IndexOf("性别:") + 3;
            int j = txt.IndexOf("年龄:");
            rt.gender = txt.Substring(i, j - i);
            i = txt.IndexOf("年龄:") + 3;
            j = txt.IndexOf("时间:");
            rt.age = txt.Substring(i, j - i);
            i = txt.IndexOf("时间:") + 3;
            j = txt.IndexOf("位置信息：");
            DateTime dt = DateTime.Now;
            dt = Convert.ToDateTime(txt.Substring(i, j - i));
            rt.time = dt.ToLongTimeString().ToString();
        }

        private void dataGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int cIndex = e.ColumnIndex;
                int row = this.dataGridView.CurrentRow.Index;
                if (cIndex != 4)
                {
                    Bigpic bp = new Bigpic();
                    bp.pictureBox1.Image = (Image)dataGridView.Rows[row].Cells[cIndex].Value;
                    bp.Show();
                }
            }
            catch { MessageBox.Show("异常!"); }
        }
    }
}
