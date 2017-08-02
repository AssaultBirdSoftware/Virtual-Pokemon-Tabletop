using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.BattleManager.BattleEffect.Data
{
    public class Variable
    {
        public Variable() { }

        /// <summary>
        /// Variable Name
        /// </summary>
        public string Variable_Name { get; set; }

        /// <summary>
        /// Variable Data Type
        /// </summary>
        public Type Variable_Type { get; private set; }

        /// <summary>
        /// Variable Data
        /// </summary>
        private object _Variable_Data;

        /// <summary>
        /// Gets the Variable Data
        /// </summary>
        /// <typeparam name="T">The Type of variable data</typeparam>
        /// <returns>Variable</returns>
        public T Variable_Data<T>()
        {
            return (T)_Variable_Data;
        }
        /// <summary>
        /// Sets the Variable with the defined new data, it will also update the Variable_Type Property
        /// </summary>
        /// <typeparam name="T">The Typeof the object being set</typeparam>
        /// <param name="Data">The data being set</param>
        public void Varieble_Data<T>(T Data)
        {
            _Variable_Data = Data;
            Variable_Type = typeof(T);
        }
    }
}
