using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.BattleManager.Battle
{
    public delegate void Participant_Changed(Battle_Participant Participant);
    public delegate void Participants_Cleared();

    public class Battle_Instance
    {
        public Battle_Instance()
        {

        }

        #region Participants
        private List<Battle_Participant> _Participants { get; set; }

        public event Participant_Changed Participants_Added_Event;
        public event Participant_Changed Participants_Removed_Event;
        public event Participants_Cleared Participants_Cleared_Event;

        public void Participants_Add(Battle_Participant Participant)
        {
            _Participants.Add(Participant);
            Participants_Added_Event?.Invoke(Participant);
        }
        public void Participants_Remove(Battle_Participant Participant)
        {
            _Participants.Remove(Participant);
            Participants_Removed_Event?.Invoke(Participant);
        }
        public void Participants_Remove(string ID)
        {
            Battle_Participant Participant = _Participants.Find(x => x.ID == ID);

            _Participants.Remove(Participant);
            Participants_Removed_Event?.Invoke(Participant);
        }
        public Battle_Participant Participants_Get(string ID)
        {
            return _Participants.Find(x => x.ID == ID);
        }
        public IEnumerable<Battle_Participant> Participants_Get()
        {
            return _Participants;
        }
        public void Participants_Clear()
        {
            _Participants.Clear();
            Participants_Cleared_Event?.Invoke();
        }
        #endregion

        #region Effects

        #endregion
    }
}
