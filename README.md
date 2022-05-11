# Meow.FaceRecon

[![CI](https://github.com/DavidSciMeow/Meow.FaceRecon/actions/workflows/dotnet.yml/badge.svg?branch=main)](https://github.com/DavidSciMeow/Meow.FaceRecon/actions/workflows/dotnet.yml)
![](https://img.shields.io/nuget/vpre/Electronicute.Meow.FaceRecon?label=NuGet%20Version)
![](https://img.shields.io/nuget/dt/Electronicute.Meow.FaceRecon?label=Nuget%20Download)

# -1. 引 言
程序集在3.0.0时进行 `重大` 改动,  

1. 改进跨平台特性,删除了`Windows`的`libgdi+`支持,  
使用不需要任何配置的 `SkiaSharp` 进行图像处理,  
本程序集自3.0.0不再支持`WindowsImage(System.Drawing.Common)`
如需要转换libgdi+的Bitmap请提交Issue,我会在第一时间处理.  

1. 使用方法简化,开放更多方案接口.`具体请参阅建议使用方法一章`

# 0. 目 录
1. [申请虹软软件开发 AppId/SDKKey](#1)
1. [Todo And Complete](#2)
1. [基类简易使用方法](#3)


## 1. 申请虹软软件开发AppKey(id)/SDKKey<a name="1"></a>
-------
点击链接 [这里](https://ai.arcsoft.com.cn/ucenter/resource/build/index.html#/login)  
登陆后创建应用 -> 抄取AppID 和 SDK_Key

请将您下载的库文件按照操作系统类别调用放置于文件目录下,  
我们建议放置在项目下方便更换和读取,  
如果您存在多个项目同时调用库文件的情况,
您可以将库文件置于环境变量位置或者  
`System32` `systemWoW64` (windows)  
`/lib` `/usr/lib` (linux)

**`请注意`**,`APPID`和`SDKKEY`均为纯字符串,而并非`APP_ID:xxxxxxx`您只需填写冒号后面的字符串`xxxxxxx`即可  
线程会在调用时出现Exception,捕捉后您可以看到错误码类似 `xx1xx Phase : [xx2xx] xx3xx`  
`xx1xx` 是 检测的阶段  
`xx2xx` 是 错误码的英文  
`xx3xx` 是 错误码的中文意义  

## 2. TODO and Complete<a name="2"></a>
TODO**                                      | **isComplete** |**UpdateAt**
------------------------------------------------|----------------|----------------
Dll外部调用引用                                  | √ | 20220421       
Dll基础引擎管理                                  | √ | 20220421       
ASFDetectMultiFace                             | √ | 20220422       
ASFProcessEx_IR                                | pending | /
ASFProcess_IR                                  | pending | /
ASFFaceFeatureExtract                          | pending | /
ASFFaceFeatureExtractEx                        | pending | /
ASFGetAge                                      | √ | 20220423               
ASFGetGender                                   | √ | 20220424               
ASFGetFace3DAngle                              | √ | 20220425        
ASFGetLivenessScore                            | √ | 20220425
ASFGetLivenessScore_IR                         | pending | /

## 3. (基类)简易使用方法<a name="3"></a>


