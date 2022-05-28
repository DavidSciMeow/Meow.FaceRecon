using Meow.FaceRecon4.NativeSDK;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meow.FaceRecon4
{
    /// <summary>
    /// 基类引擎
    /// </summary>
    public class Engine : IDisposable
    {
        /// <summary>
        /// 引擎指针
        /// </summary>
        protected IntPtr DetectEngine = IntPtr.Zero;
        /// <summary>
        /// 检测模式
        /// </summary>
        public ASF_DetectMode Detectmode { get; }
        /// <summary>
        /// 角度优先值
        /// </summary>
        public ASF_OrientPriority DetectFaceOrientPriority { get; }
        /// <summary>
        /// 最大人脸个数
        /// </summary>
        public int DetectFaceMaxNum { get; }
        /// <summary>
        /// 需要检测的人脸功能组合
        /// </summary>
        public Mask CombineMask { get; }
        /// <summary>
        /// 初始化基类引擎
        /// </summary>
        /// <param name="appid">应用程序识别号</param>
        /// <param name="winKey">winKey</param>
        /// <param name="linuxKey">LinuxKey</param>
        /// <param name="winactiveKey">winActiveKey</param>
        /// <param name="linuxactiveKey">LinuxActiveKey</param>
        /// <param name="dm">检测模式</param>
        /// <param name="op">角度模式</param>
        /// <param name="nmax">最大人脸检测个数</param>
        /// <param name="mode">引擎检测模式</param>
        /// <exception cref="Exception"></exception>
        public Engine(
            ASF_DetectMode dm = ASF_DetectMode.ASF_DETECT_MODE_IMAGE,
            ASF_OrientPriority op = ASF_OrientPriority.ASF_OP_0_ONLY,
            int nmax = 10,
            Mask mode = Mask.ASF_NONE)
        {
            Detectmode = dm;
            DetectFaceOrientPriority = op;
            DetectFaceMaxNum = nmax;
            CombineMask = mode;
            if (FaceReconPool.IsActivate)
            {
                var s = (APIResult)NativeFunction.ASFInitEngine(dm, op, nmax, (int)mode, out DetectEngine);
                if (s != APIResult.MOK)
                {
                    $"Init Phase : [{s}] {s.ApiResultToChinese()}".ToLog();
                }
            }
            else
            {
                $"Init Phase : [AERR] 全局引擎未激活,请使用FaceReconPool类激活".ToLog();
                throw new Exception("Init Phase : [AERR] 全局引擎未激活,请使用FaceReconPool类激活");
            }
        }


        /*Dispose Interface*/
        private bool disposedValue;
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
                    APIResult s = (APIResult)NativeFunction.ASFUninitEngine(DetectEngine);
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

    public sealed class MultiFaceEngine : Engine
    {
        /// <summary>
        /// 生成一个多人脸检测工具(也可以检测单个人脸)
        /// </summary>
        /// <param name="dm">检测模式</param>
        /// <param name="op">角度模式</param>
        /// <param name="nMaxFaceNum">最大人脸个数</param>
        /// <param name="mode">引擎检测模式</param>
        public MultiFaceEngine(
            ASF_DetectMode dm = ASF_DetectMode.ASF_DETECT_MODE_IMAGE,
            ASF_OrientPriority op = ASF_OrientPriority.ASF_OP_0_ONLY,
            int nMaxFaceNum = 10,
            Mask mode = Mask.ASF_FACE_DETECT) :
            base(dm, op, nMaxFaceNum, mode)
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
            var s = (APIResult)NativeFunction.ASFDetectFaces(DetectEngine, w, h, (int)ColorSpace.ASVL_PAF_RGB24_B8G8R8, ip, out ASF_MultiFaceInfo info);
            if (s != APIResult.MOK)
            {
                throw new Exception($"Detect_Face Phase : [{s}] {s.ApiResultToChinese()}");
            }
            return info;
        }
    }
}
