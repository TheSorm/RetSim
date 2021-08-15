#pragma once


#include <vector>
#include <queue>
#include <memory>
#include "../Timer.h"
#include "../Player.h"

class Event {
public:
    Event(Timer eventTimer, Player &player);

    virtual int timeUntil() = 0;

    virtual int execute(std::vector<std::shared_ptr<Event>> &resultingEvents, int *time) = 0;

    virtual std::string toString() = 0;

protected:
    Timer eventTimer;
    Player *player;
};
