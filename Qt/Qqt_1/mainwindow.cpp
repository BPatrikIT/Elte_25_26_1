#include "mainwindow.h"

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
{
    button = new QPushButton("Quit", this);
    connect(button, &QPushButton::clicked, this, &QWidget::close);
}

MainWindow::~MainWindow() {}
