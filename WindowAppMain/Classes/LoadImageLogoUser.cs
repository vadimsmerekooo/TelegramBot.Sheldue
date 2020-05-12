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
                //if (File.Exists($"../../Resource/LogoImageAccount/User{loginUser}_Image.dat"))
                //{
                //    try
                //    {
                //        byte[] byteArray = File.ReadAllBytes($"../../ Resource / LogoImageAccount / User{ loginUser }_Image.dat");
                //        DecoderBitmapImage decByte = new DecoderBitmapImage();
                //        return decByte.ByteArrayToImage(byteArray);
                //    }
                //    catch
                //    {
                //        string imagePath = $"../../Resource/logoAccount.png";
                //        Uri imageUri = new Uri(imagePath, UriKind.RelativeOrAbsolute);
                //        return new BitmapImage(imageUri);
                //    }
                //    //string imagePath = $"../../Resource/LogoImageAccount/User{loginUser}_Image.txt";
                //    //Uri imageUri = new Uri(imagePath, UriKind.RelativeOrAbsolute);
                //    //return new BitmapImage(imageUri);
                //}
                //else
                //{
                //    string imagePath = $"../../Resource/logoAccount.png";
                //    Uri imageUri = new Uri(imagePath, UriKind.RelativeOrAbsolute);
                //    return new BitmapImage(imageUri);
                //}
                using (FileStream fstream = File.OpenRead($"../../Resource/LogoImageAccount/User{ loginUser }_Image.dat"))
                {
                    byte[] byteArray = new byte[fstream.Length];
                    fstream.Read(byteArray, 0, byteArray.Length);
                    DecoderBitmapImage decByte = new DecoderBitmapImage();
                    return decByte.ByteArrayToImage(byteArray);
                }
            }
            catch
            {
                string imagePath = $"../../Resource/logoAccount.png";
                Uri imageUri = new Uri(imagePath, UriKind.RelativeOrAbsolute);
                return new BitmapImage(imageUri);
            }
        }
    }
}
