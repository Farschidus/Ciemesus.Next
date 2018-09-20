using System;
using System.Threading.Tasks;
using Ciemesus.Core.Data;
using Ciemesus.Core.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace Ciemesus.Core.User.Identity
{
    public class UserDelete
    {
        public class Command : ICiemesusRequest<ICiemesusResponse>
        {
            public string Id { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator(CiemesusDb db)
            {
                RuleFor(command => command.Id)
                    .NotEmpty()
                    .CustomAsync(async (id, context, cancel) =>
                    {
                        var result = await UserValidation.ValidateUserIdDoesExist(db, id, "Id");
                        if (result != null)
                        {
                            context.AddFailure(result);
                        }
                    });
            }
        }

        public class CommandHandler : ICiemesusAsyncRequestHandler<Command, ICiemesusResponse>
        {
            private readonly CiemesusDb _db;
            private readonly UserManager<Data.User> _userManager;

            public CommandHandler(CiemesusDb db, UserManager<Data.User> userManager)
            {
                _db = db;
                _userManager = userManager;
            }

            public async Task<ICiemesusResponse> Handle(Command message)
            {
                var user = await _userManager.FindByIdAsync(message.Id);
                var deleteResult = await _userManager.DeleteAsync(user);

                if (!deleteResult.Succeeded)
                {
                    throw new Exception(deleteResult.Errors.ToString());
                }

                return new CiemesusResponse();
            }
        }
    }
}
