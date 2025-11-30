#include <QCoreApplication>
#include <QtTest>

// add necessary includes here
#include <QSignalSpy>

#include "../../Ora_8/Hanoi/hanoimodel.h"

class HanoiTest : public QObject {
  Q_OBJECT

 public:
  HanoiTest();
  ~HanoiTest();

 private slots:
  void initTestCase();
  void cleanupTestCase();
  void test_case1();

 private:
  HanoiModel* model;
};

HanoiTest::HanoiTest() {}

HanoiTest::~HanoiTest() {}

void HanoiTest::initTestCase() { model = new HanoiModel(this); }

void HanoiTest::cleanupTestCase() { delete model; }

void raise() { throw 1; }

void HanoiTest::test_case1() {
  QSignalSpy spy{model, &HanoiModel::boardUpdate};
  QCOMPARE(spy.count(), 0);
  model->newGame(3);
  QCOMPARE(3 + 4, 7);
  QVERIFY_THROWS_EXCEPTION(int, raise());
  QVERIFY(!model->isFinished());
  QCOMPARE(spy.count(), 1);
}

QTEST_MAIN(HanoiTest)

#include "tst_hanoitest.moc"
