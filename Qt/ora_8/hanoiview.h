#ifndef HANOIVIEW_H
#define HANOIVIEW_H

#include <QHBoxLayout>
#include <QLabel>
#include <QPushButton>
#include <QSpinBox>
#include <QVBoxLayout>
#include <QWidget>

#include "hanoimodel.h"

class HanoiView : public QWidget
{
    Q_OBJECT

  public:
    HanoiView(QWidget *parent = nullptr);
    ~HanoiView();

  private:
    HanoiModel *m_pModel;
    QVBoxLayout *m_qMainLayout;
    QHBoxLayout *m_qBoardLayout, *m_qMoveLayout, *m_qNewGameLayout;
    QSpinBox *m_qFrom, *m_qTo, *m_qNewSize;
    QLabel *m_qFromLabel, *m_qToLabel, *m_qNewLabel;
    QPushButton *m_qMoveButton, *m_qNewButton;
};
#endif // HANOIVIEW_H
