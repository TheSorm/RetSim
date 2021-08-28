# RetSim
A WoW TBC Classic Retribution Paladin Simulator
## TODO
* Combat Logger 
  * Run() returns CombatLogger
  * Damage logger
* Spells
  * Cast spells
    * Low prio
    * (Player State casting -> OnCast spell with cast time -> casting = spell, new CastEvent(castTime, spell))
  * Periodic spells
    * Create CastEvents for every tick (but this "sounds wrong")
* Gear 
  * Sets
  * Gems
* Spell, Aura, Proc Data 
* Proc
  * ProcCooldwonEnd Events 
    * Option 1: Invisible (tactic can not react)
    * Option 2: Own Cooldwon Event (tactic can react)
  * Should Prio on ProcEvents should be higher than on normal cast events?
  * Cooldown on Procs (Only if we want to implement downranking)
* AutoAttack
  * Update it with maybe using SpellEffects (Pride "has some other ideas for that anyway")
* Change Event Queue to SortedSet
  * Option 1: Every event gets a reference to the queue and if the event is updated, the quque re-adds the event. 
Instead of passing around a list to collect the resulting events, the event queue itself is passed and new events are added to it with a reference to it.
  * Option 2: Two Lists (ResultinEvents and ModifiedEvents) collect all the events and updates the event queue in the end
  * Option 3: Make Event imutable. Two Lists (AddedEvents and RemovedEvents) collect all the events and removed events and update the event queue. To change an event it gets removed and a new version with changed expiration date is added.