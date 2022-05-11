using Meow.FaceRecon;
using Meow.FaceRecon.SDK;
using MeowFaceReconTest;

GlobalSetting.LogMode = -1;
var fp = "./test.jpg";
var s = SkiaSharp.SKBitmap.Decode(new SkiaSharp.SKManagedStream(File.OpenRead(fp)));
var ep = new FaceReconPool(pwd.appid, pwd.sdkwin, pwd.sdklinux);
var (mfi, afi) = new AgeFaceProcess(ep.Appid, ep.Key).Detect(s);
var (_, gfi) = new GenderFaceProcess(ep.Appid, ep.Key).Detect(s);

var fs = new Meow.FaceRecon.SDK.Model.SDK_FaceGeneral();
for (int i = 0; i < mfi.faceNum; i++)
{
    fs.faceRect.Add(mfi.faceRect[i]);
    fs.ageArray.Add(afi.ageArray[i]);
    fs.genderArray.Add(gfi.genderArray[i]);
}
fs.ConvertIntoFaces().ForEach(t => {
    s = s.DrawStringAndRect(t);
});
Console.WriteLine("已存储");
s.Save("./testrec.jpg");
