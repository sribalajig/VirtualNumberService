namespace Telephony.VritualNumberService.Entities.States
{
    public class InUse : State
    {
        public override int Id
        {
            get { return 2; }
        }

        public override string Name
        {
            get
            {
                return "InUse";
            }
        }

        public override string Description
        {
            get
            {
                return "The virtual number is currently in use and valid.";
            }
        }
    }
}