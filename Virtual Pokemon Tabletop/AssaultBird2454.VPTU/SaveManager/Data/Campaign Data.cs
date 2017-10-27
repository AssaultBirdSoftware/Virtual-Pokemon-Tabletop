using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.SaveManager.Data
{
    public class Campaign_Data
    {
        public Campaign_Data(bool InitNewSave = false)
        {
            if (InitNewSave)
            {
                Campaign_Name = "Campaign Name";
                Campaign_GM_Name = "Campaign Game Masters Name";
                Campaign_Desc = "Campaigns Description / Plot Hook / What ever you want";
            }
        }

        public void InitNullObjects()
        {
            if (string.IsNullOrWhiteSpace(Campaign_Name))
            {
                Campaign_Name = "";
            }

            if (string.IsNullOrWhiteSpace(Campaign_GM_Name))
            {
                Campaign_GM_Name = "";
            }

            if (string.IsNullOrWhiteSpace(Campaign_Desc))
            {
                Campaign_Desc = "";
            }
        }

        public string Campaign_Name { get; set; }
        public string Campaign_Desc { get; set; }
        public string Campaign_GM_Name { get; set; }
    }
}
