#include "diskwidget.h"

#include <QMouseEvent>
#include <QApplication>
#include "mydrag.h"
#include <QMimeData>

#include "towerwidget.h"

DiskWidget::DiskWidget(int count, int id, QWidget *parent) : QWidget{parent}
{
    double ratio = double(id + 1) / count;
    setFixedSize(s_iMAX_WIDTH * ratio, s_iHEIGHT);
    // setting the background color of the disk
    QPalette pal = palette();
    // WINDOW sets the background
    m_qColor = QColor(ratio * 255, (1 - ratio) * 127, int(3 * ratio) % 255);
    pal.setColor(QPalette::ColorRole::Window,
                QColor(ratio * 255, (1 - ratio) * 127, int(3 * ratio) % 255));
    setPalette(pal);
    setAutoFillBackground(true);
    m_bDragStarted = false;
}

void DiskWidget::mousePressEvent(QMouseEvent *event)
{
    if(event->button() == Qt::MouseButton::LeftButton){
        m_bDragStarted = false;
        m_qMousePressPos = event->pos();
    }
    QWidget::mousePressEvent(event);
}

void DiskWidget::mouseMoveEvent(QMouseEvent *event)
{
    if(m_bDragStarted || !(event->buttons() & Qt::MouseButton::LeftButton)){
        return;
    }
    QPoint pos = event->pos();
    if((pos - m_qMousePressPos).manhattanLength() < QApplication::startDragDistance()){
        return;
    }
    m_bDragStarted = true;
    MyDrag drag(parent());
    QMimeData* mimeData = new QMimeData;
    mimeData->setParent(parent());
    drag.setMimeData(mimeData);
    QPixmap cursor(size());
    cursor.fill(m_qColor);
    drag.setDragCursor(cursor, Qt::DropAction::MoveAction);
    TowerWidget *tower = qobject_cast<TowerWidget*>(parent());
    if(tower){
        connect(&drag, &MyDrag::nullTarget, tower, &TowerWidget::updateTower);
    }
    if(tower && tower->layout()){
        QLayout *l = tower->layout();
        if(l->count() >= 2){
            auto item = l->itemAt(l->count()-2);
            if(item->widget()){
                delete item->widget();
                l->removeItem(item);
            }
        }
    }

    drag.exec();
}
