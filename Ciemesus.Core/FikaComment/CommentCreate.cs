using System;
using System.Linq;
using System.Threading.Tasks;
using Ciemesus.Core.Data;
using Ciemesus.Core.Validation;
using FluentValidation;

namespace Ciemesus.Core.Comment
{
    public class CommentCreate
    {
        public class Command : ICiemesusRequest<ICiemesusResponse<CommandResult>>
        {
            public int CiemesusId { get; set; }

            public int MemberId { get; set; }

            public string Comment { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator(CiemesusDb db)
            {
                CascadeMode = CascadeMode.StopOnFirstFailure;

                RuleFor(c => c.CiemesusId)
                    .NotEmpty()
                    .CiemesusExists(db);

                RuleFor(c => c.MemberId)
                    .NotEmpty()
                    .MemberExists(db);
            }
        }

        public class CommandResult
        {
            public int CiemesusCommentId { get; set; }
            public string MemberName { get; set; }
            public string MemberPic { get; set; }
            public string MemberComment { get; set; }
            public DateTime CommentDate { get; set; }
            public int Likes { get; set; }
        }

        public class CommandHandler : ICiemesusAsyncRequestHandler<Command, ICiemesusResponse<CommandResult>>
        {
            private readonly CiemesusDb _db;

            public CommandHandler(CiemesusDb db)
            {
                _db = db;
            }

            public async Task<ICiemesusResponse<CommandResult>> Handle(Command message)
            {
                var comment = _db.CiemesusComments.Add(new CiemesusComment
                {
                    CiemesusId = message.CiemesusId,
                    MemberId = message.MemberId,
                    Comment = message.Comment,
                    CommentDate = DateTime.UtcNow,
                    Likes = 0
                });
                await _db.SaveChangesAsync();

                var member = _db.Members.Where(m => m.MemberId == message.MemberId).First();

                var result = new CommandResult
                {
                    CiemesusCommentId = comment.CiemesusCommentId,
                    MemberName = member.FirstName + " " + member.LastName.Substring(0, 1).ToUpper() + ".",
                    MemberPic = member.Pics,
                    MemberComment = comment.Comment,
                    CommentDate = comment.CommentDate,
                    Likes = comment.Likes
                };

                return new CiemesusResponse<CommandResult>
                {
                    Result = result
                };
            }
        }
    }
}
