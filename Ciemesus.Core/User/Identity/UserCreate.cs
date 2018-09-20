using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ciemesus.Core.Data;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;

namespace Ciemesus.Core.User.Identity
{
    public class UserCreate
    {
        public class Command : ICiemesusRequest<ICiemesusResponse<CommandResult>>
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator(CiemesusDb db)
            {
                RuleFor(command => command.Email)
                    .NotEmpty()
                    .EmailAddress()
                    .CustomAsync(async (email, context, cancel) =>
                    {
                        var result = await UserValidation.ValidateUserEmailIsNotInUse(db, email, "Email");
                        if (result != null)
                        {
                            context.AddFailure(result);
                        }
                    });
            }
        }

        public class CommandResult
        {
            public string Email { get; set; }
            public string Id { get; set; }
        }

        public class CommandHandler : ICiemesusAsyncRequestHandler<Command, ICiemesusResponse<CommandResult>>
        {
            private readonly CiemesusDb _db;
            private readonly UserManager<Data.User> _userManager;

            public CommandHandler(CiemesusDb db, UserManager<Data.User> userManager)
            {
                _db = db;
                _userManager = userManager;
            }

            public async Task<ICiemesusResponse<CommandResult>> Handle(Command message)
            {
                var user = new Data.User { UserName = message.Email, Email = message.Email };
                var createResult = await _userManager.CreateAsync(user, message.Password);
                var result = new CiemesusResponse<CommandResult>
                {
                    Result = new CommandResult()
                };

                if (createResult.Succeeded)
                {
                    result.Result.Email = user.Email;
                    result.Result.Id = user.Id;
                }
                else
                {
                    var failures = new List<ValidationFailure>();

                    createResult.Errors.ToList().ForEach(error =>
                    {
                        if (error.Code.IndexOf("Password") == 0)
                        {
                            failures.Add(new ValidationFailure("Password", error.Description));
                        }
                    });

                    result.Errors = failures;
                }

                if (createResult.Succeeded == false && result.Errors.Count() == 0)
                {
                    throw new Exception(createResult.Errors.ToString());
                }

                return result;
            }
        }
    }
}
