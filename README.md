# Meow.FaceRecon
 ## 1 SDK���ڱ�д��,û�з���Nuget.
 ```
 ���������Dll/so�����Ѿ�д��, ע������
 �������Լ����û����Լ���չ����
 ```
 ## 2 TODO and Complete
  **TODO**                                      | **isComplete** 
-----------------------------------------------|----------------
 Dll�ⲿ��������                                     | true           
 Dll�����������                                     | true           
 ASFDetectMultiFace                            | true : Useable 
 ASFProcess/ASFProcessEx                       | pending        
 ASFProcess_IR/ASFProcessEx_IR                 | pending        
 ASFFaceFeatureExtract/ASFFaceFeatureExtractEx | pending        
 ASFGetAge                                     | pending        
 ASFGetGender                                  | pending        
 ASFGetFace3DAngle                             | pending        
 ASFGetLivenessScore/ASFGetLivenessScore_IR    | pending     
 ## 3 ����ʹ�÷���
 ### 3.1 ������������ߵ�������λ��
 `������Բο�Test��ʾ��`
 ```csharp
    string fp = "D:/asd.jpeg"; //�ļ���
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