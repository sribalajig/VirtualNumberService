using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Telephony.VritualNumberService.Entities.Purpose
{
    public abstract class Purpose
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public virtual int Id { get; protected set; }

        public virtual string Name { get; protected set; }

        public virtual string Description { get; protected set; }
    }
}