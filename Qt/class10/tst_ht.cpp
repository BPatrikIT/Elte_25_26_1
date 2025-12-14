#include <QCoreApplication>
#include <QtTest>

// add necessary includes here

// New Project -> Test Project -> Qt Test Project
// Generate initialization and cleanup code, Requires QApplication
// Add test cases as slots
// The initTestCase will run before the first test case
// The cleanupTestCase will run after the last test case

// ** Adding existing files to the project **
// Right click to a folder -> Add existing files... ->
// -> Select the files in the file dialog

#include <QSignalSpy>

#include "../Hanoi/hanoimodel.h"

class HT : public QObject {
  Q_OBJECT

 public:
  HT();
  ~HT();

 private slots:
  void initTestCase();
  void cleanupTestCase();
  void test_case1();

 private:
  HanoiModel* m;
};

HT::HT() {}

HT::~HT() {}

void HT::initTestCase() { m = new HanoiModel(this); }

void HT::cleanupTestCase() { delete m; }

void throws() { throw 1; }

void HT::test_case1() {
  QSignalSpy s = QSignalSpy(m, &HanoiModel::boardUpdate);
  m->newGame(3);
  QVERIFY(!m->isFinished());
  QCOMPARE(m->diskCount(), 3);
  QVERIFY_THROWS_EXCEPTION(int, throws());
  QCOMPARE(s.count(), 1);
}

QTEST_MAIN(HT)

#include "tst_ht.moc"
