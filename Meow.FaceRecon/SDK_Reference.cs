using Meow.FaceRecon.NativeSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meow.FaceRecon.SDK.Model
{
    /// <summary>
    /// [SDK]多人脸信息
    /// </summary>
    public class SDK_MultiFaceInfo
    {
        /// <summary>
        /// 人脸框信息
        /// </summary>
        public List<MRECT> faceRect = new();
        /// <summary>
        /// 输入图像的角度，可以参考 ArcFaceCompare_OrientCode . 
        /// </summary>
        public List<ASF_OrientCode> faceOrient = new();
        /// <summary>
        /// 检测到的人脸个数
        /// </summary>
        public int faceNum;
        /// <summary>
        /// face ID，IMAGE模式下不返回FaceID
        /// </summary>
        public List<string> faceID = new();
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
    /// [SDK]面部朝向
    /// </summary>
    public class SDK_Face3DAngle
    {
        /// <summary>
        /// 滚转
        /// </summary>
        public List<float> roll = new();
        /// <summary>
        /// 偏航
        /// </summary>
        public List<float> yaw = new();
        /// <summary>
        /// 俯仰
        /// </summary>
        public List<float> pitch = new();
        /// <summary>
        /// 状态码
        /// <para>0: 正常，其他数值：出错</para>
        /// </summary>
        public List<int> status = new();
        /// <summary>
        /// 脸的位置数值
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
    /// [SDK] 多人脸识别
    /// </summary>
    public class SDK_FaceGeneral
    {
        /// <summary>
        /// 人脸框信息
        /// </summary>
        public List<MRECT> faceRect = new();
        /// <summary>
        /// 输入图像的角度，可以参考 ArcFaceCompare_OrientCode . 
        /// </summary>
        public List<ASF_OrientCode> faceOrient = new();
        /// <summary>
        /// 检测到的人脸个数
        /// </summary>
        public int faceNum;
        /// <summary>
        /// face ID，IMAGE模式下不返回FaceID
        /// </summary>
        public List<string> faceID = new();
        /// <summary>
        /// 年龄模式
        /// <para>"0" 代表不确定，大于0的数值代表检测出来的年龄结果</para>
        /// </summary>
        public List<int> ageArray = new();
        /// <summary>
        /// 性别构成
        /// <para>"0" 表示 男性, "1" 表示 女性, "-1" 表示不确定</para>
        /// </summary>
        public List<int> genderArray = new();
        /// <summary>
        /// 滚转
        /// </summary>
        public List<float> roll = new();
        /// <summary>
        /// 偏航
        /// </summary>
        public List<float> yaw = new();
        /// <summary>
        /// 俯仰
        /// </summary>
        public List<float> pitch = new();
        /// <summary>
        /// 状态码
        /// <para>0: 正常，其他数值：出错</para>
        /// </summary>
        public List<int> status = new();
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
    }
}
