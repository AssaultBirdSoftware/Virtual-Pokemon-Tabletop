using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.BattleManager.Effect
{
    public class Effect_Function
    {
        public string FunctionName { get; set; }
        public string FunctionDesc { get; set; }
        public List<object> Actions { get; set; }

        public void AddAction(object action)
        {
            // Check if object extends an interface
        }
        
        public void Invoke(Action_Handeler.ActionHandeler ActionHandeler, Effect_Function Function, List<object> Targets)
        {
            Variables variables = new Variables();
        }
    }

    public class Variables
    {
        public Variables()
        {
            Variable_Strings = new List<KeyValuePair<string, string>>();
            Variable_Ints = new List<KeyValuePair<string, int>>();
            Variable_Decimals = new List<KeyValuePair<string, decimal>>();
            Variable_Bools = new List<KeyValuePair<string, bool>>();
        }

        #region Lists
        private List<KeyValuePair<string, string>> Variable_Strings;
        private List<KeyValuePair<string, int>> Variable_Ints;
        private List<KeyValuePair<string, decimal>> Variable_Decimals;
        private List<KeyValuePair<string, bool>> Variable_Bools;
        #endregion

        #region Get
        public string Variable_Get_String(string Name)
        {
            return Variable_Strings.Find(x => x.Key == Name).Value;
        }
        public int Variable_Get_Int(string Name)
        {
            return Variable_Ints.Find(x => x.Key == Name).Value;
        }
        public decimal Variable_Get_Decimal(string Name)
        {
            return Variable_Decimals.Find(x => x.Key == Name).Value;
        }
        public bool Variable_Get_Bool(string Name)
        {
            return Variable_Bools.Find(x => x.Key == Name).Value;
        }
        #endregion

        #region Delete
        public void Variable_Delete_String(string Name)
        {
            Variable_Strings.RemoveAll(x => x.Key == Name);
        }
        public void Variable_Delete_Int(string Name)
        {
            Variable_Ints.RemoveAll(x => x.Key == Name);
        }
        public void Variable_Delete_Decimal(string Name)
        {
            Variable_Decimals.RemoveAll(x => x.Key == Name);
        }
        public void Variable_Delete_Bool(string Name)
        {
            Variable_Bools.RemoveAll(x => x.Key == Name);
        }
        #endregion

        #region Set
        public void Variable_Set_String(string Name, string Val)
        {
            if (Variable_Has_String(Name))
                Variable_Delete_String(Name);
            Variable_Strings.Add(new KeyValuePair<string, string>(Name, Val));
        }
        public void Variable_Set_Int(string Name, int Val)
        {
            if (Variable_Has_String(Name))
                Variable_Delete_String(Name);
            Variable_Ints.Add(new KeyValuePair<string, int>(Name, Val));
        }
        public void Variable_Set_Decimal(string Name, decimal Val)
        {
            if (Variable_Has_String(Name))
                Variable_Delete_String(Name);
            Variable_Decimals.Add(new KeyValuePair<string, decimal>(Name, Val));
        }
        public void Variable_Set_Bool(string Name, bool Val)
        {
            if (Variable_Has_Bool(Name))
                Variable_Delete_Bool(Name);
            Variable_Bools.Add(new KeyValuePair<string, bool>(Name, Val));
        }
        #endregion

        #region Has
        public bool Variable_Has_String(string Name)
        {
            if (Variable_Strings.FindAll(x => x.Key == Name).Count >= 1)
                return true;
            return false;
        }
        public bool Variable_Has_Int(string Name)
        {
            if (Variable_Ints.FindAll(x => x.Key == Name).Count >= 1)
                return true;
            return false;
        }
        public bool Variable_Has_Decimal(string Name)
        {
            if (Variable_Decimals.FindAll(x => x.Key == Name).Count >= 1)
                return true;
            return false;
        }
        public bool Variable_Has_Bool(string Name)
        {
            if (Variable_Bools.FindAll(x => x.Key == Name).Count >= 1)
                return true;
            return false;
        }
        #endregion
    }
}
