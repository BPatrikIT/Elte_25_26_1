#include "towerwidget.h"
//
#include <QDragEnterEvent>
#include <QDropEvent>
#include <QMimeData>

#include "diskwidget.h"
#include "hanoimodel.h"

TowerWidget::TowerWidget(HanoiModel *model, int id, QWidget *parent)
    : QWidget{parent}, m_iId(id), m_pModel(model)
{
    // direction of filling the layout
    m_qMainLayout = new QBoxLayout(QBoxLayout::BottomToTop, this);
    setLayout(m_qMainLayout);
    // eliminate space between widgets
    m_qMainLayout->setSpacing(0);
    connect(m_pModel, &HanoiModel::boardUpdate, this, &TowerWidget::updateTower);
    connect(m_pModel, &HanoiModel::invalidMove, this, &TowerWidget::updateTower);
    // keep the width of the widget regardless of its disks
    setMinimumWidth(DiskWidget::s_iMAX_WIDTH);
    setAcceptDrops(true);
}

void TowerWidget::dragEnterEvent(QDragEnterEvent *event)
{
    if(qobject_cast<TowerWidget *>(event->mimeData()->parent())){
        event->acceptProposedAction();
    }
}

void TowerWidget::dropEvent(QDropEvent *event)
{
    TowerWidget* tower = qobject_cast<TowerWidget *>(event->mimeData()->parent());
    if(tower != this){
        event->accept();
        m_pModel->moveDisk(tower->m_iId, m_iId);
    } else{
        event->ignore();
        updateTower();
    }
}

void TowerWidget::updateTower()
{
    // remove all layout items: stretch(spacerItem) too,
    // which is not a widget
    while (m_qMainLayout->count() != 0) {
        QLayoutItem *item = m_qMainLayout->itemAt(0);
        if (QWidget *w = item->widget()) {
            delete w;
        }
        m_qMainLayout->removeItem(item);
    }
    QVector<int> disks = m_pModel->disksAt(m_iId);
    int diskCount	   = m_pModel->diskCount();
    DiskWidget *w = nullptr;
    // iterate over the disk id-s in a reversed order
    for (auto it = disks.rbegin(); it != disks.rend(); ++it) {
        w = new DiskWidget(diskCount, *it, this);
        m_qMainLayout->addWidget(w, 0,
                                 // align disk to the center
                                 Qt::AlignCenter | Qt::AlignBottom);
        w->setEnabled(false);
    }
    if (w){
        w->setEnabled(true);
    }
    // add stetch to keep the disks down
    m_qMainLayout->addStretch(diskCount - disks.size());
}
