
using System;
using System.IO;

namespace CreeGuanajuato.Utils
{
    public class Global
    {

        public void saveImageBase64(string imageBase64) {
            byte[] imageBytes = Convert.FromBase64String(imageBase64);
            MemoryStream ms = new MemoryStream(imageBytes, 0,imageBytes.Length);
            
            ms.Write(imageBytes, 0, imageBytes.Length);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
            
        }
    }
}
