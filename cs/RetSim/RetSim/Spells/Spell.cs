﻿using RetSim.SpellEffects;
using System;
using System.Collections.Generic;

namespace RetSim
{
    public class Spell
    {
        public int ID { get; init; }
        public string Name { get; init; }
        public int ManaCost { get; init; } = 0;
        public int Cooldown { get; init; } = 0;
        public int CastTime { get; init; } = 0;
        public SpellGCD GCD { get; init; } = null;
        public Aura Aura { get; set; } = null;
        public List<SpellEffect> Effects { get; set; } = null;
        public Func<Player, bool> Requirements { get; init; }
    }

    public class Judgement : Spell { }

    public class Talent : Spell { }

    public class Racial : Spell { }

    public class SpellGCD
    {
        public int Duration { get; init; } = Constants.Numbers.DefaultGCD;
        public AttackCategory Category { get; init; } = AttackCategory.None;
    }

    public enum School
    {
        Typeless = 0,
        Physical = 1,
        Holy = 2,
        Fire = 3,
        Nature = 4,
        Frost = 5,
        Shadow = 6,
        Arcane = 7
    }

    public enum AttackCategory
    {
        None = 0,
        Physical = 1,
        Spell = 2
    }

    public enum DefenseType
    {
        None = 0,
        White = 1,
        Special = 2,
        Ranged = 3,
        Magic = 4
    }
}