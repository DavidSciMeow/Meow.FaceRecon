using Meow.FaceRecon;
using MeowFaceReconTest;
using System.Drawing;

string fp = "D:/asd.jpeg";

using var i = Image.FromFile(fp);
using var e = new Meow.FaceRecon.SDK.MultiFaceEngine(pwd.appid, pwd.sdkwin);
var b = e.DetectMultiFace(i);
foreach (var ri in b.faceRect)
{
    i.DrawRectangleInPicture(ri,Color.Red);
}
var p = Path.GetFileName(fp).Split(".");
i.Save($"D:/{p[0]}-Recon.{p[^1]}");




