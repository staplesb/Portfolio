// Brent Staples bds0025 Assignment 4
#pragma once
#include "User.h"
#include <iostream>

class User;

class GraphType
{
public:
	GraphType();
	~GraphType();
	GraphType(int noVertices);
	void AddVertex(User* user);
	void AddEdge(User* fromVertex, User* toVertex);
	bool SharedFollows(User* user1, User* user2);
	void DisplaySharedFollows();

	void FindStars();
	void FindFans();

private:
	int numVertices;
	int maxVertices;
	User** vertices;
	bool** edges;

};

