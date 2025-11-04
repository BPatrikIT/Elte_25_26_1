#ifndef DIGICLOCKS_H
#define DIGICLOCKS_H

#include <QWidget>

#include "digiclock.h"

class DigiClocks : public QWidget
{
    Q_OBJECT
public:
    explicit DigiClocks(QWidget *parent = nullptr);

private:
    DigiClock *m_pBp, *m_pBr, *m_pLond, *m_pTok;
    QGridLayout *m_qLayout;
};

#endif // DIGICLOCKS_H
