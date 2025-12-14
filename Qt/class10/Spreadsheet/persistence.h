#ifndef PERSISTENCE_H
#define PERSISTENCE_H

#include <QList>
#include <QObject>

struct Item {
  int row, column;
  QString value;
};

class ipersistence {
 public:
  virtual void load(const QString& path, QList<Item>& data) const = 0;
  virtual void save(const QString& path, const QList<Item>& data) const = 0;
};

class persistence : public QObject, public ipersistence {
  Q_OBJECT
 public:
  explicit persistence(QObject* parent = nullptr);
  void load(const QString& path, QList<Item>& data) const override;
  void save(const QString& path, const QList<Item>& data) const override;
};

#endif  // PERSISTENCE_H
