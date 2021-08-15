#pragma once


#include "Player.h"
#include "Enemy.h"
#include "Tactics/Tactic.h"

class FightSimulation {
public:
    FightSimulation(Player player, Enemy enemy, std::shared_ptr<Tactic> tactic, int fightDuration);

    double run();

private:
    int fightDuration;
    Player player;
    Enemy enemy;
    std::shared_ptr<Tactic> tactic;
};
