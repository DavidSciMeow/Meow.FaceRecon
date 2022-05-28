using SkiaSharp;
using System.Drawing;

namespace Meow.FaceRecon
{
    /// <summary>
    /// 静态操作获取(自动管理池)
    /// </summary>
    public class FaceReconPool
    {
        string WinKey;
        string LinuxKey;
        /// <summary>
        /// 应用程序识别号
        /// </summary>
        public string Appid { get;private set; }
        /// <summary>
        /// Key
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// 生成一个自动管理引擎池
        /// </summary>
        /// <param name="appid">应用程序识别号</param>
        /// <param name="winKey">winKey</param>
        /// <param name="linuxKey">LinuxKey</param>
        /// <param name="dm">检测模式</param>
        /// <param name="op">角度模式</param>
        /// <param name="nScale">最小人脸尺寸</param>
        /// <param name="nMaxFaceNum">最大人脸个数</param>
        public FaceReconPool(string appid, string winKey, string linuxKey)
        {
            Appid = appid;
            WinKey = winKey;
            LinuxKey = linuxKey;
            PlatformID plid = Environment.OSVersion.Platform;
            if (plid == PlatformID.Unix)
            {
                $"EnginePool Init Phase : [ETIL] SystemTypeIsLinux".ToLog();
                Key = LinuxKey;
            }
            else if (plid == PlatformID.Win32NT)
            {
                $"EnginePool Init Phase : [ETIW] SystemTypeIsWindows".ToLog();
                Key = WinKey;
            }
            else
            {
                $"EnginePool Init Phase : [EISD] 无法判断操作系统类型,您的系统可能不被虹软支持".ToLog();
                throw new Exception("EnginePool Init Phase : [EISD] 无法判断操作系统类型,您的系统可能不被虹软支持");
            }
            $"EnginePool Init Phase : [OK] 已经实例化密钥管理池".ToLog();
        }
    }
}