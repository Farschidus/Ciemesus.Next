using System.Threading.Tasks;

namespace Ciemesus.Core.Infrastructure
{
    public interface IEmailClient
    {
        Task Send(string emailTo, string subject, string body);
    }
}
