#include <iostream>
#include "Player.h"

Player::Player() {
    spellIdToFunction.emplace(35395, [](Player &player) { return player.castCrusaderStrike(); });

    spellIdToCooldownState.emplace(35395, false);
    auraIdToAuraSate.emplace(33807, false);
}

int Player::timeOfNextSwing() {
    return timeOfLastSwing + 3500;
}

int Player::meleeAttack(int time) {
    timeOfLastSwing = time;
    return 1234;
}

int Player::cast(int spellId) {
    if (spellIdToFunction.count(spellId)) {
        return spellIdToFunction[spellId](*this);
    }
    return 0;
}

bool Player::isOnCooldown(int spellId) {
    return spellIdToCooldownState[spellId];
}

int Player::castCrusaderStrike() {
    spellIdToCooldownState[35395] = true;
    return 1212;
}

void Player::finishCooldownOf(int spellId) {
    spellIdToCooldownState[spellId] = false;
}

void Player::removeAura(int auraId) {
    auraIdToAuraSate[auraId] = false;
}



