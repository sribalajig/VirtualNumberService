using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Telephony.VritualNumberService.Entities
{
    [Table("Users", Schema = "VirtualNumbers")]
    public class User
    {
        protected User() { }

        public User(BabajobUserType babajobUserType, int id)
        {
            BabajobUserType = babajobUserType;
            Id = id;
        }

        public int BabaJobUserTypeId { get; set; }

        [ForeignKey("BabaJobUserTypeId")]
        public BabajobUserType BabajobUserType { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; protected set; }
    }
}