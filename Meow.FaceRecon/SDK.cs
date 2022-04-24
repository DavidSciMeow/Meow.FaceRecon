using Meow.FaceRecon.NativeSDK;
using System.Drawing;
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
            Mask mode = Mask.ASF_ALL)
        {
            AppId = appId;
            SdkKey = sdkKey;
            Mode = dm;
            Orient = op;
            Scale = nScale;
            MaxFaceNum = nMaxFaceNum;
            DetectedMask = mode;
            if (APIResult.MOK != (APIResult)NativeFunction.ASFInitEngine(dm, op, nScale, nMaxFaceNum, (int)mode, out detectEngine))
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
                else
                {
                    throw new Exception($"Init Phase : [{s}] {s.ApiResultToChinese()}");
                }
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
        public Model.SDK_MultiFaceInfo DetectMultiFace(Image i)
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
    public class AgeFaceProcess : MultiFaceEngine
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
        /// <summary>
        /// 检测本图片年龄
        /// </summary>
        /// <param name="i">图像对象</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        protected (ASF_MultiFaceInfo,ASF_AgeInfo) DetectAgeBase(Image i)
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
        public (Model.SDK_MultiFaceInfo, Model.SDK_AgeInfo) DetectAge(Image i)
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
    public class GenderFaceProcess : MultiFaceEngine
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

        /// <summary>
        /// 检测本图片性别
        /// </summary>
        /// <param name="i">图像对象</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        protected (ASF_MultiFaceInfo, ASF_GenderInfo) DetectAgeBase(Image i)
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
        public (Model.SDK_MultiFaceInfo, Model.SDK_GenderInfo) DetectAge(Image i)
        {
            var (mfi, infox) = DetectAgeBase(i);
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
}
