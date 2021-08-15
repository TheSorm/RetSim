#include <iostream>
#include "Enemy.h"
#include "Player.h"
#include "FightSimulation.h"
#include "Tactics/PerfectTactic.h"

int main() {
    Enemy enemy;
    Player player;
    FightSimulation fightSimulation(player, enemy, std::make_shared<PerfectTactic>(), 30 * 1000);

    std::cout << "DPS:" << fightSimulation.run() << std::endl;
    return 0;
}
