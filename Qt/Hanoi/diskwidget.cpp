#include "diskwidget.h"

#include <QPalette>

DiskWidget::DiskWidget(int diskCount, int id, QWidget *parent)
    : QWidget{parent}
{
    double ratio = (double)(id + 1) / diskCount;
    setFixedSize(s_iMAX_WIDTH * ratio, s_iHEIGHT);
    QPalette p = palette();
    p.setColor(QPalette::ColorRole::Window, QColor(255 * ratio, (1-ratio) * 127, (int)(3*ratio*255)%256));
    setPalette(p);
    setAutoFillBackground(true);
}

DiskWidget::~DiskWidget(){}
