#ifndef BUTTONHUNT_H
#define BUTTONHUNT_H

#include <QWidget>
#include <QPushButton>
#include <QTimer>
#include <QWidget>

class Widget : public QWidget
{
    Q_OBJECT

public:
    Widget(QWidget *parent = nullptr);
    ~Widget();
    void closeEvent(QCloseEvent* event) override;

private slots:
    void clicked();
    void tick();


private:
    int count = 0;
    int time = 0;
    QPushButton* button;
    QTimer timer;
};
#endif // BUTTONHUNT_H
