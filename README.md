# Meow.FaceRecon
## 1 SDK仍在编写中,没有发布Nuget.
 ```
 基础的类和Dll/so调用已经写明, 注释完整
 您可以自己调用或者自己扩展基类
 ```
## 2 TODO and Complete
  **TODO**                                      | **isComplete** |**UpdateAt**
-----------------------------------------------|----------------|----------------
 Dll外部调用引用                                 | √           | 20220421       
 Dll基础引擎管理                                 | √           | 20220421       
 ASFDetectMultiFace                            | √ | 20220422       
 ASFProcess_IR/ASFProcessEx_IR                 | pending        | /
 ASFFaceFeatureExtract/ASFFaceFeatureExtractEx | pending        | /
 ASFGetAge                                     | √ | 20220423               
 ASFGetGender                                  | √ | 20220424               
 ASFGetFace3DAngle                             | √ | 20220425        
 ASFGetLivenessScore/ASFGetLivenessScore_IR    | pending        | /
## 3 (基类)简易使用方法
### 3.0 导入图片或者读取图片(Image对象)
```csharp
string fp = "D:/1234.jpg";
using var i = Image.FromFile(fp);
```
### 3.1 人脸位置标注
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
### 3.2 面部朝向
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

### 3.3 图片是否真实 
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

### 3.4 年龄和性别
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