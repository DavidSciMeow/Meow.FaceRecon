using Meow.FaceRecon;
using System.Drawing;

string appid = "CydqbcTTasdbybCxT57kKWZQmHjaWjUDWkBFML7nWtvv";
//string sdklinux = "2Wy6Yojsjyp7DH9vK88Uz4KPAqkqQweSZLsVqTX8Ezia";
string sdkwin = "2y44ZdPdeNxLGiBYrATCryNB4g8gJyv1BPUTdaq3iLp9";

string fp = "D:/asd.jpeg";
using var i = Image.FromFile(fp);

using var e = new Meow.FaceRecon.SDK.MultiFaceEngine(appid, sdkwin);
var b = e.DetectMultiFace(i);
foreach (var ri in b.faceRect)
{
    i.DrawRectangleInPicture(ri,Color.Red);
}
var p = Path.GetFileName(fp).Split(".");
i.Save($"D:/{p[0]}-Recon.{p[^1]}");




