using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Telephony.VritualNumberService.Entities.States;

namespace Telephony.VritualNumberService.Entities
{
    public class VirtualNumberAssociation
    {
        [Key]
        public virtual int Id { get; set; }

        public int CallerId { get; set; }

        [ForeignKey("CallerId")]
        public virtual User Caller { get; set; }

        public int CalleeId { get; set; }

        [ForeignKey("CalleeId")]
        public virtual User Callee { get; set; }

        public int VirtualNumberId { get; set; }

        [ForeignKey("VirtualNumberId")]
        public virtual VirtualNumber.VirtualNumber VirtualNumber { get; set; }

        public virtual int BabajobJobId { get; protected set; }

        public int StateId { get; set; }

        [ForeignKey("StateId")]
        public virtual State State { get; set; }
    }
}