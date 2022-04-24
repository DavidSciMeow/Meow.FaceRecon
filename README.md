# Meow.FaceRecon
 ## 1 SDK仍在编写中,没有发布Nuget.
 ```
 基础的类和Dll/so调用已经写明, 注释完整
 您可以自己调用或者自己扩展基类
 ```
 ## 2 TODO and Complete
  **TODO**                                      | **isComplete** 
-----------------------------------------------|----------------
 Dll外部调用引用                                 | true           
 Dll基础引擎管理                                 | true           
 ASFDetectMultiFace                            | true : Useable 
 ASFProcess_IR/ASFProcessEx_IR                 | pending        
 ASFFaceFeatureExtract/ASFFaceFeatureExtractEx | pending        
 ASFGetAge                                     | true : Useable         
 ASFGetGender                                  | true : Useable         
 ASFGetFace3DAngle                             | pending        
 ASFGetLivenessScore/ASFGetLivenessScore_IR    | pending     
 ## 3 简易使用方法
 ### 3.1 检测多个人脸或者单个人脸位置
 `具体可以参考Test的示例`
 ```csharp
    string fp = "D:/asd.jpg"; //文件名
    using var i = Image.FromFile(fp);
    using var e = new Meow.FaceRecon.SDK.MultiFaceEngine(pwd.appid, pwd.sdkwin);//申请到的appid,sdkkey
    var b = e.DetectMultiFace(i);
    foreach (var ri in b.faceRect)
    {
        i.DrawRectangleInPicture(ri,Color.Red);
    }
    var p = Path.GetFileName(fp).Split(".");
    i.Save($"D:/{p[0]}-Recon.{p[^1]}");
 ```
 ### 3.2 检测多个人脸的年龄和性别
 `具体可参考Test的示例`
 ```csharp
    string fp = "D:/asd.jpg"; //文件名
    using var e = new Meow.FaceRecon.SDK.AgeFaceProcess(pwd.appid, pwd.sdkwin);//申请到的appid,sdkkey
    using var e2 = new Meow.FaceRecon.SDK.GenderFaceProcess(pwd.appid, pwd.sdkwin);//申请到的appid,sdkkey
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