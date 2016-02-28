namespace Telephony.VritualNumberService.Entities.States
{
    public class Expired : State
    {
        public Expired()
        {
            Id = 3;
            Name = "Expired";
            Description = "This virtual number has expired";
        }

        public override sealed int Id { get; protected set; }

        public override sealed string Name { get; protected set; }

        public override sealed string Description { get; protected set; }
    }
}