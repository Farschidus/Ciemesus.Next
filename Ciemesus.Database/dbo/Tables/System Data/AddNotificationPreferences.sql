print 'inserting into dbo.NotificationPreferences'

insert into dbo.NotificationPreferences 
    (NotificationDescription, DefaultSetting)
values 
    ('DispenserTurnedRed', 1),
    ('PeopleCounterThresholdRed', 1),
    ('VisitorFeedbackRed', 1),
    ('NewMessage', 1),
    ('TaskSkipped', 0),
    ('QuickTaskOverdue', 1),
    ('PlanFinishedButLessThan75percentComplete', 0)
