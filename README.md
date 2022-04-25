# Meow.FaceRecon
## 1 SDK���ڱ�д��,û�з���Nuget.
 ```
 ���������Dll/so�����Ѿ�д��, ע������
 �������Լ����û����Լ���չ����
 ```
## 2 TODO and Complete
  **TODO**                                      | **isComplete** |**UpdateAt**
-----------------------------------------------|----------------|----------------
 Dll�ⲿ��������                                 | ��           | 20220421       
 Dll�����������                                 | ��           | 20220421       
 ASFDetectMultiFace                            | �� | 20220422       
 ASFProcess_IR/ASFProcessEx_IR                 | pending        | /
 ASFFaceFeatureExtract/ASFFaceFeatureExtractEx | pending        | /
 ASFGetAge                                     | �� | 20220423               
 ASFGetGender                                  | �� | 20220424               
 ASFGetFace3DAngle                             | �� | 20220425        
 ASFGetLivenessScore/ASFGetLivenessScore_IR    | pending        | /
## 3 (����)����ʹ�÷���
### 3.0 ����ͼƬ���߶�ȡͼƬ(Image����)
```csharp
string fp = "D:/1234.jpg";
using var i = Image.FromFile(fp);
```
### 3.1 ����λ�ñ�ע
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
### 3.2 �沿����
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

### 3.3 ͼƬ�Ƿ���ʵ 
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

### 3.4 ������Ա�
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