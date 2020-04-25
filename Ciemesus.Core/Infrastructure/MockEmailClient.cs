using System.Threading.Tasks;

namespace Ciemesus.Core.Infrastructure
{
    public class MockEmailClient : IEmailClient
    {
        public async Task Send(string emailTo, string subject, string body)
        {
            await Task.Run(() => true);
        }
    }
}
