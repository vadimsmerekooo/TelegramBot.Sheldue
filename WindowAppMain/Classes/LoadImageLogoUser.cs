using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace WindowAppMain.Classes
{
    class LoadImageLogoUser
    {
        public BitmapImage SelectImage(string loginUser)
        {
            try
            {
                using (FileStream fstream = File.OpenRead($"../../Resource/LogoImageAccount/User{ loginUser }_Image.dat"))
                {
                    byte[] byteArray = new byte[fstream.Length];
                    fstream.Read(byteArray, 0, byteArray.Length);
                    return new DecoderBitmapImage().ByteArrayToImage(byteArray);
                }
            }
            catch
            {
                return new BitmapImage(new Uri($"../../Resource/logoAccount.png", UriKind.RelativeOrAbsolute));
            }
        }
    }
}
