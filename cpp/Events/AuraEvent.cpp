#include "AuraEvent.h"

AuraEvent::AuraEvent(Timer eventTimer, Player &player, int auraId)
        : Event(eventTimer, player), auraId{auraId} {

}

int AuraEvent::timeUntil() {
    return eventTimer.timeLeft();
}

int AuraEvent::execute(std::vector<std::shared_ptr<Event>> &resultingEvents, int *time) {
    player->removeAura(auraId);
    return 0;
}

std::string AuraEvent::toString() {
    return "Aura: " + std::to_string(auraId) + " fades";
}

