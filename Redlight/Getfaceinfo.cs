using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Redlight //工程名
{
    public class Face_Info_Trace
    {
        /********************************可更改参数*********************************************************/
        public double leave_time_interval = 300.00; //同一个人间隔时间（单位:秒），若超过此值，人数将增加1
        public int faceMinWidth = 30;//为图中可识别人脸最小宽度（像素单位）
        public bool Position = true; // 若为true，则追踪函数中先根据位置判断是否为同一人，再进行特征值对比；否则只进行特征值对比
        public int Drawfacewidth = 3; //画框时 线条的宽度 为像素单位
        /***************************************************************************************************/
        private IntPtr[] _currentFaceFeature = new IntPtr[20];//存放特征值，最多识别20张人脸
        public Face_Info_Trace()//申请特征值存储位置
        {
            //初始化数据
            for (int i = 0; i < 20; i++)
            {
                _currentFaceFeature[i] = Marshal.AllocHGlobal(3000);
            }
        }
         ~Face_Info_Trace()//析构函数
        {
            //释放资源
            for (int i = 0; i < _currentFaceFeature.Length; i++)
            {
                Marshal.FreeHGlobal(_currentFaceFeature[i]);
            }
        }
        private Bitmap _currentFaceBitmp; // 要识别的图片 
        public bool _RecgRunningFlag;  //委托线程进行状态标志
        private int _statisticsFaceCount = 0;//统计的人脸数量
        public  int StatisticsFaceCount//统计的人脸数量get接口
        {
            get
            {
                return _statisticsFaceCount;
            }
        }
        private TimeSpan times; 
        private List<FaceInfo> _faceRecgResult;//识别输出结果，此列表即识别结果（或识别追踪结果）
        public List<FaceInfo> GetFaceRecgResult //_faceRecgResult 对外接口 
        {
            get
            {
                return _faceRecgResult;
            }
        }
        private List<FaceInfo> _currentFaceList = new List<FaceInfo>();//只存储最后一次识别的结果 （画框等函数使用）
        private List<FaceInfo> _faceTracingList = new List<FaceInfo>();//存储最终列表（经过对比去重后的列表）
        public  List<FaceInfo> GetCurrentFaceList//_currentFaceList 对外接口
        {
            get
            {
                return _currentFaceList;
            }
        }
        public List<FaceInfo> FaceTracingList//_FaceTracingList 对外接口
        {
            get
            {
                return _faceTracingList;
            }
        }
        public int CurrentFaceCount//现场人数get接口
        {
            get
            {
                return _currentFaceList.Count;
            }
        }
        public int TracingFaceCount//追踪人数get接口
        {
            get
            {
                return _faceTracingList.Count;
            }
        }
        protected byte[] Picformat()//图片格式转换
        {
            byte[] picby;
            Bitmap pic = new Bitmap(_currentFaceBitmp.Width, _currentFaceBitmp.Height, PixelFormat.Format24bppRgb);
            Graphics gra1 = Graphics.FromImage(pic);
            gra1.DrawImage(_currentFaceBitmp, new Rectangle(0, 0, _currentFaceBitmp.Width, _currentFaceBitmp.Height), new Rectangle(0, 0, _currentFaceBitmp.Width, _currentFaceBitmp.Height), GraphicsUnit.Pixel);
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
           // byte[] picbyte = new byte[] { };
            picby = imgStream.ToArray();
            imgStream.Close();
            imgStream.Dispose();
            imgStream = null;
            pic.Dispose();
            pic = null;
            return picby;
        }
        public List<FaceInfo> FaceRecg(Bitmap bmp)//识别函数  返回更新的_currentFaceList
        {
            _currentFaceBitmp = bmp;
            byte[] facePicByte = Picformat();//格式转换
            
            DateTime Dtime = DateTime.Now;
            Face_detect_sdk.PFD_FACE_DETECT faceInfo = new Face_detect_sdk.PFD_FACE_DETECT();
            int s = Face_detect_sdk.PFD_GetFeature(facePicByte, ref faceInfo, _currentFaceFeature, Face_detect_sdk.PFD_OP_FACE_ROLL_0, (short)faceMinWidth);
            facePicByte = null;
            _currentFaceList.Clear();
            if (faceInfo.num != 0)
            {
                for (int i = 0; i < faceInfo.num; i++)
                {
                    if (faceInfo.info[i].enable == 0)
                    {
                        byte[] f = new byte[3000];
                        Marshal.Copy(_currentFaceFeature[i], f, 0, 3000);
                        FaceInfo info1 = new FaceInfo  //此处将获取到的人脸信息导出，可增加更多信息
                        {
                            facel = faceInfo.info[i].faceInfo.rect_l,
                            facer = faceInfo.info[i].faceInfo.rect_r,
                            facet = faceInfo.info[i].faceInfo.rect_t,
                            faceb = faceInfo.info[i].faceInfo.rect_b,
                            age = faceInfo.info[i].age,
                            sex = faceInfo.info[i].gen == 0 ? "男" : "女",
                            qingxie = faceInfo.info[i].roll,
                            yaotou = faceInfo.info[i].yaw,
                            taitou = faceInfo.info[i].pitch,
                            flen = faceInfo.info[i].flen,
                            time = Dtime,
                            feature = f
                        };
                        _currentFaceList.Add(info1);
                    }
                }
            }
            else
            {
                _statisticsFaceCount+= _currentFaceList.Count;
                return _currentFaceList;
            }
            
            GC.Collect();
            _statisticsFaceCount += _currentFaceList.Count;
            return _currentFaceList;
        }
        public List<FaceInfo> FaceTrace(List<FaceInfo> Traceinglist /*正在追踪的列表*/, List<FaceInfo> Currentlist/*当前表*/)
        {
            if (Traceinglist.Count == 0)
            {
                FaceTracingList.AddRange(Currentlist);
                return FaceTracingList;
            }else
            {
                int num = 0;
                if (Position == true)
                {
                    foreach (FaceInfo Current in Currentlist)//循环当前列表
                    {
                        foreach (FaceInfo Tracking in Traceinglist)//循环追踪列表
                        {
                            if (TrackPositionJudge(Tracking, Current))//位置对比
                            {   /*更新位置*/
                                Tracking.facel = Current.facel;
                                Tracking.facer = Current.facer;
                                Tracking.facet = Current.facet;
                                Tracking.faceb = Current.faceb;
                                /*挑选更好的特征值及人脸照片*/
                                if (Math.Abs(Current.qingxie) < Math.Abs(Tracking.qingxie))
                                {
                                    Tracking.pic = Current.pic;
                                    Tracking.feature = Current.feature;
                                    return FaceTracingList;
                                }
                                /*人数统计 需-1*/
                                times = Current.time - Tracking.time;
                                if ( times.TotalSeconds>= leave_time_interval/*若两次出现的时间大于等于leave_time_interval 则计入统计*/)
                                    _statisticsFaceCount--;
                            }
                            else num++;  //结果显示不一样num+1
                        }
                        if (num == Traceinglist.Count) //追踪列表与当前列表没有相似的人
                        {
                            num = 0;
                            foreach (FaceInfo Tracking in Traceinglist)//再次循环追踪列表对比特征值
                            {
                                if (TrackFeatureJudge(Tracking, Current))
                                {   /*更新位置*/
                                    Tracking.facel = Current.facel;
                                    Tracking.facer = Current.facer;
                                    Tracking.facet = Current.facet;
                                    Tracking.faceb = Current.faceb;
                                    /*挑选更好的特征值及人脸照片*/
                                    if (Math.Abs(Current.qingxie) < Math.Abs(Tracking.qingxie))
                                    {
                                        Tracking.pic = Current.pic;
                                        Tracking.feature = Current.feature;
                                    }
                                    /*人数统计 需-1*/
                                    times = Current.time - Tracking.time;
                                    if (times.TotalSeconds >= leave_time_interval/*若两次出现的时间大于等于leave_time_interval  则计入统计*/)
                                        _statisticsFaceCount--;
                                    return FaceTracingList;
                                }
                                else num++;
                            }
                            if (num == Traceinglist.Count)//追踪列表与当前列表没有相似的人，添加新成员
                            {
                                FaceTracingList.Add(Current);
                                return FaceTracingList;
                            }
                            else return FaceTracingList;
                        }
                        else return FaceTracingList;
                    }
                    return FaceTracingList;
                }else
                {
                    foreach (FaceInfo Current in Currentlist)//循环当前列表
                    {
                        foreach (FaceInfo Tracking in Traceinglist)//再次循环追踪列表对比特征值
                        {
                            //    MessageBox.Show("特征值对比"+num);
                            if (TrackFeatureJudge(Tracking, Current))
                            {   /*更新位置*/
                                Tracking.facel = Current.facel;
                                Tracking.facer = Current.facer;
                                Tracking.facet = Current.facet;
                                Tracking.faceb = Current.faceb;
                                /*挑选更好的特征值及人脸照片*/
                                if (Math.Abs(Current.qingxie) < Math.Abs(Tracking.qingxie))
                                {
                                    Tracking.pic = Current.pic;
                                    Tracking.feature = Current.feature;
                                }
                                /*人数统计 需-1*/
                                times = Current.time - Tracking.time;
                                if (times.TotalSeconds >= leave_time_interval/*若两次出现的时间大于等于leave_time_interval  则计入统计*/)
                                    _statisticsFaceCount--;
                                return FaceTracingList;
                            }
                            else num++;
                        }
                        if (num == Traceinglist.Count)//追踪列表与当前列表没有相似的人，添加新成员
                        {
                            FaceTracingList.Add(Current);
                            return FaceTracingList;
                        }
                        else return FaceTracingList;
                    }
                    return FaceTracingList;
                }
             
            }  
        }
        public bool TrackPositionJudge(FaceInfo l, FaceInfo n)//如果根据传入的两个Faceinfo判断为同一个人（根据位置判断）则返回1，不是同一人返回0，出错返回-1
        {
            int left = Math.Abs(l.facel - n.facel);
            int right = Math.Abs(l.facer - n.facer);
            int top = Math.Abs(l.facet - n.facet);
            int bottom = Math.Abs(l.faceb - n.faceb);
            int facewidth = Math.Abs(l.facel - l.facer);
            int faceheight = Math.Abs(l.facet - l.faceb);
            if (left < facewidth / 2 || right < facewidth / 2)
            {
                if (top < faceheight / 2 || bottom < faceheight / 2)
                {
                    return true;
                }
                else return false;
            }
            else return false;
        }
        public bool TrackFeatureJudge(FaceInfo l, FaceInfo n)//如果根据传入的两个Faceinfo判断为同一个人（根据特征值判断）则返回1，不是同一人返回0，出错返回-1
        {
            try
            {
                int a = Face_detect_sdk.PFD_FeatureMatching(3000, l.feature, 3000, n.feature);
                if (a > 650)
                    return true;
                else return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("PFD_FeatureMatching Error" + ex.ToString());
                return false;
            }
        }
        public void DrawFace(List<FaceInfo> info, PictureBox box)//画框函数
        {
            try { 
            if (info.Count != 0 && box.Image != null)
            {

                Graphics a = Graphics.FromImage(box.Image);
                Pen pen1 = new Pen(Color.Green, Drawfacewidth);
                foreach (FaceInfo F in info)
                {
                    a.DrawRectangle(pen1, F.facel, F.facet, F.facer - F.facel, F.faceb - F.facet);
                }
                a.Dispose();
            }
            }catch (Exception e) { Console.WriteLine("画框出错"+e); }
        }
        protected Task<List<FaceInfo>> FaceRecgTraceDeligate(Bitmap bmp) //识别并跟踪委托函数 ReTrackinglist
        {
            return Task.Run(() =>
            {
                if (bmp != null)
                {
                    _currentFaceList = FaceRecg(bmp);
                    if (_currentFaceList.Count != 0)
                    {
                        _faceTracingList = FaceTrace(FaceTracingList, _currentFaceList);
                    }
                    return FaceTracingList;
                }
                else return FaceTracingList;

            });
        }
        protected Task<List<FaceInfo>> FaceRecgDeligate() //识别委托函数返回ReCurrentList
        {
            return Task.Run(() =>
            {
                if (_currentFaceBitmp != null)
                {
                    _currentFaceList = FaceRecg(_currentFaceBitmp);
                    return _currentFaceList;
                }
                else return null;
            });
        }
        public async void FaceRecgAndDrawAsync(Bitmap nowbit, PictureBox box)//识别并画框
        {
            try
            {
                if (_RecgRunningFlag) return;
                _RecgRunningFlag = true;
                _currentFaceBitmp = nowbit;
                _faceRecgResult = await FaceRecgDeligate();
                if (box.InvokeRequired)
                {
                    Action<List<FaceInfo>> actionDelegate = (x) =>
                    {
                        if (box.Image != null)
                        {
                            DrawFace(x, box);
                        }
                    };
                    box.Invoke(actionDelegate, _faceRecgResult);
                }
            
                _RecgRunningFlag = false;

            }
            catch (Exception e) { _RecgRunningFlag = false; Console.WriteLine(e); }

        }
        public async void FaceRecgAndTraceAsync(Bitmap nowbit)//识别追踪函数
        {
            if (_RecgRunningFlag) return;
            _RecgRunningFlag = true;
           // bmp = nowbit;
            _faceRecgResult = await FaceRecgTraceDeligate(nowbit);
            _RecgRunningFlag = false;
        }
    }
    /************************************************用法例程***********************************************************
    public Face_Info_Track  FIT  = new Face_Info_Track();   //先 new 一个Face_Info_Track 类
    FIT.FaceRecgAndDrawAsync(bitmap, pictureBox1);  //调用识别画框函数 传入要识别的图片 bitmap ，和要画框的作图区域pictureBox1
    Console.WriteLine(FIT.getFaceRecgResult.Count);    //输出结果（此张图人数）
    getFaceRecgResult 为Face_Info_Track类中的一个list指针，其中成员为Faceinfo； 如：getFaceRecgResult[i].age ->列表中第i个人的年龄

    FIT.Drawface(FIT.GetCurrentFaceList, pictureBox1);//画框函数；但是需要用到FaceRecg(Bitmap bmp)（识别函数） 更新_currentFaceList（否则只画最后一次识别的图像）

    统计的人数可直接用 FIT.FaceCount 即可  leave_time_interval 为辨判断一个人进入2次的时间差。目前为300.00秒计。可另行更改
        注：人数统计只有用到追踪函数才会有真实效果，否则不具备判断人是否相同的能力
    ********************************************************************************************************************/
}
public class FaceInfo //一张人脸信息
{
    public byte[] feature;//特征值
    public int flen;      //特征值长度
    public int age;       //年龄 
    public string sex;    //性别
    public int qingxie;   //人脸倾斜角度
    public int yaotou;    //人脸摇头角度
    public int taitou;    //人脸抬头角度
    public int facel;     //人脸最左侧位置
    public int facer;     //人脸最右侧位置
    public int facet;     //人脸最上侧位置
    public int faceb ;    //人脸最下侧位置
    public DateTime time; //开始识别时间
    public Image pic;     //人脸特写
    public Image pan1;   
    public Image pan2;    
    public Image pan3;    
}
