# RetSim
A WoW TBC Classic Retribution Paladin Simulator

## DESKTOP VERSION
* Stat weights screen
* Settings
  * Select all gems without changing the selected items
* Make it prettier

## SIM
* Gear 
  * Add missing items & their spells
  * Remove trash
* Mutually exclusive spells
* Tactic for Heroism, drums, potions etc - temporary buffs
  * Option 1: Have a cooldown grp based system 
  * Option 2: Set Heroism time and pop other cooldowns based on that

## LOW PRIORITY / THOUGHTS / DESIGN CONSIDERATIONS
* Uptime simulation (player / buffs like Expose Weakness)
* More tactics  
* Mana
  * Using / gaining
  * Mana regen events (mana per 5)
  * Procs (Judgement renew, Judgement of Wisdom, Sanctified Judgements)
* Spells
  * Periodic spells
    * Create CastEvents for every tick (but this "sounds wrong")   
  * Cast time spells
    * Low prio
    * (Player State casting -> OnCast spell with cast time -> casting = spell, new CastEvent(castTime, spell))  
* Proc
  * Should Prio on ProcEvents should be higher than on normal cast events?
* Test how much DPS is effected by CS delay. If its highly effected, build a UI page to sim that.
* Test haste leeway settings
