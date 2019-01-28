using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Configuration;
using System.Collections.Generic;

namespace Redlight
{
    public class ThreadWithState
    {
        public Faceinfotemp info;
        public Bitmap bmp;
        private Face_Info_Trace FItt = new Face_Info_Trace();

        public ThreadWithState(redcatch parrent, Bitmap getbmp)
        {
            redcatch_from = parrent;
            bmp = getbmp;

        }
        //string m;
        redcatch redcatch_from;
        int weizhi;
        int weizhi2;
        List<FaceInfo> infoList = new List<FaceInfo>();
        public void Getinfo()
        {
            try
            {
                Bitmap pic = new Bitmap(bmp.Width, bmp.Height, PixelFormat.Format24bppRgb);
                Graphics gra1 = Graphics.FromImage(pic);
                gra1.DrawImage(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height), new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
                gra1.Dispose();
                if (pic.PixelFormat != PixelFormat.Format24bppRgb)
                {
                    Bitmap temp = new Bitmap(pic.Width, pic.Height, PixelFormat.Format24bppRgb);
                    Graphics g = Graphics.FromImage(temp);
                    g.DrawImage(pic, 0, 0);
                    g.Dispose();
                    pic = temp;
                    temp.Dispose();
                    temp = null;
                }
                MemoryStream imgStream = new MemoryStream();
                pic.Save(imgStream, System.Drawing.Imaging.ImageFormat.Bmp);
                //bmp = null;
                byte[] picbyte = new byte[] { };
                picbyte = imgStream.ToArray();
                Face_detect_sdk.PFD_FACE_DETECT faceInfo1 = new Face_detect_sdk.PFD_FACE_DETECT();
                IntPtr[] kkk = new IntPtr[20];
                for (int i = 0; i < 20; i++)
                {
                    kkk[i] = Marshal.AllocHGlobal(3000);
                }
                int faceMinWidth = int.Parse(ConfigurationManager.AppSettings["facewidth"]);
                string m = ConfigurationManager.AppSettings["FACE_ROLL"];
                int s;
                switch (m)
                {
                    case "0": s = Face_detect_sdk.PFD_GetFeature(picbyte, ref faceInfo1, kkk, Face_detect_sdk.PFD_OP_FACE_ROLL_0, (short)faceMinWidth); break;
                    case "90": s = Face_detect_sdk.PFD_GetFeature(picbyte, ref faceInfo1, kkk, Face_detect_sdk.PFD_OP_FACE_ROLL_90, (short)faceMinWidth); break;
                    case "180": s = Face_detect_sdk.PFD_GetFeature(picbyte, ref faceInfo1, kkk, Face_detect_sdk.PFD_OP_FACE_ROLL_180, (short)faceMinWidth); break;
                    case "270": s = Face_detect_sdk.PFD_GetFeature(picbyte, ref faceInfo1, kkk, Face_detect_sdk.PFD_OP_FACE_ROLL_270, (short)faceMinWidth); break;
                }
                picbyte = null;
                
                if (faceInfo1.num != 0)
                {
                    //infoList.Clear();
                    for (int i = 0; i < faceInfo1.num; i++)
                    {
                        if (faceInfo1.info[i].enable == 0)
                        {
                            Faceinfotemp info1 = new Faceinfotemp();
                           // FaceInfo faino = new FaceInfo();
                           // faino.facel =
                            info1.facel = faceInfo1.info[i].faceInfo.rect_l;
                           // faino.facer =
                           info1.facer = faceInfo1.info[i].faceInfo.rect_r;
                           // faino.facet =
                            info1.facet = faceInfo1.info[i].faceInfo.rect_t;
                            //faino.faceb =
                            info1.faceb = faceInfo1.info[i].faceInfo.rect_b;
                            //infoList.Add(faino);

                            //创建新图位图
                            Bitmap bitmap = new Bitmap(info1.facer - info1.facel, info1.faceb - info1.facet);
                            //创建作图区域
                            Graphics g = Graphics.FromImage(bitmap);
                            //截取原图相应区域写入作图区
                            g.DrawImage(pic, 0, 0, new Rectangle(info1.facel, info1.facet, info1.facer - info1.facel, info1.faceb - info1.facet), GraphicsUnit.Pixel);
                            Image saveImage = Image.FromHbitmap(bitmap.GetHbitmap());
                            //保存图片
                            info1.pic = saveImage;
                            // saveImage.Dispose();
                            g.Dispose();
                            info1.age = faceInfo1.info[i].age;
                            info1.gender = faceInfo1.info[i].gen;
                            info1.taitou = faceInfo1.info[i].pitch;
                            info1.qingxie = faceInfo1.info[i].roll;
                            info1.yaotou = faceInfo1.info[i].yaw;
                            weizhi = (info1.facer - info1.facel) / 2 + info1.facel;
                            weizhi2 = (info1.faceb - info1.facet) / 2 + info1.facet;
                            if (ConfigurationManager.AppSettings["line"] == "0")
                            {
                                if (weizhi >= (float)redcatch_from.trackBar2.Value / 1920 * pic.Width)
                                {
                                    info1.pan3 = pic;
                                    info1.p = 3;
                                }
                                if (weizhi <= (float)redcatch_from.trackBar.Value / 1920 * pic.Width)
                                {
                                    info1.pan1 = pic;
                                    info1.p = 1;
                                }
                                if (weizhi > (float)redcatch_from.trackBar.Value / 1920 * pic.Width && weizhi < (float)redcatch_from.trackBar2.Value / 1920 * pic.Width)
                                {
                                    info1.pan2 = pic;
                                    info1.p = 2;
                                }
                            }
                            else
                            {
                                if (weizhi2 > (float)(1080 - redcatch_from.trackBar4.Value) / 1080 * pic.Height)
                                {
                                    info1.pan3 = pic;
                                    info1.p = 3;
                                }
                                if (weizhi2 < (float)(1080 - redcatch_from.trackBar3.Value) / 1080 * pic.Height)
                                {
                                    info1.pan1 = pic;
                                    info1.p = 1;
                                }
                                if (weizhi2 > (float)(1080 - redcatch_from.trackBar3.Value) / 1080 * pic.Height && weizhi2 < (float)(1080 - redcatch_from.trackBar4.Value) / 1080 * pic.Height)
                                {
                                    info1.pan2 = pic;
                                    info1.p = 2;
                                }
                            }
                            info1.finish = false;
                            info1.flen = faceInfo1.info[i].flen;
                            byte[] ys = new byte[3000];
                            Marshal.Copy(kkk[i], ys, 0, 3000);
                            info1.feature = ys;
                            redcatch_from.Chuli(info1, ref redcatch_from.qingli);
                        }
                    }
                   // FItt.DrawFace(infoList, redcatch_from.pictureBox1);
                }
                else
                {//释放内存
                    pic.Dispose();
                    pic = null;
                }
                for (int i1 = 0; i1 < kkk.Length; i1++)
                {
                    Marshal.FreeHGlobal(kkk[i1]);
                }
                bmp.Dispose();
                bmp = null;
                GC.Collect();
                redcatch.shibierun = false;
            }
            catch (Exception ex){ redcatch.shibierun = false; Console.WriteLine("线程出错！！"+ex); }
        }
    }
    public class Faceadd
    {
        public int facel = 0;
        public int facer = 0;
        public int facet = 0;
        public int faceb = 0;
    }

    public class Faceinfotemp
    {
        public byte[] feature;//特征值
        public int flen;  //长度
        public short flenst;
        public int age;
        public int gender;
        public int taitou;
        public int yaotou;
        public int qingxie;
        public int facel = 0;
        public int facer = 0;
        public int facet = 0;
        public int faceb = 0;
        public string coord1;
        public string coord2;
        public string coord3;
        public Image pic;
        public Image pan1;
        public Image pan2;
        public Image pan3;
        public int p;
        public bool finish = false;
    }

    public class Huakuangc
    {
        public Image yuantu;
        public int left;
        public int right;
        public int top;
        public int bottom;
    }
    public class Weizhi
    {
        public int left;
        public int right;
        public int top;
        public int bottom;

    }
    public class CustomComparer : System.Collections.IComparer
    {
        public int Compare(object x, object y)
        {
            string s1 = (string)x;
            string s2 = (string)y;
            if (s1.Length > s2.Length) return 1;
            if (s1.Length < s2.Length) return -1;
            for (int i = 0; i < s1.Length; i++)
            {
                if (s1[i] > s2[i]) return 1;
                if (s1[i] < s2[i]) return -1;
            }
            return 0;
        }
    }
    public class Readtxt
    {
        public string gender;
        public string age;
        public string data;
        public string time;
    }
}
