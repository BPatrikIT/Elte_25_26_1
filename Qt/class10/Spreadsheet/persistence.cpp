#include "persistence.h"

//
#include <QDataStream>
#include <QFile>

persistence::persistence(QObject *parent) : QObject{parent} {}

void persistence::load(const QString &path, QList<Item> &data) const {
  QFile file{path};
  if (!file.open(QFile::OpenModeFlag::ReadOnly)) {
    throw QString("Cannot open file: " + path);
  }
  QDataStream stream{&file};

  stream.setVersion(QDataStream::Version::Qt_6_0);

  data.clear();
  quint16 row, column;
  QString value;
  while (!stream.atEnd()) {
    stream >> row >> column >> value;
    data.push_back({row, column, value});
  }
}

void persistence::save(const QString &path, const QList<Item> &data) const {
  QFile file{path};
  if (!file.open(QFile::OpenModeFlag::WriteOnly)) {
    throw QString("Cannot open file: " + path);
  }
  QDataStream stream{&file};

  stream.setVersion(QDataStream::Version::Qt_6_0);

  for (const Item &item : data) {
    stream << quint16(item.row) << quint16(item.column) << item.value;
  }
}
