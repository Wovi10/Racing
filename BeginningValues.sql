USE [Racing]
GO

INSERT INTO [dbo].[Countries]
           ([Name])
     VALUES
           ('Belgium')
GO

INSERT INTO [dbo].[Pilots]
           ([LicensNr]
           ,[Name]
           ,[FirstName]
           ,[NickName]
           ,[PhotoPath]
           ,[Gender]
           ,[Birthdate]
           ,[Length]
           ,[Weight])
     VALUES
           ('0123456789AB'
           ,'Vinckevleugel'
           ,'Wout'
           ,'Wovi10'
           ,''
           ,'M'
           ,'20011126 00:00:00 AM'
           ,189
           ,66)
GO

INSERT INTO [dbo].[Series]
           ([Name]
           ,[Active]
           ,[StartDate]
           ,[EndDate]
           ,[SortingOrder])
     VALUES
           ('F2'
           ,0
           ,'20211126 00:00:00 AM'
           ,'20211226 00:00:00 AM'
           ,1)
GO

INSERT INTO [dbo].[Teams]
           ([Name])
     VALUES
           ('The mini Coopers')
GO