using RetSim.Events;
using System.Collections.Generic;

namespace RetSim
{
    public class Procs : List<Proc>
    {
        private Player Player { get; init; }

        public Procs(Player player)
        {
            Player = player;
        }

        public void Proc(ProcMask procMask, int time, List<Event> resultingEvents)
        {
            foreach (var proc in this)
            {
                if (!Player.Spellbook.IsOnCooldown(proc.Spell) && (proc.ProcMask & procMask) != ProcMask.None && CheckForProc(proc))
                {
                    resultingEvents.Add(new CastEvent(time, Player, proc.Spell)); //TODO: Increase Prio of those cast events
                }
            }
        }

        private static bool CheckForProc(Proc proc)
        {
            return Formulas.Damage.GetRNG(0, 99) < proc.Chance; //TODO: extract RNG to a better place
        }
    }
}
