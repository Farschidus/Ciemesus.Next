using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Ciemesus.Core.Data;
using Ciemesus.Core.Validation;
using FluentValidation;

namespace Ciemesus.Core.Comment
{
    public class CommentGet
    {
        public class Query : ICiemesusRequest<ICiemesusResponse<QueryResult>>
        {
            public int CiemesusId { get; set; }
        }

        public class QueryResult
        {
            public SelectedCiemesus Ciemesus { get; set; }

            public List<Comment> Comments { get; set; }

            public class SelectedCiemesus
            {
                public int CiemesusId { get; set; }
                public string CiemesusName { get; set; }
                public string CiemesusMemberName { get; set; }
                public string CiemesusMemberPic { get; set; }
                public string CiemesusPic { get; set; }
                public DateTime? CiemesusDate { get; set; }
                public int Likes { get; set; }
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
                CascadeMode = CascadeMode.StopOnFirstFailure;

                RuleFor(c => c.CiemesusId)
                    .NotEmpty()
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
                var comments = await _db.CiemesusComments.Where(f => f.CiemesusId == message.CiemesusId).ToListAsync();
                var fikaResult = await _db.Subject.Where(f => f.CiemesusId == message.CiemesusId)
                    .Select(x => new QueryResult.SelectedCiemesus
                    {
                        CiemesusId = x.CiemesusId,
                        CiemesusName = x.CiemesusName,
                        CiemesusMemberName = x.Member.FirstName + " " + x.Member.LastName.Substring(0, 1).ToUpper() + ".",
                        CiemesusMemberPic = x.Member.Pics,
                        CiemesusPic = x.Pics,
                        CiemesusDate = x.TakenDate,
                        Likes = x.Likes
                    })
                     .FirstAsync();

                var commentsResult = new List<QueryResult.Comment>();

                foreach (CiemesusComment comment in comments)
                {
                    commentsResult.Add(new QueryResult.Comment
                    {
                        CiemesusCommentId = comment.CiemesusCommentId,
                        MemberName = comment.Member.FirstName + " " + comment.Member.LastName.Substring(0, 1).ToUpper() + ".",
                        MemberPic = comment.Member.Pics,
                        MemberComment = comment.Comment,
                        CommentDate = comment.CommentDate,
                        Likes = comment.Likes
                    });
                }

                return new CiemesusResponse<QueryResult>
                {
                    Result = new QueryResult { Ciemesus = fikaResult, Comments = commentsResult }
                };
            }
        }
    }
}
