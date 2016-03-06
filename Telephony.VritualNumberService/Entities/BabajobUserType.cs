using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Telephony.VritualNumberService.Entities
{
    [Table("BabajobUserTypes", Schema = "VirtualNumbers")]
    public class BabajobUserType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserTypeId { get; set; }

        public string UserType { get; set; }
    }
}