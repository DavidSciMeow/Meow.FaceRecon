using Meow.FaceRecon4;
using Meow.FaceRecon4.SDK;
using Test;

GlobalSetting.LogMode = -1;

var fp = "./test.jpg";
var s = SkiaSharp.SKBitmap.Decode(new SkiaSharp.SKManagedStream(File.OpenRead(fp)));
_ = new FaceReconPool(pwd.appid, pwd.sdkwin, pwd.sdklinux, pwd.winTrailid, pwd.linuxTrailid);
var mfi = new MultiFaceEngine().Detect(s);

//var dfi = mfi.InfoToSDKInfo();
//for (int i = 0; i < dfi.faceNum; i++)
//{
//    s = s.DrawStringAndRect(dfi.faceRect[i], "Face", new(0, 0, 0, 150));
 //   s = s.DrawStringAndRect(dfi.foreheadRect[i], "ForHead", new(255, 255, 255, 150));
//}

Console.WriteLine();
s.Save("D:/testrec.jpg");