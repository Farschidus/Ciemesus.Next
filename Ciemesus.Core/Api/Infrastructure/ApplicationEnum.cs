using Ciemesus.Core.Api.Infrastructure.Extensions;
using System.ComponentModel;

namespace Ciemesus.Core.Api.Infrastructure
{
    public enum ApplicationEnum
    {
        [ClientId("Ciemesus.AO"), Description("AdminOffice")]
        AdminOffice,

        [ClientId("Ciemesus.PV"), Description("PublicView")]
        PublicView,
    }
}
