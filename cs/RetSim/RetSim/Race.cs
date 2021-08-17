using System.Collections.Generic;

namespace RetSim
{
    public record Race(string Name, RaceStats Stats);
    public record RaceStats(int Strength, int Agility, int Intellect, int Stamina, int Health, int Mana, int AttackPower, float CritChance, float SpellCritChance, int Expertise);

    public static class Races
    {
        public static Race Human = new Race("Human", new RaceStats(126, 77, 83, 120, 3197, 2673, 190, 0.6f, 3.32f, 5));

        public static Dictionary<string, Race> ByName = new Dictionary<string, Race>()
        {
            { Human.Name, Human }
        };
    }    
}