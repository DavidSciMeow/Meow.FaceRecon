using System.Reflection;
using System.Runtime.InteropServices;

namespace Meow.FaceRecon.NativeSDK
{
    /// <summary>
    /// 原始SDK功能组
    /// </summary>
    public static class NativeFunction
    {
        const CharSet cs = CharSet.Auto;
        const string lib = "libarcsoft_face_engine";

        /// <summary>
        /// 获取激活文件信息接口
        /// </summary>
        /// <param name="activeFileInfo">[out] 激活文件信息</param>
        /// <returns></returns>
        [DllImport(lib, EntryPoint = nameof(ASFGetActiveFileInfo), CallingConvention = CallingConvention.Cdecl)]
        public static extern int ASFGetActiveFileInfo(ref ASF_ActiveFileInfo activeFileInfo);
        /// <summary>
        /// 在线激活接口
        /// </summary>
        /// <param name="AppId">[in]  APPID</param>
        /// <param name="SDKKey">[in]  SDKKEY</param>
        /// <returns></returns>
        [Obsolete("使用ASFActivation完成")]
        [DllImport(lib, EntryPoint = nameof(ASFOnlineActivation), CallingConvention = CallingConvention.Cdecl)]
        public static extern int ASFOnlineActivation(string AppId, string SDKKey);
        /// <summary>
        /// 在线激活接口，该接口与ASFOnlineActivation接口功能一致，推荐使用该接口
        /// </summary>
        /// <param name="AppId">[in]  APPID</param>
        /// <param name="SDKKey">[in]  SDKKEY</param>
        /// <returns></returns>
        [DllImport(lib, EntryPoint = nameof(ASFActivation), CallingConvention = CallingConvention.Cdecl)]
        public static extern int ASFActivation(string AppId, string SDKKey);
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
        /// <param name="detectFaceScaleVal">
        /// [in] 用于数值化表示的最小人脸尺寸，该尺寸代表人脸尺寸相对于图片长边的占比
        /// <para>video 模式有效值范围[2, 32], 推荐值为 16</para>
        /// <para>image 模式有效值范围[2, 32], 推荐值为 32</para>
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
        [DllImport(lib, EntryPoint = nameof(ASFInitEngine), CallingConvention = CallingConvention.Cdecl)]
        public static extern int ASFInitEngine(ASF_DetectMode detectMode, ASF_OrientPriority detectFaceOrientPriority, int detectFaceScaleVal, int detectFaceMaxNum, int combinedMask, out IntPtr hEngine);
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
        [DllImport(lib, EntryPoint = nameof(ASFDetectFaces), CallingConvention = CallingConvention.Cdecl)]
        public static extern int ASFDetectFaces(IntPtr hEngine, int width, int height, int format, IntPtr imgData, ref ASF_MultiFaceInfo detectedFaces, ASF_DetectModel detectModel = ASF_DetectModel.ASF_DETECT_MODEL_RGB);
        /// <summary>
        /// VIDEO模式:人脸追踪 IMAGE模式:人脸检测 
        /// <para>图像数据以结构体形式传入，对采用更高字节对齐方式的图像兼容性更好</para>
        /// </summary>
        /// <param name="hEngine">[in] 引擎handle</param>
        /// <param name="imgData">[in] 图片数据</param>
        /// <param name="detectedFaces">[out] 检测到的人脸信息</param>
        /// <param name="detectModel">[in]	预留字段，当前版本使用默认参数即可</param>
        /// <returns></returns>
        [DllImport(lib, EntryPoint = nameof(ASFDetectFacesEx), CallingConvention = CallingConvention.Cdecl)]
        public static extern int ASFDetectFacesEx(IntPtr hEngine, ASVLOFFSCREEN imgData, out ASF_MultiFaceInfo detectedFaces, ASF_DetectModel detectModel = ASF_DetectModel.ASF_DETECT_MODEL_RGB);
        /// <summary>
        /// 设置引擎阈值
        /// <para>取值范围[0-1]，默认值 BGR:0.5 IR:0.7</para>， 
        /// <para>用户可以根据实际需求，设置不同的阈值</para>
        /// </summary>
        /// <param name="hEngine">[in] 引擎handle</param>
        /// <param name="threshold">[in] 活体置信度</param>
        /// <returns></returns>
        [DllImport(lib, EntryPoint = nameof(ASFSetLivenessParam), CallingConvention = CallingConvention.Cdecl)]
        public static extern int ASFSetLivenessParam(IntPtr hEngine, ref ASF_LivenessThreshold threshold);
        /// <summary>
        /// 年龄/性别/人脸3D角度（该接口仅支持RGB图像）
        /// <para>最多支持4张人脸信息检测，超过部分返回未知</para>
        /// <para>RGB活体仅支持单人脸检测，该接口不支持检测IR活体</para>
        /// </summary>
        /// <param name="hEngine">[in] 引擎handle</param>
        /// <param name="width">[in] 图片宽度</param>
        /// <param name="height">[in] 图片高度</param>
        /// <param name="format">[in] 颜色空间格式</param>
        /// <param name="imgData">[in] 图片数据</param>
        /// <param name="detectedFaces">[in] 人脸信息，用户根据待检测的功能选择需要使用的人脸。</param>
        /// <param name="combinedMask">[in] 只支持初始化时候指定需要检测的功能，在process时进一步在这个已经指定的功能集中继续筛选</param>
        /// <returns></returns>
        [DllImport(lib, EntryPoint = nameof(ASFProcess), CallingConvention = CallingConvention.Cdecl)]
        public static extern int ASFProcess(IntPtr hEngine, int width, int height, int format, IntPtr imgData, ref ASF_MultiFaceInfo detectedFaces, int combinedMask);
        /// <summary>
        /// 年龄/性别/人脸3D角度（该接口仅支持RGB图像）
        /// <para>最多支持4张人脸信息检测，超过部分返回未知</para>
        /// <para>RGB活体仅支持单人脸检测，该接口不支持检测IR活体</para>
        /// <para>图像数据以结构体形式传入，对采用更高字节对齐方式的图像兼容性更好</para>
        /// </summary>
        /// <param name="hEngine">[in] 引擎handle</param>
        /// <param name="imgData">[in] 图片数据</param>
        /// <param name="detectedFaces">[in] 人脸信息，用户根据待检测的功能选择需要使用的人脸。</param>
        /// <param name="combinedMask">[in] 只支持初始化时候指定需要检测的功能，在process时进一步在这个已经指定的功能集中继续筛选</param>
        /// <returns></returns>
        [DllImport(lib, EntryPoint = nameof(ASFProcessEx), CallingConvention = CallingConvention.Cdecl)]
        public static extern int ASFProcessEx(IntPtr hEngine, ASVLOFFSCREEN imgData, out ASF_MultiFaceInfo detectedFaces, int combinedMask);
        /// <summary>
        /// 该接口目前仅支持单人脸IR活体检测（不支持年龄、性别、3D角度的检测）
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
        [DllImport(lib, EntryPoint = nameof(ASFProcess_IR), CallingConvention = CallingConvention.Cdecl)]
        public static extern int ASFProcess_IR(IntPtr hEngine, int width, int height, int format, IntPtr imgData, ref ASF_MultiFaceInfo detectedFaces, int combinedMask);
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
        [DllImport(lib, EntryPoint = nameof(ASFProcessEx_IR), CallingConvention = CallingConvention.Cdecl)]
        public static extern int ASFProcessEx_IR(IntPtr hEngine, ASVLOFFSCREEN imgData, ref ASF_MultiFaceInfo detectedFaces, int combinedMask);
        /// <summary>
        /// 销毁引擎
        /// </summary>
        /// <param name="hEngine">[in] 引擎handle</param>
        /// <returns></returns>
        [DllImport(lib, EntryPoint = nameof(ASFUninitEngine), CallingConvention = CallingConvention.Cdecl)]
        public static extern int ASFUninitEngine(IntPtr hEngine);
        /// <summary>
        /// 获取版本信息
        /// </summary>
        /// <returns></returns>
        [DllImport(lib, EntryPoint = nameof(ASFGetVersion), CallingConvention = CallingConvention.Cdecl)]
        public static extern ASF_VERSION ASFGetVersion();
        /// <summary>
        /// 单人脸特征提取
        /// </summary>
        /// <param name="hEngine">[in]	引擎handle</param>
        /// <param name="width">[in] 图片宽度</param>
        /// <param name="height">[in] 图片高度</param>
        /// <param name="format">[in] 颜色空间格式</param>
        /// <param name="imgData">[in] 图片数据</param>
        /// <param name="faceInfo">[in] 单张人脸位置和角度信息</param>
        /// <param name="feature">[out] 人脸特征</param>
        /// <returns></returns>
        [DllImport(lib, EntryPoint = nameof(ASFFaceFeatureExtract), CallingConvention = CallingConvention.Cdecl)]
        public static extern int ASFFaceFeatureExtract(IntPtr hEngine, int width, int height, int format, IntPtr imgData, ref ASF_SingleFaceInfo faceInfo, ref ASF_FaceFeature feature);
        /// <summary>
        /// 单人脸特征提取
        /// <para>图像数据以结构体形式传入</para>
        /// <para>对采用更高字节对齐方式的图像兼容性更好</para>
        /// </summary>
        /// <param name="hEngine">[in]	引擎handle</param>
        /// <param name="imgData">[in] 图像数据</param>
        /// <param name="faceInfo">[in] 单张人脸位置和角度信息</param>
        /// <param name="feature">[out] 人脸特征</param>
        /// <returns></returns>
        [DllImport(lib, EntryPoint = nameof(ASFFaceFeatureExtractEx), CallingConvention = CallingConvention.Cdecl)]
        public static extern int ASFFaceFeatureExtractEx(IntPtr hEngine, ASVLOFFSCREEN imgData, ref ASF_SingleFaceInfo faceInfo, out ASF_FaceFeature feature);
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
        [DllImport(lib, EntryPoint = nameof(ASFFaceFeatureCompare), CallingConvention = CallingConvention.Cdecl)]
        public static extern int ASFFaceFeatureCompare(IntPtr hEngine, ref ASF_FaceFeature feature1, ref ASF_FaceFeature feature2, ref float confidenceLevel, ASF_CompareModel compareModel);
        /// <summary>
        /// 获取年龄信息
        /// </summary>
        /// <param name="hEngine">[in] 引擎handle</param>
        /// <param name="ageInfo">[out] 检测到的年龄信息</param>
        /// <returns></returns>
        [DllImport(lib, EntryPoint = nameof(ASFGetAge), CallingConvention = CallingConvention.Cdecl)]
        public static extern int ASFGetAge(IntPtr hEngine, out ASF_AgeInfo ageInfo);
        /// <summary>
        /// 获取性别信息
        /// </summary>
        /// <param name="hEngine">[in] 引擎handle</param>
        /// <param name="genderInfo">[out] 检测到的性别信息</param>
        /// <returns></returns>
        [DllImport(lib, EntryPoint = nameof(ASFGetGender), CallingConvention = CallingConvention.Cdecl)]
        public static extern int ASFGetGender(IntPtr hEngine, ref ASF_GenderInfo genderInfo);
        /// <summary>
        /// 获取3D角度信息
        /// </summary>
        /// <param name="hEngine">[in] 引擎handle</param>
        /// <param name="p3DAngleInfo">[out] 检测到脸部3D 角度信息</param>
        /// <returns></returns>
        [DllImport(lib, EntryPoint = nameof(ASFGetFace3DAngle), CallingConvention = CallingConvention.Cdecl)]
        public static extern int ASFGetFace3DAngle(IntPtr hEngine, ref ASF_Face3DAngle p3DAngleInfo);
        /// <summary>
        /// 获取RGB活体结果
        /// </summary>
        /// <param name="hEngine">[in] 引擎handle</param>
        /// <param name="livenessInfo">[out] 检测RGB活体结果</param>
        /// <returns></returns>
        [DllImport(lib, EntryPoint = nameof(ASFGetLivenessScore), CallingConvention = CallingConvention.Cdecl)]
        public static extern int ASFGetLivenessScore(IntPtr hEngine, ref ASF_LivenessInfo livenessInfo);
        /// <summary>
        /// 获取IR活体结果
        /// </summary>
        /// <param name="hEngine">[in] 引擎handle</param>
        /// <param name="irLivenessInfo">[out] 检测到IR活体结果</param>
        /// <returns></returns>
        [DllImport(lib, EntryPoint = nameof(ASFGetLivenessScore_IR), CallingConvention = CallingConvention.Cdecl)]
        public static extern int ASFGetLivenessScore_IR(IntPtr hEngine, ref ASF_LivenessInfo irLivenessInfo);
        /// <summary>
        /// 获取ASVL版本
        /// </summary>
        /// <returns></returns>
        [Obsolete("不可用,无法找到入口点")]
        [DllImport(lib, EntryPoint = nameof(ASVL_GetVersion), CallingConvention = CallingConvention.Cdecl)]
        public static extern ASVL_VERSION ASVL_GetVersion();
    }
}