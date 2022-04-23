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
    public class ASF_AgeInfo
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
}
