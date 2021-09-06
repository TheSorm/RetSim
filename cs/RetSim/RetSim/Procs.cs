using RetSim.Events;
using System.Collections.Generic;

namespace RetSim
{
    public class Procs : Dictionary<Proc, ProcCooldownEndEvent>
    {
        private readonly Player player;

        public Procs(Player parent)
        {
            player = parent;
        }

        public new void Add(Proc proc, ProcCooldownEndEvent cooldown = null)
        {
            if (!ContainsKey(proc))
                base.Add(proc, null);
        }

        public void CheckProcs(ProcMask mask, FightSimulation fight)
        {
            foreach (var proc in this.Keys)
            {
                if (!IsOnCooldown(proc) && (proc.ProcMask & mask) != ProcMask.None && RollProc(proc, player))
                {
                    if (proc.Cooldown > 0)
                        fight.Queue.Add(new ProcCooldownEndEvent(proc, fight, fight.Timestamp + proc.Cooldown));

                    fight.Queue.Add(new CastEvent(proc.Spell, fight, fight.Timestamp)); //TODO: Increase Prio of those cast events
                    //Program.Logger.Log($"{proc.Name} procced");
                }
            }
        }

        private static bool RollProc(Proc proc, Player player)
        {
            if (proc.PPM == 0f)
                return RNG.Roll100(proc.Chance);

            else
                return RNG.Roll100(Helpers.PPMToChance(proc.PPM, player));
        }

        public void StartCooldown(Proc proc, ProcCooldownEndEvent cooldown)
        {
            this[proc] = cooldown;
        }

        public void EndCooldown(Proc proc)
        {
            this[proc] = null;
        }

        public bool IsOnCooldown(Proc proc)
        {
            return this[proc] != null;
        }
    }
}
