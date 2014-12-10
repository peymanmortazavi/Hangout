Hangout
=======
Hangout is a messaging and social networking app designed for small groups of friends.
It was written by Vahid Mazdeh, Peyman Mortazavi, and Ryan Tabler

Repository Organization
=======================
This repository contains three main directories.
The Backend directory contains all code for the server-side backend of the product. The files here are written in C# for the MongoDB server.
The Frontend directory contains code for the iOS application itself, including Swift files for our code and Objective-C files from the SignalR tool. The work in this Frontend directory is deprecated, because it only runs on one developer's machine.
The Frontend2 directory contains code for a simplified and incomplete model of the iOS application. Frontend2 does not use or contain files from SignalR-ObjC, which would normally be found in the Frontend/Hangout/Pods directory.

Documentation
=============
To generate documentation, you just need to install the GhostDoc extention in Visual Studio and then go to "Tools > GhostDoc > Generate For File" and then select the entire Solution.

In order to extract XML files out of generated comments, you need to specify it in Visual Studio/Xamarin Studio settings.

Execution
=========
To run and test this program, you must have XCode 6 and iOS Simulator installed. Open Frontend2.xcodeproj in XCode and build the project. This will successfully launch the app in the iOS Simulator. The Backend is continuously running on a Windows Azure server at chil.cloudapp.net.

