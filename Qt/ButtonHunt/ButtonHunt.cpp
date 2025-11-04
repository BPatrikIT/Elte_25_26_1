#include "ButtonHunt.h"

#include <QRandomGenerator>
#include <QMessageBox>

Widget::Widget(QWidget *parent)
    : QWidget(parent), button(new QPushButton("Click Me!", this))
{
    connect(button, &QPushButton::clicked, this, &Widget::clicked);
    setMinimumSize(400, 300);
    connect(&timer, &QTimer::timeout, this, &Widget::tick);
    timer.setInterval(1000);
    timer.start();
}

Widget::~Widget() {}

void Widget::clicked()
{
    count++;
    int w = width();
    int h = height();
    int bw = button->width();
    int bh = button->height();

    button->setGeometry(QRandomGenerator::global()->generate() % (w - bw), QRandomGenerator::global()->generate() % (h - bh), bw, bh);
}

void Widget::tick()
{
    time++;
}

void Widget::closeEvent(QCloseEvent* event)
{
    if (time == 0)
        return;
    QMessageBox::information(this, "Game Over", "The Score is " + QString::number(count / (double)time));
}
