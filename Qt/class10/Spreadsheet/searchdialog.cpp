#include "searchdialog.h"

SearchDialog::SearchDialog(QWidget* parent) :
    QDialog{parent} {
    m_qMainLayout = new QGridLayout();
    setLayout(m_qMainLayout);

    m_qText = new QLineEdit(this);

    m_qOk = new QPushButton("Search", this);
    m_qCancel = new QPushButton("Cancel", this);

    m_qForward = new QCheckBox("Search Backward", this);
    m_qCase = new QCheckBox("Case sensitive", this);

    m_qMainLayout->addWidget(m_qText, 0, 0, 1, 2);
    m_qMainLayout->addWidget(m_qForward, 1, 0);
    m_qMainLayout->addWidget(m_qCase, 1, 1);
    m_qMainLayout->addWidget(m_qOk, 2, 0);
    m_qMainLayout->addWidget(m_qCancel, 2, 1);

    connect(m_qCancel, &QPushButton::clicked, this, &QDialog::reject);
    connect(m_qOk, &QPushButton::clicked, this, &SearchDialog::okClicked);
    connect(m_qText, &QLineEdit::textEdited, this,
            [this](){ m_qOk->setDisabled(m_qText->text().isEmpty()); });

    m_qOk->setEnabled(false);
}

void SearchDialog::okClicked()
{
    Qt::CaseSensitivity cs = m_qCase->isChecked()
    ?
        Qt::CaseSensitivity::CaseSensitive
    :
        Qt::CaseSensitivity::CaseInsensitive;
    QString text = m_qText->text();
    if(m_qForward->isChecked()) {
        emit searchBackward(text, cs);
    } else {
        emit searchForward(text, cs);
    }
    accept();
}
