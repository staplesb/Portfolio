// Brent Staples bds0025 Assignment 4
#include "stdafx.h"
#include "TestDriver.h"



TestDriver::TestDriver()//constructor
{
}

TestDriver::~TestDriver()//destructor
{
}



void TestDriver::Test(std::ofstream& outfile , UnsortedType& allUsers)//tests the observers and transformers not used to read in the data
{
	User* tempUser;
	char name[50];
	char gender;
	DateType dateOfBirth;
	float gpa;
	AddressType address;
	outfile << "Testing observers." << endl;
	for (int i = 0; i < allUsers.GetLength(); i++)// tests all get functions for each user in the user array
	{											//also test GetLength()
		tempUser = allUsers.GetNextUser();
		tempUser->GetName(name);
		outfile << name << endl;
		gender = tempUser->GetGender();
		outfile << gender << endl;
		tempUser->GetGender(gender);
		outfile << gender << endl;
		address = tempUser->GetAddress();
		outfile << address.streetName << " " << address.streetNo << " " <<
			address.city << " " << address.zip << " " << address.state << endl;
		tempUser->GetAddress(address);
		outfile << address.streetName << " " << address.streetNo << " " <<
			address.city << " " << address.zip << " " << address.state << endl;
		tempUser->GetAddress(address.streetName, address.streetNo, address.city, address.zip, address.state);
		outfile << address.streetName << " " << address.streetNo << " " <<
			address.city << " " << address.zip << " " << address.state << endl;
		gpa = tempUser->GetGPA();
		outfile << gpa << endl;
		tempUser->GetGPA(gpa);
		outfile << gpa << endl;
		dateOfBirth = tempUser->GetDateOfBirth();
		outfile << dateOfBirth.toString() << endl;
		tempUser->GetDateOfBirth(dateOfBirth);
		outfile << dateOfBirth.toString() << endl;
	
	}
	allUsers.ResetList();
	//Test initialize, set address with individual components, and display
	outfile << endl << "Testing other functions." << endl << endl;
	User newUsers[5];
	AddressType add;
	strcpy(add.streetName, "Wallstreet");
	add.streetNo = 10;
	strcpy(add.city, "Huntsville");
	add.zip = 35763;
	strcpy(add.state, "AL");
	DateType dob = DateType(9, 15, 1990);
	float num1, num2;
	num1 = 3.6;
	num2 = 3.8;
	User john = User("John Doe", 'M', add, num1, dob);
	newUsers[0].Initialize("Jane Doe", 'F', add, num2, dob);
	newUsers[0].SetAddress("Coastal", 128, "Madison", 35758, "AL");
	newUsers[0].Display(outfile);
	john.Display(outfile);
	outfile << endl << "TestList:" << endl << endl;
	TestList(outfile, allUsers); // calls new tests from old test
}

void TestDriver::Test(UnsortedType& allUsers)//the same as the previous test function, except outputs to the console. 
{
	User* tempUser;
	char name[50];
	char gender;
	DateType dateOfBirth;
	float gpa;
	AddressType address;
	cout << "Testing observers." << endl;
	for (int i = 0; i < allUsers.GetLength(); i++)// tests all get functions for each user in the user array
	{											//also test GetLength()
		tempUser = allUsers.GetNextUser();
		tempUser->GetName(name);
		cout << name << endl;
		gender = tempUser->GetGender();
		cout << gender << endl;
		tempUser->GetGender(gender);
		cout << gender << endl;
		address = tempUser->GetAddress();
		cout << address.streetName << " " << address.streetNo << " " <<
			address.city << " " << address.zip << " " << address.state << endl;
		tempUser->GetAddress(address);
		cout << address.streetName << " " << address.streetNo << " " <<
			address.city << " " << address.zip << " " << address.state << endl;
		tempUser->GetAddress(address.streetName, address.streetNo, address.city, address.zip, address.state);
		cout << address.streetName << " " << address.streetNo << " " <<
			address.city << " " << address.zip << " " << address.state << endl;
		gpa = tempUser->GetGPA();
		cout << gpa << endl;
		tempUser->GetGPA(gpa);
		cout << gpa << endl;
		dateOfBirth = tempUser->GetDateOfBirth();
		cout << dateOfBirth.toString() << endl;
		tempUser->GetDateOfBirth(dateOfBirth);
		cout << dateOfBirth.toString() << endl;

	}
	allUsers.ResetList();
	cout << endl << "Testing other functions." << endl << endl;
	User newUsers[5];
	AddressType add;
	strcpy(add.streetName, "Wallstreet");
	add.streetNo = 10;
	strcpy(add.city, "Huntsville");
	add.zip = 35763;
	strcpy(add.state, "AL");
	DateType dob = DateType(9, 15, 1990);
	float num1, num2;
	num1 = 3.6;
	num2 = 3.8;
	User john = User("John Doe", 'M', add, num1, dob);
	newUsers[0].Initialize("Jane Doe", 'F', add, num2, dob);
	newUsers[0].SetAddress("Coastal", 128, "Madison", 35758, "AL");
	newUsers[0].Display();
	john.Display();
	cout  << endl << "TestList:" << endl << endl;
	TestList(allUsers); // calls new tests from old test
}

int TestDriver::Populate(const char* input, UnsortedType& allUsers, TreeType& treeUsers) const //added treeUsers to be read in
{
	int count = 0;
	ifstream infile;
	infile.open(input);
	if (!infile)//ensures the file opened correctly
	{
		cout << "The file could not be opened" << endl;
		exit(1);
	}
	cout << "file opened" << endl;
	User* user;
	
	string first, last;
	char name[50];
	char gender;
	AddressType address;
	float gpa;
	DateType dateOfBirth;
	char streetName[30];
	int streetNo;
	char city[30];
	int zip;
	char state[3];
	int month;
	int day;
	int year;
	while (infile >> first)//loop for users info in a file
	{
		user = new User;
		if (infile >> last)
			first += " " + last;
		strcpy(name, first.c_str());
		//cout << name<< endl;
		user->SetName(name);
		if (infile >> gender)
			user->SetGender(gender);
		//cout << gender << endl;
		if (infile >> streetName)
			strcpy(address.streetName, streetName);
		if (infile >> streetNo)
			address.streetNo = streetNo;
		if (infile >> city)
			strcpy(address.city, city);
		if (infile >> zip)
			address.zip = zip;
		if (infile >> state)
		{
			strcpy(address.state, state);
			user->SetAddress(address);
		}
		//cout << address.state << endl;
		if (infile >> gpa)
			user->SetGPA(gpa);
		//cout << gpa << endl;

		if (infile >> month);
		infile >> day;
		infile >> year;
		dateOfBirth.Initialize(month, day, year);
		user->SetDateOfBirth(dateOfBirth);
		//cout << dateOfBirth.toString() << endl;
		count++;
		allUsers.PutUser(user); //test PutUser()
		treeUsers.PutUser(user);
	}
	return count;
}

void TestDriver::PopulateFollow(const char* input, UnsortedType& allUsers) const
{
	ifstream infile;
	infile.open(input);
	if (!infile)//ensures the file opened correctly
	{
		cout << "The file could not be opened" << endl;
		exit(1);
	}
	cout << "file opened" << endl;

	string first, last;
	char name[50];
	User* user;
	User* follower;
	bool found, foundFollower;
	int count;

	while (infile >> first)
	{
		if (infile >> last)
			first += " " + last;
		strcpy(name, first.c_str());
		user = allUsers.GetUser(name, found); //test GetUser()(by name)
		if (infile >> count)
			for (int i = 0; i < count; i++)
			{
				infile >> first >> last;
				first += " " + last;
				strcpy(name, first.c_str());
				follower = allUsers.GetUser(name, foundFollower); //test GetUser()(by name)
				if (found && foundFollower)
				{
					follower->AddFollowing(user); //test AddFollowing() and AddFollower()(called within AddFollowing())
				}
			}
	}
	infile.close();
}

void TestDriver::TestList(UnsortedType& allUsers) const //test isFull(), GetNextUser(), new Display() w/ printPartial(), DeleteUser(),
{														// ResetList(), AddFollowing()and AddFollower()(called within AddFollowing()),
														// DeleteFollowing() and DeleteFollower()(called within DeleteFollowing()), Print()
	bool full = (allUsers.IsFull());
	if (full)
		cout << "The user list is full" << endl;
	else
		cout << "The user list is not full" << endl;
	cout << "First user: ";
	allUsers.GetNextUser()->Display();
	cout << "Second user: ";
	allUsers.GetNextUser()->Display();
	cout << "Resetting List." << endl;
	allUsers.ResetList();
	allUsers.GetNextUser();
	cout << "Deleting second user." << endl;
	allUsers.DeleteUser(allUsers.GetNextUser());
	cout << "Resetting List again." << endl;
	allUsers.ResetList();
	cout << "First user again: ";
	allUsers.GetNextUser()->Display();
	cout << "New second user: ";
	allUsers.GetNextUser()->Display();
	allUsers.ResetList();
	cout << "Resetting list and adding a follower." << endl;
	allUsers.GetNextUser()->AddFollowing(allUsers.GetNextUser());
	allUsers.ResetList();
	allUsers.GetNextUser()->Display();
	allUsers.ResetList();
	cout << "Resetting list and deleting the follower." << endl;
	allUsers.GetNextUser()->DeleteFollowing(allUsers.GetNextUser());
	cout << "Printing all remaining users" << endl << endl;
	allUsers.Print();
	allUsers.ResetList();	
	
}

void TestDriver::TestList(std::ofstream& outFile, UnsortedType& allUsers) const 
{		//test isFull(), GetNextUser(), new Display() w/ printPartial(), DeleteUser(),
		// ResetList(), AddFollowing()and AddFollower()(called within AddFollowing()),
		// DeleteFollowing() and DeleteFollower()(called within DeleteFollowing()), Print()
	bool full = (allUsers.IsFull());
	if (full)
		outFile << "The user list is full" << endl;
	else
		outFile << "The user list is not full" << endl;
	outFile << "First user: ";
	allUsers.GetNextUser()->Display(outFile);
	outFile << "Second user: ";
	allUsers.GetNextUser()->Display(outFile);
	outFile << "Resetting List." << endl;
	allUsers.ResetList();
	allUsers.GetNextUser();
	outFile << "Deleting second user." << endl;
	allUsers.DeleteUser(allUsers.GetNextUser());
	outFile << "Resetting List again." << endl;
	allUsers.ResetList();
	outFile << "First user again: ";
	allUsers.GetNextUser()->Display(outFile);
	outFile << "New second user: ";
	allUsers.GetNextUser()->Display(outFile);
	allUsers.ResetList();
	outFile << "Resetting list and adding a follower." << endl;
	allUsers.GetNextUser()->AddFollowing(allUsers.GetNextUser());
	allUsers.ResetList();
	allUsers.GetNextUser()->Display(outFile);
	allUsers.ResetList();
	outFile << "Resetting list and deleting the follower." << endl;
	allUsers.GetNextUser()->DeleteFollowing(allUsers.GetNextUser());
	outFile << "Printing all remaining users" << endl << endl;
	allUsers.Print(outFile);
	allUsers.ResetList();

}


void TestDriver::PopulateFollowGraph(const char* input, UnsortedType& allUsers, GraphType& userNet)
{
	ifstream infile;
	infile.open(input);
	if (!infile)//ensures the file opened correctly
	{
		cout << "The file could not be opened" << endl;
		exit(1);
	}
	cout << "file opened" << endl;

	string first, last;
	char name[50];
	User* user;
	User* follower;
	bool found, foundFollower;
	int count;

	while (infile >> first)
	{
		if (infile >> last)
			first += " " + last;
		strcpy(name, first.c_str());
		user = allUsers.GetUser(name, found); //test GetUser()(by name)
		if (infile >> count)
			for (int i = 0; i < count; i++)
			{
				infile >> first >> last;
				first += " " + last;
				strcpy(name, first.c_str());
				follower = allUsers.GetUser(name, foundFollower); //test GetUser()(by name)
				if (found && foundFollower)
				{
					follower->AddFollowing(user); //test AddFollowing() and AddFollower()(called within AddFollowing())
					userNet.AddEdge(follower, user);
				}
			}

	}
	infile.close();
}


//The original testdriver populate function
//
//int TestDriver::Populate(const char* input, User users[])// populates a user array from a given input file. Also tests the set functions.
//{
//	int count = 0;
//	ifstream infile;
//	infile.open(input);
//	if (!infile)//ensures the file opened correctly
//	{
//		cout << "The file could not be opened" << endl;
//		exit(1);
//	}
//	cout << "file opened"<< endl;
//
//	string first, last;
//	char name[50];
//	char gender;
//	AddressType address;
//	float gpa;
//	DateType dateOfBirth;
//	char streetName[30];
//	int streetNo;
//	char city[30];
//	int zip;
//	char state[3];
//	int month;
//	int day;
//	int year;
//	while (infile >> first)//loop for users info in a file
//	{
//		if (infile >> last)
//			first += " " + last;
//		strcpy(name, first.c_str());
//		//cout << name<< endl;
//		users[count].SetName(name);
//		if (infile >> gender)
//			users[count].SetGender(gender);
//		//cout << gender << endl;
//		if (infile >> streetName)
//			strcpy(address.streetName, streetName);
//		if (infile >> streetNo)
//			address.streetNo = streetNo;
//		if (infile >> city)
//			strcpy(address.city, city);
//		if (infile >> zip)
//			address.zip = zip;
//		if (infile >> state)
//		{
//			strcpy(address.state, state);
//			users[count].SetAddress(address);
//		}
//		//cout << address.state << endl;
//		if (infile >> gpa)
//			users[count].SetGPA(gpa);
//		//cout << gpa << endl;
//			
//		if (infile >> month);
//			infile >> day;
//			infile >> year;
//			dateOfBirth.Initialize(month, day, year);
//			users[count].SetDateOfBirth(dateOfBirth);
//		//cout << dateOfBirth.toString() << endl;
//		count++;
//		//cout << count << endl;
//	}
//	infile.close();
//	return count;
//}