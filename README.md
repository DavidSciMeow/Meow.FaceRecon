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
1. ��̬��չ�෽�� (ʩ����)
1. ʶ��˳����������ԭ�� (ʩ����)

## 1. ��������������AppKey(id)/SDKKey<name id="1"></name>
-------
������� [����](https://ai.arcsoft.com.cn/ucenter/resource/build/index.html#/login)  
��½�󴴽�Ӧ�� -> ��ȡAppID �� SDK_Key

**`��ע��`**,`APPID`��`SDKKEY`��Ϊ���ַ���,������`APP_ID:xxxxxxx`��ֻ����дð�ź�����ַ���`xxxxxxx`����  
�̻߳��ڵ���ʱ����Exception,��׽�������Կ������������� `xx1xx Phase : [xx2xx] xx3xx`  
`xx1xx` �� ���Ľ׶�  
`xx2xx` �� �������Ӣ��  
`xx3xx` �� ���������������  

## 2. TODO and Complete<name id="2"></name>
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

## 3. (����)����ʹ�÷���<name id="3"></name>
### ����ͼƬ���߶�ȡͼƬ(Image����)<name id="30"></name>
```csharp
using Meow.FaceRecon;
using MeowFaceReconTest;
using System.Drawing;
using Meow.FaceRecon.SDK.Model;

string fp = "D:/1234.jpg";
using var i = Image.FromFile(fp);
```
### 3.1. ����λ�ñ�ע<name id="31"></name>
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
### 3.2. �沿����<name id="32"></name>
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

### 3.3. ͼƬ�Ƿ���ʵ(��֧�ֵ�����)<name id="33"></name>
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

### 3.4. ������Ա�<name id="34"></name>
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

### 3.5. ��ȫ���(ʹ��ԭʼ����)<name id="35"></name>
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

### 3.6. ��ȫ���(ʹ��ת�������б�[Util��̬��չ])<name id="36"></name>
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
