declare @StartDate DATE = '20000101', @NumberOfYears INT = 50;

-- prevent set or regional settings from interfering with 
-- interpretation of dates / literals

set datefirst 7;
set dateformat mdy;
set language US_ENGLISH;

declare @CutoffDate DATE = dateadd(year, @NumberOfYears, @StartDate);

print 'inserting into dbo.Calendar'
insert Calendar([Date]) 
select d
from
(
  select d = dateadd(day, rn - 1, @StartDate)
  from 
  (
    select top (datediff(day, @StartDate, @CutoffDate)) 
      rn = row_number() over (order by s1.[object_id])
    from sys.all_objects as s1
    cross join sys.all_objects as s2
    -- on my system this would support > 5 million days
    order by s1.[object_id]
  ) as x
) as y;

go
