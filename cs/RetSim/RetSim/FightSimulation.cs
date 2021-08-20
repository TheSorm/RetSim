using RetSim.Events;
using RetSim.Tactics;
using System.Collections.Generic;
using System.Linq;
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
            int damage = 0;
            var queue = new EventQueue();
            queue.Add(new AutoAttackEvent(0, player)); //probably gets moved into a start function of the tactic
            queue.Add(new CastEvent(0, player, SpellGlossary.SealOfCommand));
            queue.Add(new CastEvent(17200, player, SpellGlossary.SealOfBlood));

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
                    damage += currentEvent.Execute(time, resultingEvents);
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

            return (damage / (fightDuration / 1000.0));
        }


    }
}