USE EventDb;
GO

CREATE TABLE ParticipantEventDetails (
    -- Id as Primary Key
    Id INT PRIMARY KEY,

    -- ParticipantEmailId as Foreign Key (linking to UserInfo.EmailId)
    ParticipantEmailId VARCHAR(255) NOT NULL,

    -- EventId as Foreign Key (linking to EventDetails)
    EventId INT NOT NULL,

    -- SessionId as Foreign Key (linking to SessionInfo)
    SessionId INT NOT NULL,

    -- IsAttended: bit (0 for No, 1 for Yes)
    -- The CHECK constraint ensures only 0 or 1 is entered
    IsAttended BIT NOT NULL 
        CONSTRAINT CK_Attendance_Values CHECK (IsAttended IN (0, 1)),

    -- Foreign Key Constraints
    CONSTRAINT FK_Participant_User FOREIGN KEY (ParticipantEmailId) 
        REFERENCES UserInfo(EmailId),
        
    CONSTRAINT FK_Participant_Event FOREIGN KEY (EventId) 
        REFERENCES EventDetails(EventId),

    CONSTRAINT FK_Participant_Session FOREIGN KEY (SessionId) 
        REFERENCES SessionInfo(SessionId)
);
GO