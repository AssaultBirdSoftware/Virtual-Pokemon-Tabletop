using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.Server.Instances.CommandData.Resources
{
    public class ImageResource : VPTU.Networking.Data.NetworkCommand
    {
        public string Command { get; set; }
        public string UseCommand { get; set; }
        public string UseID { get; set; }
        public string Location { get; set; }
        public byte[] ImageData { get; set; }

        [JsonIgnore]
        public Bitmap Image
        {
            get
            {
                Bitmap bmp;
                using (var ms = new MemoryStream(ImageData))
                {
                    bmp = new Bitmap(ms);
                }

                return bmp;
            }
            set
            {
                using (var stream = new MemoryStream())
                {
                    value.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                    ImageData = stream.ToArray();
                }
            }
        }
    }
}
