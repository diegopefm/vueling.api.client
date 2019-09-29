# vueling.api
Vueling candidate test api part.

This test includes two projects, a DotNet Core C# Api backend and an Angular 8 frontend. This is the backend part.

Both projects are stored individually on two separate GitHub repositories for "separation of concerns".

Backend is developed using DotNet Core Api 2.1 and Visual Studio 2017.

Cors is enabled in api so to allow a cross domain request from client.

INSTALL AND RUN THE API:

1) Create a folder to store the app anywhere locally.

2) Install Source code management: Git (https://git-scm.com/downloads)

3) Open console and move to folder in step 1)

4) Get source code from console: git clone https://github.com/diegopefm/vueling.api.git

6) Launch Visual Studio, open solution file (Vueling.Api.Client.sln) and launch with F5.

CONFIGURE THE DATABASE

Engine: SQL.
Instance Name: SQLEXPRESS
Windows Authentication: Off.
User: vueling
Pass: vueling@Api2019

If SQL setup is different connection string can be modified in appsettings.json file.

Password is set in connection string in code in an encripted way for security reasons, so even if you need to change connection string DON'T MODIFY PASSWORD or connection to SQL will fail.