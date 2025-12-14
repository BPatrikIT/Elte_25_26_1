#ifndef SEARCHDIALOG_H
#define SEARCHDIALOG_H

#include <QDialog>
#include <QLineEdit>
#include <QPushButton>
#include <QCheckBox>
#include <QGridLayout>

class SearchDialog : public QDialog
{
    Q_OBJECT
public:
    SearchDialog(QWidget *parent = nullptr);

signals:
    void searchForward(const QString, Qt::CaseSensitivity);
    void searchBackward(const QString, Qt::CaseSensitivity);

private slots:
    void okClicked();

private:
    QLineEdit *m_qText;
    QPushButton *m_qOk, *m_qCancel;
    QCheckBox *m_qForward, *m_qCase;
    QGridLayout *m_qMainLayout;

};

#endif // SEARCHDIALOG_H
