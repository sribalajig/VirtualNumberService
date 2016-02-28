using System.ComponentModel.DataAnnotations;

namespace Telephony.VritualNumberService.Entities.VirtualNumber
{
    public class VirtualNumber
    {
        protected VirtualNumber() { }

        public VirtualNumber(
            PhoneNumber virtualPhoneNumber,
            Purpose.Purpose purpose,
            Provider provider)
        {
            Purpose = purpose;
            VirtualPhoneNumber = virtualPhoneNumber;
            Provider = provider;
        }

        [Key]
        public int Id { get; protected set; }

        public PhoneNumber VirtualPhoneNumber { get; protected set; }

        public Purpose.Purpose Purpose { get; protected set; }

        public Provider Provider { get; protected set; }
    }
}