﻿using SkiaSharp;
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
        /// 检测模式
        /// </summary>
        public NativeSDK.ASF_DetectMode DetMode { get; private set; } = NativeSDK.ASF_DetectMode.ASF_DETECT_MODE_IMAGE;
        /// <summary>
        /// 角度模式
        /// </summary>
        public NativeSDK.ASF_OrientPriority OrientPriority { get; private set; } = NativeSDK.ASF_OrientPriority.ASF_OP_0_ONLY;
        /// <summary>
        /// 最小人脸尺寸
        /// </summary>
        public int Scale { get; private set; } = 32;
        /// <summary>
        /// 最大人脸个数
        /// </summary>
        public int MaxFaceNumber { get; private set; } = 10;

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
        public FaceReconPool(string appid, string winKey, string linuxKey, 
            NativeSDK.ASF_DetectMode dm = NativeSDK.ASF_DetectMode.ASF_DETECT_MODE_IMAGE, 
            NativeSDK.ASF_OrientPriority op = NativeSDK.ASF_OrientPriority.ASF_OP_0_ONLY, 
            int nScale = 0, int nMaxFaceNum = 0)
        {
            Appid = appid;
            WinKey = winKey;
            LinuxKey = linuxKey;
            DetMode = dm;
            OrientPriority = op;
            Scale = nScale;
            MaxFaceNumber = nMaxFaceNum;
            PlatformID plid = Environment.OSVersion.Platform;
            if (plid == PlatformID.Unix)
            {
                AppContext.SetSwitch("System.Drawing.EnableUnixSupport", true); //设置linux模式(.net6)
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
            $"EnginePool Init Phase : [OK] 已经实例化引擎管理池".ToLog();
        }
        /// <summary>
        /// 获取多个人脸信息
        /// </summary>
        /// <param name="i">图片</param>
        /// <returns></returns>
        public Task<SDK.Model.SDK_MultiFaceInfo> DetMultiFaceAsync(Image i) 
            => Task.Factory.StartNew(() =>
            {
                $"EnginePool Task Phase : [OT_MFA] 正在检测多人脸信息".ToLog();
                using var e = new SDK.MultiFaceEngine(Appid, Key);
                var k = e.Detect(i);
                $"EnginePool Task Phase : [OF_MFA] 多人脸信息检测完成".ToLog();
                return k;
            });
        /// <summary>
        /// 获取年龄和人脸信息
        /// </summary>
        /// <param name="i">图片</param>
        /// <returns></returns>
        public Task<(SDK.Model.SDK_MultiFaceInfo mfi, SDK.Model.SDK_AgeInfo ageinfo)> DetFaceAgeAsync(Image i) 
            => Task.Factory.StartNew(() =>
            {
                $"EnginePool Task Phase : [OT_AGE] 正在检测年龄信息".ToLog();
                using var e = new SDK.AgeFaceProcess(Appid, Key);
                var k = e.Detect(i);
                $"EnginePool Task Phase : [OF_AGE] 年龄信息检测完成".ToLog();
                return k;
            });
        /// <summary>
        /// 获取性别和人脸信息
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public Task<(SDK.Model.SDK_MultiFaceInfo mfi, SDK.Model.SDK_GenderInfo genderinfo)> DetFaceGenderAsync(Image i)
            => Task.Factory.StartNew(() =>
            {
                $"EnginePool Task Phase : [OT_GDR] 正在检测性别信息".ToLog();
                using var e = new SDK.GenderFaceProcess(Appid, Key);
                var k = e.Detect(i);
                $"EnginePool Task Phase : [OF_GDR] 性别信息检测完成".ToLog();
                return k;
            });
        /// <summary>
        /// 获取人脸朝向和人脸信息
        /// </summary>
        /// <param name="i">图片</param>
        /// <returns></returns>
        public Task<(SDK.Model.SDK_MultiFaceInfo mfi, SDK.Model.SDK_Face3DAngle face3dangle)> DetFaceAngleAsync(Image i)
            => Task.Factory.StartNew(() =>
            {
                $"EnginePool Task Phase : [OT_FDA] 正在检测人脸朝向信息".ToLog();
                using var e = new SDK.AngleFaceProcess(Appid, Key);
                var k = e.Detect(i);
                $"EnginePool Task Phase : [OF_FDA] 人脸朝向信息检测完成".ToLog();
                return k;
            });
        /// <summary>
        /// 获取人脸真实程度和人脸信息
        /// </summary>
        /// <param name="i">图片</param>
        /// <returns></returns>
        public Task<(SDK.Model.SDK_MultiFaceInfo mfi, SDK.Model.SDK_LivenessInfo livenessinfo)> DetFaceLivenessAsync(Image i)
            => Task.Factory.StartNew(() =>
            {
                $"EnginePool Task Phase : [OT_LIF] 正在检测人脸真实程度信息".ToLog();
                using var e = new SDK.LivenessFaceProcess(Appid, Key);
                var k = e.Detect(i);
                $"EnginePool Task Phase : [OF_LIF] 人脸真实程度信息检测完成".ToLog();
                return k;
            });
        /// <summary>
        /// 获取总体(除了真实判定)的所有信息
        /// </summary>
        /// <param name="i">图片</param>
        /// <returns></returns>
        public Task<SDK.Model.SDK_FaceGeneral> DetAllFaceAsync(Image i)
            => Task.Factory.StartNew(() =>
            {
                $"EnginePool Task Phase : [OT_ALL] 正在检测所有人脸信息".ToLog();
                using var e = new SDK.FullFaceProcess(Appid, Key);
                var k = e.Detect(i);
                $"EnginePool Task Phase : [OF_ALL] 所有人脸信息检测完成".ToLog();
                return k;
            });
    }
}
namespace Meow.FaceRecon.Skia
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
        public string Appid { get; private set; }
        /// <summary>
        /// Key
        /// </summary>
        public string Key { get; private set; }
        /// <summary>
        /// 检测模式
        /// </summary>
        public NativeSDK.ASF_DetectMode DetMode { get; private set; } = NativeSDK.ASF_DetectMode.ASF_DETECT_MODE_IMAGE;
        /// <summary>
        /// 角度模式
        /// </summary>
        public NativeSDK.ASF_OrientPriority OrientPriority { get; private set; } = NativeSDK.ASF_OrientPriority.ASF_OP_0_ONLY;
        /// <summary>
        /// 最小人脸尺寸
        /// </summary>
        public int Scale { get; private set; } = 32;
        /// <summary>
        /// 最大人脸个数
        /// </summary>
        public int MaxFaceNumber { get; private set; } = 10;

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
        public FaceReconPool(string appid, string winKey, string linuxKey,
            NativeSDK.ASF_DetectMode dm = NativeSDK.ASF_DetectMode.ASF_DETECT_MODE_IMAGE,
            NativeSDK.ASF_OrientPriority op = NativeSDK.ASF_OrientPriority.ASF_OP_0_ONLY,
            int nScale = 0, int nMaxFaceNum = 0)
        {
            Appid = appid;
            WinKey = winKey;
            LinuxKey = linuxKey;
            DetMode = dm;
            OrientPriority = op;
            Scale = nScale;
            MaxFaceNumber = nMaxFaceNum;
            PlatformID plid = Environment.OSVersion.Platform;
            if (plid == PlatformID.Unix)
            {
                AppContext.SetSwitch("System.Drawing.EnableUnixSupport", true); //设置linux模式(.net6)
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
            $"EnginePool Init Phase : [OK] 已经实例化引擎管理池".ToLog();
        }
        /// <summary>
        /// 获取多个人脸信息
        /// </summary>
        /// <param name="i">图片</param>
        /// <returns></returns>
        public Task<SDK.Model.SDK_MultiFaceInfo> DetMultiFaceAsync(SKBitmap i)
            => Task.Factory.StartNew(() =>
            {
                $"EnginePool Task Phase : [OT_MFA] 正在检测多人脸信息".ToLog();
                using var e = new SDK.Skia.MultiFaceEngine(Appid, Key);
                var k = e.Detect(i);
                $"EnginePool Task Phase : [OF_MFA] 多人脸信息检测完成".ToLog();
                return k;
            });
        /// <summary>
        /// 获取年龄和人脸信息
        /// </summary>
        /// <param name="i">图片</param>
        /// <returns></returns>
        public Task<(SDK.Model.SDK_MultiFaceInfo mfi, SDK.Model.SDK_AgeInfo ageinfo)> DetFaceAgeAsync(SKBitmap i)
            => Task.Factory.StartNew(() =>
            {
                $"EnginePool Task Phase : [OT_AGE] 正在检测年龄信息".ToLog();
                using var e = new SDK.Skia.AgeFaceProcess(Appid, Key);
                var k = e.Detect(i);
                $"EnginePool Task Phase : [OF_AGE] 年龄信息检测完成".ToLog();
                return k;
            });
        /// <summary>
        /// 获取性别和人脸信息
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public Task<(SDK.Model.SDK_MultiFaceInfo mfi, SDK.Model.SDK_GenderInfo genderinfo)> DetFaceGenderAsync(SKBitmap i)
            => Task.Factory.StartNew(() =>
            {
                $"EnginePool Task Phase : [OT_GDR] 正在检测性别信息".ToLog();
                using var e = new SDK.Skia.GenderFaceProcess(Appid, Key);
                var k = e.Detect(i);
                $"EnginePool Task Phase : [OF_GDR] 性别信息检测完成".ToLog();
                return k;
            });
        /// <summary>
        /// 获取人脸朝向和人脸信息
        /// </summary>
        /// <param name="i">图片</param>
        /// <returns></returns>
        public Task<(SDK.Model.SDK_MultiFaceInfo mfi, SDK.Model.SDK_Face3DAngle face3dangle)> DetFaceAngleAsync(SKBitmap i)
            => Task.Factory.StartNew(() =>
            {
                $"EnginePool Task Phase : [OT_FDA] 正在检测人脸朝向信息".ToLog();
                using var e = new SDK.Skia.AngleFaceProcess(Appid, Key);
                var k = e.Detect(i);
                $"EnginePool Task Phase : [OF_FDA] 人脸朝向信息检测完成".ToLog();
                return k;
            });
        /// <summary>
        /// 获取人脸真实程度和人脸信息
        /// </summary>
        /// <param name="i">图片</param>
        /// <returns></returns>
        public Task<(SDK.Model.SDK_MultiFaceInfo mfi, SDK.Model.SDK_LivenessInfo livenessinfo)> DetFaceLivenessAsync(SKBitmap i)
            => Task.Factory.StartNew(() =>
            {
                $"EnginePool Task Phase : [OT_LIF] 正在检测人脸真实程度信息".ToLog();
                using var e = new SDK.Skia.LivenessFaceProcess(Appid, Key);
                var k = e.Detect(i);
                $"EnginePool Task Phase : [OF_LIF] 人脸真实程度信息检测完成".ToLog();
                return k;
            });
        /// <summary>
        /// 获取总体(除了真实判定)的所有信息
        /// </summary>
        /// <param name="i">图片</param>
        /// <returns></returns>
        public Task<SDK.Model.SDK_FaceGeneral> DetAllFaceAsync(SKBitmap i)
            => Task.Factory.StartNew(() =>
            {
                $"EnginePool Task Phase : [OT_ALL] 正在检测所有人脸信息".ToLog();
                using var e = new SDK.Skia.FullFaceProcess(Appid, Key);
                var k = e.Detect(i);
                $"EnginePool Task Phase : [OF_ALL] 所有人脸信息检测完成".ToLog();
                return k;
            });
    }
}