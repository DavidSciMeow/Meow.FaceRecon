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
    i.DrawRectangleInPicture(ix.faceRect, Color.Red);
    i.DrawString(ix.faceRect, $"{(ix.gender==0?"男":(ix.gender==1?"女":"不确定"))},{ix.age}岁\n头部动作指数:俯仰{Math.Round(ix.pitch)}度:偏航{Math.Round(ix.yaw)}度:滚转{Math.Round(ix.roll)}度", Color.Red);
    Console.WriteLine(i.ImgToBase64()); // Util扩展转换Base64
});

var p = Path.GetFileName(fp).Split(".");
i.Save($"D:/{p[0]}-Recon.{p[^1]}");
