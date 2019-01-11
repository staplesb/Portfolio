// Brent Staples bds0025 Assignment 4
#pragma once
#include "User.h"

class FullQueue
{};

class EmptyQueue
{};

class User;

struct NodeType;

class QueType
{
public:
	QueType();
	~QueType();

	void MakeEmpty();
	void Enqueue(User* user);
	void Dequeue(User*& user);
	bool IsEmpty() const;
	bool IsFull() const;

private:
	NodeType* front;
	NodeType* rear;
};
