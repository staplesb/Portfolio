// bds0025CS221_02_A4_socialNetworking_pt.4.cpp : Defines the entry point for the console application.
// Brent Staples bds0025 Assignment 4

#include "stdafx.h"
#include "TestDriver.h"
#include "User.h"
#include "UnsortedType.h"
#include "TreeType.h"
#include <fstream>


int main()
{
	TestDriver test;
	UnsortedType userList;
	ofstream outfile, of;
	TreeType treeUsers;

	//outfile.open("StaplesBr_users.txt");
	int num2 = test.Populate("users.txt", userList, treeUsers);
	//test.PopulateFollow("followers.txt", userList);

	int noUsers = userList.GetLength();
	GraphType userNet(noUsers);
	userList.BuildVertexGraph(userNet);

	test.PopulateFollowGraph("followers.txt", userList, userNet);
	userNet.DisplaySharedFollows();
	userNet.FindStars();
	userNet.FindFans();
	//test.Test(userList);
	
	//of.open("treeTest.txt");
	//treeUsers.Print(of);
	//userList.PrintFollows(treeUsers);
	userList.MakeEmpty(); // deallocating and testing MakeEmpty()
	treeUsers.MakeEmpty();
	//outfile.close();
	of.close();

    return 0;
}

