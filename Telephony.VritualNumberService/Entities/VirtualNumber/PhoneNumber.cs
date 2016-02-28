using System.ComponentModel.DataAnnotations;

namespace Telephony.VritualNumberService.Entities.VirtualNumber
{
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

        public string Number { get; protected set; }

        public string GetFullNumber()
        {
            return string.Format("{0}{1}{2}", "+", _countryCode, Number);
        }
    }
}