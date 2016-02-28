namespace Telephony.VritualNumberService.Entities.States
{
    public class Free : State
    {
        public override int Id
        {
            get { return 1; }
        }

        public override string Name
        {
            get { return "Free"; }
        }

        public override string Description
        {
            get { return "Number is ready to be used."; }
        }
    }
}