using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Ciemesus.Core.Data;
using Ciemesus.Core.Validation;
using FluentValidation;

namespace Ciemesus.Core.Ciemesus
{
    public class CiemesusGet
    {
        public class Query : ICiemesusRequest<ICiemesusResponse<QueryResult>>
        {
            public int CiemesusId { get; set; }
        }

        public class QueryResult
        {
            public int CiemesusId { get; set; }

            public string CiemesusName { get; set; }

            public DateTime? TakenDate { get; set; }

            public int Likes { get; set; }

            public string Pic { get; set; }

            public Member CiemesusMember { get; set; }

            public Team CiemesusTeam { get; set; }

            public class Member
            {
                public int MemberID { get; set; }
                public string MemberName { get; set; }
                public string Pic { get; set; }
            }

            public class Team
            {
                public int TeamID { get; set; }
                public string TeamName { get; set; }
                public string Pic { get; set; }
            }
        }

        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator(CiemesusDb db)
            {
                CascadeMode = CascadeMode.StopOnFirstFailure;

                RuleFor(c => c.CiemesusId)
                    .CiemesusExists(db);
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
                var result = await _db.Subject.Where(f => f.CiemesusId == message.CiemesusId)
                    .Select(f => new QueryResult
                    {
                        CiemesusId = f.CiemesusId,
                        CiemesusName = f.CiemesusName,
                        TakenDate = f.TakenDate,
                        Likes = f.Likes,
                        Pic = f.Pics,
                        CiemesusMember = new QueryResult.Member
                        {
                            MemberID = f.MemberId,
                            MemberName = f.Member.FirstName + " " + f.Member.LastName.Substring(0, 1).ToUpper(),
                            Pic = f.Member.Pics
                        },
                        CiemesusTeam = new QueryResult.Team
                        {
                            TeamID = f.TeamId,
                            TeamName = f.Team.TeamName,
                            Pic = f.Team.Pics
                        }
                    })
                    .FirstOrDefaultAsync();

                return new CiemesusResponse<QueryResult>
                {
                    Result = result
                };
            }
        }
    }
}
