using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using Ciemesus.Core.Data;
using Ciemesus.Core.User;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;

namespace Ciemesus.Core.Authentication.Identity
{
    public class UserLogin
    {
        public class Command : ICiemesusRequest<ICiemesusResponse<CommandResult>>
        {
            public string Email { get; set; }
            public string Password { get; set; }
            public bool RememberLogin { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator(CiemesusDb db, SignInManager<Data.User> signInManager)
            {
                RuleFor(command => command.Email)
                    .NotEmpty()
                    .EmailAddress();

                RuleFor(command => command.Password)
                    .NotEmpty();

                When(u => !string.IsNullOrEmpty(u.Email), () =>
                {
                    RuleFor(command => command.Email)
                        .CustomAsync(async (email, context, cancel) =>
                        {
                            var result = await UserValidation.ValidateUserEmailDoesExist(db, email, "Email");
                            if (result != null)
                            {
                                context.AddFailure(result);
                            }
                        });
                });
            }
        }

        public class CommandResult
        {
            public CommandResult()
            {
                Success = false;
            }

            public bool Success { get; set; }
        }

        public class CommandHandler : ICiemesusAsyncRequestHandler<Command, ICiemesusResponse<CommandResult>>
        {
            private readonly CiemesusDb _db;
            private readonly SignInManager<Data.User> _signInManager;

            public CommandHandler(CiemesusDb db, SignInManager<Data.User> signInManager)
            {
                _db = db;
                _signInManager = signInManager;
            }

            public async Task<ICiemesusResponse<CommandResult>> Handle(Command message)
            {
                var user = await _db.Users.SingleAsync(x => x.Email == message.Email);

                var signInResult = await _signInManager.PasswordSignInAsync(user, message.Password, isPersistent: message.RememberLogin, lockoutOnFailure: false);

                var result = new CiemesusResponse<CommandResult>
                {
                    Result = new CommandResult
                    {
                        Success = signInResult.Succeeded
                    }
                };

                if (!signInResult.Succeeded)
                {
                    var failures = new List<ValidationFailure>
                    {
                        new ValidationFailure(string.Empty, "Email and password combination not recognised")
                    };

                    if (signInResult.IsNotAllowed)
                    {
                        failures.Add(new ValidationFailure(string.Empty, "Login is not allowed for this account"));
                    }

                    if (signInResult.IsLockedOut)
                    {
                        failures.Add(new ValidationFailure(string.Empty, "Account is locked"));
                    }

                    if (signInResult.RequiresTwoFactor)
                    {
                        failures.Add(new ValidationFailure(string.Empty, "Login requires two factor authentication"));
                    }

                    result.Errors = failures;
                }

                return result;
            }
        }
    }
}
