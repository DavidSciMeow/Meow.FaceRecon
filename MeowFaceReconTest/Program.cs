using Meow.FaceRecon;
using MeowFaceReconTest;
using System.Drawing;

string fp = "D:/1234.jpg";

using var i = Image.FromFile(fp);
/*
using var e = new Meow.FaceRecon.SDK.MultiFaceEngine(pwd.appid, pwd.sdkwin);
var b = e.DetectMultiFace(i);
foreach (var ri in b.faceRect)
{
    i.DrawRectangleInPicture(ri,Color.Red);
}
var p = Path.GetFileName(fp).Split(".");
i.Save($"D:/{p[0]}-Recon.{p[^1]}");
*/

/*
using var e = new Meow.FaceRecon.SDK.AgeFaceProcess(pwd.appid, pwd.sdkwin);
using var e2 = new Meow.FaceRecon.SDK.GenderFaceProcess(pwd.appid, pwd.sdkwin);
var (b,a) = e.DetectAge(i);
var (b2,a2) = e2.DetectAge(i);
for (int j = 0; j < a.num; j++)
{
    Console.WriteLine($"A:{a.ageArray[j]}|G:{a2.genderArray[j]}");
    i.DrawRectangleInPicture(b.faceRect[j], Color.Red);
}
var p = Path.GetFileName(fp).Split(".");
i.Save($"D:/{p[0]}-Recon.{p[^1]}");
*/

