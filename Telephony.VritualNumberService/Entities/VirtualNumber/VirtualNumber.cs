using System.ComponentModel.DataAnnotations;

namespace Telephony.VritualNumberService.Entities.VirtualNumber
{
    public sealed class VirtualNumber
    {
        private VirtualNumber() { }

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
        public int Id { get; set; }

        public PhoneNumber VirtualPhoneNumber { get; set; }

        public Purpose.Purpose Purpose { get; set; }

        public Provider Provider { get; set; }
    }
}