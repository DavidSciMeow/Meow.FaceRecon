using Meow.FaceRecon;
using MeowFaceReconTest;
using System.Drawing;

Meow.FaceRecon.SDK.GlobalSetting.LogMode = -1;
string fp = "D:/123.jpg";
using var i = Image.FromFile(fp);
var base64str = i.ImgToBase64();
var ep = new FaceReconPool(pwd.appid, pwd.sdkwin, pwd.sdklinux); //生成一个面部识别引擎管理池
var a = (await ep.DetAllFaceAsync(base64str.Base64ToImage())).ConvertIntoFaces();
foreach (var ix in a)
{
    Console.WriteLine($"A:{ix.age}|G:{ix.gender}|POS:{ix.pitch}deg:{ix.yaw}deg:{ix.roll}deg");
    i.DrawRectangleInPicture(ix.faceRect, Color.Red);
    Console.WriteLine(i.ImgToBase64());
}
var p = Path.GetFileName(fp).Split(".");
i.Save($"D:/{p[0]}-Recon.{p[^1]}");
