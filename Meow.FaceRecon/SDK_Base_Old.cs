/*
using Meow.FaceRecon.NativeSDK;
using System.Drawing;
using System.Runtime.InteropServices;

/// <summary>
/// 旧引擎实现
/// </summary>
namespace Meow.FaceRecon.SDK.Old
{
    /// <summary>
    /// 引擎定义
    /// </summary>
    public class Engine : IDisposable
    {
        /// <summary>
        /// 是否销毁
        /// </summary>
        private bool disposedValue;
        /// <summary>
        /// 引擎指针
        /// </summary>
        protected IntPtr detectEngine = IntPtr.Zero;

        /// <summary>
        /// Appid
        /// </summary>
        public string AppId { get; }
        /// <summary>
        /// SdkKey
        /// </summary>
        public string SdkKey { get; }
        /// <summary>
        /// 是否已经激活
        /// </summary>
        public bool IsActivate { get; } = false;
        /// <summary>
        /// 检测模式
        /// </summary>
        public ASF_DetectMode Mode { get; }
        /// <summary>
        /// 角度模式
        /// </summary>
        public ASF_OrientPriority Orient { get; }
        /// <summary>
        /// 引擎检测模式
        /// </summary>
        public Mask DetectedMask { get; }
        /// <summary>
        /// 最小人脸尺寸
        /// </summary>
        public int Scale { get; }
        /// <summary>
        /// 最大人脸数
        /// </summary>
        public static int MaxFaceNum { get; private set; }
        /// <summary>
        /// 初始化基类引擎
        /// </summary>
        /// <param name="appId">Appid</param>
        /// <param name="sdkKey">SdkKey</param>
        /// <param name="dm">检测模式</param>
        /// <param name="op">角度模式</param>
        /// <param name="nScale">最小人脸尺寸</param>
        /// <param name="nMaxFaceNum">最大人脸个数</param>
        /// <param name="mode">引擎检测模式</param>
        public Engine(string appId, string sdkKey, 
            ASF_DetectMode dm = ASF_DetectMode.ASF_DETECT_MODE_IMAGE, 
            ASF_OrientPriority op = ASF_OrientPriority.ASF_OP_0_ONLY,
            int nScale = 32, int nMaxFaceNum = 10, 
            Mask mode = Mask.ASF_ALL)
        {
            AppId = appId;
            SdkKey = sdkKey;
            Mode = dm;
            Orient = op;
            Scale = nScale;
            MaxFaceNum = nMaxFaceNum;
            DetectedMask = mode;
            if (!File.Exists("./ArcFace64.dat"))
            {
                var s = (APIResult)NativeFunction.ASFActivation(appId, sdkKey);
                if (s != APIResult.MOK)
                {
                    if (s == APIResult.MERR_ASF_ALREADY_ACTIVATED)
                    {
                        $"Activation Phase : [{s}] {s.ApiResultToChinese()}".ToLog();
                        IsActivate = true;
                    }
                    else
                    {
                        throw new Exception($"Activate Phase : [{s}] {s.ApiResultToChinese()}");
                    }
                }
                else if (s == APIResult.MOK)
                {
                    $"Activation Phase : [{s}] {s.ApiResultToChinese()}".ToLog();
                    IsActivate = true;
                }
                else
                {
                    throw new Exception($"Init Phase : [{s}] {s.ApiResultToChinese()}");
                }
            }
            else
            {
                IsActivate = true;
            }
            var s2 = (APIResult)NativeFunction.ASFInitEngine(dm, op, nScale, nMaxFaceNum, (int)mode, out detectEngine);
            if (s2 != APIResult.MOK)
            {
                $"Init Phase : [{s2}] {s2.ApiResultToChinese()}".ToLog();
            }
        }

        /// <summary>
        /// 销毁
        /// </summary>
        /// <param name="disposing">销毁状态</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    APIResult s = (APIResult)NativeFunction.ASFUninitEngine(detectEngine);
                    $"Dispose Phase :: [{s}] {s.ApiResultToChinese()}".ToLog();
                }
                disposedValue = true;
            }
        }
        /// <summary>
        /// 析构
        /// </summary>
        ~Engine()
        {
            Dispose(disposing: false);
        }
        void IDisposable.Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
    /// <summary>
    /// 一个多人脸检测工具(也可以检测单个人脸)
    /// </summary>
    public class MultiFaceEngine : Engine
    {
        /// <summary>
        /// 生成一个多人脸检测工具(也可以检测单个人脸)
        /// </summary>
        /// <param name="appId">Appid</param>
        /// <param name="sdkKey">SdkKey</param>
        /// <param name="dm">检测模式</param>
        /// <param name="op">角度模式</param>
        /// <param name="nScale">最小人脸尺寸</param>
        /// <param name="nMaxFaceNum">最大人脸个数</param>
        public MultiFaceEngine(
            string appId, string sdkKey, 
            ASF_DetectMode dm = ASF_DetectMode.ASF_DETECT_MODE_IMAGE, 
            ASF_OrientPriority op = ASF_OrientPriority.ASF_OP_0_ONLY, 
            int nScale = 32, int nMaxFaceNum = 10) : 
            base(appId, sdkKey, dm, op, nScale, nMaxFaceNum, Mask.ASF_ALL)
        {
        }
        /// <summary>
        /// 使用引擎检测本图片(底)
        /// </summary>
        /// <param name="i">图像对象</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        protected ASF_MultiFaceInfo DetectMultiFaceBase(Image i)
        {
            var s = (APIResult)NativeFunction.ASFDetectFacesEx(detectEngine, i.GetBitMapPack(), out ASF_MultiFaceInfo info);
            if (s != APIResult.MOK)
            {
                throw new Exception($"Detect_Face Phase : [{s}] {s.ApiResultToChinese()}");
            }
            return info;
        }
        /// <summary>
        /// 使用引擎检测本图片
        /// </summary>
        /// <param name="i">图像对象</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Model.SDK_MultiFaceInfo Detect(Image i)
        {
            var info = DetectMultiFaceBase(i);
            Model.SDK_MultiFaceInfo result = new();
            result.faceNum = info.faceNum;
            for (int j = 0; j < info.faceNum; j++)
            {
                //构造类
                result.faceRect.Add(Marshal.PtrToStructure<MRECT>(info.faceRect));
                result.faceOrient.Add((ASF_OrientCode)Marshal.PtrToStructure<int>(info.faceOrient));
                //步进记录(原始)
                info.faceRect += Marshal.SizeOf(typeof(MRECT));
                info.faceOrient += Marshal.SizeOf(typeof(int));
            }
            return result;
        }
    }
    /// <summary>
    /// 一个年龄检测工具
    /// </summary>
    public sealed class AgeFaceProcess : MultiFaceEngine
    {
        /// <summary>
        /// 一个年龄检测工具
        /// </summary>
        /// <param name="appId">Appid</param>
        /// <param name="sdkKey">SdkKey</param>
        /// <param name="dm">检测模式</param>
        /// <param name="op">角度模式</param>
        /// <param name="nScale">最小人脸尺寸</param>
        /// <param name="nMaxFaceNum">最大人脸个数</param>
        public AgeFaceProcess(
            string appId, string sdkKey,
            ASF_DetectMode dm = ASF_DetectMode.ASF_DETECT_MODE_IMAGE,
            ASF_OrientPriority op = ASF_OrientPriority.ASF_OP_0_ONLY,
            int nScale = 32, int nMaxFaceNum = 10) :
            base(appId, sdkKey, dm, op, nScale, nMaxFaceNum)
        {
        }
        (ASF_MultiFaceInfo,ASF_AgeInfo) DetectAgeBase(Image i)
        {
            ASF_MultiFaceInfo info = DetectMultiFaceBase(i);
            var s = (APIResult)NativeFunction.ASFProcessEx(detectEngine, i.GetBitMapPack(), info,(int)Mask.ASF_AGE);
            if (s != APIResult.MOK)
            {
                throw new Exception($"Process Phase : [{s}] {s.ApiResultToChinese()}");
            }
            var s2 = (APIResult)NativeFunction.ASFGetAge(detectEngine,out ASF_AgeInfo infox);
            if (s2 != APIResult.MOK)
            {
                throw new Exception($"Detect_Age Phase : [{s2}] {s2.ApiResultToChinese()}");
            }
            return (info,infox);
        }
        /// <summary>
        /// 检测本图片年龄
        /// </summary>
        /// <param name="i">图像对象</param>
        /// <returns></returns>
        public new (Model.SDK_MultiFaceInfo mfi, Model.SDK_AgeInfo dfi) Detect(Image i)
        {
            var (mfi, infox) = DetectAgeBase(i);
            Model.SDK_MultiFaceInfo resultmfi = new();
            Model.SDK_AgeInfo result = new();
            resultmfi.faceNum = mfi.faceNum;
            result.num = infox.num;
            for (int j = 0; j < infox.num; j++)
            {
                //构造类
                result.ageArray.Add(Marshal.PtrToStructure<int>(infox.ageArray));
                resultmfi.faceRect.Add(Marshal.PtrToStructure<MRECT>(mfi.faceRect));
                resultmfi.faceOrient.Add((ASF_OrientCode)Marshal.PtrToStructure<int>(mfi.faceOrient));
                //步进记录(原始)
                mfi.faceRect += Marshal.SizeOf(typeof(MRECT));
                mfi.faceOrient += Marshal.SizeOf(typeof(int));
                infox.ageArray += Marshal.SizeOf(typeof(int));
            }
            return (resultmfi, result);
        }
    }
    /// <summary>
    /// 一个性别检测工具
    /// </summary>
    public sealed class GenderFaceProcess : MultiFaceEngine
    {
        /// <summary>
        /// 一个性别检测工具
        /// </summary>
        /// <param name="appId">Appid</param>
        /// <param name="sdkKey">SdkKey</param>
        /// <param name="dm">检测模式</param>
        /// <param name="op">角度模式</param>
        /// <param name="nScale">最小人脸尺寸</param>
        /// <param name="nMaxFaceNum">最大人脸个数</param>
        public GenderFaceProcess(
            string appId, string sdkKey,
            ASF_DetectMode dm = ASF_DetectMode.ASF_DETECT_MODE_IMAGE,
            ASF_OrientPriority op = ASF_OrientPriority.ASF_OP_0_ONLY,
            int nScale = 32, int nMaxFaceNum = 10) :
            base(appId, sdkKey, dm, op, nScale, nMaxFaceNum)
        {
        }
        (ASF_MultiFaceInfo, ASF_GenderInfo) DetectGenderBase(Image i)
        {
            ASF_MultiFaceInfo info = DetectMultiFaceBase(i);
            var s = (APIResult)NativeFunction.ASFProcessEx(detectEngine, i.GetBitMapPack(), info, (int)Mask.ASF_GENDER);
            if (s != APIResult.MOK)
            {
                throw new Exception($"Process Phase : [{s}] {s.ApiResultToChinese()}");
            }
            var s2 = (APIResult)NativeFunction.ASFGetGender(detectEngine, out ASF_GenderInfo infox);
            if (s2 != APIResult.MOK)
            {
                throw new Exception($"Detect_Gender Phase : [{s2}] {s2.ApiResultToChinese()}");
            }
            return (info, infox);
        }
        /// <summary>
        /// 检测本图片性别
        /// </summary>
        /// <param name="i">图像对象</param>
        /// <returns></returns>
        public new (Model.SDK_MultiFaceInfo mfi, Model.SDK_GenderInfo dfi) Detect(Image i)
        {
            var (mfi, infox) = DetectGenderBase(i);
            Model.SDK_MultiFaceInfo resultmfi = new();
            Model.SDK_GenderInfo result = new();
            resultmfi.faceNum = mfi.faceNum;
            result.num = infox.num;
            for (int j = 0; j < infox.num; j++)
            {
                //构造类
                result.genderArray.Add(Marshal.PtrToStructure<int>(infox.genderArray));
                resultmfi.faceRect.Add(Marshal.PtrToStructure<MRECT>(mfi.faceRect));
                resultmfi.faceOrient.Add((ASF_OrientCode)Marshal.PtrToStructure<int>(mfi.faceOrient));
                //步进记录(原始)
                mfi.faceRect += Marshal.SizeOf(typeof(MRECT));
                mfi.faceOrient += Marshal.SizeOf(typeof(int));
                infox.genderArray += Marshal.SizeOf(typeof(int));
            }
            return (resultmfi, result);
        }
    }
    /// <summary>
    /// 一个面部朝向检测工具
    /// </summary>
    public sealed class AngleFaceProcess : MultiFaceEngine
    {
        /// <summary>
        /// 一个面部朝向检测工具
        /// </summary>
        /// <param name="appId">Appid</param>
        /// <param name="sdkKey">SdkKey</param>
        /// <param name="dm">检测模式</param>
        /// <param name="op">角度模式</param>
        /// <param name="nScale">最小人脸尺寸</param>
        /// <param name="nMaxFaceNum">最大人脸个数</param>
        public AngleFaceProcess(
            string appId, string sdkKey,
            ASF_DetectMode dm = ASF_DetectMode.ASF_DETECT_MODE_IMAGE,
            ASF_OrientPriority op = ASF_OrientPriority.ASF_OP_0_ONLY,
            int nScale = 32, int nMaxFaceNum = 10) :
            base(appId, sdkKey, dm, op, nScale, nMaxFaceNum)
        {
        }
        /// <summary>
        /// 检测本图片面部朝向
        /// </summary>
        /// <param name="i">图像对象</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        (ASF_MultiFaceInfo, ASF_Face3DAngle) Detect3DAngleBase(Image i)
        {
            ASF_MultiFaceInfo info = DetectMultiFaceBase(i);
            var s = (APIResult)NativeFunction.ASFProcessEx(detectEngine, i.GetBitMapPack(), info, (int)Mask.ASF_FACE3DANGLE);
            if (s != APIResult.MOK)
            {
                throw new Exception($"Process Phase : [{s}] {s.ApiResultToChinese()}");
            }
            var s2 = (APIResult)NativeFunction.ASFGetFace3DAngle(detectEngine, out ASF_Face3DAngle infox);
            if (s2 != APIResult.MOK)
            {
                throw new Exception($"Detect_Detect3DAngle Phase : [{s2}] {s2.ApiResultToChinese()}");
            }
            return (info, infox);
        }
        /// <summary>
        /// 检测本图片面部朝向
        /// </summary>
        /// <param name="i">图像对象</param>
        /// <returns></returns>
        public new (Model.SDK_MultiFaceInfo mfi, Model.SDK_Face3DAngle dfi) Detect(Image i)
        {
            var (mfi, infox) = Detect3DAngleBase(i);
            Model.SDK_MultiFaceInfo resultmfi = new();
            Model.SDK_Face3DAngle result = new();
            resultmfi.faceNum = mfi.faceNum;
            result.num = infox.num;
            for (int j = 0; j < infox.num; j++)
            {
                //构造类
                result.pitch.Add(Marshal.PtrToStructure<float>(infox.pitch));
                result.roll.Add(Marshal.PtrToStructure<float>(infox.roll));
                result.yaw.Add(Marshal.PtrToStructure<float>(infox.yaw));
                result.status.Add(Marshal.PtrToStructure<int>(infox.status));
                resultmfi.faceRect.Add(Marshal.PtrToStructure<MRECT>(mfi.faceRect));
                resultmfi.faceOrient.Add((ASF_OrientCode)Marshal.PtrToStructure<int>(mfi.faceOrient));
                //步进记录(原始)
                mfi.faceRect += Marshal.SizeOf(typeof(MRECT));
                mfi.faceOrient += Marshal.SizeOf(typeof(int));
                infox.pitch += Marshal.SizeOf(typeof(float));
                infox.roll += Marshal.SizeOf(typeof(float));
                infox.yaw += Marshal.SizeOf(typeof(float));
                infox.status += Marshal.SizeOf(typeof(int));
            }
            return (resultmfi, result);
        }
    }
    /// <summary>
    /// 一个面部是否活人
    /// </summary>
    public sealed class LivenessFaceProcess : MultiFaceEngine
    {
        /// <summary>
        /// 一个面部是否活人
        /// </summary>
        /// <param name="appId">Appid</param>
        /// <param name="sdkKey">SdkKey</param>
        /// <param name="dm">检测模式</param>
        /// <param name="op">角度模式</param>
        /// <param name="nScale">最小人脸尺寸</param>
        /// <param name="nMaxFaceNum">最大人脸个数</param>
        public LivenessFaceProcess(
            string appId, string sdkKey,
            ASF_DetectMode dm = ASF_DetectMode.ASF_DETECT_MODE_IMAGE,
            ASF_OrientPriority op = ASF_OrientPriority.ASF_OP_0_ONLY,
            int nScale = 32, int nMaxFaceNum = 10) :
            base(appId, sdkKey, dm, op, nScale, nMaxFaceNum)
        {
        }

        /// <summary>
        /// 检测本图片是否活人
        /// </summary>
        /// <param name="i">图像对象</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        (ASF_MultiFaceInfo, ASF_LivenessInfo) DetectLivenessBase(Image i)
        {
            ASF_MultiFaceInfo info = DetectMultiFaceBase(i);
            var s = (APIResult)NativeFunction.ASFProcessEx(detectEngine, i.GetBitMapPack(), info, (int)Mask.ASF_LIVENESS);
            if (s != APIResult.MOK)
            {
                throw new Exception($"Process Phase : [{s}] {s.ApiResultToChinese()}");
            }
            var s2 = (APIResult)NativeFunction.ASFGetLivenessScore(detectEngine, out ASF_LivenessInfo infox);
            if (s2 != APIResult.MOK)
            {
                throw new Exception($"Detect_Detect3DAngle Phase : [{s2}] {s2.ApiResultToChinese()}");
            }
            return (info, infox);
        }
        /// <summary>
        /// 检测本图片是否活人
        /// </summary>
        /// <param name="i">图像对象</param>
        /// <returns></returns>
        public new (Model.SDK_MultiFaceInfo mfi, Model.SDK_LivenessInfo dfi) Detect(Image i)
        {
            var (mfi, infox) = DetectLivenessBase(i);
            Model.SDK_MultiFaceInfo resultmfi = new();
            Model.SDK_LivenessInfo result = new();
            resultmfi.faceNum = mfi.faceNum;
            result.num = infox.num;
            for (int j = 0; j < infox.num; j++)
            {
                //构造类
                result.isLive.Add(Marshal.PtrToStructure<int>(infox.isLive));
                resultmfi.faceRect.Add(Marshal.PtrToStructure<MRECT>(mfi.faceRect));
                resultmfi.faceOrient.Add((ASF_OrientCode)Marshal.PtrToStructure<int>(mfi.faceOrient));
                //步进记录(原始)
                mfi.faceRect += Marshal.SizeOf(typeof(MRECT));
                mfi.faceOrient += Marshal.SizeOf(typeof(int));
                infox.isLive += Marshal.SizeOf(typeof(int));
            }
            return (resultmfi, result);
        }
    }
    /// <summary>
    /// 年龄 性别 角度 检测工具
    /// </summary>
    public sealed class FullFaceProcess : MultiFaceEngine
    {
        /// <summary>
        /// 年龄 性别 角度 检测工具
        /// </summary>
        /// <param name="appId">Appid</param>
        /// <param name="sdkKey">SdkKey</param>
        /// <param name="dm">检测模式</param>
        /// <param name="op">角度模式</param>
        /// <param name="nScale">最小人脸尺寸</param>
        /// <param name="nMaxFaceNum">最大人脸个数</param>
        public FullFaceProcess(
            string appId, string sdkKey,
            ASF_DetectMode dm = ASF_DetectMode.ASF_DETECT_MODE_IMAGE,
            ASF_OrientPriority op = ASF_OrientPriority.ASF_OP_0_ONLY,
            int nScale = 32, int nMaxFaceNum = 10) :
            base(appId, sdkKey, dm, op, nScale, nMaxFaceNum)
        {
        }
        /// <summary>
        /// 检测底
        /// </summary>
        /// <param name="i">图像对象</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        (ASF_MultiFaceInfo, ASF_AgeInfo, ASF_GenderInfo, ASF_Face3DAngle) DetectBase(Image i)
        {
            ASF_MultiFaceInfo info = DetectMultiFaceBase(i);
            var s = (APIResult)NativeFunction.ASFProcessEx(detectEngine, i.GetBitMapPack(), info, (int)(Mask.ASF_AGE | Mask.ASF_GENDER | Mask.ASF_FACE3DANGLE));
            if (s != APIResult.MOK)
            {
                throw new Exception($"Process Phase : [{s}] {s.ApiResultToChinese()}");
            }
            var s1 = (APIResult)NativeFunction.ASFGetAge(detectEngine, out ASF_AgeInfo info1);
            if (s1 != APIResult.MOK)
            {
                throw new Exception($"Detect_Age Phase : [{s1}] {s1.ApiResultToChinese()}");
            }
            var s2 = (APIResult)NativeFunction.ASFGetGender(detectEngine, out ASF_GenderInfo info2);
            if (s2 != APIResult.MOK)
            {
                throw new Exception($"Detect_Gender Phase : [{s2}] {s2.ApiResultToChinese()}");
            }
            var s3 = (APIResult)NativeFunction.ASFGetFace3DAngle(detectEngine, out ASF_Face3DAngle info3);
            if (s3 != APIResult.MOK)
            {
                throw new Exception($"Detect_Detect3DAngle Phase : [{s2}] {s2.ApiResultToChinese()}");
            }
            return (info, info1, info2, info3);
        }
        /// <summary>
        /// 检测本图片
        /// </summary>
        /// <param name="i">图像对象</param>
        /// <returns></returns>
        public new Model.SDK_FaceGeneral Detect(Image i)
        {
            var (mfi,i1,i2,i3) = DetectBase(i);
            Model.SDK_FaceGeneral result = new();
            result.faceNum = mfi.faceNum;
            for (int j = 0; j < mfi.faceNum; j++)
            {
                //构造类
                result.faceRect.Add(Marshal.PtrToStructure<MRECT>(mfi.faceRect));
                result.faceOrient.Add((ASF_OrientCode)Marshal.PtrToStructure<int>(mfi.faceOrient));
                result.ageArray.Add(Marshal.PtrToStructure<int>(i1.ageArray));
                result.genderArray.Add(Marshal.PtrToStructure<int>(i2.genderArray));
                result.pitch.Add(Marshal.PtrToStructure<float>(i3.pitch));
                result.roll.Add(Marshal.PtrToStructure<float>(i3.roll));
                result.yaw.Add(Marshal.PtrToStructure<float>(i3.yaw));
                result.status.Add(Marshal.PtrToStructure<int>(i3.status));
                //步进记录(原始)
                mfi.faceRect += Marshal.SizeOf(typeof(MRECT));
                mfi.faceOrient += Marshal.SizeOf(typeof(int));
                i1.ageArray += Marshal.SizeOf(typeof(int));
                i2.genderArray += Marshal.SizeOf(typeof(int));
                i3.pitch += Marshal.SizeOf(typeof(float));
                i3.roll += Marshal.SizeOf(typeof(float));
                i3.yaw += Marshal.SizeOf(typeof(float));
                i3.status += Marshal.SizeOf(typeof(int));
            }
            return result;
        }
    }
}
*/
