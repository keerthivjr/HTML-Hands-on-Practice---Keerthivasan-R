USE EventDb;
GO

CREATE TABLE SessionInfo (
    -- SessionId as Primary Key
    SessionId INT PRIMARY KEY,

    -- EventId as Foreign Key (linking to EventDetails)
    EventId INT NOT NULL,

    -- SessionTitle: 1 to 50 characters
    SessionTitle VARCHAR(50) NOT NULL 
        CONSTRAINT CK_SessionTitle_Length CHECK (LEN(SessionTitle) >= 1),

    -- SpeakerId as Foreign Key (linking to SpeakersDetails)
    SpeakerId INT NOT NULL,

    -- Description: Nullable
    [Description] VARCHAR(MAX) NULL,

    -- Time Slots: Mandatory datetime fields
    SessionStart DATETIME NOT NULL,
    SessionEnd DATETIME NOT NULL,

    -- SessionUrl: Nullable (assumed based on standard event patterns)
    SessionUrl VARCHAR(2048) NULL,

    -- Foreign Key Constraints
    CONSTRAINT FK_Session_Event FOREIGN KEY (EventId) 
        REFERENCES EventDetails(EventId),
        
    CONSTRAINT FK_Session_Speaker FOREIGN KEY (SpeakerId) 
        REFERENCES SpeakersDetails(SpeakerId),

    -- Logical Constraint: Session cannot end before it starts
    CONSTRAINT CK_SessionTime CHECK (SessionEnd > SessionStart)
);
GO