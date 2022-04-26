# Meow.FaceRecon
```
SDK仍在编写中,没有发布Nuget.
基础的类和Dll/so调用已经写明, 注释完整
您可以自己调用或者自己扩展基类
```

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
1. 静态扩展类方法 (施工中)
1. 识别顺序和软件工作原理 (施工中)

## 1. 申请虹软软件开发AppKey(id)/SDKKey<name id="1"></name>
-------
点击链接 [这里](https://ai.arcsoft.com.cn/ucenter/resource/build/index.html#/login)  
登陆后创建应用 -> 抄取AppID 和 SDK_Key

**`请注意`**,`APPID`和`SDKKEY`均为纯字符串,而并非`APP_ID:xxxxxxx`您只需填写冒号后面的字符串`xxxxxxx`即可  
线程会在调用时出现Exception,捕捉后您可以看到错误码类似 `xx1xx Phase : [xx2xx] xx3xx`  
`xx1xx` 是 检测的阶段  
`xx2xx` 是 错误码的英文  
`xx3xx` 是 错误码的中文意义  

## 2. TODO and Complete<name id="2"></name>
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

## 3. (基类)简易使用方法<name id="3"></name>
### 导入图片或者读取图片(Image对象)<name id="30"></name>
```csharp
using Meow.FaceRecon;
using MeowFaceReconTest;
using System.Drawing;
using Meow.FaceRecon.SDK.Model;

string fp = "D:/1234.jpg";
using var i = Image.FromFile(fp);
```
### 3.1. 人脸位置标注<name id="31"></name>
```csharp
using var e = new Meow.FaceRecon.SDK.MultiFaceEngine(pwd.appid, pwd.sdkwin);
var b = e.Detect(i);
foreach (var ri in b.faceRect)
{
    i.DrawRectangleInPicture(ri,Color.Red);
}
var p = Path.GetFileName(fp).Split(".");
i.Save($"D:/{p[0]}-Recon.{p[^1]}");
```
### 3.2. 面部朝向<name id="32"></name>
```csharp
using var e = new Meow.FaceRecon.SDK.AngleFaceProcess(pwd.appid, pwd.sdkwin);
var (a,b) = e.Detect(i);
for (int j = 0; j < b.num; j++)
{
    i.DrawRectangleInPicture(a.faceRect[j], Color.Red);
    Console.WriteLine($"{b.yaw[j]}|{b.roll[j]}|{b.pitch[j]}|{b.status[j]}");

}
var p = Path.GetFileName(fp).Split(".");
i.Save($"D:/{p[0]}-Recon.{p[^1]}");
```

### 3.3. 图片是否真实(仅支持单人脸)<name id="33"></name>
```csharp
using var e = new Meow.FaceRecon.SDK.LivenessFaceProcess(pwd.appid, pwd.sdkwin);
var (a,b) = e.Detect(i);
for (int j = 0; j < b.num; j++)
{
    i.DrawRectangleInPicture(a.faceRect[j], Color.Red);
    Console.WriteLine($"{b.isLive[j]}");

}
var p = Path.GetFileName(fp).Split(".");
i.Save($"D:/{p[0]}-Recon.{p[^1]}");
```

### 3.4. 年龄和性别<name id="34"></name>
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
var p = Path.GetFileName(fp).Split(".");
i.Save($"D:/{p[0]}-Recon.{p[^1]}");
```

### 3.5. 完全检查(使用原始数组)<name id="35"></name>
```csharp
using var e = new Meow.FaceRecon.SDK.FullFaceProcess(pwd.appid, pwd.sdkwin);
SDK_FaceGeneral a = e.Detect(i);
for (int j = 0; j < a.faceNum; j++)
{
    Console.WriteLine($"A:{a.ageArray[j]}|G:{a.genderArray[j]}");
    Console.WriteLine($"POS:{a.pitch[j]}:{a.yaw[j]}:{a.roll[j]}");
    i.DrawRectangleInPicture(a.faceRect[j], Color.Red);
}
var p = Path.GetFileName(fp).Split(".");
i.Save($"D:/{p[0]}-Recon.{p[^1]}");
```

### 3.6. 完全检查(使用转换人脸列表[Util静态扩展])<name id="36"></name>
```csharp
using var e = new Meow.FaceRecon.SDK.FullFaceProcess(pwd.appid, pwd.sdkwin);
var a = e.Detect(i).ConvertIntoFaces();
foreach (var ix in a)
{
    Console.WriteLine($"A:{ix.age}|G:{ix.gender}");
    Console.WriteLine($"POS:{ix.pitch}:{ix.yaw}:{ix.roll}");
    i.DrawRectangleInPicture(ix.faceRect, Color.Red);
}
var p = Path.GetFileName(fp).Split(".");
i.Save($"D:/{p[0]}-Recon.{p[^1]}");
```
