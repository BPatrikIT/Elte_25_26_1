#include "tgen.h"

TGen::TGen(QWidget *parent): QWidget(parent)
{
    m_qMainLayout = new QVBoxLayout();
    setLayout(m_qMainLayout);

    m_qButtonLayout = new QHBoxLayout();
    m_qTopLayout = new QHBoxLayout();
    m_qSettingsLayout = new QVBoxLayout();

//widget
    m_qDisplay = new QLineEdit(this);
    m_qGenerate = new QPushButton("Gen", this);
    m_qSettings = new QPushButton("Settings", this);

    m_qStLabel = new QLabel("Student count: ", this);
    m_qThLabel = new QLabel("Thesis count: ", this);

    m_qStCount = new QSpinBox(this);
    m_qThCount = new QSpinBox(this);

    //Setting Widget
    m_qDisplay->setReadOnly(true);

    m_qStCount->setMinimum(1);
    m_qThCount->setMinimum(1);

    m_qStCount->setValue(m_iStNum);
    m_qThCount->setValue(m_iThNum);

    m_qStLabel->setVisible(false);
    m_qStCount->setVisible(false);
    m_qThLabel->setVisible(false);
    m_qThCount->setVisible(false);

    //Setting Layout
    //m_qMainLayout->addWidget(m_qDisplay);
    //m_qMainLayout->addWidget(m_qGenerate);
    m_qMainLayout->addLayout(m_qTopLayout);
    m_qMainLayout->addLayout(m_qButtonLayout);

    m_qTopLayout->addWidget(m_qDisplay);
    m_qTopLayout->addLayout(m_qSettingsLayout);

    m_qSettingsLayout->addWidget(m_qStLabel);
    m_qSettingsLayout->addWidget(m_qStCount);
    m_qSettingsLayout->addWidget(m_qThLabel);
    m_qSettingsLayout->addWidget(m_qThCount);

    m_qButtonLayout->addWidget(m_qGenerate);
    m_qButtonLayout->addWidget(m_qSettings);

    init();

    connect(m_qGenerate,
            &QPushButton::clicked, this,
            &TGen::genButtonClicked);
    connect(&m_qTimer, &QTimer::timeout,
            this, [&](){
        uint index =
            QRandomGenerator::global()->generate() % m_vecTh.size();
        m_qDisplay->setText(QString::number(m_vecTh[index]));
    });
    connect(m_qSettings, &QPushButton::clicked, this, &TGen::setButtonClicked);
}

TGen::~TGen() {}

void TGen::genButtonClicked() {
    if(m_qGenerate->text() == "Gen"){
        m_qTimer.start(100);
        m_qGenerate->setText("Stop");
        m_qSettings->setEnabled(false);
    } else if (m_qGenerate->text() == "Stop"){
        m_qTimer.stop();
        int th = m_qDisplay->text().toInt();
        m_vecTh.removeAll(th);
        m_vecTabu.push_back(th);
        if (m_vecTabu.size() >= m_iStNum){
            m_vecTh.push_back(m_vecTabu.front());
            m_vecTabu.pop_front();
        }
        m_qSettings->setEnabled(true);
        m_qGenerate->setText("Gen");
    } else{
        assert(false);
    }
}

void TGen::setButtonClicked()
{
    if (m_qSettings->text() == "Settings") {
        m_qStLabel->setVisible(true);
        m_qStCount->setVisible(true);
        m_qThLabel->setVisible(true);
        m_qThCount->setVisible(true);
        m_qGenerate->setEnabled(false);
        m_qSettings->setText("Ok");
    } else if (m_qSettings->text() == "Ok"){
        if (m_qStCount->value() <= m_qThCount->value()){
            m_iStNum = m_qStCount->value();
            m_iThNum = m_qThCount->value();
            m_qStLabel->setVisible(false);
            m_qStCount->setVisible(false);
            m_qThLabel->setVisible(false);
            m_qThCount->setVisible(false);
            m_qGenerate->setEnabled(true);
            m_qSettings->setText("Settings");
            init();
        }
    } else{
        assert(false);
    }
}

void TGen::init()
{
    m_vecTabu.clear();
    m_vecTh.clear();
    for (int i = 1; i <= m_iThNum; ++i) {
        m_vecTh.push_back(i);
    }
}
