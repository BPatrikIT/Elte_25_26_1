#ifndef MYDRAG_H
#define MYDRAG_H

#include <QDrag>

class MyDrag : public QDrag
{
    Q_OBJECT
public:
    MyDrag(QObject* source);
    ~MyDrag();

signals:
    void nullTarget();
};

#endif // MYDRAG_H
