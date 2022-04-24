using Meow.FaceRecon;
using MeowFaceReconTest;
using System.Drawing;

string fp = "D:/1234.jpg";

using var i = Image.FromFile(fp);
/* 人脸位置标注
using var e = new Meow.FaceRecon.SDK.MultiFaceEngine(pwd.appid, pwd.sdkwin);
var b = e.DetectMultiFace(i);
foreach (var ri in b.faceRect)
{
    i.DrawRectangleInPicture(ri,Color.Red);
}
var p = Path.GetFileName(fp).Split(".");
i.Save($"D:/{p[0]}-Recon.{p[^1]}");
*/

/* 面部朝向
using var e = new Meow.FaceRecon.SDK.AngleFaceProcess(pwd.appid, pwd.sdkwin);
var (a,b) = e.Detect3DAngle(i);
for (int j = 0; j < b.num; j++)
{
    i.DrawRectangleInPicture(a.faceRect[j], Color.Red);
    Console.WriteLine($"{b.yaw[j]}|{b.roll[j]}|{b.pitch[j]}|{b.status[j]}");

}
var p = Path.GetFileName(fp).Split(".");
i.Save($"D:/{p[0]}-Recon.{p[^1]}");
*/

/* 年龄和性别
using var e = new Meow.FaceRecon.SDK.AgeFaceProcess(pwd.appid, pwd.sdkwin);
using var e2 = new Meow.FaceRecon.SDK.GenderFaceProcess(pwd.appid, pwd.sdkwin);
var (b,a) = e.DetectAge(i);
var (b2,a2) = e2.DetectGender(i);
for (int j = 0; j < a.num; j++)
{
    Console.WriteLine($"A:{a.ageArray[j]}|G:{a2.genderArray[j]}");
    i.DrawRectangleInPicture(b.faceRect[j], Color.Red);
}
var p = Path.GetFileName(fp).Split(".");
i.Save($"D:/{p[0]}-Recon.{p[^1]}");
*/



