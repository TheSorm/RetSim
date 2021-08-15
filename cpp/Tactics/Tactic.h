#pragma once

#include <memory>
#include "../Player.h"
#include "../Events/Event.h"

class Tactic {
public:
    virtual std::unique_ptr<Event> getActionBetween(int *time, int timeWindowEnd, Player &player) = 0;
};

