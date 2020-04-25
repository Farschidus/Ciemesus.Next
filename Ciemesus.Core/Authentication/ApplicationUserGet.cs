using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ciemesus.Core.Contracts;
using Ciemesus.Core.Data;
using Ciemesus.Core.Validation;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ciemesus.Core.Authentication
{
    public class ApplicationUserGet
    {
        public class Query : IRequest<IResponseBase<QueryResult>>
        {
            public string Application { get; set; }
            public string IdentityProviderUserId { get; set; }
        }

        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator(CiemesusDb db)
            {
                CascadeMode = CascadeMode.StopOnFirstFailure;

                RuleFor(query => query.Application).NotEmpty();

                RuleFor(query => query.IdentityProviderUserId)
                    .NotEmpty()
                    .Must(id =>
                    {
                        var valid = Guid.TryParse(id, out Guid result);
                        return valid;
                    })
                    .WithMessage("UserId is in invalid format")
                    .CustomAsync(async (id, context, cancel) =>
                    {
                        var failures = await ValidateUserExists(db, id, "IdentityProviderUserId");
                        context.AddFailures(failures);
                    })
                    .CustomAsync(async (id, context, cancel) =>
                    {
                        var query = (Query)context.ParentContext.InstanceToValidate;
                        var failures = await ValidateUserHasApplicationAccess(db, query.Application, id, nameof(query.IdentityProviderUserId));
                        context.AddFailures(failures);
                    });
            }

            private async Task<IList<ValidationFailure>> ValidateUserExists(CiemesusDb db, string identityProviderUserId, string propertyName)
            {
                var failures = new List<ValidationFailure>();

                var userExists = await db.Users.AnyAsync(u => u.IdentityProviderUserId.HasValue && u.IdentityProviderUserId.Equals(Guid.Parse(identityProviderUserId)));

                if (!userExists)
                {
                    failures.Add(new ValidationFailure(propertyName, $"User {identityProviderUserId} does not exists"));
                }

                return failures;
            }

            private async Task<IList<ValidationFailure>> ValidateUserHasApplicationAccess(CiemesusDb db, string application, string identityProviderUserId, string propertyName)
            {
                var failures = new List<ValidationFailure>();

                var userHasApplicationAccess = await db.UserApplicationRoles.AnyAsync(u => u.User.IdentityProviderUserId.HasValue &&
                    u.User.IdentityProviderUserId.Equals(Guid.Parse(identityProviderUserId)) &&
                    u.Application.Equals(application));

                if (!userHasApplicationAccess)
                {
                    failures.Add(new ValidationFailure(propertyName, $"User {identityProviderUserId} does not have access to application {application}"));
                }

                return failures;
            }
        }

        public class QueryResult
        {
            public int Id { get; set; }
            public string Email { get; set; }
            public string UserName { get; set; }
            public string Name { get; set; }
            public string[] ApplicationRoles { get; set; }
            public int[] SiteIds { get; set; }
        }

        public class Handler : AbstractHandler<Query, IResponseBase<QueryResult>>
        {
            private readonly CiemesusDb _db;

            public Handler(CiemesusDb db)
            {
                _db = db;
            }

            public override async Task<IResponseBase<QueryResult>> Handle(Query message, CancellationToken cancellationToken)
            {
                var userResult = await _db.Users
                    .AsNoTracking()
                    .Where(x => x.IdentityProviderUserId.HasValue && x.IdentityProviderUserId.Equals(Guid.Parse(message.IdentityProviderUserId)))
                    .Include(x => x.ApplicationRoles)
                    .Select(x => new QueryResult()
                    {
                        Id = x.UserId,
                        Email = x.Email,
                        UserName = x.Username,
                        Name = x.Name,
                        ApplicationRoles = x.ApplicationRoles.Select(a => $"{a.Application}{a.Role}").ToArray(),
                    })
                    .FirstAsync();

                return Response(userResult);
            }
        }
    }
}
