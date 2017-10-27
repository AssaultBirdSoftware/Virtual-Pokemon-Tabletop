using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.SaveManager.Resource_Data
{
    public enum Resource_Type { Audio, Image }
    public class Resources
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public Resource_Type Type { get; set; }
    }
}
