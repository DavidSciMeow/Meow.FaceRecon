using System.Runtime.InteropServices;

namespace Meow.FaceRecon.NativeSDK
{
    /// <summary>
    /// SDK常量
    /// </summary>
    public enum Mask
    {
        /// <summary>
        /// 无属性
        /// </summary>
        ASF_NONE = 0,
        /// <summary>
        /// 此处detect可以是tracking或者detection两个引擎之一，
        /// 具体的选择由detect mode 确定
        /// </summary>
        ASF_FACE_DETECT = 1,
        /// <summary>
        /// 人脸特征
        /// </summary>
        ASF_FACERECOGNITION = 4,
        /// <summary>
        /// 年龄
        /// </summary>
        ASF_AGE = 8,
        /// <summary>
        /// 性别
        /// </summary>
        ASF_GENDER = 16,
        /// <summary>
        /// 3D角度
        /// </summary>
        ASF_FACE3DANGLE = 32,
        /// <summary>
        /// RGB活体
        /// </summary>
        ASF_LIVENESS = 128,
        /// <summary>
        /// IR活体
        /// </summary>
        ASF_IR_LIVENESS = 1024,
    }
    /// <summary>
    /// 检测模式
    /// </summary>
    public enum ASF_DetectMode
    {
        /// <summary>
        /// Video模式，一般用于多帧连续检测
        /// </summary>
        ASF_DETECT_MODE_VIDEO = 0,
        /// <summary>
        /// Image模式，一般用于静态图的单次检测
        /// </summary>
        ASF_DETECT_MODE_IMAGE = -1,
    }
    /// <summary>
    /// 优先级
    /// </summary>
    public enum ASF_OrientPriority
    {
        /// <summary>
        /// 常规预览下正方向
        /// </summary>
        ASF_OP_0_ONLY = 1,
        /// <summary>
        /// 基于0°逆时针旋转90°的方向
        /// </summary>
        ASF_OP_90_ONLY = 2,
        /// <summary>
        /// 基于0°逆时针旋转270°的方向
        /// </summary>
        ASF_OP_270_ONLY = 3,
        /// <summary>
        /// 基于0°旋转180°的方向（逆时针、顺时针效果一样）
        /// </summary>
        ASF_OP_180_ONLY = 4,
        /// <summary>
        /// 全角度
        /// </summary>
        ASF_OP_ALL_OUT = 5,
    }
    /// <summary>
    /// 角度
    /// </summary>
    public enum ASF_OrientCode
    {
        /// <summary>
        /// 0 degree 
        /// </summary>
        ASF_OC_0 = 1,
        /// <summary>
        /// 90 degree 
        /// </summary>
        ASF_OC_90 = 2,
        /// <summary>
        /// 270 degree 
        /// </summary>
        ASF_OC_270 = 3,
        /// <summary>
        /// 180 degree 
        /// </summary>
        ASF_OC_180 = 4,
        /// <summary>
        /// 30 degree 
        /// </summary>
        ASF_OC_30 = 5,
        /// <summary>
        /// 60 degree 
        /// </summary>
        ASF_OC_60 = 6,
        /// <summary>
        /// 120 degree 
        /// </summary>
        ASF_OC_120 = 7,
        /// <summary>
        /// 150 degree 
        /// </summary>
        ASF_OC_150 = 8,
        /// <summary>
        /// 210 degree 
        /// </summary>
        ASF_OC_210 = 9,
        /// <summary>
        /// 240 degree 
        /// </summary>
        ASF_OC_240 = 10,
        /// <summary>
        /// 300 degree 
        /// </summary>
        ASF_OC_300 = 11,
        /// <summary>
        /// 330 degree 
        /// </summary>
        ASF_OC_330 = 12,
    }
    /// <summary>
    /// 检测模型
    /// </summary>
    public enum ASF_DetectModel
    {
        /// <summary>
        /// RGB图像检测模型
        /// </summary>
        ASF_DETECT_MODEL_RGB = 1,
    }
    /// <summary>
    /// 人脸比对可选的模型
    /// </summary>
    public enum ASF_CompareModel
    {
        /// <summary>
        /// 用于生活照之间的特征比对，推荐阈值0.80
        /// </summary>
        ASF_LIFE_PHOTO = 1,
        /// <summary>
        /// 用于证件照或生活照与证件照之间的特征比对，推荐阈值0.82
        /// </summary>
        ASF_ID_PHOTO = 2,
    }
    /// <summary>
    /// 色彩空间
    /// </summary>
    public enum ColorSpace
    {
        /// <summary>
        /// ASVL_PAF_RGB16_B5G6R5
        /// </summary>
        ASVL_PAF_RGB16_B5G6R5 = 257,
        /// <summary>
        /// ASVL_PAF_RGB16_B5G5R5
        /// </summary>
        ASVL_PAF_RGB16_B5G5R5 = 258,
        /// <summary>
        /// ASVL_PAF_RGB16_B4G4R4
        /// </summary>
        ASVL_PAF_RGB16_B4G4R4 = 259,
        /// <summary>
        /// ASVL_PAF_RGB16_B5G5R5T
        /// </summary>
        ASVL_PAF_RGB16_B5G5R5T = 260,
        /// <summary>
        /// ASVL_PAF_RGB16_R5G6B5
        /// </summary>
        ASVL_PAF_RGB16_R5G6B5 = 261,
        /// <summary>
        /// ASVL_PAF_RGB16_R5G5B5
        /// </summary>
        ASVL_PAF_RGB16_R5G5B5 = 262,
        /// <summary>
        /// ASVL_PAF_RGB16_R4G4B4
        /// </summary>
        ASVL_PAF_RGB16_R4G4B4 = 263,
        /// <summary>
        /// ASVL_PAF_RGB24_B8G8R8
        /// </summary>
        ASVL_PAF_RGB24_B8G8R8 = 513,
        /// <summary>
        /// ASVL_PAF_RGB24_B6G6R6
        /// </summary>
        ASVL_PAF_RGB24_B6G6R6 = 514,
        /// <summary>
        /// ASVL_PAF_RGB24_B6G6R6T
        /// </summary>
        ASVL_PAF_RGB24_B6G6R6T = 515,
        /// <summary>
        /// ASVL_PAF_RGB24_R8G8B8
        /// </summary>
        ASVL_PAF_RGB24_R8G8B8 = 516,
        /// <summary>
        /// ASVL_PAF_RGB24_R6G6B6
        /// </summary>
        ASVL_PAF_RGB24_R6G6B6 = 517,
        /// <summary>
        /// ASVL_PAF_RGB32_B8G8R8
        /// </summary>
        ASVL_PAF_RGB32_B8G8R8 = 769,
        /// <summary>
        /// ASVL_PAF_RGB32_B8G8R8A8
        /// </summary>
        ASVL_PAF_RGB32_B8G8R8A8 = 770,
        /// <summary>
        /// ASVL_PAF_RGB32_R8G8B8
        /// </summary>
        ASVL_PAF_RGB32_R8G8B8 = 771,
        /// <summary>
        /// ASVL_PAF_RGB32_A8R8G8B8
        /// </summary>
        ASVL_PAF_RGB32_A8R8G8B8 = 772,
        /// <summary>
        /// ASVL_PAF_RGB32_R8G8B8A8
        /// </summary>
        ASVL_PAF_RGB32_R8G8B8A8 = 773,
        /// <summary>
        /// ASVL_PAF_YUV
        /// </summary>
        ASVL_PAF_YUV = 1025,
        /// <summary>
        /// ASVL_PAF_YVU
        /// </summary>
        ASVL_PAF_YVU = 1026,
        /// <summary>
        /// ASVL_PAF_UVY
        /// </summary>
        ASVL_PAF_UVY = 1027,
        /// <summary>
        /// ASVL_PAF_VUY
        /// </summary>
        ASVL_PAF_VUY = 1028,
        /// <summary>
        /// ASVL_PAF_YUYV
        /// </summary>
        ASVL_PAF_YUYV = 1281,
        /// <summary>
        /// ASVL_PAF_YVYU
        /// </summary>
        ASVL_PAF_YVYU = 1282,
        /// <summary>
        /// ASVL_PAF_UYVY
        /// </summary>
        ASVL_PAF_UYVY = 1283,
        /// <summary>
        /// ASVL_PAF_VYUY
        /// </summary>
        ASVL_PAF_VYUY = 1284,
        /// <summary>
        /// ASVL_PAF_YUYV2
        /// </summary>
        ASVL_PAF_YUYV2 = 1285,
        /// <summary>
        /// ASVL_PAF_YVYU2
        /// </summary>
        ASVL_PAF_YVYU2 = 1286,
        /// <summary>
        /// ASVL_PAF_UYVY2
        /// </summary>
        ASVL_PAF_UYVY2 = 1287,
        /// <summary>
        /// ASVL_PAF_VYUY2
        /// </summary>
        ASVL_PAF_VYUY2 = 1288,
        /// <summary>
        /// ASVL_PAF_YYUV
        /// </summary>
        ASVL_PAF_YYUV = 1289,
        /// <summary>
        /// ASVL_PAF_I420
        /// </summary>
        ASVL_PAF_I420 = 1537,
        /// <summary>
        /// ASVL_PAF_I422V
        /// </summary>
        ASVL_PAF_I422V = 1538,
        /// <summary>
        /// ASVL_PAF_I422H
        /// </summary>
        ASVL_PAF_I422H = 1539,
        /// <summary>
        /// ASVL_PAF_I444
        /// </summary>
        ASVL_PAF_I444 = 1540,
        /// <summary>
        /// ASVL_PAF_YV12
        /// </summary>
        ASVL_PAF_YV12 = 1541,
        /// <summary>
        /// ASVL_PAF_YV16V
        /// </summary>
        ASVL_PAF_YV16V = 1542,
        /// <summary>
        /// ASVL_PAF_YV16H
        /// </summary>
        ASVL_PAF_YV16H = 1543,
        /// <summary>
        /// ASVL_PAF_YV24
        /// </summary>
        ASVL_PAF_YV24 = 1544,
        /// <summary>
        /// ASVL_PAF_GRAY
        /// </summary>
        ASVL_PAF_GRAY = 1793,
        /// <summary>
        /// ASVL_PAF_NV12
        /// </summary>
        ASVL_PAF_NV12 = 2049,
        /// <summary>
        /// ASVL_PAF_NV21
        /// </summary>
        ASVL_PAF_NV21 = 2050,
        /// <summary>
        /// ASVL_PAF_LPI422H
        /// </summary>
        ASVL_PAF_LPI422H = 2051,
        /// <summary>
        /// ASVL_PAF_LPI422H2
        /// </summary>
        ASVL_PAF_LPI422H2 = 2052,
        /// <summary>
        ///ASVL_PAF_NV41
        /// </summary>
        ASVL_PAF_NV41 = 2053,
        /// <summary>
        /// ASVL_PAF_NEG_UYVY
        /// </summary>
        ASVL_PAF_NEG_UYVY = 2305,
        /// <summary>
        /// ASVL_PAF_NEG_I420
        /// </summary>
        ASVL_PAF_NEG_I420 = 2306,
        /// <summary>
        /// ASVL_PAF_MONO_UYVY
        /// </summary>
        ASVL_PAF_MONO_UYVY = 2561,
        /// <summary>
        /// ASVL_PAF_MONO_I420
        /// </summary>
        ASVL_PAF_MONO_I420 = 2562,
        /// <summary>
        /// ASVL_PAF_P8_YUYV
        /// </summary>
        ASVL_PAF_P8_YUYV = 2819,
        /// <summary>
        /// ASVL_PAF_SP16UNIT
        /// </summary>
        ASVL_PAF_SP16UNIT = 3073,
        /// <summary>
        /// ASVL_PAF_DEPTH_U16
        /// </summary>
        ASVL_PAF_DEPTH_U16 = 3074,
        /// <summary>
        /// ASVL_PAF_RAW10_RGGB_10B
        /// </summary>
        ASVL_PAF_RAW10_RGGB_10B = 3329,
        /// <summary>
        /// ASVL_PAF_RAW10_GRBG_10B
        /// </summary>
        ASVL_PAF_RAW10_GRBG_10B = 3330,
        /// <summary>
        /// ASVL_PAF_RAW10_GBRG_10B
        /// </summary>
        ASVL_PAF_RAW10_GBRG_10B = 3331,
        /// <summary>
        /// ASVL_PAF_RAW10_BGGR_10B
        /// </summary>
        ASVL_PAF_RAW10_BGGR_10B = 3332,
        /// <summary>
        /// ASVL_PAF_RAW12_RGGB_12B
        /// </summary>
        ASVL_PAF_RAW12_RGGB_12B = 3333,
        /// <summary>
        /// ASVL_PAF_RAW12_GRBG_12B
        /// </summary>
        ASVL_PAF_RAW12_GRBG_12B = 3334,
        /// <summary>
        /// ASVL_PAF_RAW12_GBRG_12B
        /// </summary>
        ASVL_PAF_RAW12_GBRG_12B = 3335,
        /// <summary>
        /// ASVL_PAF_RAW12_BGGR_12B
        /// </summary>
        ASVL_PAF_RAW12_BGGR_12B = 3336,
        /// <summary>
        /// ASVL_PAF_RAW10_RGGB_16B
        /// </summary>
        ASVL_PAF_RAW10_RGGB_16B = 3337,
        /// <summary>
        /// ASVL_PAF_RAW10_GRBG_16B
        /// </summary>
        ASVL_PAF_RAW10_GRBG_16B = 3338,
        /// <summary>
        /// ASVL_PAF_RAW10_GBRG_16B
        /// </summary>
        ASVL_PAF_RAW10_GBRG_16B = 3339,
        /// <summary>
        /// ASVL_PAF_RAW10_BGGR_16B
        /// </summary>
        ASVL_PAF_RAW10_BGGR_16B = 3340,
        /// <summary>
        /// ASVL_PAF_RAW10_GRAY_10B
        /// </summary>
        ASVL_PAF_RAW10_GRAY_10B = 3585,
        /// <summary>
        /// ASVL_PAF_RAW10_GRAY_16B
        /// </summary>
        ASVL_PAF_RAW10_GRAY_16B = 3713,
    }

    //[StructLayout(LayoutKind.Sequential)]

    /// <summary>
    /// 识别矩形
    /// </summary>
    public struct MRECT
    {
        /// <summary>
        /// 左
        /// </summary>
        public int left;
        /// <summary>
        /// 上
        /// </summary>
        public int top;
        /// <summary>
        /// 右
        /// </summary>
        public int right;
        /// <summary>
        /// 下
        /// </summary>
        public int bottom;
    }
    /// <summary>
    /// 识别点
    /// </summary>
    public struct MPOINT
    {
        /// <summary>
        /// x轴
        /// </summary>
        public int x;
        /// <summary>
        /// y轴
        /// </summary>
        public int y;
    }
    /// <summary>
    /// 版本信息
    /// </summary>
    public struct ASF_VERSION
    {
        /// <summary>
        /// 版本号
        /// </summary>
        [MarshalAs(UnmanagedType.LPStr)]
        public string Version;
        /// <summary>
        /// 构建日期
        /// </summary>
        [MarshalAs(UnmanagedType.LPStr)]
        public string BuildDate;
        /// <summary>
        /// Copyright
        /// </summary>
        [MarshalAs(UnmanagedType.LPStr)]
        public string CopyRight;
    }
    /// <summary>
    /// 单人脸信息
    /// </summary>
    public struct ASF_SingleFaceInfo
    {
        /// <summary>
        /// 人脸框信息
        /// </summary>
        public MRECT faceRect;
        /// <summary>
        /// 输入图像的角度，可以参考 ArcFaceCompare_OrientCode
        /// </summary>
        public int faceOrient;
    }
    /// <summary>
    /// 多人脸信息
    /// </summary>
    public struct ASF_MultiFaceInfo
    {
        /// <summary>
        /// 人脸框信息
        /// </summary>
        public IntPtr faceRect;
        /// <summary>
        /// 输入图像的角度，可以参考 ArcFaceCompare_OrientCode . 
        /// </summary>
        public IntPtr faceOrient;
        /// <summary>
        /// 检测到的人脸个数
        /// </summary>
        public int faceNum;
        /// <summary>
        /// face ID，IMAGE模式下不返回FaceID
        /// </summary>
        public IntPtr faceID;
    }
    /// <summary>
    /// 激活文件信息
    /// </summary>
    public struct ASF_ActiveFileInfo
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        [MarshalAs(UnmanagedType.LPStr)]
        public string startTime;
        /// <summary>
        /// 截止时间
        /// </summary>
        [MarshalAs(UnmanagedType.LPStr)]
        public string endTime;
        /// <summary>
        /// 平台
        /// </summary>
        [MarshalAs(UnmanagedType.LPStr)]
        public string platform;
        /// <summary>
        /// sdk类型
        /// </summary>
        [MarshalAs(UnmanagedType.LPStr)]
        public string sdkType;
        /// <summary>
        /// APPID
        /// </summary>
        [MarshalAs(UnmanagedType.LPStr)]
        public string appId;
        /// <summary>
        /// SDKKEY
        /// </summary>
        [MarshalAs(UnmanagedType.LPStr)]
        public string sdkKey;
        /// <summary>
        /// SDK版本号
        /// </summary>
        [MarshalAs(UnmanagedType.LPStr)]
        public string sdkVersion;
        /// <summary>
        /// 激活文件版本号
        /// </summary>
        [MarshalAs(UnmanagedType.LPStr)]
        public string fileVersion;
    }
    /// <summary>
    /// 活体阈值设置
    /// </summary>
    public struct ASF_LivenessThreshold
    {
        /// <summary>
        /// BGR
        /// </summary>
        public float thresholdmodel_BGR;
        /// <summary>
        /// IR
        /// </summary>
        public float thresholdmodel_IR;
    }
    /// <summary>
    /// 定义图片格式空间
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct ASVLOFFSCREEN
    {
        /// <summary>
        /// 无符号int类型像素数组类型
        /// </summary>
        public uint u32PixelArrayFormat;
        /// <summary>
        /// 宽度
        /// </summary>
        public int i32Width;
        /// <summary>
        /// 高度
        /// </summary>
        public int i32Height;
        /// <summary>
        /// ui8-指针-指针 平面
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.SysUInt)]
        public IntPtr[] ppu8Plane;
        /// <summary>
        /// int类指针 俯仰
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I4)]
        public int[] pi32Pitch;
    }
    /// <summary>
    /// 定义SDK版本信息(模板)
    /// </summary>
    public struct ASVL_VERSION
    {
        /// <summary>
        /// Codebase version number 
        /// </summary>
        public int lCodebase;
        /// <summary>
        /// major version number 
        /// </summary>
        public int lMajor;
        /// <summary>
        /// minor version number
        /// </summary>
        public int lMinor;
        /// <summary>
        /// Build version number, increasable only
        /// </summary>
        public int lBuild;
        /// <summary>
        /// version in string form
        /// </summary>
        [MarshalAs(UnmanagedType.LPStr)]
        public string Version;
        /// <summary>
        /// latest build Date
        /// </summary>
        [MarshalAs(UnmanagedType.LPStr)]
        public string BuildDate;
        /// <summary>
        /// copyright 
        /// </summary>
        [MarshalAs(UnmanagedType.LPStr)]
        public string CopyRight;
    }
    /// <summary>
    /// 人脸特征
    /// </summary>
    public struct ASF_FaceFeature
    {
        /// <summary>
        /// 人脸特征信息
        /// </summary>
        [MarshalAs(UnmanagedType.LPStr)]
        public string feature;
        /// <summary>
        /// 人脸特征信息长度
        /// </summary>
        public int featureSize;
    }
    /// <summary>
    /// 年龄
    /// </summary>
    public struct ASF_AgeInfo
    {
        /// <summary>
        /// 年龄模式
        /// <para>"0" 代表不确定，大于0的数值代表检测出来的年龄结果</para>
        /// </summary>
        public IntPtr ageArray;
        /// <summary>
        /// 检测的人脸个数
        /// </summary>
        public int num;
    }
    /// <summary>
    /// 性别
    /// </summary>
    public struct ASF_GenderInfo
    {
        /// <summary>
        /// 性别构成
        /// <para>"0" 表示 男性, "1" 表示 女性, "-1" 表示不确定</para>
        /// </summary>
        public IntPtr genderArray;
        /// <summary>
        /// 检测的人脸个数	
        /// </summary>
        public int num;
    }
    /// <summary>
    /// 获取3D角度信息
    /// </summary>
    public struct ASF_Face3DAngle
    {
        /// <summary>
        /// 滚转
        /// </summary>
        public IntPtr roll;
        /// <summary>
        /// 偏航
        /// </summary>
        public IntPtr yaw;
        /// <summary>
        /// 俯仰
        /// </summary>
        public IntPtr pitch;
        /// <summary>
        /// 状态码
        /// <para>0: 正常，其他数值：出错</para>
        /// </summary>
        public IntPtr status;
        /// <summary>
        /// 脸的位置数值
        /// </summary>
        public int num;
    }
    /// <summary>
    /// 活体信息
    /// </summary>
    public struct ASF_LivenessInfo
    {
        /// <summary>
        /// [out] 判断是否真人
        /// <para>0：非真人 1：真人 -1：不确定 -2:传入人脸数>1</para>
        /// <para>-3: 人脸过小 -4: 角度过大 -5: 人脸超出边界</para>
        /// </summary>
        public IntPtr isLive;
        /// <summary>
        /// 检测结果数量
        /// </summary>
        public int num;
    }
}
