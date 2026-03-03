CREATE DATABASE EventDb;
GO

USE EventDb;
GO

CREATE TABLE UserInfo (
    EmailId VARCHAR(255) PRIMARY KEY,
    
    UserName VARCHAR(50) NOT NULL 
        CONSTRAINT CK_UserName_Length CHECK (LEN(UserName) >= 1),
    
    [Role] VARCHAR(20) NOT NULL 
        CONSTRAINT CK_UserRole CHECK ([Role] IN ('Admin', 'Participant')),
    
    [Password] VARCHAR(20) NOT NULL 
        CONSTRAINT CK_Password_Length CHECK (LEN([Password]) BETWEEN 6 AND 20)
);