using System.ComponentModel.DataAnnotations;

namespace Telephony.VritualNumberService.Entities.States
{
    public abstract class State
    {
        [Key]
        public virtual int Id { get; protected set; }

        public virtual string Name { get; protected set; }

        public virtual string Description { get; protected set; }
    }
}