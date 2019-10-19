DROP TABLE IF EXISTS Systems;
CREATE TABLE Systems(
	name TEXT PRIMARY KEY,
	description TEXT,
	wing_type TEXT,
	start_date TEXT
);

DROP TABLE IF EXISTS Components;
CREATE TABLE Components(
	id INTEGER PRIMARY KEY,
	name TEXT,
	description TEXT,
	start_date TEXT,
	active NUMERIC,
	system TEXT NULL REFERENCES Systems(name)
);

DROP TABLE IF EXISTS Notes;
CREATE TABLE Notes(
	id INTEGER PRIMARY KEY,
	summary TEXT,
	description TEXT,
	component INTEGER REFERENCES Components(id)
);

DROP TABLE IF EXISTS CrashHistory;
CREATE TABLE CrashHistory(
	id INTEGER PRIMARY KEY,
	summary TEXT,
	description TEXT,
	time TEXT,
	location TEXT,
	component INTEGER REFERENCES Components(id)
);

INSERT INTO Systems(name, description, wing_type, start_date)
VALUES('Test', 'testentry', 'WingType', 'StartDate');