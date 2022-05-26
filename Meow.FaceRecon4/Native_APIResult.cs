namespace Meow.FaceRecon4
{
    /// <summary>
    /// 枚举的API返回值
    /// </summary>
    public enum APIResult
    {
        /// <summary>
        /// 无错误
        /// </summary>
        MOK = 0,

        /* -- 通用错误类型 --*/
        /// <summary>
        /// 错误原因不明
        /// </summary>
        MERR_UNKNOWN = 1,
        /// <summary>
        /// 无效的参数
        /// </summary>
        MERR_INVALID_PARAM = MERR_UNKNOWN + 1,
        /// <summary>
        /// 引擎不支持
        /// </summary>
        MERR_UNSUPPORTED = MERR_UNKNOWN + 2,
        /// <summary>
        /// 内存不足
        /// </summary>
        MERR_NO_MEMORY = MERR_UNKNOWN + 3,
        /// <summary>
        /// 状态错误
        /// </summary>
        MERR_BAD_STATE = MERR_UNKNOWN + 4,
        /// <summary>
        /// 用户取消相关操作
        /// </summary>
        MERR_USER_CANCEL = MERR_UNKNOWN + 5,
        /// <summary>
        /// 操作时间过期
        /// </summary>
        MERR_EXPIRED = MERR_UNKNOWN + 6,
        /// <summary>
        /// 用户暂停操作
        /// </summary>
        MERR_USER_PAUSE = MERR_UNKNOWN + 7,
        /// <summary>
        /// 缓冲上溢
        /// </summary>
        MERR_BUFFER_OVERFLOW = MERR_UNKNOWN + 8,
        /// <summary>
        /// 缓冲下溢
        /// </summary>
        MERR_BUFFER_UNDERFLOW = MERR_UNKNOWN + 9,
        /// <summary>
        /// 存贮空间不足
        /// </summary>
        MERR_NO_DISKSPACE = MERR_UNKNOWN + 10,
        /// <summary>
        /// 组件不存在
        /// </summary>
        MERR_COMPONENT_NOT_EXIST = MERR_UNKNOWN + 11,
        /// <summary>
        /// 全局数据不存在
        /// </summary>
        MERR_GLOBAL_DATA_NOT_EXIST = MERR_UNKNOWN + 12,
        /// <summary>
        /// 图像错误
        /// </summary>
        MERRP_IMGCODEC = MERR_UNKNOWN + 13,
        /// <summary>
        /// 文件错误
        /// </summary>
        MERR_FILE_GENERAL = MERR_UNKNOWN + 14,

        /*--Free SDK通用错误类型--*/
        /// <summary>
        /// Free SDK通用错误类型
        /// </summary>
        MERR_FSDK_BASE = 28672,
        /// <summary>
        /// 无效的App Id
        /// </summary>
        MERR_FSDK_INVALID_APP_ID = MERR_FSDK_BASE+1,
        /// <summary>
        /// 无效的SDK key
        /// </summary>
        MERR_FSDK_INVALID_SDK_ID = MERR_FSDK_BASE+2,
        /// <summary>
        /// AppId和SDKKey不匹配
        /// </summary>
        MERR_FSDK_INVALID_ID_PAIR = MERR_FSDK_BASE+3,
        /// <summary>
        /// SDKKey 和使用的SDK 不匹配
        /// </summary>
        MERR_FSDK_MISMATCH_ID_AND_SDK = MERR_FSDK_BASE+4,
        /// <summary>
        /// 系统版本不被当前SDK所支持
        /// </summary>
        MERR_FSDK_SYSTEM_VERSION_UNSUPPORTED = MERR_FSDK_BASE+5,
        /// <summary>
        /// SDK有效期过期，需要重新下载更新
        /// </summary>
        MERR_FSDK_LICENCE_EXPIRED = MERR_FSDK_BASE+6,

        /*--PhotoStyling 错误类型--*/
        /// <summary>
        /// PhotoStyling 错误类型
        /// </summary>
        MERR_FSDK_APS_ERROR_BASE = 69632,
        /// <summary>
        /// 引擎句柄非法
        /// </summary>
        MERR_FSDK_APS_ENGINE_HANDLE = MERR_FSDK_APS_ERROR_BASE+1,
        /// <summary>
        /// 内存句柄非法
        /// </summary>
        MERR_FSDK_APS_MEMMGR_HANDLE = MERR_FSDK_APS_ERROR_BASE+2,
        /// <summary>
        /// Device ID 非法
        /// </summary>
        MERR_FSDK_APS_DEVICEID_INVALID = MERR_FSDK_APS_ERROR_BASE+3,
        /// <summary>
        /// Device ID 不支持
        /// </summary>
        MERR_FSDK_APS_DEVICEID_UNSUPPORTED = MERR_FSDK_APS_ERROR_BASE+4,
        /// <summary>
        /// 模板数据指针非法
        /// </summary>
        MERR_FSDK_APS_MODEL_HANDLE = MERR_FSDK_APS_ERROR_BASE+5,
        /// <summary>
        /// 模板数据长度非法
        /// </summary>
        MERR_FSDK_APS_MODEL_SIZE = MERR_FSDK_APS_ERROR_BASE+6,
        /// <summary>
        /// 图像结构体指针非法
        /// </summary>
        MERR_FSDK_APS_IMAGE_HANDLE = MERR_FSDK_APS_ERROR_BASE+7,
        /// <summary>
        /// 图像格式不支持
        /// </summary>
        MERR_FSDK_APS_IMAGE_FORMAT_UNSUPPORTED = MERR_FSDK_APS_ERROR_BASE+8,
        /// <summary>
        /// 图像参数非法
        /// </summary>
        MERR_FSDK_APS_IMAGE_PARAM = MERR_FSDK_APS_ERROR_BASE+9,
        /// <summary>
        /// 图像尺寸大小超过支持范围
        /// </summary>
        MERR_FSDK_APS_IMAGE_SIZE = MERR_FSDK_APS_ERROR_BASE+10,
        /// <summary>
        /// 处理器不支持AVX2指令
        /// </summary>
        MERR_FSDK_APS_DEVICE_AVX2_UNSUPPORTED = MERR_FSDK_APS_ERROR_BASE+11,

        /*--Face Recognition错误类型--*/
        /// <summary>
        /// Face Recognition错误类型
        /// </summary>
        MERR_FSDK_FR_ERROR_BASE = 73728,
        /// <summary>
        /// 无效的输入内存
        /// </summary>
        MERR_FSDK_FR_INVALID_MEMORY_INFO = MERR_FSDK_FR_ERROR_BASE+1,
        /// <summary>
        /// 无效的输入图像参数
        /// </summary>
        MERR_FSDK_FR_INVALID_IMAGE_INFO = MERR_FSDK_FR_ERROR_BASE+2,
        /// <summary>
        /// 无效的脸部信息
        /// </summary>
        MERR_FSDK_FR_INVALID_FACE_INFO = MERR_FSDK_FR_ERROR_BASE+3,
        /// <summary>
        /// 当前设备无GPU可用
        /// </summary>
        MERR_FSDK_FR_NO_GPU_AVAILABLE = MERR_FSDK_FR_ERROR_BASE+4,
        /// <summary>
        /// 待比较的两个人脸特征的版本不一致
        /// </summary>
        MERR_FSDK_FR_MISMATCHED_FEATURE_LEVEL = MERR_FSDK_FR_ERROR_BASE+5,

        /*--人脸特征检测错误类型--*/
        /// <summary>
        /// 人脸特征检测错误类型
        /// </summary>
        MERR_FSDK_FACEFEATURE_ERROR_BASE = 81920,
        /// <summary>
        /// 人脸特征检测错误未知
        /// </summary>
        MERR_FSDK_FACEFEATURE_UNKNOWN = MERR_FSDK_FACEFEATURE_ERROR_BASE+1,
        /// <summary>
        /// 人脸特征检测内存错误
        /// </summary>
        MERR_FSDK_FACEFEATURE_MEMORY = MERR_FSDK_FACEFEATURE_ERROR_BASE+2,
        /// <summary>
        /// 人脸特征检测格式错误
        /// </summary>
        MERR_FSDK_FACEFEATURE_INVALID_FORMAT = MERR_FSDK_FACEFEATURE_ERROR_BASE+3,
        /// <summary>
        /// 人脸特征检测参数错误
        /// </summary>
        MERR_FSDK_FACEFEATURE_INVALID_PARAM = MERR_FSDK_FACEFEATURE_ERROR_BASE+4,
        /// <summary>
        /// 人脸特征检测结果置信度低
        /// </summary>
        MERR_FSDK_FACEFEATURE_LOW_CONFIDENCE_LEVEL = MERR_FSDK_FACEFEATURE_ERROR_BASE+5,
        /// <summary>
        /// 人脸特征检测结果操作过期
        /// </summary>
        MERR_FSDK_FACEFEATURE_EXPIRED = MERR_FSDK_FACEFEATURE_ERROR_BASE+6,
        /// <summary>
        /// 人脸特征检测人脸丢失
        /// </summary>
        MERR_FSDK_FACEFEATURE_MISSFACE = MERR_FSDK_FACEFEATURE_ERROR_BASE+7,
        /// <summary>
        /// 人脸特征检测没有人脸
        /// </summary>
        MERR_FSDK_FACEFEATURE_NO_FACE = MERR_FSDK_FACEFEATURE_ERROR_BASE+8,
        /// <summary>
        /// 人脸特征检测人脸信息错误
        /// </summary>
        MERR_FSDK_FACEFEATURE_FACEDATD = MERR_FSDK_FACEFEATURE_ERROR_BASE+9,
        /// <summary>
        /// 人脸特征检测人脸状态错误
        /// </summary>
        MERR_FSDK_FACEFEATURE_STATUES_ERROR = MERR_FSDK_FACEFEATURE_ERROR_BASE+10,

        /*--ASF错误类型--*/
        /// <summary>
        /// ASF错误类型
        /// </summary>
        MERR_ASF_EX_BASE = 86016,
        /// <summary>
        /// Engine不支持的检测属性
        /// </summary>
        MERR_ASF_EX_FEATURE_UNSUPPORTED_ON_INIT = MERR_ASF_EX_BASE+1,
        /// <summary>
        /// 需要检测的属性未初始化
        /// </summary>
        MERR_ASF_EX_FEATURE_UNINITED = MERR_ASF_EX_BASE+2,
        /// <summary>
        /// 待获取的属性未在process中处理过
        /// </summary>
        MERR_ASF_EX_FEATURE_UNPROCESSED = MERR_ASF_EX_BASE+3,
        /// <summary>
        /// PROCESS不支持的检测属性组合，例如FR，有自己独立的处理函数
        /// </summary>
        MERR_ASF_EX_FEATURE_UNSUPPORTED_ON_PROCESS = MERR_ASF_EX_BASE+4,
        /// <summary>
        /// 无效的输入图像
        /// </summary>
        MERR_ASF_EX_INVALID_IMAGE_INFO = MERR_ASF_EX_BASE+5,
        /// <summary>
        /// 无效的脸部信息
        /// </summary>
        MERR_ASF_EX_INVALID_FACE_INFO = MERR_ASF_EX_BASE+6,

        /*--人脸比对基础错误类型--*/
        /// <summary>
        /// 人脸比对基础错误类型
        /// </summary>
        MERR_ASF_BASE = 90112,
        /// <summary>
        /// SDK激活失败,请打开读写权限
        /// </summary>
        MERR_ASF_ACTIVATION_FAIL = MERR_ASF_BASE+1,
        /// <summary>
        /// SDK已激活
        /// </summary>
        MERR_ASF_ALREADY_ACTIVATED = MERR_ASF_BASE+2,
        /// <summary>
        /// SDK未激活
        /// </summary>
        MERR_ASF_NOT_ACTIVATED = MERR_ASF_BASE+3,
        /// <summary>
        /// detectFaceScaleVal 不支持
        /// </summary>
        MERR_ASF_SCALE_NOT_SUPPORT = MERR_ASF_BASE+4,
        /// <summary>
        /// 激活文件与SDK类型不匹配，请确认使用的sdk
        /// </summary>
        MERR_ASF_ACTIVEFILE_SDKTYPE_MISMATCH = MERR_ASF_BASE+5,
        /// <summary>
        /// 设备不匹配
        /// </summary>
        MERR_ASF_DEVICE_MISMATCH = MERR_ASF_BASE+6,
        /// <summary>
        /// 唯一标识不合法
        /// </summary>
        MERR_ASF_UNIQUE_IDENTIFIER_ILLEGAL = MERR_ASF_BASE+7,
        /// <summary>
        /// 参数为空
        /// </summary>
        MERR_ASF_PARAM_NULL = MERR_ASF_BASE+8,
        /// <summary>
        /// 活体已过期
        /// </summary>
        MERR_ASF_LIVENESS_EXPIRED = MERR_ASF_BASE+9,
        /// <summary>
        /// 版本不支持
        /// </summary>
        MERR_ASF_VERSION_NOT_SUPPORT = MERR_ASF_BASE+10,
        /// <summary>
        /// 签名错误
        /// </summary>
        MERR_ASF_SIGN_ERROR = MERR_ASF_BASE+11,
        /// <summary>
        /// 激活信息保存异常
        /// </summary>
        MERR_ASF_DATABASE_ERROR = MERR_ASF_BASE+12,
        /// <summary>
        /// 唯一标识符校验失败
        /// </summary>
        MERR_ASF_UNIQUE_CHECKOUT_FAIL = MERR_ASF_BASE+13,
        /// <summary>
        /// 颜色空间不支持
        /// </summary>
        MERR_ASF_COLOR_SPACE_NOT_SUPPORT = MERR_ASF_BASE+14,
        /// <summary>
        /// 图片宽高不支持，宽度需四字节对齐
        /// </summary>
        MERR_ASF_IMAGE_WIDTH_HEIGHT_NOT_SUPPORT = MERR_ASF_BASE+15,

        /*--人脸比对基础错误类型--*/
        /// <summary>
        /// 人脸比对基础错误类型
        /// </summary>
        MERR_ASF_READ_PHONE_STATE_DENIED = 90128,
        /// <summary>
        /// 激活数据被破坏,请删除激活文件，重新进行激活
        /// </summary>
        MERR_ASF_ACTIVATION_DATA_DESTROYED = MERR_ASF_READ_PHONE_STATE_DENIED + 1,
        /// <summary>
        /// 服务端未知错误
        /// </summary>
        MERR_ASF_SERVER_UNKNOWN_ERROR = MERR_ASF_READ_PHONE_STATE_DENIED + 2,
        /// <summary>
        /// INTERNET权限被拒绝
        /// </summary>
        MERR_ASF_INTERNET_DENIED = MERR_ASF_READ_PHONE_STATE_DENIED +3,
        /// <summary>
        /// 激活文件与SDK版本不匹配,请重新激活
        /// </summary>
        MERR_ASF_ACTIVEFILE_SDK_MISMATCH = MERR_ASF_READ_PHONE_STATE_DENIED + 4,
        /// <summary>
        /// 设备信息太少，不足以生成设备指纹
        /// </summary>
        MERR_ASF_DEVICEINFO_LESS = MERR_ASF_READ_PHONE_STATE_DENIED + 5,
        /// <summary>
        /// 客户端时间与服务器时间（即北京时间）前后相差在30分钟以上
        /// </summary>
        MERR_ASF_LOCAL_TIME_NOT_CALIBRATED = MERR_ASF_READ_PHONE_STATE_DENIED + 6,
        /// <summary>
        /// 数据校验异常
        /// </summary>
        MERR_ASF_APPID_DATA_DECRYPT = MERR_ASF_READ_PHONE_STATE_DENIED + 7,
        /// <summary>
        /// 传入的AppId和AppKey与使用的SDK版本不一致
        /// </summary>
        MERR_ASF_APPID_APPKEY_SDK_MISMATCH = MERR_ASF_READ_PHONE_STATE_DENIED + 8,
        /// <summary>
        /// 短时间大量请求会被禁止请求,30分钟之后解封
        /// </summary>
        MERR_ASF_NO_REQUEST = MERR_ASF_READ_PHONE_STATE_DENIED + 9,
        /// <summary>
        /// 激活文件不存在
        /// </summary>
        MERR_ASF_ACTIVE_FILE_NO_EXIST = MERR_ASF_READ_PHONE_STATE_DENIED + 10,
        /// <summary>
        /// 检测模型不支持，请查看对应接口说明，使用当前支持的检测模型
        /// </summary>
        MERR_ASF_DETECT_MODEL_UNSUPPORTED = MERR_ASF_READ_PHONE_STATE_DENIED + 11,
        /// <summary>
        /// 当前设备时间不正确，请调整设备时间
        /// </summary>
        MERR_ASF_CURRENT_DEVICE_TIME_INCORRECT = MERR_ASF_READ_PHONE_STATE_DENIED + 12,
        /// <summary>
        /// 年度激活数量超出限制，次年清零
        /// </summary>
        MERR_ASF_ACTIVATION_QUANTITY_OUT_OF_LIMIT = MERR_ASF_READ_PHONE_STATE_DENIED + 13,

        /*--网络错误类型--*/
        /// <summary>
        /// 网络错误类型
        /// </summary>
        MERR_ASF_NETWORK_BASE = 94208,
        /// <summary>
        /// 无法解析主机地址
        /// </summary>
        MERR_ASF_NETWORK_COULDNT_RESOLVE_HOST = MERR_ASF_NETWORK_BASE+1,
        /// <summary>
        /// 无法连接服务器
        /// </summary>
        MERR_ASF_NETWORK_COULDNT_CONNECT_SERVER = MERR_ASF_NETWORK_BASE+2,
        /// <summary>
        /// 网络连接超时
        /// </summary>
        MERR_ASF_NETWORK_CONNECT_TIMEOUT = MERR_ASF_NETWORK_BASE+3,
        /// <summary>
        /// 网络未知错误
        /// </summary>
        MERR_ASF_NETWORK_UNKNOWN_ERROR = MERR_ASF_NETWORK_BASE+4,

        /*--激活码错误类型--*/
        /// <summary>
        /// 激活码错误类型
        /// </summary>
        MERR_ASF_ACTIVEKEY_BASE = 98304,
        /// <summary>
        /// 无法连接激活服务器
        /// </summary>
        MERR_ASF_ACTIVEKEY_COULDNT_CONNECT_SERVER = MERR_ASF_ACTIVEKEY_BASE+1,
        /// <summary>
        /// 服务器系统错误
        /// </summary>
        MERR_ASF_ACTIVEKEY_SERVER_SYSTEM_ERROR = MERR_ASF_ACTIVEKEY_BASE+2,
        /// <summary>
        /// 请求参数错误
        /// </summary>
        MERR_ASF_ACTIVEKEY_POST_PARM_ERROR = MERR_ASF_ACTIVEKEY_BASE+3,
        /// <summary>
        /// ACTIVEKEY与APPID、SDKKEY不匹配
        /// </summary>
        MERR_ASF_ACTIVEKEY_PARM_MISMATCH = MERR_ASF_ACTIVEKEY_BASE+4,
        /// <summary>
        /// ACTIVEKEY已经被使用
        /// </summary>
        MERR_ASF_ACTIVEKEY_ACTIVEKEY_ACTIVATED = MERR_ASF_ACTIVEKEY_BASE+5,
        /// <summary>
        /// ACTIVEKEY信息异常
        /// </summary>
        MERR_ASF_ACTIVEKEY_ACTIVEKEY_FORMAT_ERROR = MERR_ASF_ACTIVEKEY_BASE+6,
        /// <summary>
        /// ACTIVEKEY与APPID不匹配
        /// </summary>
        MERR_ASF_ACTIVEKEY_APPID_PARM_MISMATCH = MERR_ASF_ACTIVEKEY_BASE+7,
        /// <summary>
        /// SDK与激活文件版本不匹配
        /// </summary>
        MERR_ASF_ACTIVEKEY_SDK_FILE_MISMATCH = MERR_ASF_ACTIVEKEY_BASE+8,
        /// <summary>
        /// ACTIVEKEY已过期
        /// </summary>
        MERR_ASF_ACTIVEKEY_EXPIRED = MERR_ASF_ACTIVEKEY_BASE+9,
        /// <summary>
        /// 批量授权激活码设备数超过限制
        /// </summary>
        MERR_ASF_ACTIVEKEY_DEVICE_OUT_OF_LIMIT = MERR_ASF_ACTIVEKEY_BASE+10,

        /*--离线激活错误码类型--*/
        /// <summary>
        /// 离线激活错误码类型
        /// </summary>
        MERR_ASF_OFFLINE_BASE = 102400,
        /// <summary>
        /// 离线授权文件不存在或无读写权限
        /// </summary>
        MERR_ASF_LICENSE_FILE_NOT_EXIST = MERR_ASF_OFFLINE_BASE + 1,
        /// <summary>
        /// 离线授权文件已损坏
        /// </summary>
        MERR_ASF_LICENSE_FILE_DATA_DESTROYED = MERR_ASF_OFFLINE_BASE + 2,
        /// <summary>
        /// 离线授权文件与SDK版本不匹配
        /// </summary>
        MERR_ASF_LICENSE_FILE_SDK_MISMATCH = MERR_ASF_OFFLINE_BASE + 3,
        /// <summary>
        /// 离线授权文件与SDK信息不匹配
        /// </summary>
        MERR_ASF_LICENSE_FILEINFO_SDKINFO_MISMATCH = MERR_ASF_OFFLINE_BASE + 4,
        /// <summary>
        /// 离线授权文件与设备指纹不匹配
        /// </summary>
        MERR_ASF_LICENSE_FILE_FINGERPRINT_MISMATCH = MERR_ASF_OFFLINE_BASE + 5,
        /// <summary>
        /// 离线授权文件已过期
        /// </summary>
        MERR_ASF_LICENSE_FILE_EXPIRED = MERR_ASF_OFFLINE_BASE + 6,
        /// <summary>
        /// 离线授权文件不可用，本地原有激活文件可继续使用
        /// </summary>
        MERR_ASF_LOCAL_EXIST_USEFUL_ACTIVE_FILE = MERR_ASF_OFFLINE_BASE + 7,
        /// <summary>
        /// 离线授权文件版本过低，请使用新版本激活助手重新进行离线激活
        /// </summary>
        MERR_ASF_LICENSE_FILE_VERSION_TOO_LOW = MERR_ASF_OFFLINE_BASE + 8,

        /*--搜索接口错误码类型--*/
        /// <summary>
        /// 搜索接口错误码类型
        /// </summary>
        MERR_ASF_SEARCH_BASE = 151552,
        /// <summary>
        /// 人脸列表为空
        /// </summary>
        MERR_ASF_SEARCH_EMPTY = MERR_ASF_SEARCH_BASE+1,
        /// <summary>
        /// 人脸不存在
        /// </summary>
        MERR_ASF_SEARCH_NO_EXIST = MERR_ASF_SEARCH_BASE+2,
        /// <summary>
        /// 特征值长度不匹配
        /// </summary>
        MERR_ASF_SEARCH_FEATURE_SIZE_MISMATCH = MERR_ASF_SEARCH_BASE+3,
        /// <summary>
        /// 相似度异常
        /// </summary>
        MERR_ASF_SEARCH_LOW_CONFIDENCE = MERR_ASF_SEARCH_BASE+4,

        /*--预留字段错误码类型--*/
        /// <summary>
        /// 预留字段错误码类型
        /// </summary>
        MERR_ASF_RESERVED_BASE = 196608,
        /// <summary>
        /// 非限制时段数量超出(限制时段：周一到周五 9：00-14：00)
        /// </summary>
        MERR_ASF_UNRESTRICTED_TIME_LIMIT_EXCEEDED = MERR_ASF_RESERVED_BASE + 1,
    }
}