// Brent Staples bds0025 Assignment 4
#include "stdafx.h"
#include "TreeType.h"


TreeType::TreeType()
{
	root = NULL;
}

void Destroy(TreeNode*& tree) // recursive function to delete a tree
{
	if (tree != NULL)
	{
		Destroy(tree->left); Destroy(tree->right); delete tree;
		tree = NULL;
	}
}

TreeType::~TreeType()
{
	Destroy(root);
}

void CopyTree(TreeNode*& copy, const TreeNode* originalTree) // recursive function to copy a tree
{
	if (originalTree == NULL)
		copy = NULL;
	else
	{
		copy = new TreeNode;
		copy->user = originalTree->user;
		CopyTree(copy->left, originalTree->left);
		CopyTree(copy->right, originalTree->right);
	}
}

TreeType::TreeType(const TreeType& originalTree)
{
	CopyTree(root, originalTree.root);
}

void TreeType::operator=(TreeType& originalTree) // operator to copy a tree
{
	
	if (&originalTree == this)
		return;
	Destroy(root);
	CopyTree(root, originalTree.root);
}
void TreeType::MakeEmpty() // deletes all nodes in a tree
{
	Destroy(root);
}
bool TreeType::IsEmpty() const // returns whether a tree is empty
{
	return root == NULL;
}

int CountNodes(TreeNode* tree) // counts the number of nodes in a tree recursively
{
	if (tree == NULL)
		return 0;
	else
		return CountNodes(tree->left) + CountNodes(tree->right) + 1;
}
int TreeType::GetLength() const //returns the number of nodes in a tree
{
	return CountNodes(root);
}
void Retrieve(TreeNode* tree, User* user, bool& found) // finds a user recursively
{
	if (tree == NULL)
		found = false;
	else if (user < tree->user)
		Retrieve(tree->left, user, found);
	else if (user > tree->user)
		Retrieve(tree->right, user, found);
	else
	{
		user = tree->user;
		found = true;
	}
}
User* TreeType::GetUser(User* user, bool& found) // returns a found user
{
	Retrieve(root, user, found);
	return user;
}

void Insert(TreeNode*& tree, User* user) // places a user in a tree recursively
{
	if (tree == NULL)
	{
		tree = new TreeNode;
		tree->left = NULL;
		tree->right = NULL;
		tree->user = user;
	}
	else if (user < tree->user)
		Insert(tree->left, user);
	else
		Insert(tree->right, user);

}
void TreeType::PutUser(User* user) // places a user in a tree
{
	Insert(root, user);
}

void GetPredecessor(TreeNode* tree, User* data) // finds the preceding node 
{
	while (tree->right != NULL)
		tree = tree->right;
	data = tree->user;
}

void DeleteNode(TreeNode*& tree);

void Delete(TreeNode*& tree, User* user) // deletes a specific node in a tree recursively
{
	if (user< tree->user)
		Delete(tree->left, user);
	else if (user > tree->user)
		Delete(tree->right, user);
	else
		DeleteNode(tree);
}

void DeleteNode(TreeNode*& tree) // deletes a node in a tree
{
	User* data;
	data = new User;
	TreeNode* tempPtr;
	tempPtr = tree;

	if (tree->left == NULL)
	{
		tree = tree->right;
		delete tempPtr;
	}
	else if (tree->right == NULL)
	{
		tree = tree->left;
		delete tempPtr;
	}
	else
	{
		GetPredecessor(tree->left, data);
		tree->user = data;
		Delete(tree->left, data);
	}
}


void TreeType::DeleteUser(User* user) // deletes a user from a tree
{
	Delete(root, user);
}

void PreOrder(TreeNode* tree, QueType* preQue) // creates a queue with the tree in preorder
{
	if (tree != NULL)
	{
		preQue->Enqueue(tree->user);
		PreOrder(tree->left, preQue);
		PreOrder(tree->right, preQue);
	}
}


void inOrder(TreeNode* tree, QueType* inQue) // creates a queue with the tree inorder
{
	if (tree != NULL)
	{
		inOrder(tree->left, inQue);
		inQue->Enqueue(tree->user);
		inOrder(tree->right, inQue);
	}
}

void PostOrder(TreeNode* tree, QueType* postQue) // creates a queue  with the tree in postorder
{
	if (tree != NULL)
	{
		PostOrder(tree->left, postQue);
		PostOrder(tree->right, postQue);
		postQue->Enqueue(tree->user);
	}
}



void TreeType::ResetTree(OrderType order) // sets an order for the get next function
{
	switch (order)
	{
		case PRE_ORDER : PreOrder(root, preQue); break;
		case IN_ORDER : inOrder(root, inQue); break;
		case POST_ORDER : PostOrder(root, postQue); break;
	}
}
User* TreeType::GetNextUser(OrderType order, bool& finished) // find the next node based on order
{
	User* user;
	user = new User;
	switch (order)
	{
	case PRE_ORDER: preQue->Dequeue(user);
		if (preQue->IsEmpty())
			finished = true;
		break;

	case IN_ORDER: inQue->Dequeue(user);
		if (inQue->IsEmpty())
			finished = true;
		break;
	case POST_ORDER: postQue->Dequeue(user);
		if (postQue->IsEmpty())
			finished = true;
		break;
	}
	return user;
}

void PrintTree(TreeNode* tree, std::ofstream& outFile) // prints out the tree to a file
{
	if (tree != NULL)
	{
		PrintTree(tree->left, outFile);
		tree->user->Display(outFile);
		PrintTree(tree->right, outFile);
	}
}
void TreeType::Print(std::ofstream& outFile) const // prints the tree to the console
{
	PrintTree(root, outFile);
}

bool TreeType::IsFull() const // returns whether the tree is full
{
	TreeNode* location;
	try
	{
		location = new TreeNode;
		delete location;
		return false;
	}
	catch (std::bad_alloc exception)
	{
		return true;
	}
}


bool TreeType::IsVisited(User* user) const // returns if a node in the tree has been visited.
{
	bool found = false;
	TreeNode* location = root;
	while (!found && location!=NULL)
	{
		if (user == location->user)
		{
			found = true;
			break;
		}
		else if (user < location->user)
			location = location->left;
		else
			location = location->right;
	}
	if (location->visited)
		return true;
	else
	{
		location->visited = true;
		return false;
	}
}

void resetVisit(TreeNode*& tree) // recursively resets all nodes visited to false
{
	if (tree != NULL)
	{
		resetVisit(tree->left); resetVisit(tree->right); 
		tree->visited = false;
	}
}

void TreeType::ClearVisits() // returns visited to default value 
{
	resetVisit(root);
}
