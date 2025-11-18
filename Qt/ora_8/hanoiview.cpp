#include "hanoiview.h"
//
#include <QMessageBox>

#include "towerwidget.h"

HanoiView::HanoiView(QWidget *parent) : QWidget(parent)
{
    m_pModel = new HanoiModel(this);

    m_qMainLayout = new QVBoxLayout(this);
    setLayout(m_qMainLayout);

    m_qBoardLayout	 = new QHBoxLayout();
    m_qMoveLayout	 = new QHBoxLayout();
    m_qNewGameLayout = new QHBoxLayout();

    m_qMainLayout->addLayout(m_qBoardLayout);
    m_qMainLayout->addLayout(m_qMoveLayout);
    m_qMainLayout->addLayout(m_qNewGameLayout);

    m_qFromLabel = new QLabel("From:", this);
    m_qToLabel	 = new QLabel("To:", this);
    m_qNewLabel	 = new QLabel("Size of new game:", this);

    m_qFrom	   = new QSpinBox(this);
    m_qTo	   = new QSpinBox(this);
    m_qNewSize = new QSpinBox(this);

    m_qMoveButton = new QPushButton("Move", this);
    m_qNewButton  = new QPushButton("New game", this);

    m_qMoveLayout->addWidget(m_qFromLabel);
    m_qMoveLayout->addWidget(m_qFrom);
    m_qMoveLayout->addWidget(m_qToLabel);
    m_qMoveLayout->addWidget(m_qTo);
    m_qMoveLayout->addWidget(m_qMoveButton);

    m_qNewGameLayout->addWidget(m_qNewLabel);
    m_qNewGameLayout->addWidget(m_qNewSize);
    m_qNewGameLayout->addWidget(m_qNewButton);

    m_qFrom->setMinimum(1);
    m_qTo->setMinimum(1);
    m_qNewSize->setMinimum(1);

    m_qFrom->setMaximum(HanoiModel::s_iTOWER_COUNT);
    m_qTo->setMaximum(HanoiModel::s_iTOWER_COUNT);
    m_qNewSize->setMaximum(8);

    // create the towers
    for (int i = 0; i < HanoiModel::s_iTOWER_COUNT; ++i) {
        TowerWidget *t = new TowerWidget(m_pModel, i, this);
        m_qBoardLayout->addWidget(t);
    }

    connect(m_qMoveButton, &QPushButton::clicked, this,
            [this]() { m_pModel->moveDisk(m_qFrom->value() - 1, m_qTo->value() - 1); });
    connect(m_qNewButton, &QPushButton::clicked, this,
            [this]() { m_pModel->newGame(m_qNewSize->value()); });
    connect(m_pModel, &HanoiModel::gameOver, this, [this]() {
        QMessageBox::information(this, "Problem solved", "Congratulation, you solved the problem!");
        // emulate a click action to start a new game
        m_qNewButton->click();
    });
    connect(m_pModel, &HanoiModel::invalidMove, this, [this]() {
        QMessageBox::warning(this, "Invalid move",
                             "There was an attempt for an invalid move.\nPlease respect the rules "
                             "of the game.\nThank you! ☺");
    });

    m_pModel->newGame(3);
}

HanoiView::~HanoiView()
{
}
