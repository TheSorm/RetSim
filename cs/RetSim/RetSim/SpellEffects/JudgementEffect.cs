namespace RetSim.SpellEffects
{
    class JudgementEffect : SpellEffect
    {
        public override ProcMask Resolve(FightSimulation fight)
        {
            if (fight.Player.Auras.CurrentSeal != null)
            {
                var judgement = fight.Player.Auras.CurrentSeal.Judgement;
                fight.Player.Auras.CurrentSeal = null;

                foreach (var seal in Glossaries.Auras.Seals)
                    fight.Player.Auras.Cancel(seal, fight);

                fight.Player.Cast(judgement, fight);
            }

            return ProcMask.None;
        }
    }
}
