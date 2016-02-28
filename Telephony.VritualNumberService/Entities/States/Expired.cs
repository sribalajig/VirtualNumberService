namespace Telephony.VritualNumberService.Entities.States
{
    public class Expired : State
    {
        public override int Id
        {
            get { return 3; }
        }

        public override string Name
        {
            get
            {
                return "Expired";
            }
        }

        public override string Description
        {
            get { return "Thiv virtual number has expired"; }
        }
    }
}