// Brent Staples bds0025 Assignment 4
#pragma once
#include "User.h"
#include "QueType.h"
#include <fstream>

class User;

class QueType;


struct TreeNode
{
	User* user;
	bool visited = false;
	TreeNode* right;
	TreeNode* left;
};

enum OrderType { PRE_ORDER, IN_ORDER, POST_ORDER };

class TreeType
{
public:
	TreeType();
	~TreeType();
	TreeType(const TreeType& originalTree);

	void operator=(TreeType& originalTree);
	void MakeEmpty();
	bool IsEmpty() const;
	bool IsFull() const;
	int GetLength() const;
	User* GetUser(User* user, bool& found);
	void PutUser(User* user);
	void DeleteUser(User* user);
	void ResetTree(OrderType order);
	User* GetNextUser(OrderType order, bool& finished);
	bool IsVisited(User* user) const;
	void ClearVisits();
	void Print(std::ofstream& outFile) const;



private:
	TreeNode* root; 
	QueType* preQue;
	QueType* inQue;
	QueType* postQue;

};

