namespace Telephony.VritualNumberService.Entities.VirtualNumber
{
    public class VirtualNumber
    {
        public VirtualNumber(
            PhoneNumber virtualPhoneNumber,
            Purpose.Purpose purpose)
        {
            Purpose = purpose;
            VirtualPhoneNumber = virtualPhoneNumber;
        }

        public PhoneNumber VirtualPhoneNumber { get; protected set; }

        public Purpose.Purpose Purpose { get; protected set; }
    }
}