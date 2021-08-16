#pragma once


#include "Event.h"

class AuraEvent : public Event{
public:
    AuraEvent(Timer eventTimer, Player &player, int auraId);

    int timeUntil() override;

    int execute(std::vector<std::shared_ptr<Event>> &resultingEvents, int *time) override;

    std::string toString() override;

private:
    int auraId;
};
