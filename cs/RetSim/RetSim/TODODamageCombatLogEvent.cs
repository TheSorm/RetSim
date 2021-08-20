using System.Collections.Generic;

namespace RetSim.Events
{
    public class TODODamageCombatLogEvent : Event
    {
        private const int DamageEventPriority = 1;

        private string source {  get; set; }
        private string result {  get; set; }
        private int damage { get; set; }
        public School school { get; set; }

        public TODODamageCombatLogEvent(int expirationTime, Player player, string source, string result, int damage, School school) : base(expirationTime, DamageEventPriority, player)
        {
            this.source = source;
            this.result = result;
            this.damage = damage;
            this.school = school;
        }

        public override int Execute(int time, List<Event> resultingEvents)
        {
            //player.RemoveAura(auraId);

            return 0;
        }

        public override string ToString()
        {
            return FormatInput();
        }

        private string FormatInput()
        {
            string result = $"[Player] [{source}] {this.result} [Enemy]";

            if (damage > 0)
                result += $" for {damage} {school} Damage";

            return result;
        }
    }
}
