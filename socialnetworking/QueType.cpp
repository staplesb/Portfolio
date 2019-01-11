// Brent Staples bds0025 Assignment 4
#include "stdafx.h"
#include "QueType.h"


QueType::QueType()
{
	front = NULL;
	rear = NULL;
}


QueType::~QueType()
{
	MakeEmpty();
}

void QueType::MakeEmpty()
{
	NodeType* tempPtr;
	while (front != NULL)
	{
		tempPtr = front;
		front = front->next;
		delete tempPtr;
	}
	rear = NULL;
}

bool QueType::IsFull() const
{
	NodeType* location;
	try
	{
		location = new NodeType;
		delete location;
		return false;
	}
	catch (std::bad_alloc exception)
	{
		return true;
	}
}

bool QueType::IsEmpty() const 
{
	return (front == NULL);
}

void QueType::Enqueue(User* user)
{
	if (IsFull())
		throw FullQueue();
	else
	{
		NodeType* newNode;
		newNode = new NodeType;
		newNode->user = user;
		newNode->next = NULL;
		if (rear == NULL)
			front = newNode;
		else
			rear->next = newNode;
		rear = newNode;
	}
}

void QueType::Dequeue(User*& user)
{
	if (IsEmpty())
		throw EmptyQueue();
	else
	{
		NodeType* tempPtr;
		tempPtr = front;
		user = front->user;
		front = front->next;
		if (front == NULL)
			rear = NULL;
		delete tempPtr;
	}
}
