#ifndef TGEN_H
#define TGEN_H

#include <QRandomGenerator>
#include <QHBoxLayout>
#include <QVBoxLayout>
#include <QPushButton>
#include <QTimer>
#include <QQueue>
#include <QWidget>
#include <QList>
#include <QLineEdit>
#include <QSpinBox>
#include <QLabel>

class TGen : public QWidget
{
    Q_OBJECT

public:
    TGen(QWidget *parent = nullptr);
    ~TGen();

private slots:
    void genButtonClicked();
    void setButtonClicked();

private:
    void init();

    int m_iThNum = 20;
    int m_iStNum = 5;
    QList<int> m_vecTh;
    QQueue<int> m_vecTabu;

    QTimer m_qTimer;

    QPushButton *m_qGenerate, *m_qSettings;
    QLineEdit *m_qDisplay;
    QLabel *m_qStLabel, *m_qThLabel;
    QSpinBox *m_qStCount, *m_qThCount;
    QVBoxLayout *m_qMainLayout, *m_qSettingsLayout;
    QHBoxLayout *m_qButtonLayout, *m_qTopLayout;
};
#endif // TGEN_H
