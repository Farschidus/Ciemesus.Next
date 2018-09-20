using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ciemesus.Core.Data;
using Ciemesus.Core.Validation;
using FluentValidation;

namespace Ciemesus.Core.Ciemesus
{
    public class CiemesusEdit
    {
        public class Command : ICiemesusRequest<ICiemesusResponse>
        {
            public int SiteId { get; set; }

            public int CiemesusId { get; set; }

            public string CiemesusName { get; set; }

            public List<LocationCommand> Locations { get; set; }
        }

        public class LocationCommand
        {
            public int LocationId { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator(CiemesusDb db)
            {
                CascadeMode = CascadeMode.StopOnFirstFailure;

                RuleFor(c => c.CiemesusId)
                    .CiemesusExists(db);

                When(c => c.CiemesusName != null, () =>
                {
                    RuleFor(c => c.CiemesusName)
                    .NotEmpty()
                    .Length(0, Data.Ciemesus.CiemesusNameMaxLength);
                });
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

                fika.CiemesusName = message.CiemesusName ?? fika.CiemesusName;

                await _db.SaveChangesAsync();

                return new CiemesusResponse();
            }
        }
    }
}
