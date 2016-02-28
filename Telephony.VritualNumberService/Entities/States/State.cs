namespace Telephony.VritualNumberService.Entities.States
{
    public abstract class State
    {
        public abstract int Id { get; }

        public abstract string Name { get; }

        public abstract string Description { get;}
    }
}