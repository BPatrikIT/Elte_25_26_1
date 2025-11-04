#include "towerwidget.h"

TowerWidget::TowerWidget(hanoimodel *model, int id, QWidget *parent)
    : QWidget{parent}, m_pModel{model}, m_iId{id}
{
    m_qMainLayout = new QBoxLayout(QBoxLayout::Direction::BottomToTop);
    setLayout(m_qMainLayout);

    m_qMainLayout->addSpacing(0);

    connect(m_pModel, &hanoimodel::gameChanged, this, &TowerWidget::updateTower);
}

TowerWidget::~TowerWidget(){}

void TowerWidget::updateTower()
{
    while (m_qMainLayout->count()){
        QLayoutItem* x = m_qMainLayout->itemAt(0);
        if (QWidget* w = x->widget()){
            delete w;
        }
        m_qMainLayout->removeItem(x);
    }
    QList<int> disks = m_pModel->disksOnTower(m_iId);
    int diskCount = m_pModel->diskCount();
    for (
        QList<int> reverse_iterator x = disks.rbegin();
        x != disks.rend();
        ++x;
        )
    {
        m_qMainLayout.addWidget(new DiskWidget(diskCount, *x, this));
    }
}
