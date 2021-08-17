using System;
using System.Collections.Generic;
using static RetSim.Program;

namespace RetSim
{
    internal class FightSimulation
    {
        private Player player;
        private Enemy enemy;
        private Tactic tactic;
        private readonly int fightDuration;

        internal FightSimulation(Player player, Enemy enemy, Tactic tactic, int fightDuration)
        {
            this.player = player;
            this.enemy = enemy;
            this.tactic = tactic;
            this.fightDuration = fightDuration;
        }

        internal double Run()
        {
            int time = 0;
            int damage = 0;
            var queue = new EventQueue();

            while (time <= fightDuration)
            {
                queue.Sort();

                if (!queue.Empty() && queue.GetNext().ExpirationTime < player.TimeOfNextSwing())
                {
                    Event currentEvent = queue.GetNext();
                    queue.RemoveNext();
                    time += currentEvent.ExpirationTime - time;

                    List<Event> resultingEvents = new();
                    damage += currentEvent.Execute(resultingEvents, time);
                    queue.AddRange(resultingEvents);

                    Logger.Log(time + ": Event: " + currentEvent.ToString());
                }

                else
                {
                    time += player.TimeOfNextSwing() - time;

                    damage += player.MeleeAttack(time);

                    Logger.Log(time + ": MeleeAttack");
                }

                queue.Sort();

                int timeUntilNextEvent;

                if (!queue.Empty() && queue.GetNext().ExpirationTime < (time - player.TimeOfNextSwing()))                
                    timeUntilNextEvent = queue.GetNext().ExpirationTime;
                
                else               
                    timeUntilNextEvent = (time - player.TimeOfNextSwing());
                
                Event playerAction = tactic.GetActionBetween(time, timeUntilNextEvent, player);

                if (playerAction != null)                
                    queue.Add(playerAction);
                

            }

            return (damage / (fightDuration / 1000.0));
        }


    }
}