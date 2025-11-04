#ifndef DISKWIDGET_H
#define DISKWIDGET_H

#include <QWidget>
#include <QColor>

class DiskWidget : public QWidget
{
    Q_OBJECT
public:
    static const int s_iHEIGHT = 30, s_iMAX_WIDTH = 240;

    explicit DiskWidget(int diskCount, int id, QWidget *parent = nullptr);
    ~DiskWidget();

signals:
};

#endif // DISKWIDGET_H
