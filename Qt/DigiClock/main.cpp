#include "digiclocks.h"

#include <QApplication>

int main(int argc, char *argv[])
{
    QApplication a(argc, argv);
    DigiClocks w;
    w.show();
    return a.exec();
}
