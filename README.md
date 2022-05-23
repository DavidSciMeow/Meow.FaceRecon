# Meow.FaceRecon

[![PPB](https://github.com/DavidSciMeow/Meow.FaceRecon/actions/workflows/ppb.yml/badge.svg?branch=main&event=workflow_run)](https://github.com/DavidSciMeow/Meow.FaceRecon/actions/workflows/ppb.yml)
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
TODO**                                      **UpdateAt**
------------------------------------------------|----------------
Dll外部调用引用                                  | 20220421       
Dll基础引擎管理                                  | 20220421       
ASFDetectMultiFace                             | 20220422       
ASFProcessEx_IR                                | pending 
ASFProcess_IR                                  | pending 
ASFFaceFeatureExtract                          | pending 
ASFFaceFeatureExtractEx                        | pending 
ASFGetAge                                      | 20220423               
ASFGetGender                                   | 20220424               
ASFGetFace3DAngle                              | 20220425        
ASFGetLivenessScore                            | 20220425
ASFGetLivenessScore_IR                         | pending

## 3. (基类)简易使用方法<a name="3"></a>
```csharp
using Meow.FaceRecon;
using Meow.FaceRecon.SDK;

GlobalSetting.LogMode = -1;//日志打印
var fp = "D:/123.png"; //文件句柄


//获取文件转换SKBitMap
var s = SkiaSharp.SKBitmap.Decode(new SkiaSharp.SKManagedStream(File.OpenRead(fp)));
//读取base64串(假设)
//var s = "".Base64ToSKBitmap();


//实例化检测池,检测所有参数
var ep = new FaceReconPool(pwd.appid, pwd.sdkwin, pwd.sdklinux);
var mfi = new MultiFaceEngine(ep.Appid, ep.Key).Detect(s);
var afi = new AgeFaceProcess(ep.Appid, ep.Key).Detect(s,mfi);
var gfi = new GenderFaceProcess(ep.Appid, ep.Key).Detect(s,mfi);
var lfi = new LivenessFaceProcess(ep.Appid, ep.Key).Detect(s, mfi);
var agfi = new AngleFaceProcess(ep.Appid, ep.Key).Detect(s, mfi);

//代理参数转换为SDK常量
var dfi = mfi.InfoToSDKInfo();
var fs = new Meow.FaceRecon.SDK.Model.SDK_FaceGeneral();
fs.faceNum = dfi.faceNum;
//生成SDK参数
for (int i = 0; i < dfi.faceNum; i++)
{
    fs.faceRect.Add(dfi.faceRect[i]);
    fs.ageArray.Add(afi.ageArray[i]);
    fs.genderArray.Add(gfi.genderArray[i]);
    fs.liveness.Add(lfi.isLive[i]);
    fs.pitch.Add(agfi.pitch[i]);
    fs.yaw.Add(agfi.yaw[i]);
    fs.roll.Add(agfi.roll[i]);
    fs.status.Add(agfi.status[i]);
}

//使用Util转换人脸模式
foreach(var t in fs.ConvertIntoFaces())
{
    s = s.DrawStringAndRect(t);//扩展的画图功能
}

//保存图像
s.Save("D:/testrec.jpg");
//保存Base64串
Console.WriteLine(s.ToBase64String());
```

