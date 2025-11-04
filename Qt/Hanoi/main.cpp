#include "hanoiview.h"

#include <QApplication>

int main(int argc, char *argv[])
{
    QApplication a(argc, argv);
    HanoiView w;
    w.show();
    return a.exec();
}
