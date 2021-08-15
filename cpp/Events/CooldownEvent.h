#pragma once


#include "Event.h"

class CooldownEvent : public Event {
public:
    CooldownEvent(Timer eventTimer, Player &player, int spellID);

    int timeUntil() override;

    int execute(std::vector<std::shared_ptr<Event>> &resultingEvents, int *time) override;

    std::string toString() override;

private:
    int spellId;
};
