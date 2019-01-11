// Brent Staples bds0025 Assignment 4
#pragma once
#include <string>

using namespace std;
class DateType
{
public:
	DateType();//constructor
	DateType(int argMonth, int argDay, int argYear); //constructor
	void GetDate(int& argMonth, int& argDay, int& argYear);//observer
	void Initialize(int argMonth, int argDay, int argYear);//transformer
	string toString() const;//observer
	~DateType();//destructor
private:
	int month;
	int day;
	int year;
};

