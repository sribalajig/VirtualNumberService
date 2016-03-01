namespace Telephony.VritualNumberService.Entities.Purpose
{
    public abstract class Purpose
    {
        public virtual int Id { get; protected set; }

        public virtual string Name { get; protected set; }

        public virtual string Description { get; protected set; }
    }
}