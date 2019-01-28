using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace Redlight
{
    public partial class parameter : Form
    {
        public parameter()
        {
            InitializeComponent();
            //trackBar.Value = int.Parse(ConfigurationManager.AppSettings["trackBar"]);
            facewidth.Value = int.Parse(ConfigurationManager.AppSettings["facewidth"]);
            score.Value = int.Parse(ConfigurationManager.AppSettings["score"]);
            int s = int.Parse(ConfigurationManager.AppSettings["FACE_ROLL"]);
            path.Text = ConfigurationManager.AppSettings["rtsp"];
            textBox_tsy.Text =ConfigurationManager.AppSettings["speaker"];

            if (ConfigurationManager.AppSettings["usb"] == "1")
            {
                usbbutton.Checked = true;
                path.Enabled = false;
            }
            else
            {
                wangluo.Checked = true;
            }
            switch (s)
            {
                case 0: this.FACE_ROLL_0.Checked = true;break;
                case 90: this.FACE_ROLL_90.Checked = true;break;
                case 180: this.FACE_ROLL_180.Checked = true;break;
                case 270: this.FACE_ROLL_270.Checked = true;break;
            }
            duankouhao.Text= ConfigurationManager.AppSettings["duankou"];
            btl.Text = ConfigurationManager.AppSettings["botelv"];
        }

        private void save_Click(object sender, EventArgs e)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //更新配置文件
            config.AppSettings.Settings["facewidth"].Value = facewidth.Value.ToString();
            config.AppSettings.Settings["score"].Value = score.Value.ToString();
            // int s = int.Parse(config.AppSettings.Settings["FACE_ROLL"].Value);
            string s = "0";
            if (this.FACE_ROLL_0.Checked) s = "0";
            if (this.FACE_ROLL_90.Checked) s = "90";
            if (this.FACE_ROLL_180.Checked) s = "180";
            if (this.FACE_ROLL_270.Checked) s = "270";
            config.AppSettings.Settings["duankou"].Value = duankouhao.Text;
            config.AppSettings.Settings["botelv"].Value = btl.Text;
            if (usbbutton.Checked)
            {
                config.AppSettings.Settings["usb"].Value = "1";
            }
            else
            {
                config.AppSettings.Settings["usb"].Value = "0";
            }
            config.AppSettings.Settings["rtsp"].Value =path.Text;
            config.AppSettings.Settings["rtsp"].Value = path.Text;
            config.AppSettings.Settings["FACE_ROLL"].Value =s;
            config.AppSettings.Settings["speaker"].Value = textBox_tsy.Text;
            config.Save(ConfigurationSaveMode.Modified);

            //强制重新载入配置文件的ConnectionStrings配置节   
            ConfigurationManager.RefreshSection("appSettings");
            MessageBox.Show("保存成功！");
        }

        private void close_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("关闭前请确认已保存更改！" + Environment.NewLine + "是否关闭参数设置界面？", "关闭", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void usbbutton_CheckedChanged(object sender, EventArgs e)
        {
            if(usbbutton.Checked)
            {
                path.Enabled = false;
            }
            else
            {
                path.Enabled = true;
            }
        }
    }
}
