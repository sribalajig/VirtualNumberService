using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Telephony.VritualNumberService.Entities.States;

namespace Telephony.VritualNumberService.Entities
{
    public class VirtualNumberAssociation
    {
        [Column(Order = 1), Key]
        public int CallerId { get; set; }

        [ForeignKey("CallerId")]
        public virtual User Caller { get; set; }

        [Column(Order = 2), Key]
        public int CalleeId { get; set; }

        [ForeignKey("CalleeId")]
        public virtual User Callee { get; set; }

        [Column(Order = 3), Key]
        public int VirtualNumberId { get; set; }

        [ForeignKey("VirtualNumberId")]
        public virtual VirtualNumber.VirtualNumber VirtualNumber { get; set; }

        [Column(Order = 4), Key]
        public virtual int BabajobJobId { get; set; }

        public int StateId { get; set; }

        [ForeignKey("StateId")]
        public virtual State State { get; set; }
    }
}