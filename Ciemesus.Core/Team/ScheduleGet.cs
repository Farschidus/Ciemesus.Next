using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Ciemesus.Core.Data;
using Ciemesus.Core.Validation;
using FluentValidation;

namespace Ciemesus.Core.Team
{
    public class ScheduleGet
    {
        public class Query : ICiemesusRequest<ICiemesusResponse<QueryResult>>
        {
            public int TeamId { get; set; }
        }

        public class QueryResult
        {
            public int TeamId { get; set; }

            public string TeamName { get; set; }

            public string DayOfWeek { get; set; }

            public string Interval { get; set; }

            public List<Member> Members { get; set; }

            public class Member
            {
                public int MemberID { get; set; }
                public string MemberName { get; set; }
                public string Pic { get; set; }
                public DateTime? CiemesusDate { get; set; }
                public string CiemesusTitle { get; set; }
            }
        }

        public class QueryResultHelper
        {
            public class CiemesusTitleAndDate
            {
                public string CiemesusTitle { get; set; }
                public DateTime? CiemesusDate { get; set; }
            }
        }

        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator(CiemesusDb db)
            {
                CascadeMode = CascadeMode.StopOnFirstFailure;

                RuleFor(c => c.TeamId)
                    .TeamExists(db);
            }
        }

        public class QueryHandler : ICiemesusAsyncRequestHandler<Query, ICiemesusResponse<QueryResult>>
        {
            private readonly CiemesusDb _db;

            public QueryHandler(CiemesusDb db)
            {
                _db = db;
            }

            public async Task<ICiemesusResponse<QueryResult>> Handle(Query message)
            {
                var teamMembers = await _db.TeamMembers.Where(tm => tm.TeamId == message.TeamId).ToListAsync();
                var members = new List<QueryResult.Member>();
                foreach (var member in teamMembers)
                {
                    var fikaMetaData = GetCiemesusDate(member.MemberId);

                    members.Add(new QueryResult.Member
                    {
                        MemberID = member.MemberId,
                        MemberName = member.Member.FirstName + " " + member.Member.LastName.Substring(0, 1).ToUpper(),
                        Pic = member.Member.Pics,
                        CiemesusDate = fikaMetaData.CiemesusDate,
                        CiemesusTitle = string.IsNullOrEmpty(fikaMetaData.CiemesusTitle) ? "Write your fika name" : fikaMetaData.CiemesusTitle
                    });
                }

                var team = await _db.Teams.Where(t => t.TeamId == message.TeamId).FirstAsync();

                var result = new QueryResult
                {
                    TeamId = team.TeamId,
                    TeamName = team.TeamName,
                    DayOfWeek = GetDayOfWeek(team.StartedAt),
                    Interval = GetInterval(team.Interval),
                    Members = members
                };

                return new CiemesusResponse<QueryResult>
                {
                    Result = result
                };
            }

            private QueryResultHelper.CiemesusTitleAndDate GetCiemesusDate(int memberId)
            {
                var fikaDate = _db.Subject
                    .Where(f => f.MemberId == memberId && f.TakenDate > DateTime.UtcNow)
                    .Select(f => new QueryResultHelper.CiemesusTitleAndDate
                    {
                        CiemesusDate = f.TakenDate,
                        CiemesusTitle = f.CiemesusName
                    })
                    .FirstOrDefault();

                return fikaDate;
            }

            private string GetDayOfWeek(DateTime startedAt)
            {
                return startedAt.DayOfWeek.ToString();
            }

            private string GetInterval(int interval)
            {
                var result = "Weekly";

                switch (interval)
                {
                    case 7:
                        result = "Weekly";
                        break;
                    case 14:
                        result = "Biweekly";
                        break;
                    case 30:
                        result = "Monthly";
                        break;
                }

                return result;
            }
        }
    }
}
