using Meow.FaceRecon.NativeSDK;
using SkiaSharp;
using System.Runtime.InteropServices;

namespace Meow.FaceRecon.SDK
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
            Mask mode = Mask.ASF_NONE)
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

        /*Dispose Interface*/
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
    public sealed class MultiFaceEngine : Engine
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
        /// <param name="mode">引擎检测模式</param>
        public MultiFaceEngine(
            string appId, string sdkKey,
            ASF_DetectMode dm = ASF_DetectMode.ASF_DETECT_MODE_IMAGE,
            ASF_OrientPriority op = ASF_OrientPriority.ASF_OP_0_ONLY,
            int nScale = 32, int nMaxFaceNum = 10,
            Mask mode = Mask.ASF_FACE_DETECT) :
            base(appId, sdkKey, dm, op, nScale, nMaxFaceNum, mode)
        {
        }
        /// <summary>
        /// 使用引擎检测本图片(底)
        /// </summary>
        /// <param name="fp">文件</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public ASF_MultiFaceInfo Detect(SKBitmap fp)
        {
            fp.GetBitMapPackX(out var w, out var h, out var ip);
            var s = (APIResult)NativeFunction.ASFDetectFaces(detectEngine, w, h, (int)ColorSpace.ASVL_PAF_RGB24_B8G8R8, ip, out ASF_MultiFaceInfo info);
            if (s != APIResult.MOK)
            {
                throw new Exception($"Detect_Face Phase : [{s}] {s.ApiResultToChinese()}");
            }
            return info;
        }
    }
    /// <summary>
    /// 一个年龄检测工具
    /// </summary>
    public sealed class AgeFaceProcess : Engine
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
            base(appId, sdkKey, dm, op, nScale, nMaxFaceNum, Mask.ASF_AGE)
        {
        }
        ASF_AgeInfo DetectAgeBase(SKBitmap fp, ASF_MultiFaceInfo info)
        {
            fp.GetBitMapPackX(out var w, out var h, out var ip);
            var s = (APIResult)NativeFunction.ASFProcess(detectEngine, w, h, (int)ColorSpace.ASVL_PAF_RGB24_B8G8R8, ip, ref info, (int)Mask.ASF_AGE);
            if (s != APIResult.MOK)
            {
                throw new Exception($"Process Phase : [{s}] {s.ApiResultToChinese()}");
            }
            var s2 = (APIResult)NativeFunction.ASFGetAge(detectEngine, out ASF_AgeInfo infox);
            if (s2 != APIResult.MOK)
            {
                throw new Exception($"Detect_Age Phase : [{s2}] {s2.ApiResultToChinese()}");
            }
            return infox;
        }
        /// <summary>
        /// 检测本图片年龄
        /// </summary>
        /// <param name="fp">文件位置</param>
        /// <param name="info">人脸信息</param>
        /// <returns></returns>
        public Model.SDK_AgeInfo Detect(SKBitmap fp, ASF_MultiFaceInfo info)
        {
            var infox = DetectAgeBase(fp, info);
            Model.SDK_AgeInfo result = new();
            result.num = infox.num;
            for (int j = 0; j < infox.num; j++)
            {
                //构造类
                result.ageArray.Add(Marshal.PtrToStructure<int>(infox.ageArray));
                //步进记录(原始)
                infox.ageArray += Marshal.SizeOf(typeof(int));
            }
            return result;
        }
    }
    /// <summary>
    /// 一个性别检测工具
    /// </summary>
    public sealed class GenderFaceProcess : Engine
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
            base(appId, sdkKey, dm, op, nScale, nMaxFaceNum, Mask.ASF_GENDER)
        {
        }
        ASF_GenderInfo DetectGenderBase(SKBitmap fp, ASF_MultiFaceInfo info)
        {
            fp.GetBitMapPackX(out var w, out var h, out var ip);
            var s = (APIResult)NativeFunction.ASFProcess(detectEngine, w, h, (int)ColorSpace.ASVL_PAF_RGB24_B8G8R8, ip, ref info, (int)Mask.ASF_GENDER);
            if (s != APIResult.MOK)
            {
                throw new Exception($"Process Phase : [{s}] {s.ApiResultToChinese()}");
            }
            var s2 = (APIResult)NativeFunction.ASFGetGender(detectEngine, out ASF_GenderInfo infox);
            if (s2 != APIResult.MOK)
            {
                throw new Exception($"Detect_Gender Phase : [{s2}] {s2.ApiResultToChinese()}");
            }
            return infox;
        }
        /// <summary>
        /// 检测本图片性别
        /// </summary>
        /// <param name="fp">图像对象</param>
        /// <param name="info">人脸识别区</param>
        /// <returns></returns>
        public Model.SDK_GenderInfo Detect(SKBitmap fp, ASF_MultiFaceInfo info)
        {
            var infox = DetectGenderBase(fp, info);
            Model.SDK_GenderInfo result = new();
            result.num = infox.num;
            for (int j = 0; j < infox.num; j++)
            {
                //构造类
                result.genderArray.Add(Marshal.PtrToStructure<int>(infox.genderArray));
                //步进记录(原始)
                infox.genderArray += Marshal.SizeOf(typeof(int));
            }
            return result;
        }
    }
    /// <summary>
    /// 一个面部朝向检测工具
    /// </summary>
    public sealed class AngleFaceProcess : Engine
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
            base(appId, sdkKey, dm, op, nScale, nMaxFaceNum, Mask.ASF_FACE3DANGLE)
        {
        }

        /// <summary>
        /// 检测本图片面部朝向
        /// </summary>
        /// <param name="i">图像对象</param>
        /// <param name="info">人脸识别区</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        ASF_Face3DAngle Detect3DAngleBase(SKBitmap i, ASF_MultiFaceInfo info)
        {
            i.GetBitMapPackX(out var w, out var h, out var ip);
            var s = (APIResult)NativeFunction.ASFProcess(detectEngine, w, h, (int)ColorSpace.ASVL_PAF_RGB24_B8G8R8, ip, ref info, (int)Mask.ASF_FACE3DANGLE);
            if (s != APIResult.MOK)
            {
                throw new Exception($"Process Phase : [{s}] {s.ApiResultToChinese()}");
            }
            var s2 = (APIResult)NativeFunction.ASFGetFace3DAngle(detectEngine, out ASF_Face3DAngle infox);
            if (s2 != APIResult.MOK)
            {
                throw new Exception($"Detect_Detect3DAngle Phase : [{s2}] {s2.ApiResultToChinese()}");
            }
            return infox;
        }
        /// <summary>
        /// 检测本图片面部朝向
        /// </summary>
        /// <param name="i">图像对象</param>
        /// <param name="info">人脸识别区</param>
        /// <returns></returns>
        public Model.SDK_Face3DAngle Detect(SKBitmap i, ASF_MultiFaceInfo info)
        {
            var infox = Detect3DAngleBase(i,info);
            Model.SDK_Face3DAngle result = new();
            result.num = infox.num;
            for (int j = 0; j < infox.num; j++)
            {
                //构造类
                result.pitch.Add(Marshal.PtrToStructure<float>(infox.pitch));
                result.roll.Add(Marshal.PtrToStructure<float>(infox.roll));
                result.yaw.Add(Marshal.PtrToStructure<float>(infox.yaw));
                result.status.Add(Marshal.PtrToStructure<int>(infox.status));
                //步进记录(原始)
                infox.pitch += Marshal.SizeOf(typeof(float));
                infox.roll += Marshal.SizeOf(typeof(float));
                infox.yaw += Marshal.SizeOf(typeof(float));
                infox.status += Marshal.SizeOf(typeof(int));
            }
            return result;
        }
    }
    /// <summary>
    /// 一个面部是否活人
    /// </summary>
    public sealed class LivenessFaceProcess : Engine
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
            base(appId, sdkKey, dm, op, nScale, nMaxFaceNum, Mask.ASF_LIVENESS)
        {
        }

        /// <summary>
        /// 检测本图片是否活人
        /// </summary>
        /// <param name="i">图像对象</param>
        /// <param name="info">人脸识别区</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        ASF_LivenessInfo DetectLivenessBase(SKBitmap i, ASF_MultiFaceInfo info)
        {
            i.GetBitMapPackX(out var w, out var h, out var ip);
            var s = (APIResult)NativeFunction.ASFProcess(detectEngine, w, h, (int)ColorSpace.ASVL_PAF_RGB24_B8G8R8, ip, ref info, (int)Mask.ASF_LIVENESS);
            if (s != APIResult.MOK)
            {
                throw new Exception($"Process Phase : [{s}] {s.ApiResultToChinese()}");
            }
            var s2 = (APIResult)NativeFunction.ASFGetLivenessScore(detectEngine, out ASF_LivenessInfo infox);
            if (s2 != APIResult.MOK)
            {
                throw new Exception($"Detect_Detect3DAngle Phase : [{s2}] {s2.ApiResultToChinese()}");
            }
            return infox;
        }
        /// <summary>
        /// 检测本图片是否活人
        /// </summary>
        /// <param name="i">图像对象</param>
        /// <param name="info">人脸识别区</param>
        /// <returns></returns>
        public Model.SDK_LivenessInfo Detect(SKBitmap i, ASF_MultiFaceInfo info)
        {
            var infox = DetectLivenessBase(i,info);
            Model.SDK_LivenessInfo result = new();
            result.num = infox.num;
            for (int j = 0; j < infox.num; j++)
            {
                //构造类
                result.isLive.Add(Marshal.PtrToStructure<int>(infox.isLive));
                //步进记录(原始)
                infox.isLive += Marshal.SizeOf(typeof(int));
            }
            return result;
        }
    }
}
