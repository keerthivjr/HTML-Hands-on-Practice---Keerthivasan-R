-- 1. Insert User
INSERT INTO UserInfo (EmailId, UserName, Role, Password)
VALUES ('john.doe@example.com', 'JohnDoe', 'Participant', 'SecurePass123');

-- 2. Insert Event
INSERT INTO EventDetails (EventId, EventName, EventCategory, EventDate, Description)
VALUES (101, 'Tech Summit 2026', 'Technology', '2026-05-15 09:00:00', 'Annual dev conference');

-- 3. Insert Speaker
INSERT INTO SpeakersDetails (SpeakerId, SpeakerName)
VALUES (501, 'Dr. Aris AI');

-- 4. Insert Session
INSERT INTO SessionInfo (SessionId, EventId, SessionTitle, SpeakerId, SessionStart, SessionEnd, SessionUrl)
VALUES (2001, 101, 'Future of SQL', 501, '2026-05-15 10:00:00', '2026-05-15 11:00:00', 'http://meet.tech.com/sql');

-- 5. Insert Participation Record
INSERT INTO ParticipantEventDetails (Id, ParticipantEmailId, EventId, SessionId, IsAttended)
VALUES (1, 'john.doe@example.com', 101, 2001, 1);