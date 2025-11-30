#ifndef CELL_H
#define CELL_H

#include <QTableWidgetItem>

class Cell : public QTableWidgetItem {
 public:
  void setValue(const QString& value);
  QString getValue() const;

  QTableWidgetItem* clone() const override;

  Cell();
};

#endif  // CELL_H
