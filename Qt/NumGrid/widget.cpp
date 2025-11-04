#include "widget.h"

Widget::Widget(QWidget *parent)
    : QWidget(parent)
{
    m_qNumLayout = new QGridLayout();
    setLayout(m_qNumLayout);

    resizeGrid(10);
}

Widget::~Widget() {}

void Widget::resizeGrid(uint size)
{
    for (QPushButton* b: m_vecButtons) {
        m_qNumLayout->removeWidget(b);
        b->deleteLater();
    }
    for (uint i = 0; i < size; ++i) {
        for (uint j = 0; j < size; ++j) {
            QPushButton* button =
                new QPushButton(QString::number(i* size + j + 1), this);
            m_vecButtons.push_back(button);
            m_qNumLayout->addWidget(button, i, j);
        }
    }
}
