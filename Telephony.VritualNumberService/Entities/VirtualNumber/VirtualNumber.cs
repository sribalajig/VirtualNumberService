using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Telephony.VritualNumberService.Entities.VirtualNumber
{
    [Table("VirtualNumbers", Schema = "VirtualNumbers")]
    public sealed class VirtualNumber
    {
        public VirtualNumber() { }

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

        public int NumberId { get; set; }

        [ForeignKey("NumberId")]
        public PhoneNumber VirtualPhoneNumber { get; set; }

        public int PurposeId { get; set; }

        [ForeignKey("PurposeId")]
        public Purpose.Purpose Purpose { get; set; }

        public int ProviderId { get; set; }

        [ForeignKey("ProviderId")]
        public Provider Provider { get; set; }
    }
}