using System.IO;
using System.Reflection;

namespace Launcher
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
                    .GetManifestResourceStream("Launcher.ProjectVariables.json"))
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