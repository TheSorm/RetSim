#pragma once


class Timer {
public:
    Timer(int* time, int timeUntilExpiration);
    int timeLeft();

private:
    int* time;
    int expirationTime;
};