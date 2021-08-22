# RetSim
A WoW TBC Classic Retribution Paladin Simulator
## TODO
* Combat Logger 
  * Damage logger instead of returns
* Proc System 
  * Proc Data
    *1. Its proc type (mask)
    *2. The spell it triggers on proc
    *3. Its proc chance (%)
    *4. Its cooldown
  * Spell Effects return proc-mask
  * Events return proc mask from spell effect
  * Compare spell effect proc-mask with all active procs proc-maskes
  * Evaluate randome chance for proc
  * Create new spell cast event if proc happens
* Player State
  * Player Stats
  * Player Stat Modifier
* Change Event Queue to SortedSet
  * Option 1: Every event gets a reference to the queue and if the event is updated, the quque re-adds the event. 
Instead of passing around a list to collect the resulting events, the event queue itself is passed and new events are added to it with a reference to it.
  * Option 2: Two Lists (ResultinEvents and ModifiedEvents) collect all the events and updates the event queue in the end
  * Option 3: Make Event imutable. Two Lists (AddedEvents and RemovedEvents) collect all the events and removed events and update the event queue. To change an event it gets removed and a new version with changed expiration date is added.