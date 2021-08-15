#include <algorithm>
#include "Timer.h"

Timer::Timer(int *time, int timeUntilExpiration) : time{time}, expirationTime{*time + timeUntilExpiration} {

}

int Timer::timeLeft() {
    return std::max(0, expirationTime - *time);
}
