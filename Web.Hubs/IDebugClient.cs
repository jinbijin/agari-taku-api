using System.Threading.Tasks;

namespace Web.Hubs
{
    public interface IDebugClient
    {
        Task ReceiveMessage(string message);
    }
}
