#include <iostream>
#include "Event.h"

Event::Event(Timer eventTimer, Player &player) : eventTimer{eventTimer} {
    this->player = &player;
}
