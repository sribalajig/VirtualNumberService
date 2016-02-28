namespace Telephony.VritualNumberService.Entities.VirtualNumber
{
    public class PhoneNumber
    {
        private readonly string _countryCode;

        public PhoneNumber(string number, Provider provider)
        {
            Number = number;
            _countryCode = "91";
            Provider = provider;
        }

        public string FullNumber
        {
            get
            {
                return string.Format("{0}{1}{2}", "+", _countryCode, Number);
            }
        }

        public string Number { get; protected set; }

        public Provider Provider { get; protected set; }
    }
}