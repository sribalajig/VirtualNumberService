using System.ComponentModel.DataAnnotations;

namespace Telephony.VritualNumberService.Entities.Purpose
{
    public class FreeJobApplication : Purpose
    {
        public FreeJobApplication()
        {
            Id = 1;
            Name = "FreeJobApplication";
            Description = "This virutal number is meant for free job applications";
        }

        public override sealed int Id { get; protected set; }

        public override sealed string Name { get; protected set; }

        public override sealed string Description { get; protected set; }
    }
}