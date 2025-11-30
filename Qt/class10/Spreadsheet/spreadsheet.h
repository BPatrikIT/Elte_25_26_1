#ifndef SPREADSHEET_H
#define SPREADSHEET_H

#include <QTableWidget>

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

 private:
  Cell* getCell(int row, int column) const;

  ipersistence* m_pPersistence;
};

#endif  // SPREADSHEET_H
