using Meow.FaceRecon;
using MeowFaceReconTest;
using System.Drawing;

Meow.FaceRecon.SDK.GlobalSetting.LogMode = -1;

string fp = "D:/123.jpg";
using var i = Image.FromFile(fp);

var base64str = i.ImgToBase64(); //Util扩展转换Image/Base64

var ep = new FaceReconPool(pwd.appid, pwd.sdkwin, pwd.sdklinux); //生成一个面部识别引擎管理池
(await ep.DetAllFaceAsync(base64str.Base64ToImage())) //Base64字符串转换Image
    .ConvertIntoFaces()
    .ForEach(ix =>
{
    Console.WriteLine($"A:{ix.age}|G:{ix.gender}|POS:{ix.pitch}deg:{ix.yaw}deg:{ix.roll}deg");
    i.DrawRectangleInPicture(ix.faceRect, Color.Red);

    Console.WriteLine(i.ImgToBase64()); // Util扩展转换Base64
});

var p = Path.GetFileName(fp).Split(".");
i.Save($"D:/{p[0]}-Recon.{p[^1]}");
