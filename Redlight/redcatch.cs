using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Emgu.CV;
using System.IO;
using System.Threading;
using System.IO.Ports;
using System.Configuration;
using System.Runtime.InteropServices;
using System.Speech.Synthesis;


namespace Redlight
{
    public partial class redcatch : Form
    {

        public redcatch()
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            //设置datagridview 添加行隐藏
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            this.dataGridView2.RowHeadersVisible = false;
            // this.MaximizeBox = false;
            //this.MaximizedBounds = Screen.PrimaryScreen.WorkingArea;
            Load_form();
        }

        public struct CopyDataStruct
        {
            public IntPtr dwData;
            public int cbData;

            [MarshalAs(UnmanagedType.LPStr)]
            public string lpData;
        }
        private void Load_form()
        {
            //from_rtsp.Text = 
            trackBar.Value = int.Parse(ConfigurationManager.AppSettings["trackBar"]);
            trackBar2.Value = int.Parse(ConfigurationManager.AppSettings["trackBar2"]);
            trackBar3.Value = int.Parse(ConfigurationManager.AppSettings["trackBar3"]);
            trackBar4.Value = int.Parse(ConfigurationManager.AppSettings["trackBar4"]);
            if (!System.IO.Directory.Exists(@Application.StartupPath + "\\redlight\\" + strYMD + "\\" + DateTime.Now.Hour.ToString()))
            {
                System.IO.Directory.CreateDirectory(@Application.StartupPath + "\\redlight\\" + strYMD + "\\" + DateTime.Now.Hour.ToString());//不存在就创建文件夹
            }
            string[] txtFiles = Directory.GetFiles(@Application.StartupPath + "\\redlight\\" + strYMD, "*.txt", SearchOption.AllDirectories);
            if (ConfigurationManager.AppSettings["line"] == "0") radioButton1.Checked = true; else radioButton2.Checked = true;
            red_renshu.Text = txtFiles.Length.ToString();
            //创建识别对象
            tws = new ThreadWithState(this, nowbit);
        }

        //private const int WM_COPYDATA = 0x004A;
        ////接收消息方法  
        //protected override void WndProc(ref System.Windows.Forms.Message e)
        //{
        //    if (e.Msg == WM_COPYDATA)
        //    {
        //        CopyDataStruct cds = (CopyDataStruct)e.GetLParam(typeof(CopyDataStruct));
        //        //textBox1.Text = cds.lpData.ToString();  //将文本信息显示到文本框  
        //        MessageBox.Show(cds.lpData);
        //    }
        //    base.WndProc(ref e);
        //}
        public string strYMD = DateTime.Now.ToString("yyyyMMdd");
        private VideoCapture _capture = null;
        public bool HasReset { get; set; }

        private int rate = 0;
        private bool _isProcessing = false;
        public static bool shibierun = false;
        private Mat _frame = new Mat();
        // private Dictionary<string, frameInfo> frameDicts = new Dictionary<string, frameInfo>();

        private Bitmap bmp;
        Faceadd fd = new Faceadd();
        SpeechSynthesizer speak = new SpeechSynthesizer();

        private void from_button_Click(object sender, EventArgs e)
        {
            try
            {

                if (!shibierun)
                {
                    shibierun = false;
                    if (ConfigurationManager.AppSettings["usb"] == "1")
                    {
                        _capture = new VideoCapture(0);
                      
                    }
                    else
                    {
                        string rts = ConfigurationManager.AppSettings["rtsp"];
                        _capture = new VideoCapture(rts);
                       
                    }
                    _capture.ImageGrabbed += ProcessFrame;
                    _capture.Start();
                    from_button.BackgroundImage = Redlight.Properties.Resources.stop;
                }
                else
                {
                    _capture.Stop();
                    pictureBox1.Image = null;
                    pictureBox1.Refresh();
                    from_button.BackgroundImage = Redlight.Properties.Resources.start;
                    pictureBox1.Image = Redlight.Properties.Resources.jingcha;
                    shibierun = false;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private Face_Info_Trace FIT = new Face_Info_Trace();
        public static int picwidth;
        public static int pichight;
        Huakuangc h = new Huakuangc();
        static Bitmap nowbit;
        ThreadWithState tws;

        
        private void ProcessFrame(object sender, EventArgs arg)
        {
            rate++;
            _isProcessing = true;
            try
            {
                if (_capture != null && _capture.Ptr != IntPtr.Zero)
                {
                    picwidth = _frame.Width;
                    pichight = _frame.Height;
                    _capture.Retrieve(_frame, 0);
                    if (shibierun == false && _redlight.Checked)
                    {
                        if (usb_ok.Checked)
                        {
                            if (usbok)
                            {

                                Shibie(_frame.Bitmap);
                                FIT.FaceRecgAndDrawAsync(_frame.Bitmap, pictureBox1);
                            }
                        }
                        else
                        {
                            Shibie(_frame.Bitmap);
                            FIT.FaceRecgAndDrawAsync(_frame.Bitmap, pictureBox1);
                        }

                    }
                    FIT.DrawFace(FIT.GetCurrentFaceList, pictureBox1);
                    pictureBox1.Image = _frame.Bitmap;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString()); shibierun = true;
            }
            _isProcessing = false;
        }

        private void Shibie(Bitmap nowbit)
        {
            shibierun = true;
            //bt = nowbit.Clone(new Rectangle(0, 0, nowbit.Width, nowbit.Height), nowbit.PixelFormat);
            tws.bmp = nowbit;
            //tws.info1 = info1;
            Thread t = new Thread(new ThreadStart(tws.Getinfo))
            {
                IsBackground = true,
                Priority = ThreadPriority.BelowNormal
            };
            t.Start();
        }
        Huakuangc hkuang1 = new Huakuangc();
        Huakuangc hkuang2 = new Huakuangc();
        Huakuangc hkuang3 = new Huakuangc();
       public void Huakuang2(Weizhi wz)
        {
            for (int i = 0; i < 30; i++)
            {
                Graphics g3 = Graphics.FromImage(pictureBox1.Image);
                Pen pen1 = new Pen(Color.Green, 4);
                if (pictureBox1.InvokeRequired)
                {
                    Action<Weizhi> actionDelegate = (x) => {
                        g3.DrawRectangle(pen1, x.left, x.top, x.right - x.left, x.bottom - x.top);
                        g3.Dispose();
                    };
                    pictureBox1.Invoke(actionDelegate, wz);
                }
            }
        }

        public void Huakuang(Huakuangc hk)
        {
            Graphics g2 = Graphics.FromImage(hk.yuantu);
            Pen pen1 = new Pen(Color.Green, 6);
            Pen pen2 = new Pen(Color.Red, 10);
            Pen pen3 = new Pen(Color.Blue, 10);
            if (ConfigurationManager.AppSettings["line"] == "0")
            {
                g2.DrawLine(pen2, (float)trackBar.Value / 1920 * picwidth, 0, (float)trackBar.Value / 1920 * picwidth, 1920);
                g2.DrawLine(pen3, (float)trackBar2.Value / 1920 * picwidth, 0, (float)trackBar2.Value / 1920 * picwidth, 1920);
            }
            else
            {
                g2.DrawLine(pen1, 0, (float)(1080 - trackBar3.Value) / 1080 * pichight, 1080, (float)(1080 - trackBar3.Value) / 1080 * pichight);
                g2.DrawLine(pen2, 0, (float)(1080 - trackBar4.Value) / 1080 * pichight, 1080, (float)(1080 - trackBar4.Value) / 1080 * pichight);
            }

            g2.DrawRectangle(pen1, hk.left, hk.top, hk.right - hk.left, hk.bottom - hk.top);
            g2.Dispose();
        }

        public void Fenge(Image a,string b,ref Huakuangc hkuang1 )
        {
            hkuang1.yuantu =a;
            string m1 = b;
            string[] s1 = m1.Split(',');//以 , 将文本分割
            hkuang1.left = int.Parse(s1[0]);
            hkuang1.right = int.Parse(s1[2]);
            hkuang1.top = int.Parse(s1[1]);
            hkuang1.bottom = int.Parse(s1[3]);
        }

        public void Dataadd(Faceinfotemp datainfo)
        {
            speaker();
            DateTime currentDT = DateTime.Now;
            string se;
            se = datainfo.gender == 0 ? "男" : "女";
            Fenge(datainfo.pan1,datainfo.coord1, ref hkuang1);
            Huakuang(hkuang1);
            Fenge(datainfo.pan2, datainfo.coord2, ref hkuang2);
            Huakuang(hkuang2);
            Fenge(datainfo.pan3, datainfo.coord3, ref hkuang3);
            Huakuang(hkuang3);
            Console.WriteLine("Dataadd++");
            DataGridViewRow dr = new DataGridViewRow();
            dr.CreateCells(dataGridView2);
            //dr.Cells["red_info"].Value = "";
            dr.Cells[0].Value = datainfo.pic;
            dr.Cells[1].Value = datainfo.pan1;
            dr.Cells[2].Value = datainfo.pan2;
            dr.Cells[3].Value = datainfo.pan3;
            dr.Cells[4].Value = "性别:"+ se +" 年龄:"+datainfo.age +" "+ Environment.NewLine + "时间:"+ DateTime.Now.ToLocalTime().ToString();
            dr.Height = 56;
            dataGridView2.Rows.Insert(0, dr);
            if (!System.IO.Directory.Exists(@Application.StartupPath + "\\redlight\\" + strYMD + "\\" + DateTime.Now.Hour.ToString()))
            {
                System.IO.Directory.CreateDirectory(@Application.StartupPath + "\\redlight\\" + strYMD + "\\" + DateTime.Now.Hour.ToString());//不存在就创建文件夹
            }
            string saveFolder =@Application.StartupPath + "\\redlight\\" + strYMD + "\\" + DateTime.Now.Hour.ToString().ToString();
            int i = 0;
            while (Directory.Exists(saveFolder + "\\" + i.ToString()))
            {
                i++;
            }
            saveFolder = saveFolder + "\\" + i.ToString();
            Directory.CreateDirectory(saveFolder);
            datainfo.pic.Save(saveFolder + "\\" + "特写_" + DateTime.Now.ToString("HH-mm-ss") + ".jpg");
            datainfo.pan1.Save(saveFolder + "\\" + "闯入1_" + DateTime.Now.ToString("HH-mm-ss") + ".jpg");
            datainfo.pan2.Save(saveFolder + "\\" + "闯入2_" + DateTime.Now.ToString("HH-mm-ss") + ".jpg");
            datainfo.pan3.Save(saveFolder + "\\" + "闯入3_" + DateTime.Now.ToString("HH-mm-ss") + ".jpg");
            if (!File.Exists(saveFolder + "\\result.txt"))
            {
                File.Create(saveFolder + "\\result.txt").Close();
            }
            FileStream stream = new FileStream(saveFolder + "\\result.txt", FileMode.Append);
            StreamWriter log = new StreamWriter(stream);
            string str = dr.Cells[4].Value.ToString() + " 位置信息：一 " + datainfo.coord1+";" + "二 "+datainfo.coord2 + ";" + "三 " + datainfo.coord3;
            log.WriteLine(str);
            log.Close();
            stream.Close();
            FileStream featrueStream = new FileStream(saveFolder + "\\feature.o", FileMode.Append);
            BinaryWriter featureWriter = new BinaryWriter(featrueStream);
            featureWriter.Write(datainfo.feature);
            featureWriter.Close();
            featrueStream.Close();
        }
        public delegate void MyInvoke(Faceinfotemp inf);
       
        List<Faceinfotemp> peopList = new List<Faceinfotemp>();
        public bool qingli = false;
        public void Chuli(Faceinfotemp people, ref bool qingli)
        {
            if (peopList.Count == 0)
            {
                peopList.Add(people);
                if (people.p == 1)
                {
                    peopList[0].pan1 = people.pan1;
                    peopList[0].coord1 = people.facel + "," + people.facet + "," + people.facer + "," + people.faceb;
                }
                if (people.p == 2)
                {
                    peopList[0].pan2 = people.pan2;
                    peopList[0].coord2 = people.facel + "," + people.facet + "," + people.facer + "," + people.faceb;
                }
                if (people.p == 3)
                {
                    peopList[0].pan3 = people.pan3;
                    peopList[0].coord3 = people.facel + "," + people.facet + "," + people.facer + "," + people.faceb;
                }
                Console.WriteLine("第一张");
            }
            else
            {
                int m = 0;
                for (int i = 0; i < peopList.Count; i++)
                {
                    int aa = Face_detect_sdk.PFD_FeatureMatching(3000, people.feature, 3000, peopList[i].feature /*(short)people.flen, new byte[people.flen], (short)peopList[i].flen, new byte[peopList[i].flen]*/);
                    if (aa > int.Parse(ConfigurationManager.AppSettings["score"]))
                    {
                        if (peopList[i].finish == false)
                        {
                            if (people.p == 1)
                            {
                                peopList[i].pan1 = people.pan1;
                                peopList[i].coord1 = people.facel + "," + people.facet + "," + people.facer + "," + people.faceb;
                            }
                            if (people.p == 2)
                            {
                                peopList[i].pan2 = people.pan2;
                                peopList[i].coord2 = people.facel + "," + people.facet + "," + people.facer + "," + people.faceb;
                            }
                            if (people.p == 3)
                            {
                                peopList[i].pan3 = people.pan3;
                                peopList[i].coord3 = people.facel + "," + people.facet + "," + people.facer + "," + people.faceb;
                            }
                            if (Math.Abs(people.yaotou) < Math.Abs(peopList[i].yaotou))
                            {
                                peopList[i].pic = people.pic;
                                peopList[i].feature = people.feature;
                            }
                            else
                            {
                                Console.WriteLine("状态不佳" + "新信息指数" + people.yaotou + "原信息位置" + peopList[i].yaotou);
                            }
                            if (peopList[i].coord1 != null && peopList[i].coord2 != null && peopList[i].coord3 != null)
                            {
                                Console.WriteLine("信息补全");
                                if (dataGridView2.InvokeRequired && peopList[i].finish == false)
                                {// 当一个控件的InvokeRequired属性值为真时，说明有一个创建它以外的线程想访问它
                                    Action<Faceinfotemp> actionDelegate = (x) =>
                                    { Dataadd(x); };
                                    this.dataGridView2.Invoke(actionDelegate, peopList[i]);
                                }
                                else
                                {
                                    Dataadd(peopList[i]);
                                }
                                peopList[i].finish = true;
                                // peopList[i] = null;
                                people.pan1 = null;
                                people.pan2 = null;
                                people.pan3 = null;
                                people.pic = null;
                                int a = 1;
                                if (red_renshu.InvokeRequired && peopList[i].finish == true)
                                {
                                    Action<int> actionDelegate = (x) =>{
                                    red_renshu.Text = (int.Parse(red_renshu.Text) + x).ToString(); };
                                    this.red_renshu.Invoke(actionDelegate, a);
                                }
                                else
                                {
                                    red_renshu.Text = (int.Parse(red_renshu.Text) + 1).ToString();
                                }
                                Console.WriteLine("显示完成");
                            }
                            else
                            {
                                if (peopList[i].pan1 == null) Console.WriteLine("信息不全   无全景图1"); else Console.WriteLine("有全景图1");
                                if (peopList[i].pan2 == null) Console.WriteLine("信息不全   无全景图2"); else Console.WriteLine("有全景图2");
                                if (peopList[i].pan3 == null) Console.WriteLine("信息不全   无全景图3"); else Console.WriteLine("有全景图3");
                            }
                        }
                    }
                    else /*Console.WriteLine("此人不对比");*/ m++;
                }
                if (m == peopList.Count)
                {
                    peopList.Add(people);
                    Console.WriteLine("新增记录" + m.ToString());
                    m = 0;
                }
            }
            qingli = true;
        }
        private SerialPort com;
        private object config;

        private void Form1_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void Save_Click(object sender, EventArgs e)
        {
            //读取配置文件
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //更新配置文件
            //
            config.AppSettings.Settings["trackBar"].Value = trackBar.Value.ToString();
            config.AppSettings.Settings["trackBar2"].Value = trackBar2.Value.ToString();
            config.AppSettings.Settings["trackBar3"].Value = trackBar3.Value.ToString();
            config.AppSettings.Settings["trackBar4"].Value = trackBar4.Value.ToString();
            if (radioButton1.Checked) config.AppSettings.Settings["line"].Value = "0"; else config.AppSettings.Settings["line"].Value = "1";
            config.Save(ConfigurationSaveMode.Modified);
            //强制重新载入配置文件的ConnectionStrings配置节   
            ConfigurationManager.RefreshSection("appSettings");
            MessageBox.Show("保存成功！");
        }

        private void dataGridView2_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
        try { 
            int cIndex = e.ColumnIndex;
            int row = this.dataGridView2.CurrentRow.Index;
            if (cIndex !=4)
            { 
                Bigpic bp = new Bigpic();
                bp.pictureBox1.Image = (Image)dataGridView2.Rows[row].Cells[cIndex].Value;
                bp.Show();
            }
            }
            catch { MessageBox.Show("异常!"); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            parameter par = new parameter();
            par.ShowDialog();
           // par.Show();
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.CheckState == CheckState.Checked)
            {
                label1.Visible = label3.Visible = trackBar.Visible = trackBar2.Visible = save.Visible = radioButton1.Visible
                    = radioButton2.Visible = label5.Visible = label6.Visible = trackBar3.Visible = trackBar4.Visible =  true;
                radioButton1.Enabled = true;
                radioButton2.Enabled = true;
                if (radioButton1.Checked)
                {
                    label1.Enabled = label3.Enabled = trackBar.Enabled = trackBar2.Enabled = true;
                    label5.Enabled = label6.Enabled = trackBar3.Enabled = trackBar4.Enabled = false;
                }
                if (radioButton2.Checked)
                {
                    label1.Enabled = label3.Enabled = trackBar.Enabled = trackBar2.Enabled = false;
                    label5.Enabled = label6.Enabled = trackBar3.Enabled = trackBar4.Enabled = true;
                    //竖排的控件变为可选状态
                }
            }
            else
            {
                label1.Visible = label3.Visible = trackBar.Visible = trackBar2.Visible = save.Visible = radioButton1.Visible
                    = radioButton2.Visible = label5.Visible = label6.Visible = trackBar3.Visible = trackBar4.Visible = false;
                label1.Enabled = label3.Enabled = trackBar.Enabled = trackBar2.Enabled = false;
                label5.Enabled = label6.Enabled = trackBar3.Enabled = trackBar4.Enabled = false;
                radioButton1.Enabled = false;
                radioButton2.Enabled = false;
            }
            
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                label1.Enabled = label3.Enabled = trackBar.Enabled = trackBar2.Enabled = true;
                label5.Enabled = label6.Enabled = trackBar3.Enabled = trackBar4.Enabled = false;
                //竖排的控件变为不可选状态
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                //竖排的控件变为可选状态
                label5.Enabled = label6.Enabled = trackBar3.Enabled = trackBar4.Enabled = true;
                label1.Enabled = label3.Enabled = trackBar.Enabled = trackBar2.Enabled = false;
            }
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
           string 路径 = @Application.StartupPath + "\\redlight\\" + strYMD;
            System.Diagnostics.Process.Start(路径);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            shibierun = true;
            Face_detect_sdk.PFD_Exit();
            peopList.Clear();
            GC.Collect();

        }


        private void _redlight_CheckedChanged(object sender, EventArgs e)
        {
            if (_redlight.Checked)
            {

            }else
            {
                peopList.Clear();
            }
        }
        private bool usbok = false;
        private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string m = serialPort.ReadLine();
                string str1 = m.Substring(0,2);
                if (str1 == " R")
                {
                    usbok = true;
                    if (deng.InvokeRequired)
                    {// 当一个控件的InvokeRequired属性值为真时，说明有一个创建它以外的线程想访问它
                        Action<string> actionDelegate = (x) =>
                        {
                            deng.ForeColor = Color.Red;
                        };
                        this.dataGridView2.Invoke(actionDelegate, str1);
                    }
                    else
                    {
                        deng.ForeColor = Color.Red;
                    }
                    Console.WriteLine("红灯亮了");
                }
                else
                {
                    usbok = false;
                    if (deng.InvokeRequired)
                    {// 当一个控件的InvokeRequired属性值为真时，说明有一个创建它以外的线程想访问它
                        Action<string> actionDelegate = (x) =>
                        {
                            deng.ForeColor = Color.LawnGreen;
                        };
                        this.dataGridView2.Invoke(actionDelegate, str1);
                    }
                    else
                    {
                        deng.ForeColor = Color.LawnGreen;
                    }
                }
            }
            catch
            {
                usbok = false;
                if (deng.InvokeRequired)
                {// 当一个控件的InvokeRequired属性值为真时，说明有一个创建它以外的线程想访问它
                    Action<string> actionDelegate = (x) =>
                    {
                        deng.ForeColor = Color.LawnGreen;
                    };
                    this.dataGridView2.Invoke(actionDelegate, null);
                }
                else
                {
                    deng.ForeColor = Color.LawnGreen;
                }
            }
        }
        
        private bool light=false;
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (light == false)
                {
                    serialPort.PortName = ConfigurationManager.AppSettings["duankou"];
                    serialPort.BaudRate = int.Parse(ConfigurationManager.AppSettings["botelv"]);
                    serialPort.Open();
                    light = true;
                    button4.BackgroundImage = Redlight.Properties.Resources._5_traffic_light;
                }
                else
                {
                    light = false;
                    serialPort.Close();
                    button4.BackgroundImage = Redlight.Properties.Resources._66_Traffic_Light;
                    // button4.Text = "连接红绿灯";
                }
            }
            catch (Exception ex)
            {
                light = false;
                MessageBox.Show(e.ToString());
            }

            
        }


        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            History his = new History();
            his.Show();
        }   
  
        private void button5_MouseMove(object sender, MouseEventArgs e)
        {
            button5.BackColor = System.Drawing.Color.Gray;
        }
        private void button5_MouseLeave(object sender, EventArgs e)
        {
            button5.BackColor = System.Drawing.Color.White;
        }

        private void button4_MouseMove(object sender, MouseEventArgs e)
        {
            button4.BackColor = System.Drawing.Color.Gray;
        }

        private void button4_MouseLeave(object sender, EventArgs e)
        {
            button4.BackColor = System.Drawing.Color.White;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                if (_capture != null)
                {
                    //画线
                    Graphics g = Graphics.FromImage(_frame.Bitmap);
                    Pen pen1 = new Pen(Color.Red, 3);
                    Pen pen2 = new Pen(Color.Blue, 3);
                    if (radioButton1.Checked)
                    {
                        g.DrawLine(pen1, (float)trackBar.Value / 1920 * picwidth, 0, (float)trackBar.Value / 1920 * picwidth, 1920);
                        g.DrawLine(pen2, (float)trackBar2.Value / 1920 * picwidth, 0, (float)trackBar2.Value / 1920 * picwidth, 1920);
                    }
                    else
                    {
                        g.DrawLine(pen1, 0, (float)(1080 - trackBar3.Value) / 1080 * pichight, 1080, (float)(1080 - trackBar3.Value) / 1080 * pichight);
                        g.DrawLine(pen2, 0, (float)(1080 - trackBar4.Value) / 1080 * pichight, 1080, (float)(1080 - trackBar4.Value) / 1080 * pichight);
                    }
                    g.Dispose();
                }
            }
            catch 
            {

            }
           
        }

        private void dataGridView2_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show(e.ColumnIndex.ToString()+"---"+e.RowIndex.ToString(), "系统提示",
            //        MessageBoxButtons.OK, MessageBoxIcon.Warning,
            //        MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
        }

        private void speaker()
        {
            speak.SpeakAsync(ConfigurationManager.AppSettings["speaker"]);
        }
    }
}

