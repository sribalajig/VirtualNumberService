using System.ComponentModel.DataAnnotations;
using Telephony.VritualNumberService.Entities.States;

namespace Telephony.VritualNumberService.Entities
{
    public class VirtualNumberAssociation
    {
        protected VirtualNumberAssociation() { }

        public VirtualNumberAssociation(
            VirtualNumber.VirtualNumber virtualNumber,
            State state,
            User caller, 
            User callee,
            int babajobJobId)
        {
            Caller = caller;
            Callee = callee;
            VirtualNumber = virtualNumber;
            BabajobJobId = babajobJobId;
            State = state;
        }

        [Key]
        public int Id { get; protected set; }

        public User Caller { get; protected set; }

        public User Callee { get; protected set; }

        public VirtualNumber.VirtualNumber VirtualNumber { get; protected set; }

        public int BabajobJobId { get; protected set; }

        public State State { get; protected set; }
    }
}