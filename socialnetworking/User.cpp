// Brent Staples bds0025 Assignment 4
#include "stdafx.h"
#include "User.h"

User::User()//default constructor
{
	strcpy(name, " ");
	gender = ' ';
	strcpy(address.streetName, " ");
	address.streetNo = -1;
	address.zip = -1;
	strcpy(address.city, " ");
	strcpy(address.state, " ");
	followers = new UnsortedType;
	following = new UnsortedType;

}

User::User(const char* argName, char argGender, AddressType argAddress, float argGPA, DateType argDateOfBirth) //constructor takes name gender address gpa and dateofbirth
{
	strcpy(name, argName);
	gender = argGender;
	address = argAddress;
	gpa = argGPA;
	dateOfBirth = argDateOfBirth;
	followers = new UnsortedType;
	following = new UnsortedType;

}


User::~User()//destructor
{
}

void User::GetName(char argName[]) const//GetName by reference
{
	strcpy(argName, name);
}
char User::GetGender() const//GetGender with return
{
	return gender;
}
void User::GetGender(char& argGender) const//GetGender by reference
{
	argGender = gender;
}
DateType User::GetDateOfBirth() const //GetDateOfBirth with return
{
	return dateOfBirth;
}
void User::GetDateOfBirth(DateType& argDateOfBirth) const //GetDateOfBirth by reference
{
	argDateOfBirth = dateOfBirth;
}
float User::GetGPA() const //GetGPA with return
{
	return gpa;
}
void User::GetGPA(float& argGPA) const //GetGPA by reference
{
	argGPA = gpa;
}
AddressType User::GetAddress() const //GetAddress with return
{
	return address;
}
void User::GetAddress(AddressType& argAddress) const //GetAddress by reference
{
	argAddress = address;
}
void User::GetAddress(char argStreetName[], int& argStreetNo, char argCity[], int& argZip, char argState[]) const //GetAddress by reference - broken up into pieces (streetName, streetNo, city, zip, state)
{
	strcpy(argStreetName, address.streetName);
	argStreetNo = address.streetNo;
	strcpy(argCity, address.city);
	argZip = address.zip;
	strcpy(argState, address.state);
}

//observer for display
void User::Display() const //Display a users information to the console 
{
	using namespace std;
	cout << "Name: " << name << endl;
	cout << "Gender: " << gender << endl;
	cout << "Address: " << address.streetName << " " << address.streetNo << " " <<
		address.city << " " << address.zip << " " << address.state << endl;
	cout << "GPA: " << gpa << endl;
	cout << "DOB: " << dateOfBirth.toString() << endl;
	cout << "Following: ";
	following->PrintPartial();
	cout << endl << "Followers: ";
	followers->PrintPartial();
	cout << endl << endl;
}
void User::Display(std::ofstream& outFile) const //output a users info to a file
{
	using namespace std;
	outFile << "Name: " << name << endl;
	outFile << "Gender: " << gender << endl;
	outFile << "Address: " << address.streetName << " " << address.streetNo << " " <<
		address.city << " " << address.zip << " " << address.state << endl;
	outFile << "GPA: " << gpa << endl;
	outFile << "DOB: " << dateOfBirth.toString() << endl;
	outFile << "Following: ";
	following->PrintPartial(outFile);
	outFile << endl << "Followers: ";
	followers->PrintPartial(outFile);
	outFile << endl << endl;
}

//transformers:
void User::SetName(const char* argName) 
{
	strcpy(name, argName);
}
void User::SetGender(char argGender)
{
	gender = argGender;
}
void User::SetDateOfBirth(DateType argDateOfBirth)
{
	dateOfBirth = argDateOfBirth;
}
void User::SetGPA(float argGPA)
{
	gpa = argGPA;
}
void User::SetAddress(AddressType argAddress)
{
	address = argAddress;
}
void User::SetAddress(const char argStreetName[], int argStreetNo,const char argCity[], int argZip,const char argState[])
{
	strcpy(address.streetName, argStreetName);
	address.streetNo = argStreetNo;
	strcpy(address.city, argCity);
	address.zip = argZip;
	strcpy(address.state, argState);
}

//Initializer
void User::Initialize(const char* argName, char argGender, AddressType argAddress, float argGPA, DateType argDateOfBirth) //for initializing a user after a default users has been created. 
{
	strcpy(name, argName);
	gender = argGender;
	address = argAddress;
	gpa = argGPA;
	dateOfBirth = argDateOfBirth;
	followers = new UnsortedType;
	following = new UnsortedType;
}

//new transformers
void User::AddFollower(User* user)
{
	followers->PutUser(user);
}
void User::DeleteFollower(User* user)
{
	followers->DeleteUser(user);
}
void User::AddFollowing(User* user)
{
	following->PutUser(user);
	user->AddFollower(this);

}
void User::DeleteFollowing(User* user)
{
	following->DeleteUser(user);
	user->DeleteFollower(this);
}

UnsortedType* User::GetFollowerList() //returns list of followers
{
	return followers;
}

UnsortedType* User::GetFollowingList() //returns list of following
{
	return following;
}

bool User::operator<(User* anotherUser) const // compares two users for less than
{
	char newName[50];
	anotherUser->GetName(newName);
	if (strcmp(name, newName) < 0)
		return true;
	return false;
}
bool User::operator>(User* anotherUser) const // compares two users for greater than
{
	char newName[50];
	anotherUser->GetName(newName);
	if (strcmp(name, newName) > 0)
		return true;
	return false;
}
bool User::operator=(User* anotherUser) const // compares two users for equals
{
	char newName[50];
	anotherUser->GetName(newName);
	if (strcmp(name, newName) == 0)
		return true;
	return false;
}
