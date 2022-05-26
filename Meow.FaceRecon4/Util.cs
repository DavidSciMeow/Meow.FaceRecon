using Meow.FaceRecon4.NativeSDK;
using Meow.FaceRecon4.SDK.Model;
using SkiaSharp;
using System.Runtime.InteropServices;

namespace Meow.FaceRecon4
{
    /// <summary>
    /// 工具类
    /// </summary>
    public static class Util
    {
        /// <summary>
        /// [Meow扩展]日志
        /// </summary>
        /// <param name="log">具体内容</param>
        /// <param name="serverity">程度</param>
        /// <returns></returns>
        public static void ToLog(this string log, int serverity = 0)
        {
            if (SDK.GlobalSetting.LogMode <= serverity)
            {
                Console.ForegroundColor = serverity switch
                {
                    0 => ConsoleColor.White,
                    1 => ConsoleColor.Yellow,
                    2 => ConsoleColor.Red,
                    _ => ConsoleColor.Blue,
                };
                Console.WriteLine($"[Meow.FR:{serverity}:{DateTime.Now:T}] {log}");
                Console.ResetColor();
            }
        }
        /// <summary>
        /// [Meow扩展]转化APIResult到中文
        /// </summary>
        /// <param name="a">APIResult</param>
        /// <returns></returns>
        public static string ApiResultToChinese(this APIResult a)
        {
            return a switch
            {
                APIResult.MOK => "OK",
                APIResult.MERR_UNKNOWN => "错误原因不明",
                APIResult.MERR_INVALID_PARAM => "无效的参数",
                APIResult.MERR_UNSUPPORTED => "引擎不支持",
                APIResult.MERR_NO_MEMORY => "内存不足",
                APIResult.MERR_BAD_STATE => "状态错误",
                APIResult.MERR_USER_CANCEL => "用户取消相关操作",
                APIResult.MERR_EXPIRED => "操作时间过期",
                APIResult.MERR_USER_PAUSE => "用户暂停操作",
                APIResult.MERR_BUFFER_OVERFLOW => "缓冲上溢",
                APIResult.MERR_BUFFER_UNDERFLOW => "缓冲下溢",
                APIResult.MERR_NO_DISKSPACE => "存贮空间不足",
                APIResult.MERR_COMPONENT_NOT_EXIST => "组件不存在",
                APIResult.MERR_GLOBAL_DATA_NOT_EXIST => "全局数据不存在",
                APIResult.MERR_FSDK_BASE => "Free SDK通用错误类型",
                APIResult.MERR_FSDK_INVALID_APP_ID => "无效的App Id",
                APIResult.MERR_FSDK_INVALID_SDK_ID => "无效的SDK key",
                APIResult.MERR_FSDK_INVALID_ID_PAIR => "AppId和SDKKey不匹配",
                APIResult.MERR_FSDK_MISMATCH_ID_AND_SDK => "SDKKey 和使用的SDK 不匹配",
                APIResult.MERR_FSDK_SYSTEM_VERSION_UNSUPPORTED => "系统版本不被当前SDK所支持",
                APIResult.MERR_FSDK_LICENCE_EXPIRED => "SDK有效期过期，需要重新下载更新",
                APIResult.MERR_FSDK_FR_ERROR_BASE => "Face Recognition错误类型",
                APIResult.MERR_FSDK_FR_INVALID_MEMORY_INFO => "无效的输入内存",
                APIResult.MERR_FSDK_FR_INVALID_IMAGE_INFO => "无效的输入图像参数",
                APIResult.MERR_FSDK_FR_INVALID_FACE_INFO => "无效的脸部信息",
                APIResult.MERR_FSDK_FR_NO_GPU_AVAILABLE => "当前设备无GPU可用",
                APIResult.MERR_FSDK_FR_MISMATCHED_FEATURE_LEVEL => "待比较的两个人脸特征的版本不一致",
                APIResult.MERR_FSDK_FACEFEATURE_ERROR_BASE => "人脸特征检测错误类型",
                APIResult.MERR_FSDK_FACEFEATURE_UNKNOWN => "人脸特征检测错误未知",
                APIResult.MERR_FSDK_FACEFEATURE_MEMORY => "人脸特征检测内存错误",
                APIResult.MERR_FSDK_FACEFEATURE_INVALID_FORMAT => "人脸特征检测格式错误",
                APIResult.MERR_FSDK_FACEFEATURE_INVALID_PARAM => "人脸特征检测参数错误",
                APIResult.MERR_FSDK_FACEFEATURE_LOW_CONFIDENCE_LEVEL => "人脸特征检测结果置信度低",
                APIResult.MERR_ASF_EX_BASE => "ASF错误类型",
                APIResult.MERR_ASF_EX_FEATURE_UNSUPPORTED_ON_INIT => "Engine不支持的检测属性",
                APIResult.MERR_ASF_EX_FEATURE_UNINITED => "需要检测的属性未初始化",
                APIResult.MERR_ASF_EX_FEATURE_UNPROCESSED => "待获取的属性未在process中处理过",
                APIResult.MERR_ASF_EX_FEATURE_UNSUPPORTED_ON_PROCESS => "PROCESS不支持的检测属性组合，例如FR，有自己独立的处理函数",
                APIResult.MERR_ASF_EX_INVALID_IMAGE_INFO => "无效的输入图像",
                APIResult.MERR_ASF_EX_INVALID_FACE_INFO => "无效的脸部信息",
                APIResult.MERR_ASF_BASE => "人脸比对基础错误类型",
                APIResult.MERR_ASF_ACTIVATION_FAIL => "SDK激活失败,请打开读写权限",
                APIResult.MERR_ASF_ALREADY_ACTIVATED => "SDK已激活",
                APIResult.MERR_ASF_NOT_ACTIVATED => "SDK未激活",
                APIResult.MERR_ASF_SCALE_NOT_SUPPORT => "detectFaceScaleVal 不支持",
                APIResult.MERR_ASF_ACTIVEFILE_SDKTYPE_MISMATCH => "激活文件与SDK类型不匹配，请确认使用的sdk",
                APIResult.MERR_ASF_DEVICE_MISMATCH => "设备不匹配",
                APIResult.MERR_ASF_UNIQUE_IDENTIFIER_ILLEGAL => "唯一标识不合法",
                APIResult.MERR_ASF_PARAM_NULL => "参数为空",
                APIResult.MERR_ASF_LIVENESS_EXPIRED => "活体已过期",
                APIResult.MERR_ASF_VERSION_NOT_SUPPORT => "版本不支持",
                APIResult.MERR_ASF_SIGN_ERROR => "签名错误",
                APIResult.MERR_ASF_DATABASE_ERROR => "激活信息保存异常",
                APIResult.MERR_ASF_UNIQUE_CHECKOUT_FAIL => "唯一标识符校验失败",
                APIResult.MERR_ASF_COLOR_SPACE_NOT_SUPPORT => "颜色空间不支持",
                APIResult.MERR_ASF_IMAGE_WIDTH_HEIGHT_NOT_SUPPORT => "图片宽高不支持，宽度需四字节对齐",
                APIResult.MERR_ASF_READ_PHONE_STATE_DENIED => "android.permission.READ_PHONE_STATE权限被拒绝",
                APIResult.MERR_ASF_ACTIVATION_DATA_DESTROYED => "激活数据被破坏,请删除激活文件，重新进行激活",
                APIResult.MERR_ASF_SERVER_UNKNOWN_ERROR => "服务端未知错误",
                APIResult.MERR_ASF_INTERNET_DENIED => "INTERNET权限被拒绝",
                APIResult.MERR_ASF_ACTIVEFILE_SDK_MISMATCH => "激活文件与SDK版本不匹配,请重新激活",
                APIResult.MERR_ASF_DEVICEINFO_LESS => "设备信息太少，不足以生成设备指纹",
                APIResult.MERR_ASF_LOCAL_TIME_NOT_CALIBRATED => "客户端时间与服务器时间（即北京时间）前后相差在30分钟以上",
                APIResult.MERR_ASF_APPID_DATA_DECRYPT => "数据校验异常",
                APIResult.MERR_ASF_APPID_APPKEY_SDK_MISMATCH => "传入的AppId和AppKey与使用的SDK版本不一致",
                APIResult.MERR_ASF_NO_REQUEST => "短时间大量请求会被禁止请求,30分钟之后解封",
                APIResult.MERR_ASF_ACTIVE_FILE_NO_EXIST => "激活文件不存在",
                APIResult.MERR_ASF_DETECT_MODEL_UNSUPPORTED => "检测模型不支持，请查看对应接口说明，使用当前支持的检测模型",
                APIResult.MERR_ASF_CURRENT_DEVICE_TIME_INCORRECT => "当前设备时间不正确，请调整设备时间",
                APIResult.MERR_ASF_ACTIVATION_QUANTITY_OUT_OF_LIMIT => "年度激活数量超出限制，次年清零",
                APIResult.MERR_ASF_NETWORK_BASE => "网络错误类型",
                APIResult.MERR_ASF_NETWORK_COULDNT_RESOLVE_HOST => "无法解析主机地址",
                APIResult.MERR_ASF_NETWORK_COULDNT_CONNECT_SERVER => "无法连接服务器",
                APIResult.MERR_ASF_NETWORK_CONNECT_TIMEOUT => "网络连接超时",
                APIResult.MERR_ASF_NETWORK_UNKNOWN_ERROR => "网络未知错误",
                APIResult.MERR_ASF_ACTIVEKEY_BASE => "激活码错误类型",
                APIResult.MERR_ASF_ACTIVEKEY_COULDNT_CONNECT_SERVER => "无法连接激活服务器",
                APIResult.MERR_ASF_ACTIVEKEY_SERVER_SYSTEM_ERROR => "服务器系统错误",
                APIResult.MERR_ASF_ACTIVEKEY_POST_PARM_ERROR => "请求参数错误",
                APIResult.MERR_ASF_ACTIVEKEY_PARM_MISMATCH => "ACTIVEKEY与APPID、SDKKEY不匹配",
                APIResult.MERR_ASF_ACTIVEKEY_ACTIVEKEY_ACTIVATED => "ACTIVEKEY已经被使用",
                APIResult.MERR_ASF_ACTIVEKEY_ACTIVEKEY_FORMAT_ERROR => "ACTIVEKEY信息异常",
                APIResult.MERR_ASF_ACTIVEKEY_APPID_PARM_MISMATCH => "ACTIVEKEY与APPID不匹配",
                APIResult.MERR_ASF_ACTIVEKEY_SDK_FILE_MISMATCH => "SDK与激活文件版本不匹配",
                APIResult.MERR_ASF_ACTIVEKEY_EXPIRED => "ACTIVEKEY已过期",
                APIResult.MERR_ASF_ACTIVEKEY_DEVICE_OUT_OF_LIMIT => "批量授权激活码设备数超过限制",
                APIResult.MERR_ASF_OFFLINE_BASE => "离线激活错误码类型",
                APIResult.MERR_ASF_LICENSE_FILE_NOT_EXIST => "离线授权文件不存在或无读写权限",
                APIResult.MERR_ASF_LICENSE_FILE_DATA_DESTROYED => "离线授权文件已损坏",
                APIResult.MERR_ASF_LICENSE_FILE_SDK_MISMATCH => "离线授权文件与SDK版本不匹配",
                APIResult.MERR_ASF_LICENSE_FILEINFO_SDKINFO_MISMATCH => "离线授权文件与SDK信息不匹配",
                APIResult.MERR_ASF_LICENSE_FILE_FINGERPRINT_MISMATCH => "离线授权文件与设备指纹不匹配",
                APIResult.MERR_ASF_LICENSE_FILE_EXPIRED => "离线授权文件已过期",
                APIResult.MERR_ASF_LOCAL_EXIST_USEFUL_ACTIVE_FILE => "离线授权文件不可用，本地原有激活文件可继续使用",
                APIResult.MERR_ASF_LICENSE_FILE_VERSION_TOO_LOW => "离线授权文件版本过低，请使用新版本激活助手重新进行离线激活",
                _ => "未知错误",
            };
        }
        /// <summary>
        /// [Meow扩展]获取打包好的Pack
        /// </summary>
        /// <param name="b"></param>
        /// <param name="w"></param>
        /// <param name="h"></param>
        /// <param name="ip"></param>
        /// <exception cref="Exception"></exception>
        public static void GetBitMapPackX(this SKBitmap b, out int w, out int h, out IntPtr ip)
        {
            int w_ = b.Width - 4;
            for (int j_ = w_; j_ < b.Width + 4; j_++)
            {
                if (j_ % 4 == 0)
                {
                    w_ = j_;
                    break;
                }
            }
            using SKBitmap bp = new(w_, b.Height, SKColorType.Rgb888x, SKAlphaType.Opaque);
            if (!b.ScalePixels(bp, SKFilterQuality.None))
            {
                throw new Exception($"IMG_Skia Phase : [exc] 图像处理异常,无法放缩图像");
            }
            h = bp.Height;
            w = bp.Width;
            var p = bp.Width * 3; //stride
            var arr = new byte[h * p];
            int i = 0;
            int j = 0;
            foreach (var k in bp.Bytes)
            {
                if (i++ == 3)
                {
                    i = 0;
                    continue;
                }
                else
                {
                    arr[j++] = k;
                }
            }
            ip = Marshal.AllocHGlobal(arr.Length);
            Marshal.Copy(arr, 0, ip, arr.Length);
        }
        /// <summary>
        /// [Meow扩展]获取打包好的Pack
        /// </summary>
        /// <param name="b">ImageArray</param>
        /// <returns></returns>
        public static ASVLOFFSCREEN GetBitMapPack(this SKBitmap b)
        {
            int w_ = b.Width - 4;
            for (int j_ = w_; j_ < b.Width + 4; j_++)
            {
                if (j_ % 4 == 0)
                {
                    w_ = j_;
                    break;
                }
            }
            using SKBitmap bp = new(w_, b.Height, SKColorType.Rgb888x, SKAlphaType.Opaque);
            if (!b.ScalePixels(bp, SKFilterQuality.None))
            {
                throw new Exception($"IMG_Skia Phase : [exc] 图像处理异常,无法放缩图像");
            }
            var h = bp.Height;
            var w = bp.Width;
            var p = bp.Width * 3; //stride
            var arr = new byte[h * p];
            int i = 0;
            int j = 0;
            foreach (var k in bp.Bytes)
            {
                if (i++ == 3)
                {
                    i = 0;
                    continue;
                }
                else
                {
                    arr[j++] = k;
                }
            }
            IntPtr ip = Marshal.AllocHGlobal(arr.Length);
            Marshal.Copy(arr, 0, ip, arr.Length);
            ASVLOFFSCREEN o = new();
            o.u32PixelArrayFormat = (int)ColorSpace.ASVL_PAF_RGB24_B8G8R8;
            o.ppu8Plane = new IntPtr[4] { ip, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero };
            o.i32Width = w;
            o.i32Height = h;
            o.pi32Pitch = new int[4] { p, 0, 0, 0 };
            return o;
        }
        /// <summary>
        /// 图上绘制框和文字
        /// </summary>
        /// <param name="b">图</param>
        /// <param name="m">方框位置</param>
        /// <param name="text">字体</param>
        /// <param name="Color">颜色</param>
        /// <param name="fontName">字体库</param>
        public static SKBitmap DrawStringAndRect(this SKBitmap b,
            MRECT m, string text, SKColor Color, string fontName = "微软雅黑")
        {
            var textsize = (m.bottom - m.top) / 2;
            var alignleftp = (m.left + (Math.Abs(m.right - m.left)) / 2);
            var aligntopp = (m.top + (Math.Abs(m.bottom - m.top)) / 2) + textsize / 2;
            using SKSurface surface = SKSurface.Create(b.Info);
            using SKCanvas c = surface.Canvas;
            c.DrawBitmap(b, 0, 0);
            c.DrawRoundRect(new SKRoundRect(new SKRect(m.left, m.top, m.right, m.bottom), 5f), new()
            {
                StrokeWidth = 5,
                Color = Color,
                Style = SKPaintStyle.Stroke,
            });
            c.DrawText(text, new SKPoint(alignleftp, aligntopp), new()
            {
                Color = Color,
                TextEncoding = SKTextEncoding.Utf8,
                TextAlign = SKTextAlign.Center,
                Typeface = SKTypeface.FromFamilyName(fontName),
                TextSize = textsize,
            });
            c.Save();
            return SKBitmap.Decode(surface.Snapshot().Encode(SKEncodedImageFormat.Jpeg, 100));
        }
        /// <summary>
        /// 图上绘制框和文字 (默认年龄,男蓝框女粉框未检测出来黑框)
        /// </summary>
        /// <param name="b"></param>
        /// <param name="t"></param>
        /// <param name="fontName"></param>
        /// <returns></returns>
        public static SKBitmap DrawStringAndRect(this SKBitmap b,
            SDK_Faces t, string fontName = "微软雅黑")
        {
            return b.DrawStringAndRect(
                t.faceRect,
                $"{t.age}",
                (t.gender == 1 ? new(255, 192, 203, 150) : (t.gender == 0 ? new(65, 105, 225, 150) : new(0, 0, 0, 150))),
                fontName
                );
        }
        /// <summary>
        /// [Meow扩展]将Image对象转换为Base64
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static string ToBase64String(this SKImage i)
        {
            MemoryStream ms = new();
            i.Encode(SkiaSharp.SKEncodedImageFormat.Jpeg, 100).SaveTo(ms);
            byte[] arr = new byte[ms.Length];
            ms.Position = 0;
            ms.Read(arr, 0, (int)ms.Length);
            ms.Close();
            return Convert.ToBase64String(arr);
        }
        /// <summary>
        /// 保存位图图像
        /// </summary>
        /// <param name="i"></param>
        /// <param name="path">路径</param>
        /// <param name="f">图像格式</param>
        /// <param name="Quality">质量参数</param>
        public static void Save(this SKBitmap i, string path, SKEncodedImageFormat f = SKEncodedImageFormat.Jpeg, int Quality = 100) => i.Encode(f, Quality).SaveTo(File.OpenWrite(path));
        /// <summary>
        /// [Meow扩展]将Bitmap对象转换为Base64
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static string ToBase64String(this SKBitmap b) => SKImage.FromBitmap(b).ToBase64String();
        /// <summary>
        /// [Meow扩展]将Base64字符串转换成Image对象
        /// </summary>
        /// <param name="base64String"></param>
        /// <returns></returns>
        public static SKBitmap Base64ToSKBitmap(this string base64String) => SKBitmap.Decode(Convert.FromBase64String(base64String));
       
        /// <summary>
        /// 转换默认类型到SDK类型
        /// </summary>
        /// <param name="info">原始类型</param>
        /// <returns></returns>
        public static SDK_MultiFaceInfo InfoToSDKInfo(this ASF_MultiFaceInfo info)
        {
            SDK_MultiFaceInfo result = new();
            result.faceNum = info.faceNum;
            for (int j = 0; j < info.faceNum; j++)
            {
                //构造类
                result.faceRect.Add(Marshal.PtrToStructure<MRECT>(info.faceRect));
                result.faceOrient.Add((ASF_OrientCode)Marshal.PtrToStructure<int>(info.faceOrient));
                result.faceID.Add(Marshal.PtrToStructure<int>(info.faceID));
                var k = Marshal.PtrToStructure<ASF_FaceDataInfo>(info.faceDataInfoList);
                result.faceDataInfoList.Add(new() { Data = Marshal.PtrToStringAuto(k.data),DataSize = k.dataSize });
                result.faceIsWithinBoundary.Add(Marshal.PtrToStructure<int>(info.faceIsWithinBoundary));
                result.foreheadRect.Add(Marshal.PtrToStructure<MRECT>(info.foreheadRect));
                var k2 = Marshal.PtrToStructure<ASF_FaceAttributeInfo>(info.faceAttributeInfo);
                result.faceAttributeInfo.Add(new SDK_FaceAttributeInfo() { WearGlass = k2.wearGlass, LeftEyeOpen = k2.leftEyeOpen == 1,RightEyeOpen = k2.rightEyeOpen == 1, MouthClose = k2.mouthClose == 1});
                var k3 = Marshal.PtrToStructure<ASF_Face3DAngle>(info.face3DAngleInfo);
                result.face3DAngleInfo.Add(new SDK_Face3DAngle() { pitch = k3.pitch, roll = k3.roll, yaw = k3.yaw});

                //步进记录(原始)
                info.faceRect += Marshal.SizeOf(typeof(MRECT));
                info.faceOrient += Marshal.SizeOf(typeof(int));
                info.faceID += Marshal.SizeOf(typeof(int));
                info.faceDataInfoList += Marshal.SizeOf(typeof(ASF_FaceDataInfo));
                info.faceIsWithinBoundary += Marshal.SizeOf(typeof(int));
                info.foreheadRect += Marshal.SizeOf(typeof(int));
                info.faceAttributeInfo += Marshal.SizeOf(typeof(ASF_FaceAttributeInfo));
                info.face3DAngleInfo += Marshal.SizeOf(typeof(ASF_Face3DAngle));

            }
            return result;
        }
    }
}
