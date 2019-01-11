// Brent Staples bds0025 Assignment 4
#pragma once
#ifndef UnsortedType_h
#define UnsortedType_h
#include "User.h"
#include "TreeType.h"
#include "GraphType.h"
#include <iostream>


class User;

class TreeType;

class GraphType;

struct NodeType
{
	User* user;
	NodeType* next;
};


class UnsortedType
{
public:
	UnsortedType();
	~UnsortedType();

	void MakeEmpty(); 
	bool IsFull() const; 
	int GetLength() const; 
	void ResetList();

	User* GetUser(const char name[], bool& found); 
	void PutUser(User* user);
	void DeleteUser(User* user);
	User* GetNextUser();

	void Print() const;
	void Print(std::ofstream& out) const;
	void PrintPartial() const;
	void PrintPartial(std::ofstream& out) const;

	void PrintFollows(TreeType& tree) const;
	void PrintDeep(TreeType& tree);

	void BuildVertexGraph(GraphType& net);

	

private:
	int length;
	NodeType* listData;
	NodeType* currentPos;

};
#endif

