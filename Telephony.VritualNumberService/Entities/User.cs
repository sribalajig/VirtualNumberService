using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Telephony.VritualNumberService.Entities
{
    public class User
    {
        protected User() { }

        public User(string babajobUserType, int id)
        {
            BabajobUserType = babajobUserType;
            Id = id;
        }

        public string BabajobUserType { get; protected set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; protected set; }
    }
}