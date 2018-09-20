using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Ciemesus.Core.Data;
using Ciemesus.Core.Validation;
using FluentValidation;
using MediatR;

namespace Ciemesus.Core.Ciemesus
{
    public class CiemesusList
    {
        public class Query : ICiemesusRequest<ICiemesusResponse<QueryResult>>
        {
            public int TeamId { get; set; }
        }

        public class QueryResult
        {
            public int TeamId { get; set; }

            public List<Ciemesus> Ciemesuss { get; set; }

            public class Ciemesus
            {
                public int CiemesusId { get; set; }
                public string CiemesusName { get; set; }
                public string CiemesusMemberName { get; set; }
                public string CiemesusMemberPic { get; set; }
                public string CiemesusPic { get; set; }
                public DateTime? CiemesusDate { get; set; }
                public int Likes { get; set; }
                public List<Comment> Comments { get; set; }
            }

            public class Comment
            {
                public int CiemesusCommentId { get; set; }
                public string MemberName { get; set; }
                public string MemberPic { get; set; }
                public string MemberComment { get; set; }
                public DateTime CommentDate { get; set; }
                public int Likes { get; set; }
            }
        }

        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator(CiemesusDb db)
            {
                RuleFor(q => q.TeamId)
                    .NotEmpty()
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
                var fikas = await _db.Subject.Where(p => p.TeamId == message.TeamId)
                    .Select(x => new QueryResult.Ciemesus
                    {
                        CiemesusId = x.CiemesusId,
                        CiemesusName = x.CiemesusName,
                        CiemesusMemberName = x.Member.FirstName + " " + x.Member.LastName.Substring(0, 1).ToUpper() + ".",
                        CiemesusMemberPic = x.Member.Pics,
                        CiemesusPic = x.Pics,
                        CiemesusDate = x.TakenDate,
                        Likes = x.Likes,
                        Comments = _db.CiemesusComments.Where(f => f.CiemesusId == x.CiemesusId)
                            .Select(c => new QueryResult.Comment
                            {
                                CiemesusCommentId = c.CiemesusCommentId,
                                MemberName = c.Member.FirstName + " " + c.Member.LastName.Substring(0, 1).ToUpper() + ".",
                                MemberPic = c.Member.Pics,
                                MemberComment = c.Comment,
                                CommentDate = c.CommentDate,
                                Likes = c.Likes
                            })
                            .ToList()
                    })
                    .OrderBy(f => f.CiemesusDate)
                .ToListAsync();

                return new CiemesusResponse<QueryResult>
                {
                    Result = new QueryResult { TeamId = message.TeamId, Ciemesuss = fikas }
                };
            }
        }
    }
}
