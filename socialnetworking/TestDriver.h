// Brent Staples bds0025 Assignment 4
#pragma once
#include "User.h"
#include "UnsortedType.h"
#include "TreeType.h"
#include <iostream>
#include <fstream>
#include <string>

class TestDriver
{
public:
	TestDriver();
	//int Populate(const char* input, User users[]);
	~TestDriver();
	void Test(std::ofstream& outfile,UnsortedType& allUsers);
	void Test(UnsortedType& allUsers);
	int Populate(const char* input, UnsortedType& allUsers, TreeType& treeUsers) const;
	void PopulateFollow(const char* input, UnsortedType& allUsers) const;
	void TestList(UnsortedType& allUsers) const;
	void TestList(std::ofstream& outFile, UnsortedType& allUsers) const;
	void PopulateFollowGraph(const char* input, UnsortedType& users, GraphType& userNet);
private:
};

