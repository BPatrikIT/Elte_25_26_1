#ifndef HANOIVIEW_H
#define HANOIVIEW_H

#include <QWidget>
#include <QSpinBox>
#include <QLabel>
#include <QPushButton>
#include <QHBoxLayout>
#include <QVBoxLayout>
#include <QList>

#include "hanoimodel.h"
#include "towerwidget.h"

class HanoiView : public QWidget
{
    Q_OBJECT

public:
    HanoiView(QWidget *parent = nullptr);
    ~HanoiView();

private:
    hanoimodel* m_pModel;

    QVBoxLayout *m_qMainLayout;
    QHBoxLayout *m_qTowerLayout, *m_qMoveLayout, *m_qNewGameLayout;
    QLabel *m_qFromLabel, *m_qToLabel, *m_qNewGameLabel;
    QSpinBox *m_qFrom, *m_qTo, *m_qNewGame;
    QPushButton *m_qMoveButton, *m_qNewGameButton;

    QList<TowerWidget*> m_vecTower;
};
#endif // HANOIVIEW_H
