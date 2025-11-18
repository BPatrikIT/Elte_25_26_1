#include "hanoimodel.h"

HanoiModel::HanoiModel(QObject *parent) : QObject{parent}
{
}

void HanoiModel::newGame(int size)
{
    m_vecBoard.resize(size);
    // iterating over the vector with a reference,
    // thus we can change the original value
    for (int &e : m_vecBoard) {
        e = 0;
    }
    emit boardUpdate();
}

void HanoiModel::moveDisk(int from, int to)
{
    // since we store the tower for each disk
    // it is enough to find the smallest disk on the two towers
    if (from == to) {
        return;
    }
    assert(to >= 0 && to < s_iTOWER_COUNT);
    for (int &e : m_vecBoard) {
        // if the smallest disk is in the destination tower
        // the movement is invalid
        if (e == to) {
            emit invalidMove();
            return;
        }
        // if the smallest disk is in the source tower
        // we should move that to the other one
        if (e == from) {
            e = to;
            emit boardUpdate();
            if (isFinished()) {
                emit gameOver();
            }
            return;
        }
    }
    // if no disk was found, we cannot move anything
    emit invalidMove();
}

bool HanoiModel::isFinished() const
{
    // all the disks should be in the last tower
    for (int e : m_vecBoard) {
        if (e != s_iTOWER_COUNT - 1) {
            return false;
        }
    }
    return true;
}

QVector<int> HanoiModel::disksAt(int tower) const
{
    QVector<int> res;
    for (int i = 0; i < m_vecBoard.size(); ++i) {
        if (m_vecBoard[i] == tower) {
            res.push_back(i);
        }
    }
    return res;
}
