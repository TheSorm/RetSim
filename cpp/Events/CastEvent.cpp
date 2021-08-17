#include "CastEvent.h"
#include "CooldownEvent.h"
#include "../Spells.h"
#include <iostream>

CastEvent::CastEvent(Timer eventTimer, Player &player, int spellID) : Event(eventTimer, player), spellId{spellID} {
}

int CastEvent::timeUntil() {
    return eventTimer.timeLeft();
}

int CastEvent::execute(std::vector<std::shared_ptr<Event>> &resultingEvents, int *time) {
    resultingEvents.emplace_back(
            std::make_unique<CooldownEvent>(Timer(time, Spells::spellInformation[spellId].cooldown, *player, spellId));
    return player->cast(spellId);
}

std::string CastEvent::toString() {
    return "Cast spell: " + std::to_string(spellId);
}


