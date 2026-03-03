SELECT 
    U.UserName, 
    E.EventName, 
    S.SessionTitle, 
    SD.SpeakerName,
    CASE WHEN P.IsAttended = 1 THEN 'Yes' ELSE 'No' END AS Attended
FROM ParticipantEventDetails P
JOIN UserInfo U ON P.ParticipantEmailId = U.EmailId
JOIN EventDetails E ON P.EventId = E.EventId
JOIN SessionInfo S ON P.SessionId = S.SessionId
JOIN SpeakersDetails SD ON S.SpeakerId = SD.SpeakerId;