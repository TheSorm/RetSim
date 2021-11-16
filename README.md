# RetSim
A WoW TBC Classic Retribution Paladin Simulator

## DESKTOP VERSION
* User Config
  * Store user settings on exit
  * Defaults if user config file is corrupt or missing
* Stat weights screen
* Multi-threading
* Gear Select Screen
  * Add Tooltips to gems
  * Select all gems without changing the selected items
  * Display socket bonuses (boni for Sulis) -- tooltips for now
  * Remove useless stats
  * Move spell power before stamina
* Stats Panel
  * Add missing stats (health, mana)
  * Move ratings etc to helpful tooltips
* Make it prettier

## SIM
* Implement stat weight support
* Spell Effects
  * Add the "Modify Effectiveness of Other Spell" spell effect, f.e. for Judgement of the Crusader.
* Talents
  * Add ranks
* Gear 
  * Implement armor auras / procs
  * Remove trash
* Mutually exclusive spells
* Tactic for Heroism, drums, potions etc - temporary buffs
* Spell cast speed & spell GCD
* Uptime simulation (player / buffs like Expose Weakness)

## LOW PRIORITY / THOUGHTS / DESIGN CONSIDERATIONS
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
