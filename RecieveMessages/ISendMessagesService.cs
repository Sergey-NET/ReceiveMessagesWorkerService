using System.Threading.Tasks;

namespace RecieveMessages
{
    public interface ISendMessagesService
    {
        Task StartListen();
    }
}