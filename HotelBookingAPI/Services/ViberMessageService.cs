using ViberMessageSenderAPI.Models;
using ViberMessageSenderAPI.Contracts;
using ViberMessageSenderAPI.Data;

namespace ViberMessageSenderAPI.Services
{
    public class ViberMessageService : IViberMessageService
    {
        private readonly ApiContext _dbContext;

        public ViberMessageService(ApiContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool SendViberMessage(PhoneSender message)
        {
            message.Id = 0;
            _dbContext.PhoneSenders.Add(message);
            int affectedRows = _dbContext.SaveChanges();

            return affectedRows > 0;
        }

        public PhoneSender GetViberMessageById(int id)
        {
            return _dbContext.PhoneSenders.Find(id);
        }

        public PhoneSender UpdateViberMessage(int id, PhoneSender message)
        {
            var existingMessage = _dbContext.PhoneSenders.Find(id);
            if (existingMessage != null)
            {
                existingMessage.Phone = message.Phone;
                existingMessage.Message = message.Message;
                existingMessage.PhoneReceiverId = message.PhoneReceiverId;
                _dbContext.SaveChanges();
                return existingMessage;
            }
            return null;
        }

        public bool DeleteViberMessage(int id)
        {
            var message = _dbContext.PhoneSenders.Find(id);
            if (message != null)
            {
                _dbContext.PhoneSenders.Remove(message);
                int affectedRows = _dbContext.SaveChanges();
                return affectedRows > 0;
            }
            return false;
        }
    }
}