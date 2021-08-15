#pragma once

#include "Event.h"

class CastEvent : public Event{
public:
    explicit CastEvent(Timer eventTimer, Player& player, int spellID);
    int timeUntil() override;
    int execute(std::vector<std::shared_ptr<Event>>& resultingEvents, int* time) override;
    std::string toString() override;

private:
    int spellID;
};
