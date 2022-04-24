# Meow.FaceRecon
 ## 1 SDK���ڱ�д��,û�з���Nuget.
 ```
 ���������Dll/so�����Ѿ�д��, ע������
 �������Լ����û����Լ���չ����
 ```
 ## 2 TODO and Complete
  **TODO**                                      | **isComplete** 
-----------------------------------------------|----------------
 Dll�ⲿ��������                                 | true           
 Dll�����������                                 | true           
 ASFDetectMultiFace                            | true : Useable 
 ASFProcess_IR/ASFProcessEx_IR                 | pending        
 ASFFaceFeatureExtract/ASFFaceFeatureExtractEx | pending        
 ASFGetAge                                     | true : Useable         
 ASFGetGender                                  | true : Useable         
 ASFGetFace3DAngle                             | pending        
 ASFGetLivenessScore/ASFGetLivenessScore_IR    | pending     
 ## 3 ����ʹ�÷���
 ### 3.1 ������������ߵ�������λ��
 `������Բο�Test��ʾ��`
 ```csharp
    string fp = "D:/asd.jpg"; //�ļ���
    using var i = Image.FromFile(fp);
    using var e = new Meow.FaceRecon.SDK.MultiFaceEngine(pwd.appid, pwd.sdkwin);//���뵽��appid,sdkkey
    var b = e.DetectMultiFace(i);
    foreach (var ri in b.faceRect)
    {
        i.DrawRectangleInPicture(ri,Color.Red);
    }
    var p = Path.GetFileName(fp).Split(".");
    i.Save($"D:/{p[0]}-Recon.{p[^1]}");
 ```
 ### 3.2 �����������������Ա�
 `����ɲο�Test��ʾ��`
 ```csharp
    string fp = "D:/asd.jpg"; //�ļ���
    using var e = new Meow.FaceRecon.SDK.AgeFaceProcess(pwd.appid, pwd.sdkwin);//���뵽��appid,sdkkey
    using var e2 = new Meow.FaceRecon.SDK.GenderFaceProcess(pwd.appid, pwd.sdkwin);//���뵽��appid,sdkkey
    var (b,a) = e.DetectAge(i);
    var (b2,a2) = e2.DetectAge(i);
    for (int j = 0; j < a.num; j++)
    {
        Console.WriteLine($"A:{a.ageArray[j]}|G:{a2.genderArray[j]}");
        i.DrawRectangleInPicture(b.faceRect[j], Color.Red);
    }
    var p = Path.GetFileName(fp).Split(".");
    i.Save($"D:/{p[0]}-Recon.{p[^1]}");
 ```