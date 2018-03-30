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
    public class List_Typing : Networking.Data.NetworkCommand
    {
        public List_Typing()
        {
            Command = "Battle_Typing_List";
        }

        public string Command { get; }
        public List<BattleManager.Typing.Typing_Data> Types { get; set; }
    }
    public class Get_Typing : Networking.Data.NetworkCommand
    {
        public Get_Typing()
        {
            Command = "Battle_Typing_Get";
        }

        public string Command { get; }
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
