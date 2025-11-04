#ifndef DIGICLOCK_H
#define DIGICLOCK_H

#include <QWidget>
#include <QTime>
#include <QTimer>
#include <QLCDNumber>
#include <QVBoxLayout>
#include <QLabel>

class DigiClock : public QWidget
{
    Q_OBJECT

public:
    DigiClock(QString city, int timeZone, QWidget *parent = nullptr);
    ~DigiClock();

private:
    QTimer timer;
    QLCDNumber* display;
    QVBoxLayout* m_qLayout;
    QLabel* m_qLabel;
    int m_iTimeZone;
};
#endif // DIGICLOCK_H
