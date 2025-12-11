#include "mainwindow.h"

//
#include <QToolBar>
#include <QClipboard>
#include <QApplication>
#include <QFileDialog>
#include <QMessageBox>
#include "persistence.h"
#include "cell.h"
#include "searchdialog.h"
#include "gotodialog.h"

MainWindow::MainWindow(QWidget *parent) : QMainWindow(parent) {
  m_pSpreadsheet = new Spreadsheet(new persistence(this), this);
  setCentralWidget(m_pSpreadsheet);

  setupToolBar();
}

MainWindow::~MainWindow() {}

void MainWindow::newFile()
{
    m_pSpreadsheet->clean();
}

void MainWindow::saveFile()
{
    QString path = QFileDialog::getSaveFileName(this, "Save file", ".", "*,,Spreadsheet file (*.sp)");

    if (path.isEmpty()) {
        QMessageBox::warning(this, "Save file", "Cannot save file: no file was chosen");
        return;
    }
    m_pSpreadsheet->save(path);
}

void MainWindow::loadFile()
{
    QString path = QFileDialog::getOpenFileName(this, "Load file", ".", "*,,Spreadsheet file (*.sp)");

    if (path.isEmpty()) {
        QMessageBox::warning(this, "Load file", "Cannot load file: no file was chosen");
        return;
    }
    m_pSpreadsheet->load(path);
}

void MainWindow::copy()
{
    QTableWidgetSelectionRange range = m_pSpreadsheet->selectedRanges()[0];
    QString content = "", csep = "", rsep = "";
    for (int row = range.topRow(); row <= range.bottomRow(); ++row) {
        content += rsep;
        csep = "";
        for (int column = range.leftColumn(); column <= range.rightColumn(); ++column){
            content +=csep;
            content += m_pSpreadsheet->getAt(row, column);
            csep = '\t';
        }
        rsep = "\n";
    }
    QApplication::clipboard()->setText(content);
}

void MainWindow::cut()
{
    copy();
    del();
}

void MainWindow::paste()
{
    QString content = QApplication::clipboard()->text();
    QList<QString> rows = content.split('\n');
    int row_count = rows.count(), column_count = rows[0].count('\t')+1;
    QTableWidgetSelectionRange range = m_pSpreadsheet->selectedRanges()[0];
    if (range.columnCount() * range.rowCount() != 1 && (range.columnCount() != column_count || range.rowCount() != row_count)){
        QMessageBox::warning(this, "Paste error", "Cannot paste content to the given selection.\n Can only paste to a 1x1 or to the sam size.");
        return;
    }

    int row_ind = range.topRow(), column_ind;
    for (const QString &row : rows){
        column_ind = range.leftColumn();
        for (const QString& item: row.split('\t')){
            m_pSpreadsheet->setAt(row_ind, column_ind, item);
            column_ind++;
            if (column_ind >= Spreadsheet::COLUMN_COUNT){
                break;
            }
        }
        row_ind++;
        if (row_ind >= Spreadsheet::ROW_COUNT) {
            break;
        }
    }
}

void MainWindow::del()
{
    for(QTableWidgetItem *item: m_pSpreadsheet->selectedItems()){
        dynamic_cast<Cell*>(item)->setValue("");
    }
}

void MainWindow::search()
{
    SearchDialog s;
    connect(&s, &SearchDialog::searchForward, this->m_pSpreadsheet, &Spreadsheet::searchForward);
    connect(&s, &SearchDialog::searchBackward, this->m_pSpreadsheet, &Spreadsheet::searchBackward);
    s.exec();
}

void MainWindow::jump()
{
    GoToDialog dialog(this);
    if (dialog.exec() == QDialog::DialogCode::Accepted) {
        QString target = dialog.target();
        m_pSpreadsheet->setCurrentCell(target.right(target.size()-1).toInt() -1, target[0].toUpper().toLatin1() - 'A');
    }
}

QAction *MainWindow::create_action(const QString &name, QIcon icon, void (MainWindow::*func)(), const QString &shortcut, const QString &tooltip)
{
    QAction *action = new QAction(icon, name, this);
    if (!shortcut.isEmpty()) {
        action->setShortcut(shortcut);
    }
    if (!tooltip.isEmpty()){
        action->setToolTip(tooltip);
    }
    connect(action, &QAction::triggered, this, func);
    return action;
}

void MainWindow::setupToolBar()
{
    QAction *newFile, *save, *load, *cut, *copy, *del, *paste, *find, *goTo;
    newFile = create_action("New File", QIcon(":/images/new.png"),
                            &MainWindow::newFile, "Ctrl+N", "Create a new file");
    save = create_action("Save File", QIcon(":/images/save.png"),
                            &MainWindow::saveFile, "Ctrl+S", "Save file");
    load = create_action("Load File", QIcon(":/images/open.png"),
                            &MainWindow::loadFile, "Ctrl+O", "Load file");
    cut = create_action("Cut", QIcon(":/images/cut.png"),
                            &MainWindow::cut, "Ctrl+X", "Cut content");
    copy = create_action("Copy", QIcon(":/images/copy.png"),
                            &MainWindow::copy, "Ctrl+C", "Copy content");
    paste = create_action("Paste", QIcon(":/images/paste.png"),
                            &MainWindow::paste, "Ctrl+V", "Paste content");
    del = create_action("Delete", QIcon(":/images/del.png"),
                            &MainWindow::del, "Del", "Delete content");
    find = create_action("Search", QIcon(":/images/find.png"),
                            &MainWindow::search, "Ctrl+F", "Search");
    goTo = create_action("GoTo", QIcon(":/images/gotocell.png"),
                            &MainWindow::jump, "Ctrl+G", "Jump to cell");
    QToolBar* fileToolBar, *editToolBar;
    fileToolBar = new QToolBar("File methods", this);
    editToolBar = new QToolBar("Edit methods", this);

    addToolBar(fileToolBar);
    addToolBar(editToolBar);

    fileToolBar->addAction(newFile);
    fileToolBar->addAction(save);
    fileToolBar->addAction(load);

    editToolBar->addAction(cut);
    editToolBar->addAction(copy);
    editToolBar->addAction(paste);
    editToolBar->addAction(del);
    editToolBar->addAction(find);
    editToolBar->addAction(goTo);
}
