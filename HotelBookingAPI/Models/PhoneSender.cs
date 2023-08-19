namespace ViberMessageSenderAPI.Models
{
    public class PhoneSender
    {
        public int Id { get; set; }
        public string Phone { get; set; }
        public string Message { get; set; }

        public int PhoneReceiverId { get; set; }
        public PhoneReceiver PhoneReceiver { get; set; }
    }
}
