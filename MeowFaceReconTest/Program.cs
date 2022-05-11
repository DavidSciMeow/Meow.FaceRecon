using Meow.FaceRecon;
using Meow.FaceRecon.Skia;
using MeowFaceReconTest;

Meow.FaceRecon.SDK.GlobalSetting.LogMode = -1;

var fp = "D:/test.jpg";
var s = SkiaSharp.SKBitmap.Decode(new SkiaSharp.SKManagedStream(File.OpenRead(fp)));
var ep = new Meow.FaceRecon.Skia.FaceReconPool(pwd.appid, pwd.sdkwin, pwd.sdklinux); //生成一个面部识别引擎管理池
(await ep.DetAllFaceAsync(s)) //Base64字符串转换Image
    .ConvertIntoFaces()
    .ForEach(t =>
{
    s = s.DrawStringAndRect(t);
});
s.Save("D:/testrec.jpg");
