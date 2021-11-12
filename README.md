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
* Gear 
  * Implement auras / procs
  * Remove trash
* Talents
  * Add ranks
* Proc
  * Should Prio on ProcEvents should be higher than on normal cast events?

** BUGS **
* Seal of Command procs if you cast it on the same timestamp as an auto attack, and SoB also procs
