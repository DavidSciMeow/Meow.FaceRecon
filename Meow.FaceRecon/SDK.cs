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
        /// 图像对象列表
        /// </summary>
        protected List<ASVLOFFSCREEN> imgdata = new();


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

            var s = (APIResult)NativeFunction.ASFActivation(appId, sdkKey);
            $"Activation Phase : [{s}] {s.ApiResultToChinese()}".ToLog();
            if (s != APIResult.MOK)
            {
                if (s == APIResult.MERR_ASF_ALREADY_ACTIVATED)
                {
                    IsActivate = true;
                }
                else
                {
                    throw new Exception($"Activate Phase : [{s}] {s.ApiResultToChinese()}");
                }
            }
            if (APIResult.MOK != (APIResult)NativeFunction.ASFInitEngine(dm, op, nScale, nMaxFaceNum, (int)mode, out detectEngine))
            {
                throw new Exception($"Init Phase : [{s}] {s.ApiResultToChinese()}");
            }
        }
        /// <summary>
        /// 添加图片
        /// </summary>
        /// <param name="i">Image对象</param>
        public void ImportGraph(Image i) => imgdata.Add(i.GetBitMapPack());


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
                    APIResult s = (APIResult)NativeSDK.NativeFunction.ASFUninitEngine(detectEngine);
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
        /// 检测人脸
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception">初始化操作失败</exception>
        public List<Model.SDK_MultiFaceInfo> DetectAllMultiFaceInlist()
        {
            List<Model.SDK_MultiFaceInfo> retVal = new();
            foreach(var k in imgdata)
            {
                var s = (APIResult)NativeFunction.ASFDetectFacesEx(detectEngine, k, out ASF_MultiFaceInfo info);
                if (s != APIResult.MOK)
                {
                    throw new Exception($"Detect Phase : [{s}] {s.ApiResultToChinese()}");
                }
                Model.SDK_MultiFaceInfo result = new();
                for (int i = 0; i < info.faceNum; i++)
                {
                    //构造类
                    result.faceRect.Add(Marshal.PtrToStructure<MRECT>(info.faceRect));
                    result.faceOrient.Add((ASF_OrientCode)Marshal.PtrToStructure<int>(info.faceOrient));
                    //步进记录(原始)
                    info.faceRect += Marshal.SizeOf(typeof(MRECT));
                    info.faceOrient += Marshal.SizeOf(typeof(int));
                }
                retVal.Add(result);
            }
            return retVal;
        }
        /// <summary>
        /// 使用引擎检测本图片
        /// </summary>
        /// <param name="i">图像对象</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Model.SDK_MultiFaceInfo DetectMultiFace(Image i)
        {
            var s = (APIResult)NativeFunction.ASFDetectFacesEx(detectEngine, i.GetBitMapPack(), out ASF_MultiFaceInfo info);
            if (s != APIResult.MOK)
            {
                throw new Exception($"Detect Phase : [{s}] {s.ApiResultToChinese()}");
            }
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
}
