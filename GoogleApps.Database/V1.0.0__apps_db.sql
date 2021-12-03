CREATE TABLE Apps
(
	guid UUID NOT NULL PRIMARY KEY,
	name varchar (100) null,
	googleplayid varchar (100) not null,
	hl varchar (100) null,
	gl varchar (100) null,
	downloads bigint not null,
	UNIQUE (googleplayid, hl, gl)
)