using System;
using System.Collections.Generic;

namespace RetSim
{
    public class Modifiers
    {
        public SchoolModifiers Schools { get; init; } = new SchoolModifiers();
        public SpellModifiers Spells { get; init; } = new SpellModifiers();
        public SpellCritModifiers SpellCrit { get; init; } = new SpellCritModifiers();
        public SpellBonuses Bonuses { get; init; } = new SpellBonuses();

        public StatModifiers Stats { get; init; } = new StatModifiers();

        public float AttackSpeed { get; set; } = 1f;
        public float CastSpeed { get; set; } = 1f;
        public float WeaponDamage { get; set; } = 1f;
    }

    public abstract class FailsafeDictionary<Key, Value> : Dictionary<Key, Value>
    {
        protected Value Default { get; init; }

        public Value GetValue(Key key)
        {
            if (ContainsKey(key))
                return this[key];

            else
                return Default;
        }
    }

    public class SchoolModifiers : FailsafeDictionary<School, float>
    {
        public SchoolModifiers()
        {
            Default = 1f;

            foreach (School school in Enum.GetValues(typeof(School)))
            {
                Add(school, Default);
            }
        }
    }

    public class SpellModifiers : FailsafeDictionary<Spell, float>
    {
        public SpellModifiers()
        {
            Default = 1f;

            foreach (Spell spell in Data.Spells.ByID.Values)
            {
                Add(spell, Default);
            }
        }
    }
    public class SpellCritModifiers : FailsafeDictionary<Spell, float>
    {
        public SpellCritModifiers()
        {
            Default = 1f;

            foreach (Spell spell in Data.Spells.ByID.Values)
            {
                Add(spell, Default);
            }
        }
    }

    public class SpellBonuses : FailsafeDictionary<Spell, int>
    {
        public SpellBonuses()
        {
            Default = 0;

            foreach (Spell spell in Data.Spells.ByID.Values)
            {
                Add(spell, Default);
            }
        }
    }

    public class StatModifiers
    {
        public float All { get; set; } = 1f;

        public float Strength { get; set; } = 1f;

        public float Intellect { get; set; } = 1f;

        public float AttackPower { get; set; } = 1f;

        public StatModifiers()
        {
            All = 1f;
            Strength = 1f;
            Intellect = 1f;
            AttackPower = 1f;
        }
    }
}
