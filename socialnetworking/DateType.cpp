// Brent Staples bds0025 Assignment 4
#include "stdafx.h"
#include "DateType.h"


DateType::DateType() //default constructor
{
	month = -1;
	day = -1;
	year = -1;
}

DateType::DateType(int argMonth, int argDay, int argYear) // constructor takes month day year 
{
	month = argMonth;
	day = argDay;
	year = argYear;
}

void DateType::Initialize(int argMonth, int argDay, int argYear) // method to initialize a DateType
{
	month = argMonth;
	day = argDay;
	year = argYear;
	
}

void DateType::GetDate(int& argMonth, int& argDay, int& argYear) // GetDate method by reference
{
	argMonth = month;
	argDay = day;
	argYear = year;
}

string DateType::toString() const //Method to turn DateType into a string of the for MM/DD/YYYY
{
	string date = "";
	date += to_string(month) + "/" + to_string(day) + "/" + to_string(year);
	return date;
}


DateType::~DateType() //Destructor
{
}
