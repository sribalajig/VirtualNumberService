namespace Telephony.VritualNumberService.Entities.States
{
    public class Free : State
    {
        public Free()
        {
            Id = 1;
            Name = "Free";
            Description = "Number is ready to be used.";
        }

        public override sealed int Id { get; protected set; }

        public override sealed string Name { get; protected set; }

        public override sealed string Description { get; protected set; }
    }
}