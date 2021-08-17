namespace RetSim
{
    class Race
    {
        public string Name { get; set; } = "Fictional Race";

        public class BaseStats 
        { 
            public int Strength { get; set; }
            public int Agility { get; set; }
            public int Intellect { get; set; }
            public int Stamina { get; set; }
        }
    }
}
