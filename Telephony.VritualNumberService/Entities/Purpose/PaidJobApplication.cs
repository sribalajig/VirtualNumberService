using System.ComponentModel.DataAnnotations;

namespace Telephony.VritualNumberService.Entities.Purpose
{
    public class PaidJobApplication : Purpose
    {
        public PaidJobApplication()
        {
            Id = 2;
            Name = "PaidJobApplication";
            Description = "This virutal number is meant for paid job applications";
        }

        [Key]
        public override sealed int Id { get; protected set; }

        public override sealed string Name { get; protected set; }

        public override sealed string Description { get; protected set; }
    }
}