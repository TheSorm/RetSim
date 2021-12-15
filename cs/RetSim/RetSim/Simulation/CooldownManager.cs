using RetSim.Simulation.Events;
using RetSim.Spells;
using System.Linq;

namespace RetSim.Simulation
{
    public class CooldownManager
    {
        public FightSimulation Parent { get; init; }

        public int FightDuration { get; init; }
        public List<int> HeroismTimings { get; init; }
        public Dictionary<int, List<Spell>> Cooldowns { get; init; }

        public Dictionary<int, List<int>> Possibilities { get; init; }

        public List<int> TotalPossibilities { get; init; }

        public CooldownManager(FightSimulation parent, List<Spell> cooldowns, List<int> heroism)
        {
            Parent = parent;
            FightDuration = parent.Duration;
            HeroismTimings = heroism;

            HeroismTimings.Sort();

            Cooldowns = new();
            Possibilities = new();
            TotalPossibilities = new();

            foreach (Spell spell in cooldowns)
            {
                if (Cooldowns.ContainsKey(spell.Cooldown))
                    Cooldowns[spell.Cooldown].Add(spell);

                else
                    Cooldowns[spell.Cooldown] = new List<Spell> { spell };
            }

            //foreach (int cooldown in Cooldowns.Keys)
            //{
            //    for (int i = 0; i < FightDuration; i += cooldown)
            //    {
            //        if (Possibilities.ContainsKey(cooldown))
            //        {
            //            if (!Possibilities[cooldown].Contains(i))
            //                Possibilities[cooldown].Add(i);
            //        }

            //        else
            //            Possibilities[cooldown] = new() { i };

            //        if (!TotalPossibilities.Contains(i))
            //            TotalPossibilities.Add(i);
            //    }
            //}

            //foreach (int hero in HeroismTimings)
            //{
            //    if (!TotalPossibilities.Contains(hero))
            //        TotalPossibilities.Add(hero);
            //}

            //TotalPossibilities.Sort();

            Calculate();
        }

        public void Test(int cooldown)
        {
            int maxUses = Possibilities[cooldown].Count;

            foreach (int poss in TotalPossibilities)
            {

            }
        }

        public void Calculate()
        {
            foreach (int cooldown in Cooldowns.Keys)
            {
                int max = (int) Math.Ceiling(FightDuration / ((float)cooldown));
                int[] current = new int[max];

                int defaultCount = 0;

                for (int i = 0; i < FightDuration; i += cooldown)
                {
                    current[defaultCount] = i;
                    defaultCount++;
                }

                foreach (int heroism in HeroismTimings)
                {
                    int[] uses = new int[max];
                    int count = 0;
                    int match = 0;

                    for (int i = 0; i < FightDuration; count++)
                    {
                        if (heroism > i + cooldown || i > heroism)
                        {
                            uses[count] = i;
                            i += cooldown;
                        }

                        else
                        {
                            uses[count] = heroism;
                            match = count;
                            i = heroism + cooldown;
                        }
                    }

                    if (count >= defaultCount)
                    {
                        for (int i = match; i < count; i++)
                        {
                            current[i] = uses[i];
                        }

                        defaultCount = count;
                    }
                }

                foreach (int timing in current)
                {
                    foreach (Spell spell in Cooldowns[cooldown])
                    {
                        Parent.Queue.Add(new CastEvent(spell, Parent.Player, Parent.Player.GetSpellTarget(spell.Target, Parent), Parent, timing));
                    }
                }
            }
        }
    }
}
