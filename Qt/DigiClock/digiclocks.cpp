#include "digiclocks.h"

DigiClocks::DigiClocks(QWidget *parent): QWidget{parent},
    m_pBp(new DigiClock("Budapest", 0, this)),
    m_pBr(new DigiClock("Bucharest", 1, this)),
    m_pLond(new DigiClock("London", -1, this)),
    m_pTok (new DigiClock("Tokyo", -9, this)),
    m_qLayout(new QGridLayout())
{
    setLayout(m_qLayout);

    m_qLayout->addWidget(m_pBp, 0, 0);
    m_qLayout->addWidget(m_pBr, 0, 1);
    m_qLayout->addWidget(m_pLond, 1, 0);
    m_qLayout->addWidget(m_pTok, 1, 1);
}
