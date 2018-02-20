using AssaultBird2454.VPTU.Networking.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.Server.Instances.CommandData.Auth
{
    public enum AuthState { Authenticated, DeAuthenticated }
    public class Login : NetworkCommand
    {
        public string Command { get { return "Auth_Login"; } }
        public bool Waiting { get; set; }
        public string Waiting_Code { get; set; }
        public ResponseCode Response { get; set; }
        public string Response_Message { get; set; }

        public string Client_Key { get; set; }
        public Authentication_Manager.Data.User UserData { get; set; }
        public AuthState Auth_State { get; set; }
    }
    public class Logout
    {
        public AuthState Auth_State { get; set; }
    }
}
