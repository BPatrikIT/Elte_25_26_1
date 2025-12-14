#ifndef SPREADSHEET_H
#define SPREADSHEET_H

#include <QTableWidget>
#include <QApplication>

class Cell;
class ipersistence;

class Spreadsheet : public QTableWidget {
  Q_OBJECT
 public:
  Spreadsheet(ipersistence* persistence, QWidget* parent = nullptr);

  QString getAt(int row, int column) const;
  void setAt(int row, int column, const QString& value);

  void clean();

  bool load(const QString& path);
  bool save(const QString& path);

  static const int ROW_COUNT = 999, COLUMN_COUNT = 26;

  public slots:
  void searchForward(const QString text, Qt:: CaseSensitivity cs);
  void searchBackward(const QString text, Qt:: CaseSensitivity cs);

 private:
  Cell* getCell(int row, int column) const;
     template <int diff>
  void search(const QString& text, Qt::CaseSensitivity cs){
         int row = currentRow(), column = currentColumn() + diff;
         for (; row < ROW_COUNT && row >= 0; row += diff){
             // TODO
             for (; column < COLUMN_COUNT && column >= 0; column += diff) {
                 if (getAt(row, column).contains(text, cs)){
                     setCurrentCell(row, column);
                     return;
                 }
             }
             column = diff > 0 ? 0 : COLUMN_COUNT -1;
         }
         QApplication::beep();
     }

  ipersistence* m_pPersistence;
};

#endif  // SPREADSHEET_H
