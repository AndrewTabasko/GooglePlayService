CREATE TABLE [dbo].[Apps]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[Guid] nvarchar(50) not null,
	[Name] nvarchar (100) not null,
	[Downloads] bigint not null,
	[URL] nvarchar (max) unique not null
)
