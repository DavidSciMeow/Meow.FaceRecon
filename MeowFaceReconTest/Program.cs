using Meow.FaceRecon;
using Meow.FaceRecon.SDK;
using MeowFaceReconTest;

GlobalSetting.LogMode = -1;

var fp = "D:/123.png";
var s = SkiaSharp.SKBitmap.Decode(new SkiaSharp.SKManagedStream(File.OpenRead(fp)));
var ep = new FaceReconPool(pwd.appid, pwd.sdkwin, pwd.sdklinux);
var mfi = new MultiFaceEngine(ep.Appid, ep.Key).Detect(s);
var dfi = mfi.InfoToSDKInfo();
var afi = new AgeFaceProcess(ep.Appid, ep.Key).Detect(s,mfi);
var gfi = new GenderFaceProcess(ep.Appid, ep.Key).Detect(s,mfi);
var lfi = new LivenessFaceProcess(ep.Appid, ep.Key).Detect(s, mfi);
var agfi = new AngleFaceProcess(ep.Appid, ep.Key).Detect(s, mfi);

var fs = new Meow.FaceRecon.SDK.Model.SDK_FaceGeneral();
fs.faceNum = dfi.faceNum;
for (int i = 0; i < dfi.faceNum; i++)
{
    fs.faceRect.Add(dfi.faceRect[i]);
    fs.ageArray.Add(afi.ageArray[i]);
    fs.genderArray.Add(gfi.genderArray[i]);
    fs.liveness.Add(lfi.isLive[i]);
    fs.pitch.Add(agfi.pitch[i]);
    fs.yaw.Add(agfi.yaw[i]);
    fs.roll.Add(agfi.roll[i]);
    fs.status.Add(agfi.status[i]);
}

foreach(var t in fs.ConvertIntoFaces())
{
    s = s.DrawStringAndRect(t);
}

s.Save("D:/testrec.jpg");


