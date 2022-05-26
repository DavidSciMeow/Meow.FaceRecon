using Meow.FaceRecon4.NativeSDK;

namespace Meow.FaceRecon4.SDK.Model
{
    /// <summary>
    /// [SDK]人脸属性信息
    /// </summary>
    public class SDK_FaceAttributeInfo
    {
        /// <summary>
        /// 戴眼镜状态, 0 未戴眼镜；1 戴眼镜；2 墨镜
        /// </summary>
        public int WearGlass;
        /// <summary>
        /// 左眼状态 false 闭眼(0) true 睁眼(1)
        /// </summary>
        public bool LeftEyeOpen;
        /// <summary>
        /// 右眼状态 false 闭眼(0) true 睁眼(1)
        /// </summary>
        public bool RightEyeOpen;
        /// <summary>
        /// 是否张嘴 false 张嘴(0) true 合嘴(1)
        /// </summary>
        public bool MouthClose;
    }
    /// <summary>
    /// [SDK]面部朝向
    /// </summary>
    public class SDK_Face3DAngle
    {
        /// <summary>
        /// 滚转
        /// </summary>
        public float roll = new();
        /// <summary>
        /// 偏航
        /// </summary>
        public float yaw = new();
        /// <summary>
        /// 俯仰
        /// </summary>
        public float pitch = new();
    }
    /// <summary>
    /// [SDK]人脸信息
    /// </summary>
    public class SDK_FaceDataInfo
    {
        /// <summary>
        /// 人脸信息
        /// </summary>
        public string? Data;
        /// <summary>
        /// 人脸信息长度
        /// </summary>
        public int DataSize;
    }
    /// <summary>
    /// [SDK]单人脸信息
    /// </summary>
    public class SDK_SingleFaceInfo
    {
        /// <summary>
        /// 人脸框信息
        /// </summary>
        public MRECT faceRect;
        /// <summary>
        /// 输入图像的角度，可以参考 ArcFaceCompare_OrientCode
        /// </summary>
        public int faceOrient;
        /// <summary>
        /// 单张人脸信息
        /// </summary>
        public SDK_FaceDataInfo faceDataInfo = new();
    }
    /// <summary>
    /// [SDK]多人脸信息
    /// </summary>
    public class SDK_MultiFaceInfo
    {
        /// <summary>
        /// 检测到的人脸个数
        /// </summary>
        public int faceNum;
        /// <summary>
        /// 人脸框信息
        /// </summary>
        public List<MRECT> faceRect = new();
        /// <summary>
        /// 输入图像的角度，可以参考 ArcFaceCompare_OrientCode . 
        /// </summary>
        public List<ASF_OrientCode> faceOrient = new();
        /// <summary>
        /// face ID
        /// </summary>
        public List<int> faceID = new();
        /// <summary>
        /// 人脸检测信息
        /// </summary>
        public List<SDK_FaceDataInfo> faceDataInfoList = new();
        /// <summary>
        /// 人脸是否在边界内 0 人脸溢出；1 人脸在图像边界内
        /// </summary>
        public List<int> faceIsWithinBoundary = new();
        /// <summary>
        /// 人脸额头区域
        /// </summary>
        public List<MRECT> foreheadRect = new();
        /// <summary>
        /// 人脸属性信息
        /// </summary>
        public List<SDK_FaceAttributeInfo> faceAttributeInfo = new();
        /// <summary>
        /// 人脸3D角度
        /// </summary>
        public List<SDK_Face3DAngle> face3DAngleInfo = new();
    }




    /// <summary>
    /// [SDK]年龄
    /// </summary>
    public class SDK_AgeInfo
    {
        /// <summary>
        /// 年龄模式
        /// <para>"0" 代表不确定，大于0的数值代表检测出来的年龄结果</para>
        /// </summary>
        public List<int> ageArray = new();
        /// <summary>
        /// 检测的人脸个数
        /// </summary>
        public int num;
    }
    /// <summary>
    /// [SDK]性别
    /// </summary>
    public class SDK_GenderInfo
    {
        /// <summary>
        /// 性别构成
        /// <para>"0" 表示 男性, "1" 表示 女性, "-1" 表示不确定</para>
        /// </summary>
        public List<int> genderArray = new();
        /// <summary>
        /// 检测的人脸个数
        /// </summary>
        public int num;
    }
    
    /// <summary>
    /// [SDK]活体信息
    /// </summary>
    public class SDK_LivenessInfo
    {
        /// <summary>
        /// [out] 判断是否真人
        /// <para>0：非真人 1：真人 -1：不确定 -2:传入人脸数>1</para>
        /// <para>-3: 人脸过小 -4: 角度过大 -5: 人脸超出边界</para>
        /// </summary>
        public List<int> isLive = new();
        /// <summary>
        /// 检测结果数量
        /// </summary>
        public int num;
    }
    /// <summary>
    /// 单人脸总体属性
    /// </summary>
    public class SDK_Faces
    {
        /// <summary>
        /// 人脸框信息
        /// </summary>
        public MRECT faceRect;
        /// <summary>
        /// 输入图像的角度，可以参考 ArcFaceCompare_OrientCode . 
        /// </summary>
        public ASF_OrientCode faceOrient;
        /// <summary>
        /// 年龄模式
        /// <para>"0" 代表不确定，大于0的数值代表检测出来的年龄结果</para>
        /// </summary>
        public int age;
        /// <summary>
        /// 性别构成
        /// <para>"0" 表示 男性, "1" 表示 女性, "-1" 表示不确定</para>
        /// </summary>
        public int gender;
        /// <summary>
        /// 滚转
        /// </summary>
        public float roll;
        /// <summary>
        /// 偏航
        /// </summary>
        public float yaw;
        /// <summary>
        /// 俯仰
        /// </summary>
        public float pitch;
        /// <summary>
        /// 状态码
        /// <para>0: 正常，其他数值：出错</para>
        /// </summary>
        public int status;
        /// <summary>
        /// [out] 判断是否真人
        /// <para>0：非真人 1：真人 -1：不确定 -2:传入人脸数>1</para>
        /// <para>-3: 人脸过小 -4: 角度过大 -5: 人脸超出边界</para>
        /// </summary>
        public int liveness;
    }
}
