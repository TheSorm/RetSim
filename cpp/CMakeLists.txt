cmake_minimum_required(VERSION 3.20)
project(RetSim)

set(CMAKE_CXX_STANDARD 14)

add_executable(RetSim main.cpp Player.cpp Player.h FightSimulation.cpp FightSimulation.h Enemy.cpp Enemy.h Events/Event.cpp Events/Event.h Events/CastEvent.cpp Events/CastEvent.h Timer.cpp Timer.h Tactics/Tactic.cpp Tactics/Tactic.h Tactics/PerfectTactic.cpp Tactics/PerfectTactic.h Events/CooldownEvent.cpp Events/CooldownEvent.h Events/AuraEvent.cpp Events/AuraEvent.h)
