#include "gotodialog.h"

#include <QRegularExpression>
#include <QRegularExpressionValidator>

GoToDialog::GoToDialog(QWidget* parent) : QDialog{parent} {
    m_qMainLayout = new QGridLayout();
    setLayout(m_qMainLayout);

    m_qAddress = new QLineEdit(this);

    m_qOk = new QPushButton("jump", this);
    m_qCancel = new QPushButton("Cancel", this);

    m_qMainLayout->addWidget(m_qAddress, 0, 0, 1, 2);
    m_qMainLayout->addWidget(m_qOk, 1, 0);
    m_qMainLayout->addWidget(m_qCancel, 1, 1);

    connect(m_qOk, &QPushButton::clicked, this, &GoToDialog::accept);
    connect(m_qCancel, &QPushButton::clicked, this, &QDialog::reject);

    QRegularExpression re{"^[a-zA-Z][1-9][0-9]{0,2}$"};
    m_qAddress->setValidator(new QRegularExpressionValidator(re));
    connect(m_qAddress, &QLineEdit::textChanged, this,
            [this]() { m_qOk->setEnabled(m_qAddress->hasAcceptableInput());});

    m_qOk->setEnabled(false);
}
