#ifndef TOWERWIDGET_H
#define TOWERWIDGET_H

#include <QWidget>
#include <QBoxLayout>
#include <QList>

#include "hanoimodel.h"

class TowerWidget : public QWidget
{
    Q_OBJECT
public:
    explicit TowerWidget(hanoimodel* model, int id, QWidget *parent = nullptr);
    ~TowerWidget();

private slots:
    void updateTower();

private:
    hanoimodel *m_pModel;
    const int m_iId;

    QBoxLayout* m_qMainLayout;

signals:
};

#endif // TOWERWIDGET_H
