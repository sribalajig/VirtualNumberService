using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Telephony.VritualNumberService.Entities
{
    [Table("Providers", Schema = "VirtualNumbers")]
    public class Provider
    {
        public Provider() { }

        public Provider(int id, string name)
        {
            Id = id;
            Name = name;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; protected set; }

        public string Name { get; protected set; }
    }
}