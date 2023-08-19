using ViberMessageSenderAPI.Models;

namespace ViberMessageSenderAPI.Contracts
{
    public interface IViberMessageService
    {
        bool SendViberMessage(PhoneSender message);
        PhoneSender GetViberMessageById(int id);
        PhoneSender UpdateViberMessage(int id, PhoneSender message);
        bool DeleteViberMessage(int id);
    }
}