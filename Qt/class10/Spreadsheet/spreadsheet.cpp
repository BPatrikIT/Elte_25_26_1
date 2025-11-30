#include "spreadsheet.h"

//
#include <QList>
#include <QMessageBox>

#include "cell.h"
#include "persistence.h"

Spreadsheet::Spreadsheet(ipersistence *persistence, QWidget *parent)
    : QTableWidget{parent}, m_pPersistence{persistence} {
  setSelectionMode(QTableWidget::SelectionMode::ContiguousSelection);

  clean();

  setItemPrototype(new Cell());
}

QString Spreadsheet::getAt(int row, int column) const {
  Cell *cell = getCell(row, column);
  if (cell == nullptr) {
    return "";
  }
  return cell->getValue();
}

void Spreadsheet::setAt(int row, int column, const QString &value) {
  Cell *cell = getCell(row, column);
  if (cell == nullptr) {
    cell = new Cell();
    setItem(row, column, cell);
  }
  cell->setValue(value);
}

void Spreadsheet::clean() {
  setRowCount(0);
  setColumnCount(0);

  setRowCount(ROW_COUNT);
  setColumnCount(COLUMN_COUNT);

  for (int i = 0; i < COLUMN_COUNT; ++i) {
    setHorizontalHeaderItem(i, new QTableWidgetItem(QString(QChar('A' + i))));
  }

  setCurrentCell(0, 0);
}

bool Spreadsheet::load(const QString &path) {
  QList<Item> data;
  try {
    m_pPersistence->load(path, data);
  } catch (const QString &exc) {
    QMessageBox::warning(this, "Loading error", exc);
    return false;
  }
  clean();
  for (const Item &item : data) {
    setAt(item.row, item.column, item.value);
  }
  return true;
}

bool Spreadsheet::save(const QString &path) {
  QList<Item> data;
  for (int row = 0; row < ROW_COUNT; ++row) {
    for (int column = 0; column < COLUMN_COUNT; ++column) {
      QString value = getAt(row, column);
      if (!value.isEmpty()) {
        data.push_back({row, column, value});
      }
    }
  }
  try {
    m_pPersistence->save(path, data);
  } catch (const QString &exc) {
    QMessageBox::warning(this, "Saving error", exc);
    return false;
  }
  return true;
}

Cell *Spreadsheet::getCell(int row, int column) const {
  return static_cast<Cell *>(item(row, column));
}
