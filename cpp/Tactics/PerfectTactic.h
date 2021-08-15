#pragma once


#include "Tactic.h"

class PerfectTactic : public Tactic {
    std::unique_ptr<Event> getActionBetween(int* time, int timeWindowEnd, Player& player) override;
};
