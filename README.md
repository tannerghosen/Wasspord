# Wasspord
Password Manager
By Tanner Ghosen

<b>Requirements:</b>
<ul>
<li>Visual Studio 2019-2022</li>
<ul>
  <li>C#</li>
  <li>.NET 4.8</li>
</ul>
</ul>

# Summary
## What is this program?
This is a password management program for storing passwords in a centralized way,
so if you use multiple browsers or devices you can use this program to record those details.

## How does it accomplish this? (Code Explanation)
There are methods for saving and loading multiple users details, creating, deleting and updating user accounts, and displaying the accounts information, specifically where the account is used (location, like a website or somewhere else), the username and password. This is done by storing it in a dictionary that is initialized with the load method, and saved to a text file to be read by the load method by the save method. There is flexibility after the accounts are created and added to the list by allowing you to update the password (by providing where the account is used, the account name, and password) and delete the account (only needing to provide the location and account name).
