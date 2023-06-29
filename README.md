# Wasspord
Password Manager
By Tanner Ghosen

<b>Requirements:</b>
* Visual Studio 2019-2022
** C#
** .NET 7.8 (Could possibly run with older versions above 4.0/5.0)

# Summary
## What is this program?
This is a password management program for storing passwords in a centralized way,
so if you use multiple browsers or devices you can use this program to record those details.

## How does it accomplish this? (Code Explanation)
There are methods for saving, loading, creating, deleting, updating (passwords), and displaying the accounts information, specifically where the account is used (location, like a website or somewhere else), the username and password. This is done by storing it in a dictionary that is initialized with the load method, and saved to a text file to be read by the load method by the save method. There is flexibility after the accounts are created and added to the list by allowing you to update the password (by providing where the account is used, the account name, and password) and delete the account (only needing to provide the location and account name).