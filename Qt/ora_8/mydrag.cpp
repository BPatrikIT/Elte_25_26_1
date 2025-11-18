#include "mydrag.h"

MyDrag::MyDrag(QObject *source) : QDrag{source}
{

}

MyDrag::~MyDrag()
{
    if(target() == nullptr){
        emit nullTarget();
    }
}
