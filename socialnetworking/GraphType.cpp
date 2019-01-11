// Brent Staples bds0025 Assignment 4
#include "stdafx.h"
#include "GraphType.h"


GraphType::GraphType() // default constructor
{
	numVertices = 0;
	maxVertices = 50;
	vertices = new User*[maxVertices];
	edges = new bool*[maxVertices];
	for (int i = 0; i < maxVertices; i++)
		edges[i] = new bool[maxVertices];
	
}


GraphType::~GraphType()
{
}

GraphType::GraphType(int noVertices) // constructor with argument number of users
{
	numVertices = 0;
	maxVertices = noVertices;
	vertices = new User*[maxVertices];
	edges = new bool*[maxVertices];
	for (int i = 0; i < maxVertices; i++)
		edges[i] = new bool[maxVertices];

}

int IndexIs(User** vertices, User* vertex) //finds a user and returns its index
{
	int index = 0;
	while (!(vertex == vertices[index]))
		index++;
	return index;
}


void GraphType::AddVertex(User* user) // adds a user to vertex array
{
	vertices[numVertices] = user;
	for (int index = 0; index < numVertices; index++)
	{
		edges[numVertices][index] = false;
		edges[index][numVertices] = false;
		
	}
	numVertices++;
}

void GraphType::AddEdge(User* fromVertex, User* toVertex) // makes fromvertex point to tovertex
{
	int row;
	int col;
	
	row = IndexIs(vertices, fromVertex);
	col = IndexIs(vertices, toVertex);
	edges[row][col] = true;
}

bool GraphType::SharedFollows(User* user1, User* user2) // determines if a user is following and being followed by the same users as a second user
{
	int user1Index;
	int user2Index;

	user1Index = IndexIs(vertices, user1);
	user2Index = IndexIs(vertices, user2);

	if (edges[user1Index][user2Index] && edges[user2Index][user1Index])	//Don't display if already following eachother. 
		return false;

	bool sharedFollowers = false;
	bool sharedFollowing = false;

	for (int i = 0; i < maxVertices; i++)
	{
		if (edges[user1Index][i] && edges[user2Index][i])// rows indicate shared following
			sharedFollowing = true;
		if (edges[i][user1Index] && edges[i][user2Index]) // columns indicate shared followers
			sharedFollowers = true;
	}

	return (sharedFollowers && sharedFollowing); // return true if shared followers and following
}

void GraphType::DisplaySharedFollows() // displays suggested followers based on whether two users follow similar people. 
{
	char user1[50];
	char user2[50];

	for (int i = 0; i < maxVertices; i++)
	{
		//vertices[i]->GetName(user1);
		//cout << user1 << " ";
		for (int j = 0; j < maxVertices; j++)
		{
			if (edges[i][j] == 205)// not entirely sure why but for some reason all pairs (x,x) have the value 205
				edges[i][j] = false;
			//cout << edges[i][j] << " ";
		}
	
		//cout << endl;
	}

	for (int i = 0; i < maxVertices; i++)
	{
		for (int j = i + 1; j < maxVertices; j++)
		{
			if (SharedFollows(vertices[i], vertices[j]))
			{
				vertices[i]->GetName(user1);
				vertices[j]->GetName(user2);
				cout << "It's suggested that " << user1 << " and " << user2 << " follow each other." << endl;
			}
		}
	}
}

void GraphType::FindStars() //finds users with 2 or more followers who aren't following anyone
{
	cout << "Stars:" << endl;

	char name[50];

	for (int i = 0; i < maxVertices; i++)
	{
		int followedBy = 0;
		bool noFollowing = true;
		for (int j = 0; j < maxVertices; j++)
		{
			if (edges[j][i])
				followedBy++;
			if (edges[i][j])
				noFollowing = false;
		}
		if (noFollowing && followedBy > 1)
		{
			vertices[i]->GetName(name);
			cout << name << endl;
		}
	}
}

void GraphType::FindFans() //finds users who are following 2 or more users but aren't followed by anyone.
{
	cout << "Fans:" << endl;

	char name[50];

	for (int i = 0; i < maxVertices; i++)
	{
		int following = 0;
		bool noFollowers = true;
		for (int j = 0; j < maxVertices; j++)
		{
			if (edges[i][j])
				following++;
			if (edges[j][i])
				noFollowers = false;
		}
		if (noFollowers && following > 1)
		{
			vertices[i]->GetName(name);
			cout << name << endl;
		}
	}
}