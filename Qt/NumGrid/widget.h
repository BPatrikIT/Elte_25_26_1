#ifndef WIDGET_H
#define WIDGET_H

#include <QWidget>
#include <QGridLayout>
#include <QPushButton>
#include <QList>

class Widget : public QWidget
{
    Q_OBJECT

public:
    Widget(QWidget *parent = nullptr);
    ~Widget();

private:
    void resizeGrid(uint size);

    QGridLayout *m_qNumLayout;
    QList<QPushButton *> m_vecButtons;
};
#endif // WIDGET_H
