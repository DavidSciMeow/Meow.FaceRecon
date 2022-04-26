using Meow.FaceRecon;
using MeowFaceReconTest;
using System.Drawing;
using Meow.FaceRecon.SDK.Model;

string fp = "D:/1234.jpg";

using var i = Image.FromFile(fp);
/* 人脸位置标注
using var e = new Meow.FaceRecon.SDK.MultiFaceEngine(pwd.appid, pwd.sdkwin);
var b = e.Detect(i);
foreach (var ri in b.faceRect)
{
    i.DrawRectangleInPicture(ri,Color.Red);
}
var p = Path.GetFileName(fp).Split(".");
i.Save($"D:/{p[0]}-Recon.{p[^1]}");
*/

/* 面部朝向
using var e = new Meow.FaceRecon.SDK.AngleFaceProcess(pwd.appid, pwd.sdkwin);
var (a,b) = e.Detect(i);
for (int j = 0; j < b.num; j++)
{
    i.DrawRectangleInPicture(a.faceRect[j], Color.Red);
    Console.WriteLine($"{b.yaw[j]}|{b.roll[j]}|{b.pitch[j]}|{b.status[j]}");

}
var p = Path.GetFileName(fp).Split(".");
i.Save($"D:/{p[0]}-Recon.{p[^1]}");
*/

/* 图片是否真实 
using var e = new Meow.FaceRecon.SDK.LivenessFaceProcess(pwd.appid, pwd.sdkwin);
var (a,b) = e.Detect(i);
for (int j = 0; j < b.num; j++)
{
    i.DrawRectangleInPicture(a.faceRect[j], Color.Red);
    Console.WriteLine($"{b.isLive[j]}");

}
var p = Path.GetFileName(fp).Split(".");
i.Save($"D:/{p[0]}-Recon.{p[^1]}");
*/

/* 年龄和性别
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
*/

/*完全检查(使用原始数组)
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
*/

/*完全检查(使用转换人脸列表)
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
*/