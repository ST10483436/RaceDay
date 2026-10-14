-- =====================================================
-- RACEDAY DATABASE - PROG6212 Part 2
-- =====================================================

IF EXISTS (SELECT * FROM sys.databases WHERE name = 'RACEDAY')
BEGIN
    ALTER DATABASE RACEDAY SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE RACEDAY;
END


CREATE DATABASE RACEDAY;

USE RACEDAY;


-- 1. USERS
CREATE TABLE Users(
    UsersId  INT PRIMARY KEY IDENTITY(1,1),
    Username VARCHAR(100) NOT NULL,
    Password VARCHAR(255) NOT NULL,
    Email    VARCHAR(255) NOT NULL UNIQUE,
    Role     VARCHAR(20)  NOT NULL
);

-- 2. ORGANISERS
CREATE TABLE Organisers(
    OrganiserId   INT PRIMARY KEY IDENTITY(1,1),
    UsersId       INT NOT NULL UNIQUE,
    OrganiserName VARCHAR(200) NOT NULL,
    PhoneNumber   VARCHAR(20)  NOT NULL,
    FOREIGN KEY (UsersId) REFERENCES Users(UsersId)
);
GO

-- 3. PARTICIPANTS
CREATE TABLE Participants(
    ParticipantId INT PRIMARY KEY IDENTITY(1,1),
    UsersId       INT NOT NULL UNIQUE,
    FirstName     VARCHAR(100) NOT NULL,
    LastName      VARCHAR(100) NOT NULL,
    DateOfBirth   DATE NOT NULL,
    PhoneNumber   VARCHAR(20)  NOT NULL,
    FOREIGN KEY (UsersId) REFERENCES Users(UsersId)
);


-- 4. EVENTS
CREATE TABLE Events(
    EventId     INT PRIMARY KEY IDENTITY(1,1),
    OrganiserId INT NOT NULL,
    EventName   VARCHAR(200) NOT NULL,
    Description VARCHAR(1000) NULL,
    EventDate   DATETIME NOT NULL,
    Location    VARCHAR(300) NOT NULL,
    Distance    DECIMAL(5,2) NOT NULL,
    EventType   VARCHAR(20) NOT NULL,
    FOREIGN KEY (OrganiserId) REFERENCES Organisers(OrganiserId)
);


-- 5. CATEGORIES
CREATE TABLE Categories(
    CategoryId   INT PRIMARY KEY IDENTITY(1,1),
    EventId      INT NOT NULL,
    CategoryName VARCHAR(100) NOT NULL,
    MinAge       INT NULL,
    MaxAge       INT NULL,
    FOREIGN KEY (EventId) REFERENCES Events(EventId)
);


-- 6. EVENT ENROLMENTS
CREATE TABLE EventEnrolments(
    EnrolmentId   INT PRIMARY KEY IDENTITY(1,1),
    ParticipantId INT NOT NULL,
    EventId       INT NOT NULL,
    CategoryId    INT NOT NULL,
    Status        VARCHAR(20) DEFAULT 'Registered',
    FOREIGN KEY (ParticipantId) REFERENCES Participants(ParticipantId),
    FOREIGN KEY (EventId)       REFERENCES Events(EventId),
    FOREIGN KEY (CategoryId)    REFERENCES Categories(CategoryId)
);


-- 7. RESULTS (NEW)
CREATE TABLE Results(
    ResultId      INT PRIMARY KEY IDENTITY(1,1),
    EventId       INT NOT NULL,
    ParticipantId INT NOT NULL,
    CategoryId    INT NOT NULL,
    FinishTime    TIME NOT NULL,
    Position      INT NOT NULL,
    FOREIGN KEY (EventId)       REFERENCES Events(EventId),
    FOREIGN KEY (ParticipantId) REFERENCES Participants(ParticipantId),
    FOREIGN KEY (CategoryId)    REFERENCES Categories(CategoryId)
);


SELECT 'RACEDAY database created. Tables are EMPTY — populate via Swagger.' AS Status;
