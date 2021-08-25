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
                if (!Player.Spellbook.IsOnCooldown(proc.Spell) && (proc.ProcMask & procMask) != ProcMask.None && CheckForProc(proc, Player))
                {
                    resultingEvents.Add(new CastEvent(time, Player, proc.Spell)); //TODO: Increase Prio of those cast events
                    //Program.Logger.Log($"{proc.Name} procced");
                }
            }
        }

        private static bool CheckForProc(Proc proc, Player player)
        {
            if (proc.PPM == 0)
                return RNG.Roll100(proc.Chance);

            else
                return RNG.Roll10000(Helpers.UpgradeFraction(player.WeaponSpeed * proc.PPM / 600));

        }
    }
}
