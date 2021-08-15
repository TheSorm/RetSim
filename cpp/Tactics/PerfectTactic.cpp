#include <memory>
#include <iostream>
#include "PerfectTactic.h"
#include "../Events/CastEvent.h"

std::unique_ptr<Event> PerfectTactic::getActionBetween(int *time, int timeWindowEnd, Player &player) {
    if (!player.isOnCooldown(35395)) {
        return std::make_unique<CastEvent>(Timer(time, 0), player, 35395);
    }
    return std::unique_ptr<Event>{};
}
