CREATE TABLE AppsDb
(
	Guid UUID NOT NULL PRIMARY KEY,
	Name varchar (100) null,
	GooglePlayId varchar (100) not null,
	Hl varchar (100) null,
	Gl varchar (100) null,
	Downloads bigint not null,
	UNIQUE (GooglePlayId, Hl, Gl)
)