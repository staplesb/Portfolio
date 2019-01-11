// Brent Staples bds0025 Assignment 4
#pragma once
#ifndef User_h
#define User_h

#include "UnsortedType.h"
#include "DateType.h"
#include <string>
#include <iostream>
#include <fstream>

class UnsortedType;

struct AddressType //Structure for storing address 
{
	char streetName[30];
	int streetNo;
	char city[30];
	int zip;
	char state[3];
};

enum LIST_NAME {FOLLOWING, FOLLOWERS};

class User
{
public:
	User();//constructor
	User(const char* argName, char argGender, AddressType argAddress, float argGPA, DateType argDateOfBirth);//constructor
	~User();//destructor

	//observers:
	void GetName(char argName[]) const;
	char GetGender() const;
	void GetGender(char& argGender) const;
	DateType GetDateOfBirth() const; 
	void GetDateOfBirth(DateType& argDateOfBirth) const; 
	float GetGPA() const; 
	void GetGPA(float& argGPA) const;
	AddressType GetAddress() const; 
	void GetAddress(AddressType& argAddress) const; 
	void GetAddress(char argStreetName[], int& argStreetNo, char argCity[], int& argZip, char argState[]) const; 

	//observer for display
	void Display() const;
	void Display(std::ofstream& outFile) const;

	//transformers:
	void SetName(const char* argName); 
	void SetGender(char argGender); 
	void SetDateOfBirth(DateType argDateOfBirth); 
	void SetGPA(float argGPA); 
	void SetAddress(AddressType argAddress); 
	void SetAddress(const char argStreetName[], int argStreetNo, const char argCity[], int argZip, const char argState[]);
	void Initialize(const char* argName, char argGender, AddressType argAddress, float argGPA, DateType argDateOfBirth);
	
	void AddFollower(User* user); 
	void DeleteFollower(User* user);
	void AddFollowing(User* user);
	void DeleteFollowing(User* user);

	UnsortedType* GetFollowerList();
	UnsortedType* GetFollowingList();

	bool operator<(User* anotherUser) const;
	bool operator>(User* anotherUser) const;
	bool operator=(User* anotherUser) const;

	

private:
	char name [50];
	char gender;
	AddressType address;
	float gpa;
	DateType dateOfBirth;
	UnsortedType* followers;
	UnsortedType* following;
	
};
#endif

