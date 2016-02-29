using System.ComponentModel.DataAnnotations;
using Telephony.VritualNumberService.Entities.States;

namespace Telephony.VritualNumberService.Entities
{
    public class VirtualNumberAssociation
    {
        [Key]
        public virtual int Id { get; set; }

        public virtual User Caller { get; set; }

        public virtual User Callee { get; set; }

        public virtual VirtualNumber.VirtualNumber VirtualNumber { get; set; }

        public virtual int BabajobJobId { get; protected set; }

        public virtual State State { get; set; }
    }
}