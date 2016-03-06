using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Telephony.VritualNumberService.Entities.VirtualNumber
{
    [Table("PhoneNumbers", Schema = "VirtualNumbers")]
    public class PhoneNumber
    {
        private readonly string _countryCode;

        protected PhoneNumber() { }

        public PhoneNumber(string number)
        {
            Number = number;
            _countryCode = "91";
        }

        [Key]
        public int Id { get; protected set; }

        [Index(IsUnique = true)]
        public string Number { get; protected set; }

        public string GetFullNumber()
        {
            return string.Format("{0}{1}{2}", "+", _countryCode, Number);
        }
    }
}