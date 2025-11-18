#ifndef TOWERWIDGET_H
#define TOWERWIDGET_H

#include <QBoxLayout>
#include <QWidget>

class HanoiModel;

class TowerWidget : public QWidget
{
    Q_OBJECT
  public:
    explicit TowerWidget(HanoiModel *model, int id, QWidget *parent = nullptr);

    void dragEnterEvent(QDragEnterEvent *event) override;
    void dropEvent(QDropEvent *event) override;

  public slots:
    void updateTower();

  private:
    const int m_iId;
    HanoiModel *m_pModel;
    QBoxLayout *m_qMainLayout;
};

#endif // TOWERWIDGET_H
