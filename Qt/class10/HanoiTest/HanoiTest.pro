QT += testlib
QT -= gui

CONFIG += qt console warn_on depend_includepath testcase
CONFIG -= app_bundle

TEMPLATE = app

SOURCES +=  tst_hanoitest.cpp \
    ../../Ora_8/Hanoi/hanoimodel.cpp

HEADERS += \
    ../../Ora_8/Hanoi/hanoimodel.h
