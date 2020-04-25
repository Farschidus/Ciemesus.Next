using Ciemesus.Core.Contracts;
using Ciemesus.Core.Data;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ciemesus.Core.Api.ApplicationUpdates
{
    public class DatabaseSystemTimeGet
    {
        public class Query : IRequest<IResponseBase<DateTimeOffset>>
        {
        }

        public class QueryValidator : AbstractValidator<Query>
        {
        }

        public class QueryHandler : AbstractHandler<Query, IResponseBase<DateTimeOffset>>
        {
            private readonly CiemesusDb _db;

            public QueryHandler(CiemesusDb db)
            {
                _db = db;
            }

            public override async Task<IResponseBase<DateTimeOffset>> Handle(Query message, CancellationToken cancellationToken)
            {
                var con = _db.Database.GetDbConnection();
                var cmd = con.CreateCommand();
                cmd.CommandText = "SELECT SYSDATETIMEOFFSET()";
                con.Open();
                var result = (DateTimeOffset)await cmd.ExecuteScalarAsync();

                return Response(result);
            }
        }
    }
}
