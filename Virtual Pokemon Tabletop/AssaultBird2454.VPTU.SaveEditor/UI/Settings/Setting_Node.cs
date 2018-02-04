using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.SaveEditor.UI.Settings
{
    public enum Setting_NodeType { Bool, Button, Decimal, DropDown, String }

    public delegate void Node_Setting_Clicked();
    public delegate void Node_Setting_Changed(object Old, object New);

    public class Setting_Node
    {
        public string Name { get; set; }
        public string Dir { get; set; }
        public object Value { get; set; }
        public object Default { get; set; }
        public Setting_NodeType Type { get; set; }

        public event Node_Setting_Clicked Node_Setting_Clicked_Event;
        public event Node_Setting_Changed Node_Setting_Changed_Event;
    }
}
