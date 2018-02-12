using AssaultBird2454.VPTU.Networking.Server.TCP;
using System;
using System.Collections.Generic;

namespace AssaultBird2454.VPTU.Networking.Server.Command_Handeler
{
    #region Delegates
    public delegate Data.Response Command_Callback(object Data, TCP_ClientNode Client, bool Waiting);
    public delegate void RateLimiting(string Command, bool Enabled, int Limit);
    #endregion

    public class Command
    {
        public event RateLimiting RateLimitChanged_Event;// Event invoked when ratelimiting settings changed
        public Command_Callback Command_Executed;// Event invoked when the command is executed

        /// <summary>
        /// Creates a new command object
        /// </summary>
        /// <param name="_Name">Command Name</param>
        /// <param name="T">Command Data Type</param>
        public Command(string _Name, Type T)
        {
            Name = _Name;
            DataType = T;
        }
        private bool _Rate_Enable = false;
        private int _Rate_Limit = 60;

        public bool Rate_Enabled
        {
            get { return _Rate_Enable; }
        }
        public int Rate_Limit
        {
            get { return _Rate_Limit; }
        }
        public void SetRateLimit(bool Enabled, int Limit = 60)
        {
            _Rate_Enable = Enabled;
            _Rate_Limit = Limit;
            RateLimitChanged_Event?.Invoke(Name, Rate_Enabled, Rate_Limit);
        }

        public string Name { get; set; }// Command Name
        public Type DataType { get; set; }// Command Data Type

        internal void Invoke(Data.NetworkCommand Data, TCP_ClientNode Client)
        {
            Data.Response response = Command_Executed?.Invoke(Data.Data, Client, Data.Waiting);

            if (Data.Waiting)
            {
                Data.Response = response.Code;
                Client.Send(Data, Data.Command);
            }
        }
    }
}