using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Redlight
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            int init = Face_detect_sdk.PFD_Init(1);
            if (init < 0)
            {
                DialogResult dr = MessageBox.Show("面部识别库初始化失败" + Environment.NewLine + "是否重启程序？", "失败", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                {
                    System.Windows.Forms.Application.Restart();
                }
                else if (dr == DialogResult.No)
                {
                    System.Environment.Exit(0);
                }

            }
            else
                 Application.Run(new redcatch());
        }
    }
}
