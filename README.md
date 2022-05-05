# Meow.FaceRecon
```
SDK���ڱ�д��,û�з���Nuget.
���������Dll/so�����Ѿ�д��, ע������
�������Լ����û����Լ���չ����
```

# 0. Ŀ ¼
1. [�������������� AppId/SDKKey](#1)
1. [Todo And Complete](#2)
1. [�������ʹ�÷���](#3)
    1. [����ͼƬ](#30)
    1. [����(��)��עλ��](#31)
    1. [�沿����](#32)
    1. [ͼƬ�Ƿ���ʵ (��֧�ֵ�����)](#33)
    1. [������Ա�](#34)
    1. [��ȫ��� (ʹ��ԭʼ����)](#35)
    1. [��ȫ��� (ʹ��ת�������б�)](#36)
1. [����������ʹ�÷���](#4)
1. ��̬��չ�෽�� (ʩ����)
1. ʶ��˳����������ԭ�� (ʩ����)

## 1. ��������������AppKey(id)/SDKKey<a name="1"></a>
-------
������� [����](https://ai.arcsoft.com.cn/ucenter/resource/build/index.html#/login)  
��½�󴴽�Ӧ�� -> ��ȡAppID �� SDK_Key

�뽫�����صĿ��ļ����ղ���ϵͳ�����÷������ļ�Ŀ¼��,  
���ǽ����������Ŀ�·�������Ͷ�ȡ,  
��������ڶ����Ŀͬʱ���ÿ��ļ������,
�����Խ����ļ����ڻ�������λ�û���  
`System32` `systemWoW64` (windows)  
`/lib` `/usr/lib` (linux)

**`��ע��`**,`APPID`��`SDKKEY`��Ϊ���ַ���,������`APP_ID:xxxxxxx`��ֻ����дð�ź�����ַ���`xxxxxxx`����  
�̻߳��ڵ���ʱ����Exception,��׽�������Կ������������� `xx1xx Phase : [xx2xx] xx3xx`  
`xx1xx` �� ���Ľ׶�  
`xx2xx` �� �������Ӣ��  
`xx3xx` �� ���������������  

## 2. TODO and Complete<a name="2"></a>
TODO**                                      | **isComplete** |**UpdateAt**
------------------------------------------------|----------------|----------------
Dll�ⲿ��������                                  | �� | 20220421       
Dll�����������                                  | �� | 20220421       
ASFDetectMultiFace                             | �� | 20220422       
ASFProcessEx_IR                                | pending | /
ASFProcess_IR                                  | pending | /
ASFFaceFeatureExtract                          | pending | /
ASFFaceFeatureExtractEx                        | pending | /
ASFGetAge                                      | �� | 20220423               
ASFGetGender                                   | �� | 20220424               
ASFGetFace3DAngle                              | �� | 20220425        
ASFGetLivenessScore                            | �� | 20220425
ASFGetLivenessScore_IR                         | pending | /

## 3. (����)����ʹ�÷���<a name="3"></a>
### ����ͼƬ���߶�ȡͼƬ(Image����)<a name="30"></a>
```csharp
using Meow.FaceRecon;
using System.Drawing;
using Meow.FaceRecon.SDK.Model;

Meow.FaceRecon.SDK.GlobalSetting.LogMode = -1;
string fp = "D:/1234.jpg";
using var i = Image.FromFile(fp);
```
### 3.1. ����λ�ñ�ע<a name="31"></a>
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
### 3.2. �沿����<a name="32"></a>
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

### 3.3. ͼƬ�Ƿ���ʵ(��֧�ֵ�����)<a name="33"></a>
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

### 3.4. ������Ա�<a name="34"></a>
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

### 3.5. ��ȫ���(ʹ��ԭʼ����)<a name="35"></a>
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

### 3.6. ��ȫ���(ʹ��ת�������б�[Util��̬��չ])<a name="36"></a>
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

## 4.����ʹ�õ�����ؼ�ⷽ��<a name="4"></a>
```csharp
var ep = new FaceReconPool(pwd.appid, pwd.sdkwin, pwd.sdklinux); //����һ���沿ʶ����������
(await ep.DetAllFaceAsync(i)).ConvertIntoFaces()
.ForEach(ix =>{
    Console.WriteLine($"A:{ix.age}|G:{ix.gender}|POS:{ix.pitch}deg:{ix.yaw}deg:{ix.roll}deg");
    i.DrawRectangleInPicture(ix.faceRect, Color.Red);
});
var p = Path.GetFileName(fp).Split(".");
i.Save($"D:/{p[0]}-Recon.{p[^1]}");
```
### ����д��(*)
```csharp
using Meow.FaceRecon;
using MeowFaceReconTest;
using System.Drawing;

Meow.FaceRecon.SDK.GlobalSetting.LogMode = -1;//��־����

string fp = "D:/123.jpg";//�ļ�Ŀ¼
using var i = Image.FromFile(fp);//��ȡImage

var base64str = i.ImgToBase64(); //Util��չת��Image/Base64 (������Ǵ������ȡ)

var ep = new FaceReconPool(pwd.appid, pwd.sdkwin, pwd.sdklinux); //����һ���沿ʶ����������
(await ep.DetAllFaceAsync(base64str.Base64ToImage())) //Base64�ַ���ת��Image
    .ConvertIntoFaces()
    .ForEach(ix =>
{
    Console.WriteLine($"A:{ix.age}|G:{ix.gender}|POS:{ix.pitch}deg:{ix.yaw}deg:{ix.roll}deg");
    i.DrawRectangleInPicture(ix.faceRect, Color.Red);

    Console.WriteLine(i.ImgToBase64()); // Util��չת��Base64
});

var p = Path.GetFileName(fp).Split(".");
i.Save($"D:/{p[0]}-Recon.{p[^1]}");
```