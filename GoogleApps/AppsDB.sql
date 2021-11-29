CREATE TABLE [dbo].[Apps]
(
	[Guid] uniqueidentifier NOT NULL PRIMARY KEY,
	[Name] nvarchar (100) not null,
	[Downloads] bigint not null,
	[URL] nvarchar (max) unique not null
)