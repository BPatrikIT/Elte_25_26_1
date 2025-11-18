#ifndef HANOIMODEL_H
#define HANOIMODEL_H

#include <QObject>
#include <QVector>

class HanoiModel : public QObject
{
    Q_OBJECT
  public:
    /// the count of the towers in the problem
    static const int s_iTOWER_COUNT = 3;
    explicit HanoiModel(QObject *parent = nullptr);

    void newGame(int size);
    void moveDisk(int from, int to);
    int diskCount() const { return m_vecBoard.size(); }
    bool isFinished() const;
    /// returns all the indices of disks on the given tower
    QVector<int> disksAt(int tower) const;

  signals:
    void gameOver();
    void boardUpdate();
    void invalidMove();

  private:
    /// Disks are in an increasing order
    /// Every index represents the tower of the disk with the same index
    QVector<int> m_vecBoard;
};

#endif // HANOIMODEL_H
