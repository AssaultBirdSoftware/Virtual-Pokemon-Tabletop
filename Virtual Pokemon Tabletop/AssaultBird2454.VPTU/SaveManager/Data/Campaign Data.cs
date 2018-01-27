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
                Server_Address = "";
                Server_Port = 25444;
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

            if (string.IsNullOrWhiteSpace(Server_Address))
            {
                Server_Address = "";
            }
        }

        public string Campaign_Name { get; set; }
        public string Campaign_Desc { get; set; }
        public string Campaign_GM_Name { get; set; }

        public string Server_Address { get; set; }
        public int Server_Port { get; set; }
    }

    public class Campaign_Settings
    {
        public Campaign_Settings(bool InitNewSave = false)
        {
            if (InitNewSave)
            {
                // New Save
            }
        }

        public void InitNullObjects()
        {
            // Check for Null and Set to new object
        }

        // Settings and variables
    }

    public class Server_Settings
    {
        public Server_Settings(bool InitNewSave = false)
        {
            if (InitNewSave)
            {
                // New Save
            }
        }

        public void InitNullObjects()
        {
            // Check for Null and Set to new object
        }

        // Settings and variables
    }
}
