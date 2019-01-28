using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Redlight
{
    class Face_detect_sdk
    {
        public const int PFD_MAX_FACE_NUM = 20;
        public const int PFD_OP_FACE_ROLL_0 = 0x10;
        public const int PFD_OP_FACE_ROLL_90 = 0x20;
        public const int PFD_OP_FACE_ROLL_180 = 0x40;
        public const int PFD_OP_FACE_ROLL_270 = 0x80;

        public const int PFD_STATUS_OK = 0;
        public const int PFD_STATUS_NG = -1;
        public const int PFD_STATUS_INVALID = -2;

        public const int PFD_MALE = 0;
        public const int PFD_FEMALE = 1;

        public const int PFD_ENABLEINFO = 1;
        public const int PFD_DISABLEINFO = 0;

        public const int IMG_SIZE_VGA = 0;
        public const int IMG_SIZE_FULLHD = 1;
        
        [StructLayout(LayoutKind.Sequential,Pack=1,CharSet=CharSet.Ansi)]
        public struct PFD_POINT {
            public short x;
            public short y;
        };

        [StructLayout(LayoutKind.Sequential,Pack=1,CharSet=CharSet.Ansi)]
        public struct PFD_FACE_POSITION {
            public short conf;	/* ﾆ・R信V酷x(0~1000 低~高) */
            public short rect_l;	/* 人8″ﾅ左ｧO坐h*/
            public short rect_r;	/*人8″ﾅ右ｧO坐h*/
            public short rect_t;	/*人8″ﾅ上ｧO坐h*/
            public short rect_b;	/*人8″ﾅ下ｧO坐h*/
            public short eye_lx;	/*人8″ｶ眼x坐h*/
            public short eye_ly;	/*人8″ｶ眼y坐h*/
            public short eye_rx;	/*人8♂E眼x坐h*/
            public short eye_ry;	/*人8♂E眼y坐h*/
        };
        [StructLayout(LayoutKind.Sequential,Pack=1,CharSet=CharSet.Ansi)]
        public struct PFD_DETECT_INFO{
	        public PFD_FACE_POSITION faceInfo;
	        public short ageConf;
            public short genConf;
            public short age;
            public short gen;
            public short smile;
            public short pitch;
            public short yaw;
            public short roll;
            public short lb;
            public short rb;
            public short flen;
            public short enable;
        };
        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
        public struct PFD_DETECT_INFO_LOG
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 83)]
            public PFD_POINT[] fp;
            public short faceVx;
            public short faceVy;
            public short eyeVx;
            public short eyeVy;
        };
        [StructLayout(LayoutKind.Sequential,Pack=1,CharSet=CharSet.Ansi)]
        public struct PFD_FACE_DETECT {
	        public short	num;	
            [MarshalAs(UnmanagedType.ByValArray,SizeConst=20)]
            public PFD_DETECT_INFO[] info;							/*顔座標*/
        };
        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
        public struct PFD_FACE_DETECT_LOG
        {
            public short num;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public PFD_DETECT_INFO[] info;							/*顔座標*/
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public PFD_DETECT_INFO_LOG[] infoLog;
        };
        [StructLayout(LayoutKind.Sequential,Pack=1,CharSet=CharSet.Ansi)]
        public struct PFD_AGR_INFO{
            public PFD_FACE_POSITION faceInfo; 
	        public short ageConf;
	        public short genConf;
	        public short age;
	        public short gen;
        };

        [StructLayout(LayoutKind.Sequential,Pack=1,CharSet=CharSet.Ansi)]
        public struct PFD_AGR_DETECT{
	        public short num;
            [MarshalAs(UnmanagedType.ByValArray,SizeConst=20)]
	        public PFD_AGR_INFO[] info;
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
        public struct PFD_SMILE_INFO
        {
            public PFD_FACE_POSITION faceInfo;
            public short smile;
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
        public struct PFD_SMILE_DETECT
        {
            public short num;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public PFD_SMILE_INFO[] info;
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
        public struct PFD_DIRECT_INFO
        {
            public PFD_FACE_POSITION faceInfo;
            public short pitch;
            public short yaw;
            public short roll;
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
        public struct PFD_DIRECT_DETECT
        {
            public short num;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public PFD_DIRECT_INFO[] info;
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
        public struct PFD_BLINK_INFO
        {
            public PFD_FACE_POSITION faceInfo;
            public short lb;
            public short rb;
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
        public struct PFD_BLINK_DETECT
        {
            public short num;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public PFD_BLINK_INFO[] info;
        }

        [DllImport("EVAL_x64_Accuracy.dll")]
        public static extern int PFD_Init(int imgSize);
        [DllImport("EVAL_x64_Accuracy.dll")]
        public static extern int PFD_Exit();
        [DllImport("EVAL_x64_Accuracy.dll")]
        public static extern int PFD_FaceRecog([MarshalAs(UnmanagedType.LPArray)] byte[] mem, ref PFD_FACE_DETECT faceInfo,int faceInfoFlag,short faceRote);
        [DllImport("EVAL_x64_Accuracy.dll")]
        public static extern int PFD_AgrRecogImg([MarshalAs(UnmanagedType.LPArray)] byte[] mem, ref PFD_AGR_DETECT agrInfo, short faceRote);
        [DllImport("EVAL_x64_Accuracy.dll")]
        public static extern int PFD_SmileRecogImg([MarshalAs(UnmanagedType.LPArray)] byte[] mem, ref PFD_SMILE_DETECT smileInfo, short faceRote);
        [DllImport("EVAL_x64_Accuracy.dll")]
        public static extern int PFD_DirectRecogImg([MarshalAs(UnmanagedType.LPArray)] byte[] mem, ref PFD_DIRECT_DETECT directInfo, short faceRote);
        [DllImport("EVAL_x64_Accuracy.dll")]
        public static extern int PFD_BlinkRecogImg([MarshalAs(UnmanagedType.LPArray)] byte[] mem, ref PFD_BLINK_DETECT blinkInfo, short faceRote);
        [DllImport("EVAL_x64_Accuracy.dll")]
        public static extern int PFD_GetFeature([MarshalAs(UnmanagedType.LPArray)] byte[] mem, ref PFD_FACE_DETECT faceInfo, [MarshalAs(UnmanagedType.LPArray)] IntPtr[] feature, short faceRote, short min_width);
        [DllImport("EVAL_x64_Accuracy.dll")]
        public static extern int PFD_GetFeatureLog([MarshalAs(UnmanagedType.LPArray)] byte[] mem, ref PFD_FACE_DETECT_LOG faceInfo, [MarshalAs(UnmanagedType.LPArray)] IntPtr[] feature, short faceRote, short min_width);
        [DllImport("EVAL_x64_Accuracy.dll")]
        public static extern int PFD_GetFeatureByManual([MarshalAs(UnmanagedType.LPArray)] byte[] mem, ref PFD_DETECT_INFO faceInfo, IntPtr feature, short faceRote, ref short fsize);
        [DllImport("EVAL_x64_Accuracy.dll")]
        public static extern int PFD_FeatureMatching(short flen1, [MarshalAs(UnmanagedType.LPArray)] byte[] feature1, short flen2, [MarshalAs(UnmanagedType.LPArray)] byte[] feature2);
        [DllImport("EVAL_x64_Accuracy.dll")]
        public static extern int PDB_StoreFeature(int dbId, int id, int usrId, short fsize, [MarshalAs(UnmanagedType.LPArray)] byte[] feature);
        [DllImport("EVAL_x64_Accuracy.dll")]
        public static extern int PDB_DeleteFeature(int dbId, int usid);
        [DllImport("EVAL_x64_Accuracy.dll")]
        public static extern int PDB_DeleteFeatureById(int dbId, int id);
        [DllImport("EVAL_x64_Accuracy.dll")]
        public static extern int PDB_MatchFeature(int dbId, uint threshold, short fsize, [MarshalAs(UnmanagedType.LPArray)] byte[] feature, ushort num, [MarshalAs(UnmanagedType.LPArray)] int[] candidate, [MarshalAs(UnmanagedType.LPArray)] short[] score);
        [DllImport("EVAL_x64_Accuracy.dll")]
        public static extern int PDB_ResetDataBase(int dbId);
        [DllImport("EVAL_x64_Accuracy.dll")]
        public static extern int PDB_DeleteDataBase(int dbId);
        [DllImport("EVAL_x64_Accuracy.dll")]
        public static extern int PDB_AddDataBase(int maxFaceNum);
        [DllImport("EVAL_x64_Accuracy.dll")]
        public static extern int PDB_ChangeFeature(int dbId, int usid, short fsize, [MarshalAs(UnmanagedType.LPArray)] byte[] feature);
        [DllImport("EVAL_x64_Accuracy.dll")]
        public static extern int PDB_GetUserId(int dbId, int id);
    }
}
