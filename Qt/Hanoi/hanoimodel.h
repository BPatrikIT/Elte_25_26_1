#ifndef HANOIMODEL_H
#define HANOIMODEL_H

#include <QObject>

class hanoimodel : public QObject
{
    Q_OBJECT
public:
    static const int s_iTOWER_COUNT;

    explicit hanoimodel(QObject *parent = nullptr);
    ~hanoimodel();

    void newGame(int size);
    void moveTo(int from, int to);
    bool isGameOver() const;
    QList<int> disksOnTower(int id) const;
    int diskCount() const {return m_vecDisks.size();}

signals:
    void gameChanged();
    void gameOver();
    void invalidMove();

private:
    QList<int> m_vecDisks;

};

#endif // HANOIMODEL_H
