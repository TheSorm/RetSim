using RetSim.Events;
using RetSim.Tactics;
using System.Collections.Generic;
using static RetSim.Program;

namespace RetSim
{
    public class FightSimulation
    {
        private Player player;
        private Enemy enemy;
        private Tactic tactic;
        private readonly int fightDuration;

        public FightSimulation(Player player, Enemy enemy, Tactic tactic, int fightDuration)
        {
            this.player = player;
            this.enemy = enemy;
            this.tactic = tactic;
            this.fightDuration = fightDuration;
        }

        public double Run()
        {
            int time = 0;
            var queue = new EventQueue();

            player.Procs.Add(Glossaries.Procs.MagtheridonMeleeTrinket);

            queue.AddRange(tactic.PreFight(player));

            while (time <= fightDuration)
            {
                int timeOfNextEvent = fightDuration;

                if (!queue.Empty())
                {
                    queue.Sort();
                    Event currentEvent = queue.GetNext();
                    queue.RemoveNext();
                    time = currentEvent.ExpirationTime;

                    List<Event> resultingEvents = new();
                    ProcMask procMask = currentEvent.Execute(time, resultingEvents);
                    player.CheckForProcs(procMask, time, resultingEvents);
                    queue.AddRange(resultingEvents);

                    Logger.Log(time + ": Event: " + currentEvent.ToString());

                    if (!queue.Empty())
                    {
                        queue.Sort();
                        timeOfNextEvent = queue.GetNext().ExpirationTime;
                        if (time == timeOfNextEvent) continue;
                    }
                }

                Event playerAction = tactic.GetActionBetween(time, timeOfNextEvent, player);

                if (playerAction != null)
                    queue.Add(playerAction);
            }

            return (1111 / (fightDuration / 1000.0));
        }


    }
}