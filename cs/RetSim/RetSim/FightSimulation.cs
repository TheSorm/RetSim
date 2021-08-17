using System;
using System.Collections.Generic;

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
            EventQueue eventQueue = new();
            while (time <= fightDuration)
            {
                eventQueue.Sort();
                if (!eventQueue.Empty() && eventQueue.GetNext().ExpirationTime < player.TimeOfNextSwing())
                {
                    Event currentEvent = eventQueue.GetNext();
                    eventQueue.RemoveNext();
                    time += currentEvent.ExpirationTime - time;

                    List<Event> resultingEvents = new();
                    damage += currentEvent.Execute(resultingEvents, time);
                    eventQueue.Push(resultingEvents);

                    Console.WriteLine(time + ": Event: " + currentEvent.ToString());
                }
                else
                {
                    time += player.TimeOfNextSwing() - time;

                    damage += player.MeleeAttack(time);

                    Console.WriteLine(time + ": MeleeAttack");
                }

                eventQueue.Sort();
                int timeUntilNextEvent;
                if (!eventQueue.Empty() && eventQueue.GetNext().ExpirationTime < (time - player.TimeOfNextSwing()))
                {
                    timeUntilNextEvent = eventQueue.GetNext().ExpirationTime;
                }
                else
                {
                    timeUntilNextEvent = (time - player.TimeOfNextSwing());
                }
                Event playerAction = tactic.getActionBetween(time, timeUntilNextEvent, player);

                if (playerAction != null)
                {
                    eventQueue.Push(playerAction);
                }

            }

            return (damage / (fightDuration / 1000.0));
        }


    }
}