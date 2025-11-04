#include "digiclock.h"

DigiClock::DigiClock(QString city, int timeZone, QWidget *parent)
    : QWidget(parent), m_iTimeZone(timeZone)
{

    m_qLayout = new QVBoxLayout();
    setLayout(m_qLayout);

    m_qLabel = new QLabel(city, this);
    m_qLabel->setMaximumHeight(30);
    m_qLayout->addWidget(m_qLabel);

    display = new QLCDNumber(8, this);
    display->display(QTime::currentTime().toString("hh:mm:ss"));

    m_qLayout->addWidget(display);

    connect(&timer, &QTimer::timeout, this, [&](){
        QString timeFormat = "hh:mm:ss";
        QTime time = QTime::currentTime();
        if (time.second() % 2){
            timeFormat[2] = ' ';
            timeFormat[5] = ' ';
        }
        time = time.addSecs(3600 * m_iTimeZone);
        display->display(time.toString(timeFormat));
    });
    timer.start(100);
    //display->setMinimumSize(50, 50);
    display->setFixedSize(500, 250);

}

DigiClock::~DigiClock() {}
