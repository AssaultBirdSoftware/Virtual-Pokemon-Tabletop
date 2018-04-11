using AssaultBird2454.VPTU.Networking.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.Server.Instances.CommandData.Entities
{
    public class Entities_Pokemon_Get : NetworkCommand
    {
        public string Command { get { return "Entities_Pokemon_Get"; } }
        public bool Waiting { get; set; }
        public string Waiting_Code { get; set; }
        public ResponseCode Response { get; set; }
        public string Response_Message { get; set; }

        public string ID { get; set; }
        public EntitiesManager.Pokemon.PokemonCharacter Pokemon { get; set; }
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
