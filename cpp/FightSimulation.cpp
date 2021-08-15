#include <queue>
#include <iostream>
#include <memory>
#include <utility>
#include "FightSimulation.h"
#include "Events/Event.h"
#include "Events/CastEvent.h"

FightSimulation::FightSimulation(Player player, Enemy enemy, std::shared_ptr<Tactic> tactic, int fightDuration)
        : player{std::move(player)}, enemy{enemy}, tactic{std::move(tactic)},
          fightDuration{fightDuration} {

}

double FightSimulation::run() {
    int time = 0;
    int damage = 0;
    auto cmp = [](const std::shared_ptr<Event> &left, const std::shared_ptr<Event> &right) {
        return left->timeUntil() > right->timeUntil();
    };
    std::priority_queue<std::shared_ptr<Event>, std::vector<std::shared_ptr<Event>>, decltype(cmp)> eventQueue(cmp);
    while (time <= fightDuration) {
        if (!eventQueue.empty() && eventQueue.top()->timeUntil() < (player.timeOfNextSwing() - time)) {
            std::shared_ptr<Event> currentEvent = eventQueue.top();
            eventQueue.pop();

            std::vector<std::shared_ptr<Event>> resultingEvents;
            time += currentEvent->timeUntil();
            damage += currentEvent->execute(resultingEvents, &time);
            for (auto const &resultingEvent: resultingEvents) {
                eventQueue.push(resultingEvent);
            }

            std::cout << time << ": Event: " << currentEvent->toString() << std::endl;
        } else {
            time += player.timeOfNextSwing() - time;

            damage += player.meleeAttack(time);
            std::cout << time << ": MeleeAttack" << std::endl;
        }


        int timeUntilNextEvent = 0;
        if (!eventQueue.empty() && eventQueue.top()->timeUntil() < (time - player.timeOfNextSwing())) {
            timeUntilNextEvent = eventQueue.top()->timeUntil();
        } else {
            timeUntilNextEvent = (time - player.timeOfNextSwing());
        }
        std::shared_ptr<Event> playerAction = tactic->getActionBetween(&time, timeUntilNextEvent, player);

        if (playerAction) {
            eventQueue.push(playerAction);
        }

    }

    std::cout << damage << "\n";
    return (damage / (fightDuration / 1000.0));
}

