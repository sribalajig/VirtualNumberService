namespace Telephony.VritualNumberService.Entities.Purpose
{
    public class FreeJobApplication : Purpose
    {
        public override int Id
        {
            get
            {
                return 1;
            }
        }

        public override string Name
        {
            get
            {
                return "FreeJobApplication";
            }
        }

        public override string Description
        {
            get
            {
                return "This virutal number is meant for free job applications";
            }
        }
    }
}