#include "hanoiview.h"

HanoiView::HanoiView(QWidget *parent)
    : QWidget(parent)
{
    m_pModel = new hanoimodel(this);

    m_qMainLayout = new QVBoxLayout(this);
    setLayout(m_qMainLayout);

    m_qTowerLayout = new QHBoxLayout();
    m_qMoveLayout = new QHBoxLayout();
    m_qNewGameLayout = new QHBoxLayout();

    m_qFromLabel = new QLabel("From: ", this);
    m_qToLabel = new QLabel("To: ", this);
    m_qNewGameLabel = new QLabel("New game size: ", this);

    m_qFrom = new QSpinBox(this);
    m_qTo = new QSpinBox(this);
    m_qNewGame = new QSpinBox(this);

    m_qMoveButton = new QPushButton("Move", this);
    m_qNewGameButton = new QPushButton("New Game", this);

    m_qFrom->setMinimum(1);
    m_qTo->setMinimum(1);
    m_qNewGame->setMinimum(1);

    m_qFrom->setMaximum(hanoimodel::s_iTOWER_COUNT);
    m_qTo->setMaximum(hanoimodel::s_iTOWER_COUNT);
    m_qNewGame->setMaximum(8);

    m_qMainLayout->addLayout(m_qTowerLayout);
    m_qMainLayout->addLayout(m_qMoveLayout);
    m_qMainLayout->addLayout(m_qNewGameLayout);

    for (int i = 0; i < hanoimodel::s_iTOWER_COUNT; ++i){
        m_vecTower* t = new TowerWidget(m_pModel, i, this);
        m_vecTower.append(t);
        m_qTowerLayout->addWidget(t);
    }

    m_qMoveLayout->addWidget(m_qFromLabel);
    m_qMoveLayout->addWidget(m_qFrom);
    m_qMoveLayout->addWidget(m_qToLabel);
    m_qMoveLayout->addWidget(m_qTo);
    m_qMoveLayout->addWidget(m_qMoveButton);

    m_qNewGameLayout->addWidget(m_qNewGameLabel);
    m_qNewGameLayout->addWidget(m_qNewGame);
    m_qNewGameLayout->addWidget(m_qNewGameButton);

}

HanoiView::~HanoiView() {}
