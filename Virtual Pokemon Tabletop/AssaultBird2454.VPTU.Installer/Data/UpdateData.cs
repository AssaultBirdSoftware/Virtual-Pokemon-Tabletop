using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.Installer.Data
{
    public class VersionName
    {
        public int Build_ID { get; set; }
        public string Version_Name { get; set; }
    }

    public class UpdateData
    {
        public int Build_ID { get; set; }
        public string Commit_ID { get; set; }
        public bool Commit_Verified { get; set; }
        public string Download_Bin { get; set; }
        public string Download_Binx64 { get; set; }
        public string Download_Binx86 { get; set; }
        public string Version_Name { get; set; }
        public string Release_Page { get; set; }
        public string Version_String { get; set; }
        public Stream Version_Type { get; set; }
    }

    public class Version_List
    {
        public List<VersionName> Alpha { get; set; }
        public List<VersionName> Beta { get; set; }
        public List<VersionName> Master { get; set; }
    }
}
