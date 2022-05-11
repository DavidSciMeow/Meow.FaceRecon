# Meow.FaceRecon

[![CI](https://github.com/DavidSciMeow/Meow.FaceRecon/actions/workflows/dotnet.yml/badge.svg?branch=main)](https://github.com/DavidSciMeow/Meow.FaceRecon/actions/workflows/dotnet.yml)
![](https://img.shields.io/nuget/vpre/Electronicute.Meow.FaceRecon?label=NuGet%20Version)
![](https://img.shields.io/nuget/dt/Electronicute.Meow.FaceRecon?label=Nuget%20Download)

# 0. 目 录
1. [申请虹软软件开发 AppId/SDKKey](#1)
1. [Todo And Complete](#2)
1. [基类简易使用方法](#3)
    1. [导入图片](#30)
    1. [人脸(多)标注位置](#31)
    1. [面部朝向](#32)
    1. [图片是否真实 (仅支持单人脸)](#33)
    1. [年龄和性别](#34)
    1. [完全检查 (使用原始数组)](#35)
    1. [完全检查 (使用转换人脸列表)](#36)
1. [建议的引擎池使用方法](#4)

## 20220511 关于建议使用Skia实现功能并逐步废弃WinImage处理方案
```csharp
using Meow.FaceRecon;
using Meow.FaceRecon.Skia;
using MeowFaceReconTest;

Meow.FaceRecon.SDK.GlobalSetting.LogMode = -1;

var fp = "D:/123.jpg";
var s = SkiaSharp.SKBitmap.Decode(new SkiaSharp.SKManagedStream(File.OpenRead(fp)));
var ep = new Meow.FaceRecon.Skia.FaceReconPool(pwd.appid, pwd.sdkwin, pwd.sdklinux); //生成一个面部识别引擎管理池
(await ep.DetAllFaceAsync(s)) //Base64字符串转换Image
    .ConvertIntoFaces()
    .ForEach(t =>
{
    s = s.DrawStringAndRect(t);
});
Console.WriteLine(s.ToBase64String());
s.Save("D:/testrec.jpg");
```

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
### 导入图片或者读取图片(Image对象)<a name="30"></a>
```csharp
using Meow.FaceRecon;
using System.Drawing;
using Meow.FaceRecon.SDK.Model;

Meow.FaceRecon.SDK.GlobalSetting.LogMode = -1;
string fp = "D:/1234.jpg";
using var i = Image.FromFile(fp);
```
### 3.1. 人脸位置标注<a name="31"></a>
```csharp
using var e = new Meow.FaceRecon.SDK.MultiFaceEngine(pwd.appid, pwd.sdkwin);
var b = e.Detect(i);
foreach (var ri in b.faceRect)
{
    i.DrawRectangleInPicture(ri,Color.Red);
}
var p = Path.GetFilename=(fp).Split(".");
i.Save($"D:/{p[0]}-Recon.{p[^1]}");
```
### 3.2. 面部朝向<a name="32"></a>
```csharp
using var e = new Meow.FaceRecon.SDK.AngleFaceProcess(pwd.appid, pwd.sdkwin);
var (a,b) = e.Detect(i);
for (int j = 0; j < b.num; j++)
{
    i.DrawRectangleInPicture(a.faceRect[j], Color.Red);
    Console.WriteLine($"{b.yaw[j]}|{b.roll[j]}|{b.pitch[j]}|{b.status[j]}");

}
var p = Path.GetFilename=(fp).Split(".");
i.Save($"D:/{p[0]}-Recon.{p[^1]}");
```

### 3.3. 图片是否真实(仅支持单人脸)<a name="33"></a>
```csharp
using var e = new Meow.FaceRecon.SDK.LivenessFaceProcess(pwd.appid, pwd.sdkwin);
var (a,b) = e.Detect(i);
for (int j = 0; j < b.num; j++)
{
    i.DrawRectangleInPicture(a.faceRect[j], Color.Red);
    Console.WriteLine($"{b.isLive[j]}");

}
var p = Path.GetFilename=(fp).Split(".");
i.Save($"D:/{p[0]}-Recon.{p[^1]}");
```

### 3.4. 年龄和性别<a name="34"></a>
```csharp
using var e = new Meow.FaceRecon.SDK.AgeFaceProcess(pwd.appid, pwd.sdkwin);
using var e2 = new Meow.FaceRecon.SDK.GenderFaceProcess(pwd.appid, pwd.sdkwin);
var (b,a) = e.Detect(i);
var (b2,a2) = e2.Detect(i);
for (int j = 0; j < a.num; j++)
{
    Console.WriteLine($"A:{a.ageArray[j]}|G:{a2.genderArray[j]}");
    i.DrawRectangleInPicture(b.faceRect[j], Color.Red);
}
var p = Path.GetFilename=(fp).Split(".");
i.Save($"D:/{p[0]}-Recon.{p[^1]}");
```

### 3.5. 完全检查(使用原始数组)<a name="35"></a>
```csharp
using var e = new Meow.FaceRecon.SDK.FullFaceProcess(pwd.appid, pwd.sdkwin);
SDK_FaceGeneral a = e.Detect(i);
for (int j = 0; j < a.faceNum; j++)
{
    Console.WriteLine($"A:{a.ageArray[j]}|G:{a.genderArray[j]}");
    Console.WriteLine($"POS:{a.pitch[j]}:{a.yaw[j]}:{a.roll[j]}");
    i.DrawRectangleInPicture(a.faceRect[j], Color.Red);
}
var p = Path.GetFilename=(fp).Split(".");
i.Save($"D:/{p[0]}-Recon.{p[^1]}");
```

### 3.6. 完全检查(使用转换人脸列表[Util静态扩展])<a name="36"></a>
```csharp
using var e = new Meow.FaceRecon.SDK.FullFaceProcess(pwd.appid, pwd.sdkwin);
var a = e.Detect(i).ConvertIntoFaces();
foreach (var ix in a)
{
    Console.WriteLine($"A:{ix.age}|G:{ix.gender}");
    Console.WriteLine($"POS:{ix.pitch}:{ix.yaw}:{ix.roll}");
    i.DrawRectangleInPicture(ix.faceRect, Color.Red);
}
var p = Path.GetFilename=(fp).Split(".");
i.Save($"D:/{p[0]}-Recon.{p[^1]}");
```

## 4.建议使用的引擎池检测方法<a name="4"></a>
```csharp
var ep = new FaceReconPool(pwd.appid, pwd.sdkwin, pwd.sdklinux); //生成一个面部识别引擎管理池
(await ep.DetAllFaceAsync(i)).ConvertIntoFaces()
.ForEach(ix =>{
    Console.WriteLine($"A:{ix.age}|G:{ix.gender}|POS:{ix.pitch}deg:{ix.yaw}deg:{ix.roll}deg");
    i.DrawRectangleInPicture(ix.faceRect, Color.Red);
});
var p = Path.GetFileName(fp).Split(".");
i.Save($"D:/{p[0]}-Recon.{p[^1]}");
```
### 完整写法(*)
```csharp
using Meow.FaceRecon;
using MeowFaceReconTest;
using System.Drawing;

Meow.FaceRecon.SDK.GlobalSetting.LogMode = -1;//日志类型

string fp = "D:/123.jpg";//文件目录
using var i = Image.FromFile(fp);//读取Image

var base64str = i.ImgToBase64(); //Util扩展转换Image/Base64 (如果您是从网络获取)

var ep = new FaceReconPool(pwd.appid, pwd.sdkwin, pwd.sdklinux); //生成一个面部识别引擎管理池
(await ep.DetAllFaceAsync(base64str.Base64ToImage())) //Base64字符串转换Image
    .ConvertIntoFaces()
    .ForEach(ix =>
{
    Console.WriteLine($"A:{ix.age}|G:{ix.gender}|POS:{ix.pitch}deg:{ix.yaw}deg:{ix.roll}deg");
    i.DrawRectangleInPicture(ix.faceRect, Color.Red);

    Console.WriteLine(i.ImgToBase64()); // Util扩展转换Base64
});

var p = Path.GetFileName(fp).Split(".");
i.Save($"D:/{p[0]}-Recon.{p[^1]}");
```

