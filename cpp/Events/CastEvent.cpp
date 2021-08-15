#include "CastEvent.h"
#include <iostream>

CastEvent::CastEvent(Timer eventTimer, Player& player, int spellID) : Event(eventTimer, player), spellID{spellID}{
}

int CastEvent::timeUntil() {
    return eventTimer.timeLeft();
}

int CastEvent::execute(std::vector<std::shared_ptr<Event>>& resultingEvents, int* time) {
    //resultingEvents.emplace_back(std::make_unique<CastEvent>(Timer(time, 12 * 1000), player, 99));
    return player->cast(spellID);
}

std::string CastEvent::toString() {
    return "Cast spell: " + std::to_string(spellID);
}


