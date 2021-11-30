CREATE TABLE [dbo].[Apps]
(
	[Guid] uniqueidentifier NOT NULL PRIMARY KEY,
	[Name] nvarchar (100) null,
	[GooglePlayId] nvarchar (100) not null,
	[Hl] nvarchar (100) null,
	[Gl] nvarchar (100) null,
	[Gownloads] bigint not null,
)
CREATE UNIQUE INDEX UqApps
  ON dbo.Apps(GooglePlayId, Hl, Gl);