# C# Web-Api
Calling Web Api and storing json data into sql database.

    Using C#
    Calling a Web API of your choosing
    Parsing the JSON payload that is returned
    Storing that data in a SQL database 
    
  Database create:
  
        CREATE TABLE [dbo].[apiData] (
          [Id]     INT            IDENTITY (1, 1) NOT NULL,
          [userId] INT            NULL,
          [title]  NVARCHAR (MAX) NULL,
          [body]   NVARCHAR (MAX) NULL,
          PRIMARY KEY CLUSTERED ([Id] ASC)
      );
      
  Run WebAPIDemo.exe inside C-Web-Api/WebAPIDemo/WebAPIDemo/bin/Release/ 
