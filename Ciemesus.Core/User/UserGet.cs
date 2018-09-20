using Ciemesus.Core.Data;
using FluentValidation;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Ciemesus.Core.User
{
    public class UserGet
    {
        public class Query : ICiemesusRequest<ICiemesusResponse<IEnumerable<QueryResult>>>
        {
        }

        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator(CiemesusDb db)
            {
                CascadeMode = CascadeMode.StopOnFirstFailure;
            }
        }

        public class QueryResult
        {
            public string Id { get; set; }
            public string Email { get; set; }
        }

        public class QueryHandler : ICiemesusAsyncRequestHandler<Query, ICiemesusResponse<IEnumerable<QueryResult>>>
        {
            private readonly CiemesusDb _db;

            public QueryHandler(CiemesusDb db)
            {
                _db = db;
            }

            public async Task<ICiemesusResponse<IEnumerable<QueryResult>>> Handle(Query message)
            {
                var result = await _db.Users
                    .Select(x => new QueryResult()
                    {
                        Id = x.Id,
                        Email = x.Email
                    })
                    .ToListAsync();

                return new CiemesusResponse<IEnumerable<QueryResult>>
                {
                    Result = result
                };
            }
        }
    }
}
