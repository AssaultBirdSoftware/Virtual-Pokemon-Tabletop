using System.IO;
using System.Reflection;

namespace AssaultBird2454.VPTU.SaveEditor
{
    public class ProjectInfo
    {
        public string Version { get; set; }
        public string Compile_Commit { get; set; }
    }

    public static class VersionInfo
    {
        public static ProjectInfo VersioningInfo
        {
            get
            {
                using (var str = Assembly.GetExecutingAssembly()
                    .GetManifestResourceStream("AssaultBird2454.VPTU.SaveEditor.ProjectVariables.json"))
                {
                    using (var read = new StreamReader(str))
                    {
                        return Newtonsoft.Json.JsonConvert.DeserializeObject<ProjectInfo>(read.ReadToEnd());
                    }
                }
            }
        }
    }
}