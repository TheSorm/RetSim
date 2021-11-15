# RetSim
A WoW TBC Classic Retribution Paladin Simulator
## TODO
* Uptime simulation (player / buffs)
* Combat Logger 
  * More combat events (like mana regen)
* Spells
  * Cast time spells
    * Low prio
    * (Player State casting -> OnCast spell with cast time -> casting = spell, new CastEvent(castTime, spell))
  * Periodic spells
    * Create CastEvents for every tick (but this "sounds wrong")    
* Spell Effects
  * Add the "Modify Effectiveness of Other Spell" spell effect, f.e. for Judgement of the Crusader.
* Gear 
  * Implement auras / procs
  * Remove trash
* Talents
  * Add ranks
* Proc
  * Should Prio on ProcEvents should be higher than on normal cast events?
  * Seperate active procs from proc cooldwons (Currently if a proc is removed from the active procs, the cooldown is also removed)
* UI
  * Add Tooltips to gems