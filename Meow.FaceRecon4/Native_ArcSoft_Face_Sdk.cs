using System.ComponentModel;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Meow.FaceRecon4.NativeSDK
{
    /// <summary>
    /// 原始SDK功能组
    /// </summary>
    public static class NativeFunction
    {
        const string lib = "libarcsoft_face_engine";
        const CallingConvention cc = default;
        public static bool SupressThrowException = false;

        //    return s == APIResult.MOK || (SupressThrowException ? false : throw new Exception($"[{s}] {s.ApiResultToChinese()}"));
        

        /// <summary>
        /// 获取激活文件信息接口
        /// </summary>
        /// <param name="activeFileInfo">[out] 激活文件信息</param>
        /// <returns></returns>
        [DllImport(lib, EntryPoint = nameof(ASFGetActiveFileInfo), CallingConvention = cc)]
        static extern int ASFGetActiveFileInfo(out ASF_ActiveFileInfo activeFileInfo);
        /// <summary>
        /// 获取激活文件信息接口
        /// </summary>
        /// <param name="activeFileInfo">[out] 激活文件信息</param>
        /// <returns></returns>
        public static bool SDKGetActiveFileInfo(out ASF_ActiveFileInfo activeFileInfo)
        {
            var s = (APIResult)ASFGetActiveFileInfo(out activeFileInfo);
            return s == APIResult.MOK || (SupressThrowException ? false : throw new Exception($"[{s}] {s.ApiResultToChinese()}"));
        }

        /// <summary>
        /// 在线激活接口
        /// </summary>
        /// <param name="AppId">[in]  APPID</param>
        /// <param name="SDKKey">[in]  SDKKEY</param>
        /// <param name="ActiveKey">[in]  ActiveKey	官网下载</param>
        /// <returns></returns>
        [DllImport(lib, EntryPoint = nameof(ASFOnlineActivation), CallingConvention = cc)]
        static extern int ASFOnlineActivation(string AppId, string SDKKey, string ActiveKey);
        /// <summary>
        /// 在线激活接口
        /// </summary>
        /// <param name="AppId">[in]  APPID</param>
        /// <param name="SDKKey">[in]  SDKKEY</param>
        /// <param name="ActiveKey">[in]  ActiveKey	官网下载</param>
        /// <returns></returns>
        public static bool SDKOnlineActivation(string AppId, string SDKKey, string ActiveKey)
        {
            var s = (APIResult)ASFOnlineActivation(AppId, SDKKey, ActiveKey);
            if (s != APIResult.MOK)
            {
                if (s == APIResult.MERR_ASF_ALREADY_ACTIVATED)
                {
                    $"Activation Phase : [{s}] {s.ApiResultToChinese()}".ToLog();
                    return true;
                }
                else
                {
                    throw new Exception($"Activate Phase : [{s}] {s.ApiResultToChinese()}");
                }
            }
            else if (s == APIResult.MOK)
            {
                $"Activation Phase : [{s}] {s.ApiResultToChinese()}".ToLog();
                return true;
            }
            else
            {
                throw new Exception($"Init Phase : [{s}] {s.ApiResultToChinese()}");
            }
        }

        /// <summary>
        /// 获取设备信息接口
        /// </summary>
        /// <param name="deviceInfo">[out] 采集的设备信息，用于到开发者中心做离线激活，生成离线授权文件</param>
        /// <returns></returns>
        [DllImport(lib, EntryPoint = nameof(ASFGetActiveDeviceInfo), CallingConvention = cc)]
        static extern int ASFGetActiveDeviceInfo(out string deviceInfo);
        /// <summary>
        /// 获取设备信息接口
        /// </summary>
        /// <param name="deviceInfo">[out] 采集的设备信息，用于到开发者中心做离线激活，生成离线授权文件</param>
        /// <returns></returns>
        public static bool SDKGetActiveDeviceInfo(out string deviceInfo)
        {
            var s = (APIResult)ASFGetActiveDeviceInfo(out deviceInfo);
            return s == APIResult.MOK || (SupressThrowException ? false : throw new Exception($"[{s}] {s.ApiResultToChinese()}"));
        }

        /// <summary>
        /// 离线激活接口
        /// </summary>
        /// <param name="filePath">[in]  许可文件路径(离线授权文件)，需要读写权限</param>
        /// <returns></returns>
        [DllImport(lib, EntryPoint = nameof(ASFOfflineActivation), CallingConvention = cc)]
        static extern int ASFOfflineActivation(string filePath);
        /// <summary>
        /// 离线激活接口
        /// </summary>
        /// <param name="filePath">[in]  许可文件路径(离线授权文件)，需要读写权限</param>
        /// <returns></returns>
        public static bool SDKOfflineActivation(string filePath)
        {
            var s = (APIResult)ASFOfflineActivation(filePath);
            if (s != APIResult.MOK)
            {
                if (s == APIResult.MERR_ASF_ALREADY_ACTIVATED)
                {
                    $"Activation Phase : [{s}] {s.ApiResultToChinese()}".ToLog();
                    return true;
                }
                else
                {
                    throw new Exception($"Activate Phase : [{s}] {s.ApiResultToChinese()}");
                }
            }
            else if (s == APIResult.MOK)
            {
                $"Activation Phase : [{s}] {s.ApiResultToChinese()}".ToLog();
                return true;
            }
            else
            {
                throw new Exception($"Init Phase : [{s}] {s.ApiResultToChinese()}");
            }
        }

        /// <summary>
        /// 初始化引擎
        /// </summary>
        /// <param name="detectMode">
        /// [in] AF_DETECT_MODE_VIDEO 视频模式：适用于摄像头预览，视频文件识别
        /// <para>AF_DETECT_MODE_IMAGE 图片模式：适用于静态图片的识别</para>
        /// </param>
        /// <param name="detectFaceOrientPriority">
        /// [in]	检测脸部的角度优先值，参考 ArcFaceCompare_OrientPriority
        /// </param>
        /// <param name="detectFaceMaxNum">
        /// [in] 最大需要检测的人脸个数
        /// </param>
        /// <param name="combinedMask">
        /// [in] 用户选择需要检测的功能组合，可单个或多个
        /// </param>
        /// <param name="hEngine">
        /// [out] 初始化返回的引擎handle
        /// </param>
        /// <returns></returns>      
        [DllImport(lib, EntryPoint = nameof(ASFInitEngine), CallingConvention = cc)]
        static extern int ASFInitEngine(ASF_DetectMode detectMode, ASF_OrientPriority detectFaceOrientPriority, int detectFaceMaxNum, int combinedMask, out IntPtr hEngine);
        /// <summary>
        /// 初始化引擎
        /// </summary>
        /// <param name="detectMode">
        /// [in] AF_DETECT_MODE_VIDEO 视频模式：适用于摄像头预览，视频文件识别
        /// <para>AF_DETECT_MODE_IMAGE 图片模式：适用于静态图片的识别</para>
        /// </param>
        /// <param name="detectFaceOrientPriority">
        /// [in]	检测脸部的角度优先值，参考 ArcFaceCompare_OrientPriority
        /// </param>
        /// <param name="detectFaceMaxNum">
        /// [in] 最大需要检测的人脸个数
        /// </param>
        /// <param name="combinedMask">
        /// [in] 用户选择需要检测的功能组合，可单个或多个
        /// </param>
        /// <param name="hEngine">
        /// [out] 初始化返回的引擎handle
        /// </param>
        /// <returns></returns>
        public static bool SDKInitEngine(ASF_DetectMode detectMode, ASF_OrientPriority detectFaceOrientPriority, int detectFaceMaxNum, int combinedMask, out IntPtr hEngine)
        {
            var s = (APIResult)ASFInitEngine(detectMode, detectFaceOrientPriority, detectFaceMaxNum,  combinedMask, out hEngine);
            return s == APIResult.MOK || (SupressThrowException ? false : throw new Exception($"[{s}] {s.ApiResultToChinese()}"));
        }

        /// <summary>
        /// 设置面部属性
        /// <para>取值范围[0-1]， 默认阈值均为:0.5， 用户可以根据实际需求，设置遮挡范围</para>
        /// </summary>
        /// <param name="hEngine">[in] 引擎handle</param>
        /// <param name="width"> [in] 人脸属性阈值</param>
        /// <returns></returns>
        [DllImport(lib, EntryPoint = nameof(ASFSetFaceAttributeParam), CallingConvention = cc)]
        public static extern int ASFSetFaceAttributeParam(IntPtr hEngine, ASF_FaceAttributeThreshold width);
        /// <summary>
        /// VIDEO模式:人脸追踪 IMAGE模式:人脸检测
        /// </summary>
        /// <param name="hEngine">[in] 引擎handle</param>
        /// <param name="width">[in] 图片宽度</param>
        /// <param name="height">[in] 图片高度</param>
        /// <param name="format">[in] 颜色空间格式</param>
        /// <param name="imgData">[in] 图片数据</param>
        /// <param name="detectedFaces">[out] 检测到的人脸信息 </param>
        /// <param name="detectModel">[in] 预留字段，当前版本使用默认参数即可</param>
        /// <returns></returns>
        [DllImport(lib, EntryPoint = nameof(ASFDetectFaces), CallingConvention = cc)]
        public static extern int ASFDetectFaces(IntPtr hEngine, int width, int height, int format, IntPtr imgData, out ASF_MultiFaceInfo detectedFaces, ASF_DetectModel detectModel = ASF_DetectModel.ASF_DETECT_MODEL_RGB);
        /// <summary>
        /// VIDEO模式:人脸追踪 IMAGE模式:人脸检测 
        /// <para>图像数据以结构体形式传入，对采用更高字节对齐方式的图像兼容性更好</para>
        /// </summary>
        /// <param name="hEngine">[in] 引擎handle</param>
        /// <param name="imgData">[in] 图片数据</param>
        /// <param name="detectedFaces">[out] 检测到的人脸信息</param>
        /// <param name="detectModel">[in]	预留字段，当前版本使用默认参数即可</param>
        /// <returns></returns>
        [DllImport(lib, EntryPoint = nameof(ASFDetectFacesEx), CallingConvention = cc)]
        public static extern int ASFDetectFacesEx(IntPtr hEngine, ASVLOFFSCREEN imgData, out ASF_MultiFaceInfo detectedFaces, ASF_DetectModel detectModel = ASF_DetectModel.ASF_DETECT_MODEL_RGB);
        /// <summary>
        /// 传入修改后的人脸框，更新人脸信息，用于做双目对齐或其他策略
        /// <para>注意：LPASF_MultiFaceInfo在该接口中既是入参也是出参</para>
        /// </summary>
        /// <param name="hEngine">[in] 引擎handle</param>
        /// <param name="width">[in] 图片宽度</param>
        /// <param name="height">[in] 图片高度</param>
        /// <param name="format">[in] 颜色空间格式</param>
        /// <param name="imgData">[in] 图片数据</param>
        /// <param name="detectedFaces">[out] 检测到的人脸信息</param>
        /// <returns></returns>
        [DllImport(lib, EntryPoint = nameof(ASFUpdateFaceData), CallingConvention = cc)]
        public static extern int ASFUpdateFaceData(IntPtr hEngine, int width, int height, int format, IntPtr imgData, out ASF_MultiFaceInfo detectedFaces);
        /// <summary>
        /// 传入修改后的人脸框，更新人脸信息，用于做双目对齐或其他策略
        /// <para>注意：LPASF_MultiFaceInfo在该接口中既是入参也是出参</para>
        /// </summary>
        /// <param name="hEngine">[in] 引擎handle</param>
        /// <param name="width">[in] 图片宽度</param>
        /// <param name="height">[in] 图片高度</param>
        /// <param name="format">[in] 颜色空间格式</param>
        /// <param name="imgData">[in] 图片数据</param>
        /// <param name="detectedFaces">[in] 检测到的人脸信息</param>
        /// <returns></returns>
        [DllImport(lib, EntryPoint = nameof(ASFUpdateFaceData), CallingConvention = cc)]
        public static extern int ASFUpdateFaceData(IntPtr hEngine, int width, int height, int format, IntPtr imgData, ASF_MultiFaceInfo detectedFaces);
        /// <summary>
        /// 传入修改后的人脸框，更新人脸信息，用于做双目对齐或其他策略
        /// <para>注意：LPASF_MultiFaceInfo在该接口中既是入参也是出参</para>
        /// <para>图像数据以结构体形式传入，对采用更高字节对齐方式的图像兼容性更好</para>
        /// </summary>
        /// <param name="hEngine">[in] 引擎handle</param>
        /// <param name="imgData">[in] 图像数据</param>
        /// <param name="detectedFaces">[out] 检测到的人脸信息</param>
        /// <returns></returns>
        [DllImport(lib, EntryPoint = nameof(ASFUpdateFaceDataEx), CallingConvention = cc)]
        public static extern int ASFUpdateFaceDataEx(IntPtr hEngine, ASVLOFFSCREEN imgData, out ASF_MultiFaceInfo detectedFaces);
        /// <summary>
        /// 传入修改后的人脸框，更新人脸信息，用于做双目对齐或其他策略
        /// <para>注意：LPASF_MultiFaceInfo在该接口中既是入参也是出参</para>
        /// <para>图像数据以结构体形式传入，对采用更高字节对齐方式的图像兼容性更好</para>
        /// </summary>
        /// <param name="hEngine">[in] 引擎handle</param>
        /// <param name="imgData">[in] 图像数据</param>
        /// <param name="detectedFaces">[in] 检测到的人脸信息</param>
        /// <returns></returns>
        [DllImport(lib, EntryPoint = nameof(ASFUpdateFaceDataEx), CallingConvention = cc)]
        public static extern int ASFUpdateFaceDataEx(IntPtr hEngine, ASVLOFFSCREEN imgData, ASF_MultiFaceInfo detectedFaces);
        /// <summary>
        /// 图像质量检测，
        /// <para>（RGB模式： 识别阈值：0.49 注册阈值：0.63  口罩模式：识别阈值：0.29）</para>
        /// </summary>
        /// <param name="hEngine">[in] 引擎handle</param>
        /// <param name="width">[in] 图片宽度</param>
        /// <param name="height">[in] 图片高度</param>
        /// <param name="format">[in] 颜色空间格式</param>
        /// <param name="imgData">[in] 图片数据</param>
        /// <param name="faceInfo">[in] 人脸位置信息</param>
        /// <param name="isMask">[in] 仅支持传入1、0、-1，戴口罩 1，否则认为未戴口罩</param>
        /// <param name="confidenceLevel">[out] float* 图像质量检测结果</param>
        /// <param name="detectModel">[in] 预留字段，当前版本使用默认参数即可</param>
        /// <returns></returns>
        [DllImport(lib, EntryPoint = nameof(ASFImageQualityDetect), CallingConvention = cc)]
        public static extern int ASFImageQualityDetect(IntPtr hEngine, int width, int height, int format, IntPtr imgData, ASF_MultiFaceInfo faceInfo, int isMask, out IntPtr confidenceLevel, ASF_DetectModel detectModel = ASF_DetectModel.ASF_DETECT_MODEL_RGB);
        /// <summary>
        /// 年龄/性别/人脸3D角度/口罩/遮挡/额头区域（该接口仅支持RGB图像）
        /// <para>最多支持4张人脸信息检测，超过部分返回未知</para>
        /// <para>RGB活体仅支持单人脸检测，该接口不支持检测IR活体</para>
        /// </summary>
        /// <param name="hEngine">[in] 引擎handle</param>
        /// <param name="width">[in] 图片宽度</param>
        /// <param name="height">[in] 图片高度</param>
        /// <param name="format">[in] 颜色空间格式</param>
        /// <param name="imgData">[in] 图片数据</param>
        /// <param name="detectedFaces">[in] 人脸信息，用户根据待检测的功能选择需要使用的人脸</param>
        /// <param name="combinedMask">[in] 只支持初始化时候指定需要检测的功能，在process时进一步在这个已经指定的功能集中继续筛选</param>
        /// <returns></returns>
        [DllImport(lib, EntryPoint = nameof(ASFProcess), CallingConvention = cc)]
        public static extern int ASFProcess(IntPtr hEngine, int width, int height, int format, IntPtr imgData, ref ASF_MultiFaceInfo detectedFaces, int combinedMask);
        /// <summary>
        /// 年龄/性别/人脸3D角度/口罩/遮挡/额头区域（该接口仅支持RGB图像）
        /// <para>最多支持4张人脸信息检测，超过部分返回未知</para>
        /// <para>RGB活体仅支持单人脸检测，该接口不支持检测IR活体</para>
        /// <para>图像数据以结构体形式传入，对采用更高字节对齐方式的图像兼容性更好</para>
        /// </summary>
        /// <param name="hEngine">[in] 引擎handle</param>
        /// <param name="imgData">[in] 图片数据</param>
        /// <param name="detectedFaces">[in] 人脸信息，用户根据待检测的功能选择需要使用的人脸。</param>
        /// <param name="combinedMask">[in] 只支持初始化时候指定需要检测的功能，在process时进一步在这个已经指定的功能集中继续筛选</param>
        /// <returns></returns>
        [DllImport(lib, EntryPoint = nameof(ASFProcessEx), CallingConvention = cc)]
        public static extern int ASFProcessEx(IntPtr hEngine, ASVLOFFSCREEN imgData, ref ASF_MultiFaceInfo detectedFaces, int combinedMask);
        /// <summary>
        /// 该接口目前仅支持单人脸IR活体检测
        /// <para>默认取第一张人脸</para>
        /// </summary>
        /// <param name="hEngine">[in] 引擎handle</param>
        /// <param name="width">[in] 图片宽度</param>
        /// <param name="height">[in] 图片高度</param>
        /// <param name="format">[in] 颜色空间格式</param>
        /// <param name="imgData">[in] 图片数据</param>
        /// <param name="detectedFaces">[in] 人脸信息，用户根据待检测的功能选择需要使用的人脸。</param>
        /// <param name="combinedMask">[in] 目前只支持传入ASF_IR_LIVENESS属性的传入，且初始化接口需要传入 </param>
        /// <returns></returns>
        [DllImport(lib, EntryPoint = nameof(ASFProcess_IR), CallingConvention = cc)]
        public static extern int ASFProcess_IR(IntPtr hEngine, int width, int height, int format, IntPtr imgData, out ASF_MultiFaceInfo detectedFaces, int combinedMask);
        /// <summary>
        /// 该接口目前仅支持单人脸IR活体检测（不支持年龄、性别、3D角度的检测）,
        /// <para>默认取第一张人脸</para>
        /// <para>图像数据以结构体形式传入，对采用更高字节对齐方式的图像兼容性更好</para>
        /// </summary>
        /// <param name="hEngine">[in] 引擎handle</param>
        /// <param name="imgData">[in] 图片数据</param>
        /// <param name="detectedFaces">[in] 人脸信息，用户根据待检测的功能选择需要使用的人脸。</param>
        /// <param name="combinedMask">[in] 目前只支持传入ASF_IR_LIVENESS属性的传入，且初始化接口需要传入</param>
        /// <returns></returns>
        [DllImport(lib, EntryPoint = nameof(ASFProcessEx_IR), CallingConvention = cc)]
        public static extern int ASFProcessEx_IR(IntPtr hEngine, ASVLOFFSCREEN imgData, ref ASF_MultiFaceInfo detectedFaces, int combinedMask);
        /// <summary>
        /// 单人脸特征提取
        /// </summary>
        /// <param name="hEngine">[in]	引擎handle</param>
        /// <param name="width">[in] 图片宽度</param>
        /// <param name="height">[in] 图片高度</param>
        /// <param name="format">[in] 颜色空间格式</param>
        /// <param name="imgData">[in] 图片数据</param>
        /// <param name="faceInfo">[in] 单张人脸位置和角度信息</param>
        /// <param name="refisterOrNot">[in] 注册 1 识别为 0</param>
        /// <param name="Mask">[in] 带口罩 1，否则0</param>
        /// <param name="feature">[out] 人脸特征</param>
        /// <returns></returns>
        [DllImport(lib, EntryPoint = nameof(ASFFaceFeatureExtract), CallingConvention = cc)]
        public static extern int ASFFaceFeatureExtract(IntPtr hEngine, int width, int height, int format, IntPtr imgData, ASF_SingleFaceInfo faceInfo, ASF_RegisterOrNot refisterOrNot, int Mask, out ASF_FaceFeature feature);
        /// <summary>
        /// 单人脸特征提取
        /// <para>图像数据以结构体形式传入，对采用更高字节对齐方式的图像兼容性更好</para>
        /// </summary>
        /// <param name="hEngine">[in]	引擎handle</param>
        /// <param name="imgData">[in] 图像数据</param>
        /// <param name="faceInfo">[in] 单张人脸位置和角度信息</param>
        /// <param name="registerOrNot">[in] 注册 1 识别为 0</param>
        /// <param name="Mask">[in] 带口罩 1，否则0</param>
        /// <param name="feature">[out] 人脸特征</param>
        /// <returns></returns>
        [DllImport(lib, EntryPoint = nameof(ASFFaceFeatureExtractEx), CallingConvention = cc)]
        public static extern int ASFFaceFeatureExtractEx(IntPtr hEngine, ASVLOFFSCREEN imgData, ASF_SingleFaceInfo faceInfo, ASF_RegisterOrNot registerOrNot, int Mask, out ASF_FaceFeature feature);
        /// <summary>
        /// 人脸特征比对，
        /// <para>推荐阈值 ASF_LIFE_PHOTO：0.80  ASF_ID_PHOTO：0.82</para>
        /// </summary>
        /// <param name="hEngine">[in] 引擎handle</param>
        /// <param name="feature1">[in] 待比较人脸特征1</param>
        /// <param name="feature2">[in] 待比较人脸特征2</param>
        /// <param name="confidenceLevel">[out] 比较结果，置信度数值</param>
        /// <param name="compareModel">
        /// [in] 特征比对模式
        /// <para>ASF_LIFE_PHOTO：用于生活照之间的特征比对</para>
        /// <para>ASF_ID_PHOTO：用于证件照或证件照和生活照之间的特征比对</para>
        /// </param>
        /// <returns></returns>
        [DllImport(lib, EntryPoint = nameof(ASFFaceFeatureCompare), CallingConvention = cc)]
        public static extern int ASFFaceFeatureCompare(IntPtr hEngine, ASF_FaceFeature feature1, ASF_FaceFeature feature2, out float confidenceLevel, ASF_CompareModel compareModel);
        /// <summary>
        /// 搜索人脸特征
        /// <para>推荐阈值 ASF_LIFE_PHOTO：0.80  ASF_ID_PHOTO：0.82</para>
        /// </summary>
        /// <param name="hEngine">[in] 　引擎handle</param>
        /// <param name="feature">[in] 　待比较人脸特征</param>
        /// <param name="confidenceLevel">[out]　比较结果，最大置信度数值</param>
        /// <param name="featureInfo">[out]  最大置信度人脸信息</param>
        /// <param name="compareModel">[in]   搜索模式</param>
        /// <returns></returns>
        [DllImport(lib, EntryPoint = nameof(ASFFaceFeatureCompare_Search), CallingConvention = cc)]
        public static extern int ASFFaceFeatureCompare_Search(IntPtr hEngine, ASF_FaceFeature feature, out float confidenceLevel, out ASF_FaceFeatureInfo featureInfo, ASF_CompareModel compareModel);
        /// <summary>
        /// 注册人脸特征
        /// </summary>
        /// <param name="hEngine">[in] 　引擎handle</param>
        /// <param name="featureInfoList">[in]   注册列表</param>
        /// <param name="size">[in]   列表数量</param>
        /// <returns></returns>
        [DllImport(lib, EntryPoint = nameof(ASFRegisterFaceFeature), CallingConvention = cc)]
        public static extern int ASFRegisterFaceFeature(IntPtr hEngine, IntPtr featureInfoList, int size);
        /// <summary>
        /// 删除人脸特征
        /// </summary>
        /// <param name="hEngine">[in] 　引擎handle</param>
        /// <param name="searchId">[in] 　待删除人脸ID</param>
        /// <returns></returns>
        [DllImport(lib, EntryPoint = nameof(ASFRemoveFaceFeature), CallingConvention = cc)]
        public static extern int ASFRemoveFaceFeature(IntPtr hEngine, int searchId);
        /// <summary>
        /// 更新人脸特征
        /// </summary>
        /// <param name="hEngine">[in] 　引擎handle</param>
        /// <param name="featureInfo">[in] 　待更新人脸特征</param>
        /// <returns></returns>
        [DllImport(lib, EntryPoint = nameof(ASFUpdateFaceFeature), CallingConvention = cc)]
        public static extern int ASFUpdateFaceFeature(IntPtr hEngine, ASF_FaceFeatureInfo featureInfo);
        /// <summary>
        /// 获取人脸特征
        /// </summary>
        /// <param name="hEngine">[in] 　引擎handle</param>
        /// <param name="searchId">[in] 　查询人脸ID</param>
        /// <param name="featureInfo">[out]  注册时信息</param>
        /// <returns></returns>
        [DllImport(lib, EntryPoint = nameof(ASFGetFaceFeature), CallingConvention = cc)]
        public static extern int ASFGetFaceFeature(IntPtr hEngine, int searchId, ASF_FaceFeatureInfo featureInfo);
        /// <summary>
        /// 获取人脸数量
        /// </summary>
        /// <param name="hEngine">[in] 　引擎handle</param>
        /// <param name="num">[out]  人脸数量</param>
        /// <returns></returns>
        [DllImport(lib, EntryPoint = nameof(ASFGetFaceCount), CallingConvention = cc)]
        public static extern int ASFGetFaceCount(IntPtr hEngine, out int num);
        /// <summary>
        /// 获取年龄信息
        /// </summary>
        /// <param name="hEngine">[in] 引擎handle</param>
        /// <param name="ageInfo">[out] 检测到的年龄信息</param>
        /// <returns></returns>
        [DllImport(lib, EntryPoint = nameof(ASFGetAge), CallingConvention = cc)]
        public static extern int ASFGetAge(IntPtr hEngine, out ASF_AgeInfo ageInfo);
        /// <summary>
        /// 获取性别信息
        /// </summary>
        /// <param name="hEngine">[in] 引擎handle</param>
        /// <param name="genderInfo">[out] 检测到的性别信息</param>
        /// <returns></returns>
        [DllImport(lib, EntryPoint = nameof(ASFGetGender), CallingConvention = cc)]
        public static extern int ASFGetGender(IntPtr hEngine, out ASF_GenderInfo genderInfo);
        /// <summary>
        /// 获取相关活体阈值
        /// </summary>
        /// <param name="hEngine">[in] 引擎handle</param>
        /// <param name="threshold">[out] 活体置信度</param>
        /// <returns></returns>
        [DllImport(lib, EntryPoint = nameof(ASFGetLivenessParam), CallingConvention = cc)]
        public static extern int ASFGetLivenessParam(IntPtr hEngine, out ASF_LivenessThreshold threshold);
        /// <summary>
        /// 设置引擎阈值
        /// <para>取值范围[0-1]，默认值 BGR:0.5 IR:0.7</para>， 
        /// <para>用户可以根据实际需求，设置不同的阈值</para>
        /// </summary>
        /// <param name="hEngine">[in] 引擎handle</param>
        /// <param name="threshold">[in] 活体置信度</param>
        /// <returns></returns>
        [DllImport(lib, EntryPoint = nameof(ASFSetLivenessParam), CallingConvention = cc)]
        public static extern int ASFSetLivenessParam(IntPtr hEngine, ASF_LivenessThreshold threshold);
        /// <summary>
        /// 获取RGB活体结果
        /// </summary>
        /// <param name="hEngine">[in] 引擎handle</param>
        /// <param name="livenessInfo">[out] 检测RGB活体结果</param>
        /// <returns></returns>
        [DllImport(lib, EntryPoint = nameof(ASFGetLivenessScore), CallingConvention = cc)]
        public static extern int ASFGetLivenessScore(IntPtr hEngine, out ASF_LivenessInfo livenessInfo);
        /// <summary>
        /// 获取IR活体结果
        /// </summary>
        /// <param name="hEngine">[in] 引擎handle</param>
        /// <param name="irLivenessInfo">[out] 检测到IR活体结果</param>
        /// <returns></returns>
        [DllImport(lib, EntryPoint = nameof(ASFGetLivenessScore_IR), CallingConvention = cc)]
        public static extern int ASFGetLivenessScore_IR(IntPtr hEngine, out ASF_LivenessInfo irLivenessInfo);
        /// <summary>
        /// 获取口罩检测的结果
        /// </summary>
        /// <param name="hEngine">[in] 引擎handle</param>
        /// <param name="p3DAngleInfo">[out] 检测到的口罩检测相关</param>
        /// <returns></returns>
        [DllImport(lib, EntryPoint = nameof(ASFGetMask), CallingConvention = cc)]
        public static extern int ASFGetMask(IntPtr hEngine, out ASF_MaskInfo p3DAngleInfo);
        /// <summary>
        /// 获取3D角度信息
        /// </summary>
        /// <param name="hEngine">[in] 引擎handle</param>
        /// <param name="p3DAngleInfo">[out] 检测到脸部3D 角度信息</param>
        /// <returns></returns>
        [Obsolete("新版文档未提及",true)]
        [DllImport(lib, EntryPoint = nameof(ASFGetFace3DAngle), CallingConvention = cc)]
        public static extern int ASFGetFace3DAngle(IntPtr hEngine, out ASF_Face3DAngle p3DAngleInfo);
        /// <summary>
        /// 销毁引擎
        /// </summary>
        /// <param name="hEngine">[in] 引擎handle</param>
        /// <returns></returns>
        [DllImport(lib, EntryPoint = nameof(ASFUninitEngine), CallingConvention = cc)]
        public static extern int ASFUninitEngine(IntPtr hEngine);
    }
}