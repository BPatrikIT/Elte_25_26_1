#include "hanoimodel.h"

const int hanoimodel::s_iTOWER_COUNT = 3;

hanoimodel::hanoimodel(QObject *parent)
    : QObject{parent}
{

}

hanoimodel::~hanoimodel() {}

void hanoimodel::newGame(int size)
{
    m_vecDisks.resize(size);
    for (int &e: m_vecDisks){
        e = 0;
    }
    emit gameChanged();
}

void hanoimodel::moveTo(int from, int to){
    if (from < 0 || from >= s_iTOWER_COUNT){
        throw "";
    }
    if (to < 0 || to >= s_iTOWER_COUNT){
        throw "";
    }
    if (from == to) {
        return;
    }
    for (int &e:m_vecDisks){
        if (e == to) {
            emit invalidMove();
            return;
        }
        else if (e == from) {
            e = to;
            emit gameChanged();
            if (isGameOver()) {
                emit gameOver();
            }
            return;
        }
    }
    emit invalidMove();
}

bool hanoimodel::isGameOver() const
{
    for (int e: m_vecDisks) {
        if (e != s_iTOWER_COUNT - 1) {
            return false;
        }
    }
}

QList<int> hanoimodel::disksOnTower(int id) const
{
    QList<int> res {};
    int i = 0;
    for (int e:  m_vecDisks) {
        if (e == id) {
            res.append(i);
        }
        ++i;
    }
}
