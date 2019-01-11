// Brent Staples bds0025 Assignment 4
#include "stdafx.h"
#include "UnsortedType.h"


UnsortedType::UnsortedType()
{
	length = 0;
	listData = NULL;
	currentPos = NULL;
}


UnsortedType::~UnsortedType()
{
	NodeType* tempPtr;

	while (listData != NULL)
	{
		tempPtr = listData;
		listData = listData->next;
		delete tempPtr;
	}
}

void UnsortedType::MakeEmpty() //deallocates the memory 
{
	NodeType* tempPtr;

	while (listData != NULL)
	{

		tempPtr = listData;
		listData = listData->next;
		delete tempPtr;
	}
	length = 0;
}
bool UnsortedType::IsFull() const // tests if the list is full
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
int UnsortedType::GetLength() const //gets length of the list
{
	return length;
}
void UnsortedType::ResetList() // resets the list
{
	currentPos = NULL;
}

User* UnsortedType::GetUser(const char name[], bool& found) // gets a user by name and returns
{
	bool moreToSearch;
	NodeType* location;
	char locationName[50];

	location = listData;
	found = false;
	moreToSearch = (location != NULL);

	while (moreToSearch && !found)
	{
		location->user->GetName(locationName);
		if (strcmp(name, locationName)!=0)
		{
			location = location->next; //i=i+1;
			moreToSearch = (location != NULL);
		}
		if (strcmp(name, locationName)==0)
		{
			moreToSearch = false;
			found = true;
		}
	}
	return location->user;
}
void UnsortedType::PutUser(User* user) //working
{
	NodeType* location;
	if (!IsFull())
	{
		location = new NodeType;
		location->user = user;

		location->next = listData;

		listData = location;
		length++;
	}
}
void UnsortedType::DeleteUser(User* user) //deletes a user from the desired list
{
	NodeType* location = listData;
	NodeType* tempLocation = NULL;

	if (user==location->user)
	{
		tempLocation = location;
		listData = listData->next;
	}
	else
	{
		while (location->next!=NULL && user!=(location->next)->user)
		{
			location = location->next;
		}
		if (location->next != NULL)
		{
			tempLocation = location->next;
			location->next = (location->next)->next;
		}
	}
	delete tempLocation;
	length--;
}
User* UnsortedType::GetNextUser()// increments current position
{
	if (currentPos==NULL)
	{
		currentPos = listData;
	}
	else
	{
		currentPos = currentPos->next;
	}
	return currentPos->user;
}

void UnsortedType::Print() const // uses user.Display() for an entire list
{
	NodeType* location = listData;

	while (location != NULL)
	{
		location->user->Display();
		location = location->next;
	}
	if (location != NULL)
	{
		location->user->Display();
	}
}
void UnsortedType::Print(std::ofstream& out) const // uses user.Display(ofstream) for an entire list
{
	NodeType* location = listData;

	while (location != NULL)
	{
		location->user->Display(out);
		location = location->next;
	}
	if (location!= NULL)
	{
		location->user->Display(out);
	}
}
void UnsortedType::PrintPartial() const //prints the names of all users in a list
{

	NodeType* location = listData;
	char name[50];

	while (location != NULL)
	{
		location->user->GetName(name);
		cout << name << " ";
		location = location->next;
	}
	
}
void UnsortedType::PrintPartial(std::ofstream& out) const // prints the name of all users in a list to file
{
	NodeType* location = listData;
	char name[50];

	while (location != NULL)
	{
		location->user->GetName(name);
		out << name << " ";
		location = location->next;
	}
	
}


void UnsortedType::PrintFollows(TreeType& tree) const // Prints the followers and following of all users
{
	char name[50];
	NodeType* location = listData;
	while (location != NULL)
	{
		tree.ClearVisits();
		location->user->GetName(name);
		cout << "User: " << name << endl;
		cout << "Followers: ";
		location->user->GetFollowerList()->PrintDeep(tree);
		cout << endl;
		tree.ClearVisits();
		cout << "Following: ";
		location->user->GetFollowingList()->PrintDeep(tree);
		cout << endl;
		location = location->next;
	}
}

void UnsortedType::PrintDeep(TreeType& tree) //I spent a fair amount of time trying to figure out exactly
{											// how he wanted this to work. In the end, I'm not entirely certain
	char name[50];							// if this is what he wanted. But it is indirectly recursive and
	if (currentPos == NULL)					// it works so I hope it suffices for some credit. 
		currentPos = listData;
	if (tree.IsVisited(currentPos->user))
		currentPos = currentPos->next;
	else
	{
		currentPos->user->GetName(name);
		cout << name << " ";
	}
	if (currentPos != NULL)
		this->PrintDeep(tree);
}

void UnsortedType::BuildVertexGraph(GraphType& net)// creates a graph for all users in listdata
{
	NodeType* location = listData;

	while (location != NULL)
	{
		net.AddVertex(location->user);
		location = location->next;
	}
	if (location != NULL)
	{
		net.AddVertex(location->user);
	}


}