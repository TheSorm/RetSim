namespace RetSim.SpellEffects
{
    class JudgementEffect : SpellEffect
    {
        public override ProcMask Resolve(FightSimulation fight)
        {
            if (fight.Player.Auras.CurrentSeal != null)
            {
                var seal = fight.Player.Auras.CurrentSeal;

                fight.Player.Auras.Cancel(seal, fight);

                return fight.Player.Cast(seal.Judgement, fight);
            }

            else
                return ProcMask.None;
        }
    }
}
