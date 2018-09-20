using System.Threading.Tasks;
using Ciemesus.Core.Data;
using Ciemesus.Core.Validation;
using FluentValidation;

namespace Ciemesus.Core.Ciemesus
{
    public class CiemesusCreate
    {
        public class Command : ICiemesusRequest<ICiemesusResponse<CommandResult>>
        {
            public int CiemesusId { get; set; }

            public string CiemesusName { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator(CiemesusDb db)
            {
                CascadeMode = CascadeMode.StopOnFirstFailure;

                RuleFor(c => c.CiemesusName)
                    .NotEmpty()
                    .Length(0, Data.Ciemesus.CiemesusNameMaxLength);
            }
        }

        public class CommandResult
        {
            public int CiemesusId { get; set; }

            public string CiemesusName { get; set; }
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
                var fika = _db.Subject.Add(new Data.Ciemesus { CiemesusName = message.CiemesusName });
                await _db.SaveChangesAsync();

                var result = new CommandResult
                {
                    CiemesusId = fika.CiemesusId,
                    CiemesusName = fika.CiemesusName
                };

                return new CiemesusResponse<CommandResult>
                {
                    Result = result
                };
            }
        }
    }
}
