#ifndef DISKWIDGET_H
#define DISKWIDGET_H

#include <QWidget>

class DiskWidget : public QWidget
{
    Q_OBJECT
  public:
    static const int s_iMAX_WIDTH = 240, s_iHEIGHT = 30;
    explicit DiskWidget(int count, int id, QWidget *parent = nullptr);

    void mousePressEvent(QMouseEvent *event) override;
    void mouseMoveEvent(QMouseEvent *event) override;

  private:
    bool m_bDragStarted;
    QPoint m_qMousePressPos;
    QColor m_qColor;
};

#endif // DISKWIDGET_H
