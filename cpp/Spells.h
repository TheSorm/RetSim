#pragma once


#include <unordered_map>

class Spells {
    struct spell {
        int spellId;
        std::string name;
        int cooldown;
    };
    static std::unordered_map<int, spell> spellInformation;
};
