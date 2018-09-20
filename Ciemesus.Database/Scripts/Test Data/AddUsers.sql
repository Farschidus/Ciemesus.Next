print 'inserting into dbo.Users'

insert into dbo.Users
    (Id, AccessFailedCount, Email, NormalizedEmail,	UserName, NormalizedUserName, EmailConfirmed, ConcurrencyStamp,	SecurityStamp, LockoutEnabled, PhoneNumberConfirmed, TwoFactorEnabled, PasswordHash)
values
    ('2E9646C4-8CF6-46FF-BAD6-50B6B9C5CE8E',0,'admin@Ciemesus.com','admin@Ciemesus.com','CiemesusAdmin','CiemesusAdmin',1,'41CD1040-38D1-437F-AC84-4510612A3EBB','7A812D23-A37B-4679-BCEE-F92DBF34FAA2',0,0,0,'AQAAAAEAACcQAAAAEAz7KBghICfsyzjgcsZ6zBreQAU8oECm8LxbsKi5c/WEami2X9A8l8eh7VdGO/YvOw=='),
    ('D9C4C07F-02B4-44F7-A64E-E0D7CC3604C5',0,'farschidus@Ciemesus.com','farschidus@Ciemesus.com','farschidus','farschidus',1,'41CD1040-38D1-437F-AC84-4510612A3EBB','7A812D23-A37B-4679-BCEE-F92DBF34FAA2',0,0,0,'AQAAAAEAACcQAAAAEAz7KBghICfsyzjgcsZ6zBreQAU8oECm8LxbsKi5c/WEami2X9A8l8eh7VdGO/YvOw=='),
    ('495840D2-1F8F-40A5-AEDC-42BAAEAEEB5F',0,'seb@Ciemesus.com','seb@Ciemesus.com','CiemesusSeb','CiemesusSeb',1,'41CD1040-38D1-437F-AC84-4510612A3EBB','7A812D23-A37B-4679-BCEE-F92DBF34FAA2',0,0,0,'AQAAAAEAACcQAAAAEAz7KBghICfsyzjgcsZ6zBreQAU8oECm8LxbsKi5c/WEami2X9A8l8eh7VdGO/YvOw==');
