using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.BattleManager.Typing
{
    public class Typing_Data
    {
        /// <summary>
        /// The name of the type
        /// </summary>
        public string Type_Name { get; set; }
        /// <summary>
        /// The hex color code for the type
        /// </summary>
        public System.Windows.Media.Color Type_Color { get; set; }

        /// <summary>
        /// The Resource ID for the Typeing Icon
        /// </summary>
        public string Resource_Icon { get; set; }
        /// <summary>
        /// The Resource ID for the Typeing Badge
        /// </summary>
        public string Resource_Badge { get; set; }

        /// <summary>
        /// List other types that have no effect against this type
        /// </summary>
        public List<string> Effect_Immune { get; set; }
        /// <summary>
        /// List other types that are not very effective against this type
        /// </summary>
        public List<string> Effect_NotVery { get; set; }
        /// <summary>
        /// List other types that have no type advantages against this type
        /// </summary>
        public List<string> Effect_Normal { get; set; }
        /// <summary>
        /// List other types that are super effective against this type
        /// </summary>
        public List<string> Effect_SuperEffective { get; set; }
    }
}
