using AssaultBird2454.VPTU.Networking.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.Server.Instances.CommandData.Battle
{
    public class List_Typing : NetworkCommand
    {
        public string Command { get { return "Battle_Typing_List"; } }
        public bool Waiting { get; set; }
        public string Waiting_Code { get; set; }
        public ResponseCode Response { get; set; }
        public string Response_Message { get; set; }

        public List<BattleManager.Typing.Typing_Data> Types { get; set; }
    }
    public class Get_Typing : NetworkCommand
    {
        public string Command { get { return "Battle_Typing_Get"; } }
        public bool Waiting { get; set; }
        public string Waiting_Code { get; set; }
        public ResponseCode Response { get; set; }
        public string Response_Message { get; set; }

        public string Type_Name { get; set; }
        public BattleManager.Typing.Typing_Data Type { get; set; }
        public byte[] Type_Icon { get; set; }
        public byte[] Type_Badge { get; set; }

        [JsonIgnore]
        public Bitmap Type_Icon_Image
        {
            get
            {
                Bitmap bmp;
                using (var ms = new MemoryStream(Type_Icon))
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
                    Type_Icon = stream.ToArray();
                }
            }
        }
        [JsonIgnore]
        public Bitmap Type_Badge_Image
        {
            get
            {
                Bitmap bmp;
                using (var ms = new MemoryStream(Type_Badge))
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
                    Type_Badge = stream.ToArray();
                }
            }
        }
    }
}
