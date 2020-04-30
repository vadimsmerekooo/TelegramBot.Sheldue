using System;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WindowAppMain.Classes
{
    class DecoderBitmapImage
    {
        public byte[] ImageToByteArray(ImageSource imageIn)
        {
            MemoryStream memStream = new MemoryStream();
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(imageIn as BitmapImage));
            encoder.Save(memStream);
            return memStream.ToArray();
        }
        public byte[] ImageToByteArray(BitmapImage imageIn)
        {
            MemoryStream memStream = new MemoryStream();
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(imageIn as BitmapImage));
            encoder.Save(memStream);
            return memStream.ToArray();
        }



        public BitmapImage ByteArrayToImage(byte[] byteArray)
        {
            MemoryStream memorystream = new MemoryStream();
            memorystream.Write(byteArray, 0, (int)byteArray.Length);
            memorystream.Seek(0, SeekOrigin.Begin);
            BitmapImage imgsource = new BitmapImage();
            imgsource.BeginInit();
            imgsource.StreamSource = memorystream;
            imgsource.EndInit();
            return imgsource;
        }
    }
}
