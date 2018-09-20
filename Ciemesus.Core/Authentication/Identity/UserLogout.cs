using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Ciemesus.Core.Authentication.Identity
{
    public class UserLogout
    {
        public class Command : ICiemesusRequest<ICiemesusResponse<bool>>
        {
        }

        public class CommandHandler : ICiemesusAsyncRequestHandler<Command, ICiemesusResponse<bool>>
        {
            private readonly SignInManager<Data.User> _signInManager;

            public CommandHandler(SignInManager<Data.User> signInManager)
            {
                _signInManager = signInManager;
            }

            public async Task<ICiemesusResponse<bool>> Handle(Command message)
            {
                await _signInManager.SignOutAsync();

                return new CiemesusResponse<bool>
                {
                    Result = true
                };
            }
        }
    }
}
