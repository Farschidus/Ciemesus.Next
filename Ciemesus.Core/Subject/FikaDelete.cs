using System.Threading.Tasks;
using Ciemesus.Core.Data;
using Ciemesus.Core.Validation;
using FluentValidation;

namespace Ciemesus.Core.Ciemesus
{
    public class CiemesusDelete
    {
        public class Command : ICiemesusRequest<ICiemesusResponse>
        {
            public int CiemesusId { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator(CiemesusDb db)
            {
                CascadeMode = CascadeMode.StopOnFirstFailure;

                RuleFor(c => c.CiemesusId)
                    .CiemesusExists(db);
            }
        }

        public class CommandHandler : ICiemesusAsyncRequestHandler<Command, ICiemesusResponse>
        {
            private readonly CiemesusDb _db;

            public CommandHandler(CiemesusDb db)
            {
                _db = db;
            }

            public async Task<ICiemesusResponse> Handle(Command message)
            {
                var fika = await _db.Subject.FindAsync(message.CiemesusId);

                _db.Subject.Remove(fika);

                await _db.SaveChangesAsync();

                return new CiemesusResponse();
            }
        }
    }
}
