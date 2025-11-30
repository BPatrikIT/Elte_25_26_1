#include "cell.h"

void Cell::setValue(const QString &value) {
  setData(Qt::ItemDataRole::EditRole, value);
}

QString Cell::getValue() const {
  return data(Qt::ItemDataRole::EditRole).toString();
}

QTableWidgetItem *Cell::clone() const { return new Cell(*this); }

Cell::Cell() {}
