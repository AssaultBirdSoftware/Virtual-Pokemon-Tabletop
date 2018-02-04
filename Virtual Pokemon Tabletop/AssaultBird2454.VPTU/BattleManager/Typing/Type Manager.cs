using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.BattleManager.Typing
{
    public class Manager
    {
        public Manager(bool Init)
        {
            if (Init)
                Types = new List<Typing_Data>();
        }

        public void InitNullObjects()
        {
            if (Types == null)
                Types = new List<Typing_Data>();
        }

        /// <summary>
        /// A list of all the types in the campaign
        /// </summary>
        public List<Typing_Data> Types { get; set; }

        /// <summary>
        /// Returns a list of all types in the savefile
        /// </summary>
        [JsonIgnore]
        public List<string> Type_Names
        {
            get
            {
                List<string> list = new List<string>();
                foreach (Typing_Data type in Types)
                    list.Add(type.Type_Name);
                return list;
            }
        }

        /// <summary>
        /// Calculates what the bonus for attacks should be
        /// </summary>
        /// <param name="AttackingType">The Type of the attack</param>
        /// <param name="DefendingType">A List of all types that the defending target is</param>
        /// <returns>The type advantage bonus</returns>
        public decimal Calculate_TypeBonus(string AttackingType, List<string> DefendingType)
        {
            int Bonus = 0;

            Typing_Data a_type;
            try
            {
                a_type = Types.Find(x => x.Type_Name == AttackingType);
            }
            catch { return 0; }

            foreach (string type in DefendingType)
            {
                Typing_Data d_type;
                try
                {
                    d_type = Types.Find(x => x.Type_Name == type);

                    if (d_type.Effect_Immune.Contains(a_type.Type_Name))
                    {
                        return 0;
                    }
                    else if (d_type.Effect_Normal.Contains(a_type.Type_Name))
                    {
                        continue;
                    }
                    if (d_type.Effect_NotVery.Contains(a_type.Type_Name))
                    {
                        Bonus -= 1;
                    }
                    if (d_type.Effect_SuperEffective.Contains(a_type.Type_Name))
                    {
                        Bonus += 1;
                    }
                }
                catch { continue; }
            }

            if (Bonus == 1)
                return 1.5M;
            else if (Bonus == 2)
                return 2.0M;
            else if (Bonus >= 3)
                return 3.0M;
            else if (Bonus == -1)
                return 0.5M;
            else if (Bonus == -2)
                return 0.25M;
            else if (Bonus <= -3)
                return 0.125M;
            else
                return 1.0M;
        }
    }
}
