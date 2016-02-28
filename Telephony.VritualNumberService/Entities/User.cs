namespace Telephony.VritualNumberService.Entities
{
    public class User
    {
        public User(string babajobUserType, int babajobUserId)
        {
            BabajobUserType = babajobUserType;
            BabajobUserId = babajobUserId;
        }

        protected User()
        {
        }

        public string BabajobUserType { get; protected set; }

        public int BabajobUserId { get; protected set; }
    }
}