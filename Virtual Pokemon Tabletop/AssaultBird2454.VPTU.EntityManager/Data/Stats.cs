using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.EntityManager.Data
{
    public delegate void StatsChanged();

    public class Stats
    {
        public string Name { get; set; }

        public event StatsChanged StatsChanged;

        #region Species Base
        private int SpeciesBase;
        public int Species
        {
            get
            {
                return SpeciesBase;
            }
            set
            {
                SpeciesBase = value;
                StatsChanged?.Invoke();
            }
        }
        #endregion

        #region Base Mod
        private int BaseMod;
        public int Mod
        {
            get
            {
                return BaseMod;
            }
            set
            {
                BaseMod = value;
                StatsChanged?.Invoke();
            }
        }
        #endregion

        #region Base Stat
        public int Base
        {
            get
            {
                return Species + Mod;
            }
        }
        #endregion

        #region Add Stat
        private int AddStat;
        public int Add
        {
            get
            {
                return AddStat;
            }
            set
            {
                AddStat = value;
                StatsChanged?.Invoke();
            }
        }
        #endregion

        #region Total
        public int Total
        {
            get
            {
                return Base + Add;
            }
        }
        #endregion

        #region Combat Stage
        private int CS_Stage;
        public int CS
        {
            get
            {
                return CS_Stage;
            }
            set
            {
                CS_Stage = value;
                StatsChanged?.Invoke();
            }
        }
        #endregion

        #region Total
        public int Adjusted
        {
            get
            {
                switch (CS)
                {
                    case -6:
                        return (int)Math.Floor(Total * 0.4);

                    case -5:
                        return (int)Math.Floor(Total * 0.5);

                    case -4:
                        return (int)Math.Floor(Total * 0.6);

                    case -3:
                        return (int)Math.Floor(Total * 0.7);

                    case -2:
                        return (int)Math.Floor(Total * 0.8);

                    case -1:
                        return (int)Math.Floor(Total * 0.9);

                    case 0:
                        return Total;

                    case 1:
                        return (int)Math.Floor(Total * 1.2);

                    case 2:
                        return (int)Math.Floor(Total * 1.4);

                    case 3:
                        return (int)Math.Floor(Total * 1.6);

                    case 4:
                        return (int)Math.Floor(Total * 1.8);

                    case 5:
                        return (int)Math.Floor(Total * 2.0);

                    case 6:
                        return (int)Math.Floor(Total * 2.2);

                    default:
                        return Total;
                }
            }
        }
        #endregion
    }
}
