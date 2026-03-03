USE EventDb;
GO

CREATE TABLE EventDetails (
    -- EventId as Primary Key
    EventId INT PRIMARY KEY,
    
    -- EventName: 1 to 50 characters
    EventName VARCHAR(50) NOT NULL 
        CONSTRAINT CK_EventName_Length CHECK (LEN(EventName) >= 1),
    
    -- EventCategory: 1 to 50 characters
    EventCategory VARCHAR(50) NOT NULL 
        CONSTRAINT CK_EventCategory_Length CHECK (LEN(EventCategory) >= 1),
    
    -- EventDate: Standard datetime
    EventDate DATETIME NOT NULL,
    
    -- Description: Explicitly Nullable (no length constraint provided)
    [Description] VARCHAR(MAX) NULL
);
GO