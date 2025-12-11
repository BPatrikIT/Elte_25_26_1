#ifndef GOTODIALOG_H
#define GOTODIALOG_H

#include <QDialog>
#include <QLineEdit>
#include <QPushButton>
#include <QGridLayout>


class GoToDialog : public QDialog
{
    Q_OBJECT
public:
    GoToDialog(QWidget* parent = nullptr);

    QString target() const {
        return m_qAddress->text();
    }

private:
    QGridLayout* m_qMainLayout;
    QLineEdit* m_qAddress;
    QPushButton *m_qOk, *m_qCancel;
};

#endif // GOTODIALOG_H
