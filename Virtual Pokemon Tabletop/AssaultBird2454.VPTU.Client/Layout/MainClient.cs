using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.AvalonDock.Layout.Serialization;

namespace AssaultBird2454.VPTU.Client.Layout
{
    public static class MainClient
    {
        public static void SaveLayout(string FilePath, Xceed.Wpf.AvalonDock.DockingManager Dock)
        {
            var serializer = new XmlLayoutSerializer(Dock);
            using (var stream = new StreamWriter(FilePath))
                serializer.Serialize(stream);
        }

        public static void LoadLayout(string FilePath, Xceed.Wpf.AvalonDock.DockingManager Dock)
        {
            var serializer = new XmlLayoutSerializer(Dock);
            using (var stream = new StreamReader(FilePath))
                serializer.Deserialize(stream);
        }
    }
}
