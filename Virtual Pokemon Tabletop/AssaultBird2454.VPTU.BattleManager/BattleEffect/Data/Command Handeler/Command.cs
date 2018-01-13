using System;

namespace AssaultBird2454.VPTU.BattleManager.BattleEffect.Data.Command_Handeler
{
    public class Command
    {
        /// <summary>
        ///     Creates a new command object
        /// </summary>
        /// <param name="_Name">Command Name</param>
        /// <param name="T">Command Data Type</param>
        /// <param name="_Callback">Callback</param>
        public Command(string _Name, Type T, Action<object> _Callback)
        {
            Name = _Name;
            DataType = T;
            Callback = _Callback;
        }

        public string Name { get; set; } // Command Name
        public Type DataType { get; set; } // Command Data Type
        private Action<object> Callback { get; } // Callback

        /// <summary>
        ///     Invokes the callback
        /// </summary>
        /// <param name="Data">The Data to send the callback</param>
        /// <param name="node">The Client that send the command</param>
        internal void Invoke(object Data)
        {
            Callback.Invoke(Data);
        }
    }
}