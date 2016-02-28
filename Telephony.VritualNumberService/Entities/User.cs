using System.ComponentModel.DataAnnotations;

namespace Telephony.VritualNumberService.Entities
{
    public class User
    {
        protected User() { }

        public User(string babajobUserType, int babajobUserId)
        {
            BabajobUserType = babajobUserType;
            BabajobUserId = babajobUserId;
        }

        public string BabajobUserType { get; protected set; }

        [Key]
        public int BabajobUserId { get; protected set; }
    }
}