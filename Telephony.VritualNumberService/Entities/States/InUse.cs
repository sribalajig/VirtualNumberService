namespace Telephony.VritualNumberService.Entities.States
{
    public class InUse : State
    {
        public InUse()
        {
            Id = 2;
            Name = "InUse";
            Description = "The virtual number is currently in use and valid.";
        }

        public override sealed int Id { get; protected set; }

        public override sealed string Name { get; protected set; }

        public override sealed string Description { get; protected set; }
    }
}