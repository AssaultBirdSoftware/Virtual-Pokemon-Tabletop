using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace AssaultBird2454.VPTU.BattleManager
{
    public class BattleManager
    {
        public BattleManager()
        {
            #region Versioning Info

            using (var str = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("AssaultBird2454.VPTU.BattleManager.ProjectVariables.json"))
            {
                using (var read = new StreamReader(str))
                {
                    VersioningInfo = JsonConvert.DeserializeObject<ProjectInfo>(read.ReadToEnd());
                }
            }

            #endregion
        }

        public ProjectInfo VersioningInfo { get; }
    }
}