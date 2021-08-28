using RetSim.Events;
using RetSim.Tactics;
using System.Collections.Generic;
using static RetSim.Program;

namespace RetSim
{
    public class FightSimulation
    {
        private readonly Player player;
        private readonly Enemy enemy;
        private readonly Tactic tactic;
        private readonly int duration;


        public FightSimulation(Player player, Enemy enemy, Tactic tactic, int minDuration, int maxDuration)
        {
            this.player = player;
            this.enemy = enemy;
            this.tactic = tactic;
            duration = RNG.RollRange(minDuration, maxDuration);

        }

        public double Run()
        {
            int time = 0;
            var queue = new EventQueue();

            player.Procs.Add(Glossaries.Procs.DragonspineTrophy);

            queue.AddRange(tactic.PreFight(player));

            while (time <= duration)
            {
                int nextTimestamp = duration;

                if (!queue.IsEmpty())
                {
                    queue.Sort();
                    Event curent = queue.GetNext();
                    queue.RemoveNext();
                    time = curent.ExpirationTime;

                    List<Event> results = new();
                    ProcMask mask = curent.Execute(time, results);
                    player.CheckForProcs(mask, time, results);
                    queue.AddRange(results);

                    Logger.Log(time + ": Event: " + curent.ToString());

                    if (!queue.IsEmpty())
                    {
                        queue.Sort();
                        nextTimestamp = queue.GetNext().ExpirationTime;
                        if (time == nextTimestamp) continue;
                    }
                }

                Event playerAction = tactic.GetActionBetween(time, nextTimestamp, player);

                if (playerAction != null)
                    queue.Add(playerAction);
            }

            return 0;
        }
    }

}