#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include <QMainWindow>

#include "spreadsheet.h"

class MainWindow : public QMainWindow {
  Q_OBJECT

 public:
  MainWindow(QWidget* parent = nullptr);
  ~MainWindow();

  void newFile();
  void saveFile();
  void loadFile();
  void copy();
  void cut();
  void paste();
  void del();
  void search();
  void jump();

 private:
  QAction* create_action (const QString& name, QIcon icon,
                            void (MainWindow::* func)(),
                            const QString& shortcut = "",
                            const QString& tooltip = "");

     void setupToolBar();


  Spreadsheet* m_pSpreadsheet;
};
#endif  // MAINWINDOW_H
