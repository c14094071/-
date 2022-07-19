# GroupChatApp
- make mutiple users talk to each other at local computer
# Overview of architecture
-
|Name |Description|
|-----|--------|
|ChatClient| use MVVM to dynamatically update the view of client   |
|ChatServer| to make connection and broadcast messages to everyone    |
- MVVM

|Name |Description|
|-----|--------|
|Model  |make user model |
|View|show the view of client with xaml|
|ViewModel  |to check variety of user and messages|
- Other

|Name |Description|
|-----|--------|
|Net  |make packets and read packets|
|ChatClient.Server|make clients connect to port|
|ChatServer.Client  |to trace users and users' messages|
# Notice
- only support versions above NET 5.0
- Build projects before compile
# Reference
- https://www.youtube.com/watch?v=I-Xmp-mulz4
