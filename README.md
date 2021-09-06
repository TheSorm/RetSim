# RetSim
A WoW TBC Classic Retribution Paladin Simulator
## TODO
* Events
  * Twist window event?
* Uptime simulation (player / buffs)
* Combat Logger 
  * More combat events (like mana regen)
* Spells
  * Cast time spells
    * Low prio
    * (Player State casting -> OnCast spell with cast time -> casting = spell, new CastEvent(castTime, spell))
  * Periodic spells
    * Create CastEvents for every tick (but this "sounds wrong")
* Gear 
  * Enchants
* Spell, Aura, Proc Data 
* Proc
  * ProcCooldownEvent - Implemented
    * Option 1: Proc has (invisible) Cooldown (tactic can not react)
    * Option 2: Proc-Spell has Cooldown (tactic can react)
  * Should Prio on ProcEvents should be higher than on normal cast events?
