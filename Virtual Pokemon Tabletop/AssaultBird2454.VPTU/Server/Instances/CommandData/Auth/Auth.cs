using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.Server.Instances.CommandData.Auth
{
    public enum AuthState { Passed, Failed, DeAuthenticated }
    public class Login
    {
        public string Client_Key { get; set; }
        public Authentication_Manager.Data.User UserData { get; set; }
        public AuthState Auth_State { get; set; }
    }
    public class Logout : Networking.Data.NetworkCommand
    {
        public AuthState Auth_State { get; set; }
    }
}
