#include "CooldownEvent.h"

CooldownEvent::CooldownEvent(Timer eventTimer, Player &player, int spellId) : Event(eventTimer, player),
                                                                              spellId{spellId} {
}

int CooldownEvent::timeUntil() {
    return eventTimer.timeLeft();
}

int CooldownEvent::execute(std::vector<std::shared_ptr<Event>> &resultingEvents, int *time) {
    player->finishCooldownOf(spellId);
    return 0;
}

std::string CooldownEvent::toString() {
    return "Cooldown of spell: " + std::to_string(spellId) + " ends";
}
