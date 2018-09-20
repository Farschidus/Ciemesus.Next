using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ciemesus.Core.Data;
using Ciemesus.Core.Validation;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;

namespace Ciemesus.Core.User.Identity
{
    public class UserEdit
    {
        public class Command : ICiemesusRequest<ICiemesusResponse<CommandResult>>
        {
            public string Id { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator(CiemesusDb db)
            {
                RuleFor(command => command.Email)
                    .NotEmpty()
                    .EmailAddress();

                RuleFor(command => command)
                    .CustomAsync(async (command, context, cancel) =>
                    {
                        var result = await UserValidation.ValidateUserEmailIsNotInUseByOtherUsers(db, command.Email, command.Id, "Email");
                        if (result != null)
                        {
                            context.AddFailure(result);
                        }
                    });

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
                var user = await _userManager.FindByIdAsync(message.Id);

                user.Email = message.Email;
                user.UserName = message.Email;
                var editResult = await _userManager.UpdateAsync(user);
                IdentityResult changePassword = null;

                if (string.IsNullOrEmpty(message.Password) == false)
                {
                    await _userManager.RemovePasswordAsync(user);
                    changePassword = await _userManager.AddPasswordAsync(user, message.Password);
                }

                var result = new CiemesusResponse<CommandResult>
                {
                    Result = new CommandResult()
                };

                if (editResult.Succeeded && (changePassword == null || changePassword.Succeeded == true))
                {
                    result.Result.Email = user.Email;
                    result.Result.Id = user.Id;
                }
                else if (changePassword != null && changePassword.Succeeded == false)
                {
                    var failures = new List<ValidationFailure>();

                    changePassword.Errors.ToList().ForEach(error =>
                    {
                        if (error.Code.IndexOf("Password") == 0)
                        {
                            failures.Add(new ValidationFailure("Password", error.Description));
                        }
                    });

                    result.Errors = failures;
                }

                if ((editResult.Succeeded == false || (changePassword != null && changePassword.Succeeded == false)) &&
                        result.Errors.Count() == 0)
                {
                    throw new Exception(editResult.Errors.ToString());
                }

                return result;
            }
        }
    }
}
