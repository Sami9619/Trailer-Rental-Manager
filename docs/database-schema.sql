PRAGMA foreign_keys = ON;

CREATE TABLE IF NOT EXISTS Customer (
    CustomerId INTEGER PRIMARY KEY AUTOINCREMENT,
    Gender TEXT NOT NULL CHECK (Gender IN ('w', 'm', 'd')),
    FirstName TEXT NOT NULL,
    LastName TEXT NOT NULL,
    PostalCode TEXT,
    Street TEXT,
    HouseNumber TEXT,
    City TEXT,
    IdDocumentNumber TEXT,
    DrivingLicenseIssueDate TEXT,
    DrivingLicenseClass TEXT
);

CREATE TABLE IF NOT EXISTS Trailer (
    TrailerId INTEGER PRIMARY KEY AUTOINCREMENT,
    TrailerName TEXT NOT NULL,
    TrailerType TEXT NOT NULL,
    MaxPayload NUMERIC,
    Height NUMERIC,
    Width NUMERIC,
    Length NUMERIC
);

CREATE TABLE IF NOT EXISTS Garage (
    GarageId INTEGER PRIMARY KEY AUTOINCREMENT,
    Street TEXT NOT NULL,
    HouseNumber TEXT NOT NULL,
    PostalCode TEXT NOT NULL,
    City TEXT NOT NULL,
    MonthlyRent NUMERIC NOT NULL CHECK (MonthlyRent >= 0)
);

CREATE TABLE IF NOT EXISTS RentalOrder (
    RentalOrderId INTEGER PRIMARY KEY AUTOINCREMENT,
    CustomerId INTEGER NOT NULL,
    TrailerId INTEGER NOT NULL,
    StartDate TEXT NOT NULL CHECK (
        StartDate GLOB '[0-9][0-9][0-9][0-9]-[0-9][0-9]-[0-9][0-9]'
        AND date(StartDate) = StartDate
    ),
    EndDate TEXT NOT NULL CHECK (
        EndDate GLOB '[0-9][0-9][0-9][0-9]-[0-9][0-9]-[0-9][0-9]'
        AND date(EndDate) = EndDate
    ),
    Price NUMERIC NOT NULL CHECK (Price >= 0),
    CHECK (EndDate >= StartDate),
    FOREIGN KEY (TrailerId) REFERENCES Trailer(TrailerId) ON DELETE CASCADE,
    FOREIGN KEY (CustomerId) REFERENCES Customer(CustomerId) ON DELETE CASCADE
);
