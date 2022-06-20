using Meow.FaceRecon4.NativeSDK;

namespace Meow.FaceRecon4
{
    /// <summary>
    /// 静态操作获取(自动管理池)
    /// </summary>
    public class FaceReconPool
    {
        /// <summary>
        /// 引擎是否激活
        /// </summary>
        public static bool IsActivate { get; private set; }
        /// <summary>
        /// 在线激活引擎
        /// </summary>
        /// <param name="appid">应用程序识别号</param>
        /// <param name="winKey">winKey</param>
        /// <param name="linuxKey">LinuxKey</param>
        /// <param name="winactiveKey">winActiveKey</param>
        /// <param name="linuxactiveKey">LinuxActiveKey</param>
        public FaceReconPool(
            string appid,
            string winKey, string linuxKey,
            string winactiveKey, string linuxactiveKey)
        {
            PlatformID plid = Environment.OSVersion.Platform;
            string key;
            string activeKey;
            if (plid == PlatformID.Unix)
            {
                $"EnginePool Init Phase : [ETIL] SystemTypeIsLinux".ToLog();
                key = linuxKey;
                activeKey = linuxactiveKey;
            }
            else if (plid == PlatformID.Win32NT)
            {
                $"EnginePool Init Phase : [ETIW] SystemTypeIsWindows".ToLog();
                key = winKey;
                activeKey = winactiveKey;
            }
            else
            {
                $"EnginePool Init Phase : [EISD] 无法判断操作系统类型,您的系统可能不被虹软支持".ToLog();
                throw new Exception("EnginePool Init Phase : [EISD] 无法判断操作系统类型,您的系统可能不被虹软支持");
            }
            $"EnginePool Init Phase : [OK] 已经实例化密钥管理池".ToLog();
            IsActivate = NativeFunction.SDKOnlineActivation(appid, key, activeKey);
        }
        /// <summary>
        /// 离线激活引擎
        /// </summary>
        /// <param name="offlineactivepath">离线激活证书存放位置</param>
        /// <exception cref="Exception"></exception>
        public FaceReconPool(string offlineactivepath)
        {
            $"EnginePool Init Phase : [OK] 使用离线激活方案".ToLog();
            if (File.Exists(offlineactivepath))
            {
                $"EnginePool Init Phase : [FFOK] 文件路径: {offlineactivepath}".ToLog();
            }
            else
            {
                $"EnginePool Init Phase : [FNFE] 激活文件未找到".ToLog();
                throw new Exception("EnginePool Init Phase : [FNFE] 激活文件未找到");
            }
            IsActivate = NativeFunction.SDKOfflineActivation(offlineactivepath);
        }
    }
}
