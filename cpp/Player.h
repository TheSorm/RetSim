#pragma once

#include <unordered_map>
#include <functional>
#include "Timer.h"

class Player {
public:
    Player();

    int timeOfNextSwing();

    int meleeAttack(int time);

    int cast(int spellId);

    bool isOnCooldown(int spellId);

    void finishCooldownOf(int spellId);

    void removeAura(int auraId);

    int getSpellCooldown(int spellId);

private:
    int castCrusaderStrike();

    int timeOfLastSwing = 0;
    std::unordered_map<int, std::function<int(Player &player)>> spellIdToFunction;
    std::unordered_map<int, bool> spellIdToCooldownState;
    std::unordered_map<int, bool> auraIdToAuraSate;
    std::unordered_map<int, int> spellIdToCooldown;

};

